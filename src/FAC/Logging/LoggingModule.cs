﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Module = Autofac.Module;

namespace FAC.Logging
{

    public class LoggingModule : Module
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggerCache;

        public LoggingModule()
        {
            _loggerCache = new ConcurrentDictionary<string, ILogger>();
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            // by default, use FAC's logger that delegates to Castle's logger factory
            moduleBuilder.RegisterType<CastleLoggerFactory>().As<ILoggerFactory>().InstancePerLifetimeScope();
            if (ConfigurationManager.AppSettings["DefaultFileLogger"] == "log4net")
            {
                moduleBuilder.RegisterType<FACLog4netFactory>().As<Castle.Core.Logging.ILoggerFactory>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<FACSerilogFactory>().As<Castle.Core.Logging.ILoggerFactory>().InstancePerLifetimeScope();
            }

            // call CreateLogger in response to the request for an ILogger implementation
            moduleBuilder.Register(CreateLogger).As<ILogger>().InstancePerDependency();
        }
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {

            var implementationType = registration.Activator.LimitType;


            registration.Preparing +=
                 (sender, args) =>
                 {
                     var forType = args.Component.Activator.LimitType;

                     var logParameter = new Autofac.Core.ResolvedParameter(
                         (p, c) => p.ParameterType == typeof(ILogger),
                         (p, c) => c.Resolve<ILogger>(TypedParameter.From(forType)));

                     args.Parameters = args.Parameters.Union(new[] { logParameter });
                 };

            // build an array of actions on this type to assign loggers to member properties
            var injectors = BuildLoggerInjectors(implementationType).ToArray();

            // if there are no logger properties, there's no reason to hook the activated event
            if (!injectors.Any())
                return;

            // otherwise, when an instance of this component is activated, inject the loggers on the instance
            registration.Activated += (s, e) =>
            {
                foreach (var injector in injectors)
                    injector(e.Context, e.Instance);
            };
        }

        private IEnumerable<Action<IComponentContext, object>> BuildLoggerInjectors(Type componentType)
        {
            // Look for settable properties of type "ILogger" 
            var loggerProperties = componentType
                .GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new
                {
                    PropertyInfo = p,
                    p.PropertyType,
                    IndexParameters = p.GetIndexParameters(),
                    Accessors = p.GetAccessors(false)
                })
                .Where(x => x.PropertyType == typeof(ILogger)) // must be a logger
                .Where(x => x.IndexParameters.Count() == 0) // must not be an indexer
                .Where(x => x.Accessors.Length != 1 || x.Accessors[0].ReturnType == typeof(void)); //must have get/set, or only set

            // Return an array of actions that resolve a logger and assign the property
            foreach (var entry in loggerProperties)
            {
                var propertyInfo = entry.PropertyInfo;

                yield return (ctx, instance) =>
                {
                    string component = componentType.ToString();
                    if (component != instance.GetType().ToString())
                    {
                        return;
                    }
                    var logger = _loggerCache.GetOrAdd(component, key => ctx.Resolve<ILogger>(new TypedParameter(typeof(Type), componentType)));
                    propertyInfo.SetValue(instance, logger, null);
                };
            }
        }

        private static ILogger CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters)
        {
            // return an ILogger in response to Resolve<ILogger>(componentTypeParameter)
            var loggerFactory = context.Resolve<ILoggerFactory>();
            var containingType = parameters.TypedAs<Type>();
            return loggerFactory.CreateLogger(containingType);
        }
    }
}
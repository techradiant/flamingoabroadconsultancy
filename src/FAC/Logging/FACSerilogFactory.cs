﻿using System;
using System.Configuration;
using Castle.Core.Logging;
using Serilog;

namespace FAC.Logging
{
    public class FACSerilogFactory : AbstractLoggerFactory
    {
        private static bool _isFileWatched = false;
        private Serilog.ILogger logger;
        public FACSerilogFactory()
            : this(ConfigurationManager.AppSettings["Serilog.Config"]) { }

        public FACSerilogFactory(string configFilename)
        {
            if (!_isFileWatched && !string.IsNullOrWhiteSpace(configFilename))
            {
                //logger = Log.Logger;
                //Load settings from json file
                _isFileWatched = true;
            }
            else
            {
                logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            }
        }

        public override Castle.Core.Logging.ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }

        public override Castle.Core.Logging.ILogger Create(string name)
        {
            Serilog.ILogger loggerInstance = logger.ForContext(Serilog.Core.Constants.SourceContextPropertyName, name);
            return new FACSerilogLogger(loggerInstance, this);
        }
    }
}

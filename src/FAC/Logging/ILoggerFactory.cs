using System;

namespace FAC.Logging {
    public interface ILoggerFactory {
        ILogger CreateLogger(Type type);
    }
}
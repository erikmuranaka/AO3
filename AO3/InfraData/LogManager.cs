using AO3.Domain;
using Newtonsoft.Json;
using NLog;

namespace AO3.InfraData
{
    public class LogManager : ILogManager
    {
        private readonly LogModel _logModel;
        private ILogger logger;

        public LogManager()
        {
            _logModel = new LogModel();
        }

        public void Configure(string callerClassName)
        {
            logger = NLog.LogManager.GetLogger(callerClassName);

            _logModel.Application = callerClassName;
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogFatal(string message)
        {
            logger.Fatal(message);
        }

        public void LogInfo(LogModel log)
        {
            _logModel.InPut = JsonConvert.SerializeObject(log.InPut);
            _logModel.OutPut = JsonConvert.SerializeObject(log.OutPut);
            _logModel.Message = log.Message;
            _logModel.MessageException = log.MessageException;
            _logModel.Method = log.Method;

            logger.Info("ok");
            logger.Info(JsonConvert.SerializeObject(_logModel));

            _logModel.MessageException = string.Empty;
            _logModel.Message = string.Empty;
            _logModel.OutPut = string.Empty;
            _logModel.InPut = string.Empty;
        }

        public void LogError(LogModel log)
        {
            _logModel.InPut = log.InPut;
            _logModel.OutPut = log.OutPut;
            _logModel.Message = log.Message;
            _logModel.MessageException = log.MessageException;
            _logModel.Method = log.Method;

            logger.Error(JsonConvert.SerializeObject(_logModel));

            _logModel.MessageException = string.Empty;
            _logModel.Message = string.Empty;
            _logModel.OutPut = string.Empty;
            _logModel.InPut = string.Empty;
        }

        public void LogWarn(LogModel log)
        {
            _logModel.InPut = log.InPut;
            _logModel.OutPut = log.OutPut;
            _logModel.Message = log.Message;
            _logModel.MessageException = log.MessageException;
            _logModel.Method = log.Method;

            logger.Warn(JsonConvert.SerializeObject(_logModel));

            _logModel.MessageException = string.Empty;
            _logModel.Message = string.Empty;
            _logModel.OutPut = string.Empty;
            _logModel.InPut = string.Empty;
        }

        public void LogFatal(LogModel log)
        {
            _logModel.InPut = log.InPut;
            _logModel.OutPut = log.OutPut;
            _logModel.Message = log.Message;
            _logModel.MessageException = log.MessageException;
            _logModel.Method = log.Method;

            logger.Fatal(JsonConvert.SerializeObject(_logModel));

            _logModel.MessageException = string.Empty;
            _logModel.Message = string.Empty;
            _logModel.OutPut = string.Empty;
            _logModel.InPut = string.Empty;
        }
    }
}
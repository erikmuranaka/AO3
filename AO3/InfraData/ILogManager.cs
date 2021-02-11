using AO3.Domain;

namespace AO3.InfraData
{
    public interface ILogManager
    {
        void Configure(string callerClassName);

        void LogDebug(string message);

        void LogError(string message);

        void LogInfo(string message);

        void LogWarn(string message);

        void LogFatal(string message);

        void LogInfo(LogModel log);

        void LogError(LogModel log);

        void LogWarn(LogModel log);

        void LogFatal(LogModel log);
    }
}
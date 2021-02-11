using System;

namespace AO3.Domain
{
    public class LogModel
    {
        public LogModel()
        {
            Id = Guid.NewGuid();
            Clean();
        }

        public Guid Id { get; set; }

        public DateTime TimeStamp => DateTime.Now;

        public string Application { get; set; }

        public string Method { get; set; }

        public object InPut { get; set; }

        public object OutPut { get; set; }

        public string Message { get; set; }

        public string MessageException { get; set; }

        public void Clean()
        {
            Application = string.Empty;
            Method = string.Empty;
            Message = string.Empty;
            MessageException = string.Empty;
            InPut = null;
            OutPut = null;
        }
    }
}
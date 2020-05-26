using AmericaVirtual.WebAPI.Persistance.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericaVirtual.WebAPI.Persistance.Logger
{
    public sealed class Logger
    {
        private readonly static Logger _instance = new Logger();

        private Logger()
        {
            
        }

        public static Logger Instance
        {
            get
            {
                return _instance;
            }
        }

        public void WriteInLog(LogType logType, string message , string username = null, string exception = null)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                Logs log = new Logs();

                if (!string.IsNullOrEmpty(username))
                    log.Username = username;

                log.LogType = logType.ToString();
                log.Message = message;

                if(!string.IsNullOrEmpty(exception))
                    log.Exception = exception;

                context.Logs.Add(log);

                context.SaveChanges();
            }
        }
    }
}

public enum LogType
{
    WARNING,
    ERROR,
    INFO
}

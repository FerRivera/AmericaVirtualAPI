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

        public void WriteErrorInLog(string username,string message, string exception)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                Logs log = new Logs();

                log.Username = username;
                log.LogType = "Error";
                log.Message = message;
                log.Exception = exception;

                context.Logs.Add(log);

                context.SaveChanges();
            }
        }

        public void WriteInfoInLog(string username, string message)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                Logs log = new Logs();

                log.Username = username;
                log.LogType = "Info";
                log.Message = message;

                context.Logs.Add(log);

                context.SaveChanges();
            }
        }

        public void WriteWarningInLog(string username, string message)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                Logs log = new Logs();

                log.Username = username;
                log.LogType = "Warning";
                log.Message = message;

                context.Logs.Add(log);

                context.SaveChanges();
            }
        }
    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.BLL
{

    public interface IBllLogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message, System.Exception exception);
    }

  
    public class BllLogger:IBllLogger
    {
        private ILog _logger;

        public BllLogger()
        {
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Used to log Debug messages in an explicit Debug BllLogger
        /// </summary>
        /// <param name="message">The object message to log</param>
        public void Debug(string message)
        {
             _logger.Debug(message);
        }
       
        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public  void Error(string message, System.Exception exception)
        {
            _logger.Error(message, exception);
        }
       
    }
}

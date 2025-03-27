using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL
{
    public interface IDalLogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message, System.Exception exception);
    }

    public class DalLogger:IDalLogger
    {
        
        private ILog _logger;
    
        public DalLogger()
        {
            _logger  = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            
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
        /// <param name="exception">The exception to log, including its stack trace </param>
        public void Debug(string message, System.Exception exception)
        {
            _logger.Debug(message, exception);
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
        public void Info(string message, System.Exception exception)
        {
            _logger.Info(message, exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">The object message to log</param>
        /// <param name="exception">The exception to log, including its stack trace </param>
        public void Error(string message, System.Exception exception)
        {
            _logger.Error(message, exception);
        }

    }
}

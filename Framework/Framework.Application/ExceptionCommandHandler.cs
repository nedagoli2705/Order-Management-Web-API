using System;
using System.Linq;
using Framework.Core.Application;
//using NLog.Web;

namespace Framework.Application
{
    public class ExceptionCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> commandHandler;


        public ExceptionCommandHandler(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }


        public void Execute(TCommand command)
        {
           // var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
            //    logger.Debug("init main " + command.GetType());
                commandHandler.Execute(command);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count > 1 && ex.InnerExceptions.All(z => z.Message == ex.InnerException.Message))
                {
             
                    throw ex.Flatten().InnerException;
                }

                /*if (ex.InnerExceptions.Count >= 1)
                {
                    var joined = string.Join(' ', ex.InnerExceptions.Select(z => z.Message).ToList());
                    throw new Exception(ex.Message+joined);
                }*/
            //    logger.Error(ex, ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
            //    logger.Error(ex, ex.Message);
                throw;
            }
            finally
            {
                //NLog.LogManager.Shutdown();
            }
        }
    }
}
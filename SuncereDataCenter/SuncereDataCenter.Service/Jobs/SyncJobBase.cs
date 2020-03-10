using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Service.Jobs
{
    class SyncJobBase<TSync> : IJob
    {
        ILog logger;

        public SyncJobBase()
        {
            logger = LogManager.GetLogger<SyncJobBase<TSync>>();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                sw.Stop();
                logger.InfoFormat("", typeof(TSync).Name, sw.Elapsed);
            }
            catch (Exception e)
            {
                logger.Error("Execute failed.", e);
            }
        }
    }
}

using Common.Logging;
using Quartz;
using SuncereDataCenter.Core.Sync;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Service.Jobs
{
    class SyncJobBase<TSync> : IJob where TSync : ISync
    {
        ILog logger;

        public SyncJobBase()
        {
            logger = LogManager.GetLogger<SyncJobBase<TSync>>();
        }

        public void Execute(IJobExecutionContext context)
        {
            context.Scheduler.PauseJob(context.JobDetail.Key);
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
                {
                    ISync sync = (ISync)Activator.CreateInstance(typeof(TSync), entities);
                    sync.CheckQueue();
                    entities.SaveChanges();
                    sync.Sync();
                    entities.SaveChanges();
                }
                sw.Stop();
                logger.InfoFormat("{0} Sync {1}.", typeof(TSync).Name, sw.Elapsed);
            }
            catch (Exception e)
            {
                logger.Error("Execute failed.", e);
            }
            context.Scheduler.ResumeJob(context.JobDetail.Key);
        }
    }
}

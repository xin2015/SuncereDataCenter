using Common.Logging;
using Quartz;
using Quartz.Impl;
using SuncereDataCenter.Core.Sync;
using SuncereDataCenter.Service.Jobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SuncereDataCenter.Service
{
    class SuncereDataCenterService : ServiceControl
    {
        private ILog logger;
        private IScheduler scheduler;

        public SuncereDataCenterService()
        {
            logger = LogManager.GetLogger<SuncereDataCenterService>();
            Initialize();
        }

        public virtual void Initialize()
        {
            try
            {
                logger.Info("-------- Initialization Start --------");
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                logger.Info("-------- Scheduling Jobs --------");
                Action<string, Type> scheduleJobAction = (jobName, jobType) =>
                {
                    IJobDetail job = JobBuilder.Create(jobType)
                        .WithIdentity(jobName)
                        .Build();
                    ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                        .WithIdentity(string.Format("Trigger{0}", jobName))
                        .WithCronSchedule(ConfigurationManager.AppSettings[string.Format("{0}CronExpression", jobName)])
                        .Build();
                    scheduler.ScheduleJob(job, trigger);
                };
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                IEnumerable<Type> jobTypes = types.Where(o => o.Name.EndsWith("Job") && o.GetInterfaces().Contains(typeof(IJob)));
                foreach (Type type in jobTypes)
                {
                    scheduleJobAction(type.Name, type);
                }
                Type syncJobBaseType = typeof(SyncJobBase<>);
                types = Assembly.GetAssembly(typeof(ISync)).GetTypes();
                jobTypes = types.Where(o => o.Name.EndsWith("Sync") && o.GetInterfaces().Contains(typeof(ISync)));
                foreach (Type type in jobTypes)
                {
                    scheduleJobAction(type.Name, syncJobBaseType.MakeGenericType(type));
                }
                logger.Info("-------- Initialization Complete --------");
            }
            catch (Exception e)
            {
                logger.Fatal("Server initialization failed.", e);
            }
        }

        public virtual void Start()
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Fatal("Scheduler start failed.", ex);
            }
        }

        public virtual void Stop()
        {
            try
            {
                scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                logger.Error("Scheduler stop failed.", ex);
            }
        }

        public bool Start(HostControl hostControl)
        {
            Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Stop();
            return true;
        }
    }
}

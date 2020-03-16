using Common.Logging;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Sync
{
    public interface ISync
    {
        void Sync();
        void CheckQueue();
        void CheckQueue(DateTime time);
        void CheckQueue(DateTime startTime, DateTime endTime);
    }

    public abstract class SyncBase<TEntity> : ISync
    {
        protected SuncereDataCenterModel Model { get; set; }
        protected string Class { get; set; }
        protected ILog Logger { get; set; }
        protected TimeSpan Interval { get; set; }
        protected TimeSpan StartTimeDeviation { get; set; }
        protected TimeSpan EndTimeDeviation { get; set; }

        public SyncBase(SuncereDataCenterModel model)
        {
            Model = model;
            Class = typeof(TEntity).Name;
            Logger = LogManager.GetLogger(Class);
        }

        public void Sync()
        {
            DateTime now = DateTime.Now;
            List<SyncDataQueue> queues = Model.SyncDataQueue.Where(o => o.Class == Class && !o.Status && o.StartTime <= now && o.EndTime >= now).ToList();
            foreach (SyncDataQueue queue in queues)
            {
                Sync(queue);
            }
        }

        protected abstract void Sync(SyncDataQueue queue);

        public void CheckQueue()
        {
            DateTime time = GetTime();
            CheckQueue(time);
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime GetTime();

        public void CheckQueue(DateTime time)
        {
            SyncDataQueue queue = Model.SyncDataQueue.FirstOrDefault(o => o.Class == Class && o.Time == time);
            if (queue == null)
            {
                queue = new SyncDataQueue()
                {
                    Class = Class,
                    Time = time,
                    StartTime = time.Add(StartTimeDeviation),
                    EndTime = time.Add(EndTimeDeviation),
                    LastTime = DateTime.Now
                };
                Model.SyncDataQueue.Add(queue);
            }
        }

        public void CheckQueue(DateTime startTime, DateTime endTime)
        {
            for (DateTime time = startTime; time <= endTime; time = time.Add(Interval))
            {
                CheckQueue(time);
            }
        }
    }
}

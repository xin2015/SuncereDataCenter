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
        protected virtual DateTime GetTime()
        {
            return DateTime.Today.AddDays(-1);
        }

        protected virtual DateTime GetStartTime(DateTime time)
        {
            return time.AddDays(1);
        }

        protected virtual DateTime GetEndTime(DateTime time)
        {
            return time.AddDays(6);
        }

        protected virtual DateTime GetNextTime(DateTime time)
        {
            return time.AddDays(1);
        }

        public void CheckQueue(DateTime time)
        {
            SyncDataQueue queue = Model.SyncDataQueue.FirstOrDefault(o => o.Class == Class && o.Time == time);
            if (queue == null)
            {
                queue = new SyncDataQueue()
                {
                    Class = Class,
                    Time = time,
                    StartTime = GetStartTime(time),
                    EndTime = GetEndTime(time),
                    LastTime = DateTime.Now
                };
                Model.SyncDataQueue.Add(queue);
            }
        }

        public void CheckQueue(DateTime startTime, DateTime endTime)
        {
            for (DateTime time = startTime; time <= endTime; time = GetNextTime(time))
            {
                CheckQueue(time);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    /// <summary>
    /// 空气质量综合指数数据接口
    /// </summary>
    public interface IAirQualityCompositeIndex
    {
        /// <summary>
        /// 空气质量综合指数
        /// </summary>
        double? AQCI { get; set; }
        /// <summary>
        /// 首要污染物
        /// </summary>
        string PrimaryPollutant { get; set; }
    }
}

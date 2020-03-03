using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    /// <summary>
    /// 空气质量计算器基类
    /// </summary>
    public class BaseAirQualityCalculator
    {
        /// <summary>
        /// 检查完整性（即是否检查基本评价项均有数据，缺少任意项则不计算空气质量指数或空气质量综合指数）
        /// </summary>
        public bool CheckIntegrity { get; set; }

        /// <summary>
        /// 空气质量计算器基类构造函数
        /// </summary>
        public BaseAirQualityCalculator()
        {

        }

        protected virtual Dictionary<string, double?> ToDictionary(IAirQualityPollutantConcentration data)
        {
            Dictionary<string, double?> dic = new Dictionary<string, double?>();
            dic.Add("SO2", data.SO2);
            dic.Add("NO2", data.NO2);
            dic.Add("PM10", data.PM10);
            dic.Add("CO", data.CO);
            dic.Add("O3", data.O3);
            dic.Add("PM25", data.PM25);
            return dic;
        }

        protected bool Validate(double? value)
        {
            return value.HasValue && value.Value >= 0;
        }
    }
}

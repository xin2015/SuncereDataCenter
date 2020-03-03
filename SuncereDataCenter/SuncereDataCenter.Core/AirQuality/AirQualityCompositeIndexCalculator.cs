using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    /// <summary>
    /// 空气质量综合指数计算器
    /// </summary>
    public class AirQualityCompositeIndexCalculator : BaseAirQualityCalculator
    {
        /// <summary>
        /// 污染物二级浓度限值字典
        /// </summary>
        protected Dictionary<string, double> PollutantConcentrationLimitDic { get; set; }

        public AirQualityCompositeIndexCalculator()
        {
            PollutantConcentrationLimitDic = new Dictionary<string, double>();
            PollutantConcentrationLimitDic.Add("SO2", 60);
            PollutantConcentrationLimitDic.Add("NO2", 40);
            PollutantConcentrationLimitDic.Add("PM10", 70);
            PollutantConcentrationLimitDic.Add("CO", 4);
            PollutantConcentrationLimitDic.Add("O3", 160);
            PollutantConcentrationLimitDic.Add("PM25", 35);
        }

        #region 私有方法
        protected virtual double CalculateAirQualityIndividualIndex(string item, double value)
        {
            return Math.Round(value / PollutantConcentrationLimitDic[item], 2);
        }

        /// <summary>
        /// 计算空气质量单项指数字典
        /// </summary>
        /// <param name="data">空气质量基本评价项目浓度值字典</param>
        /// <returns></returns>
        protected virtual Dictionary<string, double> CalculateAirQualityIndividualIndexDic(Dictionary<string, double?> data)
        {
            Dictionary<string, double> airQualityIndividualIndexDic = new Dictionary<string, double>();
            foreach (var item in data)
            {
                if (Validate(item.Value))
                {
                    airQualityIndividualIndexDic.Add(item.Key, CalculateAirQualityIndividualIndex(item.Key, item.Value.Value));
                }
            }
            return airQualityIndividualIndexDic;
        }

        /// <summary>
        /// 计算空气质量综合指数
        /// </summary>
        /// <param name="airQualityCompositeIndex">空气质量综合指数数据接口</param>
        /// <param name="airQualityIndividualIndexDic">空气质量单项指数字典</param>
        protected virtual void CalculateAirQualityCompositeIndex(IAirQualityCompositeIndex airQualityCompositeIndex, Dictionary<string, double> airQualityIndividualIndexDic)
        {
            if (airQualityIndividualIndexDic.Count > 0)
            {
                bool calculate = CheckIntegrity ? airQualityIndividualIndexDic.Count == 6 : true;
                if (calculate)
                {
                    airQualityCompositeIndex.AQCI = airQualityIndividualIndexDic.Sum(o => o.Value);
                    double max = airQualityIndividualIndexDic.Max(o => o.Value);
                    airQualityCompositeIndex.PrimaryPollutant = string.Join(",", airQualityIndividualIndexDic.Where(o => o.Value == max).Select(o => o.Key));
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 计算空气质量综合指数
        /// </summary>
        /// <param name="data">空气质量综合指数计算接口</param>
        public virtual void CalculateAirQualityCompositeIndex(IAirQualityCompositeIndexCalculate data)
        {
            Dictionary<string, double> airQualityIndividualIndexDic = CalculateAirQualityIndividualIndexDic(data);
            CalculateAirQualityCompositeIndex(data, airQualityIndividualIndexDic);
        }

        /// <summary>
        /// 计算空气质量单项指数字典
        /// </summary>
        /// <param name="data">空气质量基本评价项目浓度值接口</param>
        /// <returns></returns>
        public virtual Dictionary<string, double> CalculateAirQualityIndividualIndexDic(IAirQualityPollutantConcentration data)
        {
            Dictionary<string, double?> dic = ToDictionary(data);
            return CalculateAirQualityIndividualIndexDic(dic);
        }
        #endregion
    }
}

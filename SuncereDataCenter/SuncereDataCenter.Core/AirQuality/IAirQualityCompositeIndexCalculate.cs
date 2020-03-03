﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    /// <summary>
    /// 空气质量综合指数计算接口
    /// </summary>
    public interface IAirQualityCompositeIndexCalculate : IAirQualityPollutantConcentration, IAirQualityCompositeIndex
    {
    }
}

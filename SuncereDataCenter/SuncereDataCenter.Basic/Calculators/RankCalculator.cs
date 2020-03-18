using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Basic.Calculators
{
    public class RankCalculator
    {
        public void Calculate<T>(IEnumerable<T> data, Func<T, double?> valueFunc, Action<T, double?> rankAction)
        {
            IEnumerable<T> validData = data.Where(o => valueFunc(o).HasValue).OrderBy(o => valueFunc(o).Value);
            IEnumerable<T> invalidData = data.Where(o => !valueFunc(o).HasValue);
            CalculateBase<T>(validData, invalidData, valueFunc, rankAction);
        }

        public void CalculateDescending<T>(IEnumerable<T> data, Func<T, double?> valueFunc, Action<T, double?> rankAction)
        {
            IEnumerable<T> validData = data.Where(o => valueFunc(o).HasValue).OrderByDescending(o => valueFunc(o).Value);
            IEnumerable<T> invalidData = data.Where(o => !valueFunc(o).HasValue);
            CalculateBase<T>(validData, invalidData, valueFunc, rankAction);
        }

        private void CalculateBase<T>(IEnumerable<T> validData, IEnumerable<T> invalidData, Func<T, double?> valueFunc, Action<T, double?> rankAction)
        {
            int i = 1;
            if (validData.Any())
            {
                double value = valueFunc(validData.First()).Value;
                int rank = i;
                foreach (T item in validData)
                {
                    if (valueFunc(item) != value)
                    {
                        value = valueFunc(item).Value;
                        rank = i;
                    }
                    rankAction(item, rank);
                    i++;
                }
            }
            foreach (T item in invalidData)
            {
                rankAction(item, i);
            }
        }
    }
}

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
            IEnumerable<T> temp = data.Where(o => valueFunc(o).HasValue).OrderBy(o => valueFunc(o).Value);
            double value = double.MaxValue;
            int i = 1, rank = 1;
            foreach (T item in temp)
            {
                if (valueFunc(item) != value)
                {
                    value = valueFunc(item).Value;
                    rank = i;
                }
                rankAction(item, rank);
                i++;
            }
            temp = data.Where(o => !valueFunc(o).HasValue);
            foreach (T item in temp)
            {
                rankAction(item, i);
            }
        }
    }
}

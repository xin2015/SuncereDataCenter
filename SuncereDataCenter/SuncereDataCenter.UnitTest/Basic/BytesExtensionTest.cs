using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Basic
{
    [TestClass]
    public class BytesExtensionTest
    {
        [TestMethod]
        public void Test()
        {
            Random rand = new Random();
            double a = 0;
            for (int i = 0; i < 100; i++)
            {
                a += Math.Round(rand.NextDouble(), 2);
            }
        }
    }
}

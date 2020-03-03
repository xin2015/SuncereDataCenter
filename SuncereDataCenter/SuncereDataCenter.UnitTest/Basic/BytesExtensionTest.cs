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
            byte[] buffer = new byte[10];
            rand.NextBytes(buffer);
            string base64String = buffer.ToBase64String();
            string utf8String = buffer.ToUTF8String();
            string x2String = buffer.ToFormatString("x2");
        }
    }
}

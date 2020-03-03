using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Basic.CryptoTransverters
{
    public interface IDecrypt
    {
        byte[] DecryptToBytes(byte[] inputBuffer);
        byte[] DecryptToBytes(string inString);
        string DecryptToString(byte[] inputBuffer);
        string DecryptToString(string inString);
    }
}

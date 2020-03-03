using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Basic.CryptoTransverters
{
    public interface IEncrypt
    {
        byte[] EncryptToBytes(byte[] inputBuffer);
        byte[] EncryptToBytes(string inString);
        string EncryptToString(byte[] inputBuffer);
        string EncryptToString(string inString);
    }
}

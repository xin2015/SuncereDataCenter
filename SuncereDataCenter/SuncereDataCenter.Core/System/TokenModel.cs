using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.System
{
    public class TokenModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Time { get; set; }

        public TokenModel()
        {
            Time = DateTime.Now;
        }

        public TokenModel(string userName, string password) : this()
        {
            UserName = userName;
            Password = password;
        }
    }
}

using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.System
{
    public class TokenValidator
    {
        public static bool Validate(string token, string controller, string action)
        {
            bool result;
            try
            {
                string json = AsymmetricEncryption.Default.DecryptToString(token);
                TokenModel tm = JsonConvert.DeserializeObject<TokenModel>(json);
                result = tm.UserName == "admin" && tm.Password == "abc@123";
                //result = tm.UserName == "admin" && tm.Password == "abc@123" && tm.Time.AddMinutes(30) <= DateTime.Now;
            }
            catch (Exception e)
            {
                result = true;
            }
            return result;
        }
    }
}

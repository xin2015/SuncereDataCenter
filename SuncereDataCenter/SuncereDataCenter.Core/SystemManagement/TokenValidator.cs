using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.SystemManagement
{
    public class TokenValidator
    {
        public SuncereDataCenterEntities Entities { get; set; }

        public TokenValidator(SuncereDataCenterEntities entities)
        {
            Entities = entities;
        }

        public bool Validate(string token, string controller, string action)
        {
            bool result;
            try
            {
                string json = AsymmetricEncryption.Default.DecryptToString(token);
                TokenModel tm = JsonConvert.DeserializeObject<TokenModel>(json);
                if (tm.Time.AddMinutes(30) < DateTime.Now)
                {
                    result = false;
                }
                else
                {
                    string password = SHA1Encryption.Default.EncryptPassword(tm.Password);
                    SuncereUser user = Entities.SuncereUser.FirstOrDefault(o => o.Status && o.UserName == tm.UserName && o.Password == password);
                    if (user == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = user.SuncereRole.Any(o => o.Status && o.SuncerePermission.Any(p => p.Status && p.Controller == controller && p.Action == action));
                    }
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
    }
}

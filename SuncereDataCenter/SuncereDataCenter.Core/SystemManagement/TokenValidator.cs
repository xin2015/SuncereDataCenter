using Common.Logging;
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
        private ILog logger;
        public SuncereDataCenterModel Model { get; set; }

        public TokenValidator(SuncereDataCenterModel model)
        {
            logger = LogManager.GetLogger<TokenValidator>();
            Model = model;
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
                    SuncereUser user = Model.SuncereUser.FirstOrDefault(o => o.Status && o.UserName == tm.UserName && o.Password == password);
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
                logger.Error(string.Format("验证出错，token={0}&controller={1}&action={2}", token, controller, action), e);
                result = false;
            }
            return result;
        }
    }
}

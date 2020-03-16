using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.SystemManagement
{
    public class TokenModel
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
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

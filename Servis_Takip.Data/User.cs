using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWFanClub.Data
{
    public class User : Base
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Code { get; set; }
        public string? NewCode { get; set; }
        public bool ValidEmail { get; set; }
        public string? NewEmail { get; set; }
    }
}

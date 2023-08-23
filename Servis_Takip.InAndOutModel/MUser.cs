using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWFanClub.InAndOutModel
{
    public class MUser
    {

        public class Form
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
        public class FiltreForm : MFiltreFormBase
        {

        }

        public class Response
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class LoginForm
        {
            public string Email { get;set; }
            public string Password { get; set; }
        }

        public class FilterForm
        {
            public string Search { get; set; }
        }
    }
}

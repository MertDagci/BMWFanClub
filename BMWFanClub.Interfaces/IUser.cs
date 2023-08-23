using BMWFanClub.InAndOutModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWFanClub.Interfaces
{
    public interface IUser : IBase2<MUser.Response, MUser.Form, MUser.FilterForm>
    {
        Task<MethodResponse<MUser.Response>> Login(MUser.LoginForm form, string currentUserId);
    }
}

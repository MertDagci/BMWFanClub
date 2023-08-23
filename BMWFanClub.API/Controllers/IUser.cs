using BMWFanClub.InAndOutModel;

namespace BMWFanClub.API.Controllers
{
    internal interface IUser
    {
        object Add(MUser.Form form, object value);
    }
}
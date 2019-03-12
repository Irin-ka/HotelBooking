using HotelBooking.ViewModels;

namespace HotelBooking.ControllerHelpers.UserControllerHelpers.Abstract {
    public interface IRegisterHandler {

        string TryRegister(RegisterViewModel registerViewModel);
    }
}

using HotelBooking.ViewModels;
using System.Security.Claims;

namespace HotelBooking.ModelBuilders.Abstract {
    public interface IProfileModelBuilder {
        ProfileViewModel BuildModel(ClaimsPrincipal userLoged);
    }
}

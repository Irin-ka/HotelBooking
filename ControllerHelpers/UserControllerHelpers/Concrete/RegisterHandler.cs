using System.Linq;
using Microsoft.AspNetCore.Identity;
using HotelBooking.Models;
using HotelBooking.ControllerHelpers.UserControllerHelpers.Abstract;
using HotelBooking.ViewModels;

namespace HotelBooking.ControllerHelpers.UserControllerHelpers.Concrete {
    public class RegisterHandler : IRegisterHandler {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public RegisterHandler(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public string TryRegister(RegisterViewModel registerViewModel) {
            var user = userManager.FindByNameAsync(registerViewModel.Username).Result;

            if (user != null) {
                return "Username already exists";
            }

            user = userManager.FindByEmailAsync(registerViewModel.Email).Result;

            if (user != null) {
                return "Email already exists";
            }

            int id = wdaContext.User.Max(r => r.UserId) + 1;
            user = new User {
                Username = registerViewModel.Username,
                Email = registerViewModel.Email,          
                UserId = id
            };

            var result = userManager.CreateAsync(user, registerViewModel.Password).Result;

            return result.Succeeded ? "Registration complete" :  "Oops, something went wrong";
        }
    }
}

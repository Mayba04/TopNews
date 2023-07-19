using TopNews.Core.DTOs.User;

namespace TopNews.WEB.Models.ViewModels
{
    public class UpdateProfileVM
    {
        public UpdateUserDTO UserInfo { get; set; }

        public UpdatePasswordDTO UsserPassword { get; set; }
    }
}

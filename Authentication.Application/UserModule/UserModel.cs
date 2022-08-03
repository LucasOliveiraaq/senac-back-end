using FluentValidation;

namespace Authentication.Application.UserModule
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(m => m.Email).NotEmpty().WithMessage("Email invalido").MaximumLength(20);
        }
    }
}
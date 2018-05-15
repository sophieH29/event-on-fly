using FluentValidation.Attributes;
using EventOnFly.Web.Auth.Validations;

namespace EventOnFly.Web.Auth.ViewModels
{
  [Validator(typeof(CredentialsViewModelValidator))]
  public class CredentialsViewModel
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}

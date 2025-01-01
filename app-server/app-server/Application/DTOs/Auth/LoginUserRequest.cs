using System.ComponentModel.DataAnnotations;

namespace app_server.Application.DTOs.Auth;

public class LoginUserRequest
{
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}
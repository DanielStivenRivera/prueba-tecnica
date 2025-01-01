using System.ComponentModel.DataAnnotations;

namespace app_server.Application.DTOs.Auth;

public class CreateUserRequest
{
    [Required]
    public string username { get; set; }
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}
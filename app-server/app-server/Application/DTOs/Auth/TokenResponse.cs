using System.ComponentModel.DataAnnotations;

namespace app_server.Application.DTOs.Auth;

public class TokenResponse
{
    [Required]
    public string token { get; set; }
}
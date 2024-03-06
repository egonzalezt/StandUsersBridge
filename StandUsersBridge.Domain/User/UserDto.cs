namespace StandUsersBridge.Domain.User;

using System.ComponentModel.DataAnnotations;

public class UserDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public EmailDto Email { get; set; }

    [Required]
    public IdentificationDto IdentificationNumber { get; set; }
}
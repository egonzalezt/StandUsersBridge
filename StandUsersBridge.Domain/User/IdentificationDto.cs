namespace StandUsersBridge.Domain.User;

using System.ComponentModel.DataAnnotations;

public class IdentificationDto
{

    [Required]
    public int Value { get; set; }
}

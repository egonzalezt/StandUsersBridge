namespace StandUsersBridge.Domain.User;

using System.ComponentModel.DataAnnotations;

public class EmailDto
{
    [Required]
    [EmailAddress]
    public string Value { get; set; }
}

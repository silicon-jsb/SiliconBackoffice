using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SiliconBackoffice.Data;

public class UserBasicInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Display(Name = "First name")]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
}

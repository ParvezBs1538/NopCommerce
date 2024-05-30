using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.NopStationTeams.Models;

public class Profile
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Please enter name")]
    public string Name { get; set; }
    [Required]
    public string Designation { get; set; }
    [Required]
    public IFormFile ImagePath { get; set; }
    [Required]
    public bool IsNopMVP { get; set; }
    [Required]
    public bool IsCertified { get; set; }
}

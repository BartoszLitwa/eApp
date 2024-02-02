using System.ComponentModel.DataAnnotations;

namespace eApp.CommandService.Domain.Models;

public class Command
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string HowTo { get; set; }
    [Required]
    public string CommandLine { get; set; }
    [Required]
    public int PlatformId { get; set; }
    
    public virtual Platform Platform { get; set; }
}
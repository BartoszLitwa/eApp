using System.ComponentModel.DataAnnotations;

namespace eApp.CommandService.Domain.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int ExternalId { get; set; }
    [Required]
    public string Name { get; set; }

    public virtual ICollection<Command> Commands { get; set; } = new List<Command>();
}
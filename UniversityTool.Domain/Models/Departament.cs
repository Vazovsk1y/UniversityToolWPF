using System.Collections.Generic;

namespace UniversityTool.Domain.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}

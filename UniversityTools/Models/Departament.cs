using System.Collections.Generic;

namespace UniversityTool.Models
{
    public class Departament
    {
        public string? Title { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}

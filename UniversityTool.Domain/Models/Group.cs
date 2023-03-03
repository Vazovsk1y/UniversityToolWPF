using System.Collections.Generic;

namespace UniversityTool.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public Departament Departament { get; set; }
        public string Title { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}

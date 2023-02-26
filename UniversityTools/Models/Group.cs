using System.Collections.Generic;

namespace UniversityTool.Models
{
    public class Group
    {
        public string Title { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}

using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    public class Group : BaseModel
    {
        public Departament Departament { get; set; }
        public string Title { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}

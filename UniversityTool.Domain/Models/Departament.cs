using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    public class Departament : BaseModel
    {
        public string? Title { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}

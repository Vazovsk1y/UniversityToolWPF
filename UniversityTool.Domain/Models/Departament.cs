using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    /// <summary>
    /// Departament have a group list, each group have students list. TreeView source.
    /// </summary>
    public class Departament : BaseModel
    {
        public string? Title { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}

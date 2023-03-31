using System.Collections.ObjectModel;
using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    /// <summary>
    /// Departament have a group list, each group have a student list.
    /// </summary>
    public class Departament : BaseModel
    {
        public string Title { get; set; }
        public IList<Group> Groups { get; set; } = new ObservableCollection<Group>();
    }
}

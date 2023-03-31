using System.Collections.ObjectModel;
using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    /// <summary>
    /// Group have a parent - departament and collection of sons - students.
    /// </summary>
    public class Group : BaseModel
    {
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }

        public string Title { get; set; }
        public IList<Student> Students { get; set; } = new ObservableCollection<Student>();
    }
}

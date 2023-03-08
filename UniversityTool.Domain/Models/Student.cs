using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    /// <summary>
    /// Student have a parent - group, also by his parent he could take info about his departament.
    /// </summary>
    public class Student : BaseModel
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public string Name { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
    }
}

using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Models
{
    public class Student : BaseModel
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public string Name { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
    }
}

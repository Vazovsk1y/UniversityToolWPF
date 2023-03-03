namespace UniversityTool.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
    }
}

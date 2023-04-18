namespace UniversityTool.Domain.Models.Base
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}

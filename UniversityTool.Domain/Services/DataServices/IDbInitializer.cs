namespace UniversityTool.Domain.Services.DataServices
{
    public interface IDbInitializer
    {
        Task InitializeDataBaseAsync();
    }
}

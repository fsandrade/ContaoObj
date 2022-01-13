namespace ContaObj.Application.Interfaces
{
    public interface IAgenciaRepository
    {
        Task<bool> ExistsOnDatabaseAsync(int id);
    }
}

using ContaObj.Application.Interfaces;
using ContaObj.Infra.Database;

namespace ContaObj.Infra.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly ContaObjContext context;

        public AgenciaRepository(ContaObjContext context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsOnDatabaseAsync(int id)
        {
            return await context.Agencias.FindAsync(id) != null;
        }
    }
}

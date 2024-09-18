using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;

namespace Services.NetCore.Application.Core
{
    public class BaseAppService : IDisposable
    {
        protected readonly IGenericRepository<IGenericDataContext> _repository;

        public BaseAppService(IGenericRepository<IGenericDataContext> repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}

using System.Threading.Tasks;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Web
{
    public interface IAuditStore
    {
        Task SaveAsync(Audit audit);
        void Save(Audit audit);
    }

    public class AuditStore : IAuditStore
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IRepository<Audit> _auditRepository;

        public AuditStore(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
            _auditRepository = _unitofwork.Repository<Audit>();
        }
        public async Task SaveAsync(Audit audit)
        {
            _auditRepository.Insert(audit);
            await _unitofwork.SaveChangesAsync();
        }

        public void Save(Audit audit)
        {
            _auditRepository.Insert(audit);
            _unitofwork.SaveChanges();
        }
    }
}

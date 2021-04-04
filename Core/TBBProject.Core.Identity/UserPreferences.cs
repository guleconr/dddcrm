using TBBProject.Core.Data.Domain;

namespace TBBProject.Web.Models
{
    public class UserPreferences : IEntity
    {
        public long Id { get; set; }
        public bool ReceiveEmails { get; set; } = true;
        public bool ReceiveSms { get; set; } = true;
    }
}

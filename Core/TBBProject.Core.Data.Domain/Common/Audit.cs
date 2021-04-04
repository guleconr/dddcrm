using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBBProject.Core.Data.Domain
{
    public class Audit : IEntity
    {
        public long Id { get; set; }
        public string User { set; get; }
        public string ClassName { set; get; }
        public string MethodName { set; get; }
        public string Args { set; get; }
        public DateTime DateTime { set; get; }
        public string UserIp { set; get; }
        [NotMapped]
        public Exception Exception { set; get; }
        public override string ToString()
        {
            var exceptionOrSuccessMessage = Exception != null ? "exception: " + Exception.Message : "success";

            return $"{ClassName}.{MethodName} and params {Args} executed by {User} on {DateTime} GMT + 02:00 from {UserIp} resulted in {exceptionOrSuccessMessage}.";
        }
    }
}

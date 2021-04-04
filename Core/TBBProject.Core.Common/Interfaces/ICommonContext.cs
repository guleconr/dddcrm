namespace TBBProject.Core.Common
{
    public interface ICommonContext
    {
        string AssemblyName { get; }
        string WorkingLanguage { get; }
        string CorrelationId { get; }
        string RequesterIpAddress { get; }
        string SessionId { get; set; }
        string MachineIp { get; }
        string GetRequesterIpAddress();
        bool IsRequestAvailable();
    }
}

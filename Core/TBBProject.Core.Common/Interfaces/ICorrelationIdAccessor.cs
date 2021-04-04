namespace TBBProject.Core.Common
{
    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
        void SetCorrelationId(string correlationId);
    }
}

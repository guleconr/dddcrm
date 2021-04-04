namespace TBBProject.Core.Common
{
    public interface IDataProtection
    {
        string Protect(string input);

        string Unprotect(string input);
    }
}

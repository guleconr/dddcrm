namespace TBBProject.Core.UiMessages
{
    public interface IMessageInfo
    {
        string Title { get; set; }
        string Message { get; set; }
    }

    public interface IErrorMessage : IMessageInfo
    {
    }

    public interface ISuccessMessage : IMessageInfo
    {
    }

    public interface IWarningMessage : IMessageInfo
    {
    }

    public interface IInfoMessage : IMessageInfo
    {
    }
}

namespace TBBProject.Core.UiMessages
{
    public sealed class SuccessMessage : MessageInfo, ISuccessMessage
    {
        public SuccessMessage() => MessageType = MessageType.Success;
        public override string ToString() => $"toastr.success('{Message}', '{Title}');";
    }
    public sealed class ErrorMessage : MessageInfo, IErrorMessage
    {
        public ErrorMessage() => MessageType = MessageType.Error;
        public override string ToString() => $"toastr.error('{Message}', '{Title}');";
    }
    public sealed class WarningMessage : MessageInfo, IWarningMessage
    {
        public WarningMessage() => MessageType = MessageType.Warning;
        public override string ToString() => $"toastr.warning('{Message}', '{Title}');";
    }
    public sealed class InfoMessage : MessageInfo, IInfoMessage
    {
        public InfoMessage() => MessageType = MessageType.Info;
        public override string ToString() => $"toastr.info('{Message}', '{Title}');";
    }
}

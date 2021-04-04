namespace TBBProject.Core.UiMessages
{
    public class ToastrMessages : Message<MessageInfo>
    {
        public ToastrMessages(IMessageInfoContainerFactory messageInfoContainerFactory)
            : base(messageInfoContainerFactory)
        {
        }

        private void SetOrDefault(IMessageInfo toastMessage, IMessageInfo message)
        {
            if (string.IsNullOrEmpty(message.Message))
            {
                toastMessage.Message = ErrorOptions.DefaultInfoMessage;
            }
            else
            {
                toastMessage.Message = message.Message;
            }

            if (string.IsNullOrEmpty(message.Title))
            {
                toastMessage.Title = ErrorOptions.DefaultInfoTitle;
            }
            else
            {
                toastMessage.Title = message.Title;
            }
        }

        public override void Error(IErrorMessage errorMessage)
        {
            var toastr = new ErrorMessage();

            SetOrDefault(toastr, errorMessage);

            Add(toastr);
        }

        public override void Info(IInfoMessage infoMessage)
        {
            var toastr = new InfoMessage();

            SetOrDefault(toastr, infoMessage);

            Add(toastr);
        }

        public override void Info(string title, string message)
        {
            var toastr = new InfoMessage
            {
                Title = title,
                Message = message,
                MessageType = MessageType.Info
            };

            SetOrDefault(toastr, toastr);

            Add(toastr);
        }

        public override void Success(ISuccessMessage successMessage)
        {
            var toastr = new SuccessMessage();

            SetOrDefault(toastr, successMessage);

            Add(toastr);
        }

        public override void Success(string title, string message)
        {
            var toastr = new SuccessMessage()
            {
                Message = message,
                Title = title,
                MessageType = MessageType.Success
            };

            SetOrDefault(toastr, toastr);

            Add(toastr);
        }

        public override void Warning(IWarningMessage warningMessage)
        {
            var toastr = new WarningMessage();

            SetOrDefault(toastr, warningMessage);

            Add(toastr);
        }

        public override void Warning(string title, string message)
        {
            var toastr = new WarningMessage
            {
                Title = title,
                Message = message,
                MessageType = MessageType.Warning
            };

            SetOrDefault(toastr, toastr);

            Add(toastr);
        }

        public override void Error(string title, string message)
        {
            var toastr = new ErrorMessage
            {
                Title = title,
                Message = message,
                MessageType = MessageType.Error
            };

            SetOrDefault(toastr, toastr);

            Add(toastr);
        }
    }
}

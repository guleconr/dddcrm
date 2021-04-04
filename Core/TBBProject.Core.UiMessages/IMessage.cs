using System.Collections.Generic;

namespace TBBProject.Core.UiMessages
{
    public interface IMessage
    {
        /// <summary>
        /// Notifies a success message
        /// </summary>
        /// <param name="messages">messages.</param>
        void Success(ISuccessMessage messages);
        void Success(string title, string message);

        /// <summary>
        /// Notifies an Info message
        /// </summary>
        /// <param name="message">message.</param>
        void Info(IInfoMessage message);
        void Info(string title, string message);

        /// <summary>
        /// Notifies a Error message
        /// </summary>
        /// <param name="message">message.</param>
        void Error(IErrorMessage message);
        void Error(string title, string message);

        /// <summary>
        /// Notifies a warning message
        /// </summary>
        /// <param name="message">message.</param>
        void Warning(IWarningMessage message);
        void Warning(string title, string message);

        /// <summary>
        /// Gets all UI messages
        /// </summary>
        /// <returns>The messages.</returns>
        IEnumerable<IMessageInfo> Get();

        /// <summary>
        /// Clears messages
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds message info.
        /// </summary>
        /// <param name="messageInfo"></param>
        void Add(IMessageInfo messageInfo);
    }
}

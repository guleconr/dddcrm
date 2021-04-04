using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TBBProject.Core.UiMessages
{
    public class MessageInfo : IMessageInfo
    {
        public virtual string Title { get; set; }
        public virtual string Message { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType MessageType { get; set; }
        public override string ToString()
        {
            var result = string.Empty;

            if (MessageType == MessageType.Error)
            {
                result = $"toastr.error('{Message}', '{Title}');";
            }
            if (MessageType == MessageType.Info)
            {
                result = $"toastr.info('{Message}', '{Title}');";
            }
            if (MessageType == MessageType.Warning)
            {
                result = $"toastr.warning('{Message}', '{Title}');";
            }
            if (MessageType == MessageType.Success)
            {
                result = $"toastr.success('{Message}', '{Title}');";
            }

            return result;
        }
    }
}

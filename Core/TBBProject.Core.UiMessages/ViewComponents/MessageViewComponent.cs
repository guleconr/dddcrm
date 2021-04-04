using Microsoft.AspNetCore.Mvc;

namespace TBBProject.Core.UiMessages
{
    [ViewComponent(Name = "ToastrMessages")]
    public class MessageViewComponent : ViewComponent
    {
        private readonly IMessage _message;


        public MessageViewComponent(IMessage message) => _message = message;

        public IViewComponentResult Invoke()
        {
            var messageViewModel = new MessageViewModel
            {
                Messages = _message.Get()
            };

            return View("~/Views/Shared/Components/Toastr.cshtml", messageViewModel);
        }
    }
}

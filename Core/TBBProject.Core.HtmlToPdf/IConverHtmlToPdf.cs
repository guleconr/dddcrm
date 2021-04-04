using System.Threading.Tasks;

namespace TBBProject.Core.HtmlToPdf
{
    public interface IConverHtmlToPdf
    {
        Task<byte[]> ConvertAsync(string viewName);
        Task<byte[]> ConvertAsync<TModel>(string viewName, TModel model);
        Task<byte[]> ConvertFromHtml(string html);
    }

    public class ConvertHtmlToPdf : IConverHtmlToPdf
    {
        private readonly IRazorViewRenderer _viewRenderer;
        private readonly IPdfConverter _pdfConverter;
        public ConvertHtmlToPdf(IRazorViewRenderer viewRenderer, IPdfConverter pdfConverter)
        {
            _viewRenderer = viewRenderer;
            _pdfConverter = pdfConverter;
        }

        public async Task<byte[]> ConvertAsync(string viewName)
        {
            var html = await _viewRenderer.RenderViewToStringAsync(viewName);
            byte[] pdf = _pdfConverter.Convert(html);

            return pdf;
        }

        public async Task<byte[]> ConvertFromHtml(string html)
        {
            await Task.CompletedTask;
            byte[] pdf = _pdfConverter.Convert(html);

            return pdf;
        }

        public async Task<byte[]> ConvertAsync<TModel>(string viewName, TModel model)
        {
            var html = await _viewRenderer.RenderViewToStringAsync(viewName, model);
            byte[] pdf = _pdfConverter.Convert(html);

            return pdf;
        }
    }
}

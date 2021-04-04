using System.Drawing;
using System.Threading;

namespace TBBProject.Core.HtmlToPdf
{
    // See : https://github.com/rdvojmoc/DinkToPdf for more info.

    public interface IPdfConverter
    {
        byte[] Convert(string htmlContent);
    }

    public class HtmlToPdfConverter : IPdfConverter
    {
        public byte[] Convert(string htmlContent)
        {
            var pdf = new byte[50];
            return pdf;
        }
    }
}

using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;

namespace InvoicerNETCore
{
    public class Test
    {
        public static void SaveDoc()
        {
            GlobalFontSettings.FontResolver = new FontResolver();

            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("OpenSans", 20, XFontStyle.Bold);

            gfx.DrawString("Hello World!", font, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.Center);

            document.Save("test.pdf");
        }

        //public class FontResolver : IFontResolver
        //{
        //    public byte[] GetFont(string faceName)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            using (var fs = File.Open(faceName, FileMode.Open))
        //            {
        //                fs.CopyTo(ms);
        //                ms.Position = 0;
        //                return ms.ToArray();
        //            }
        //        }
        //    }
        //    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        //    {
        //        if (familyName.Equals("OpenSans", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            if (isBold && isItalic)
        //            {
        //                return new FontResolverInfo("OpenSans-BoldItalic.ttf");
        //            }
        //            else if (isBold)
        //            {
        //                return new FontResolverInfo("OpenSans-Bold.ttf");
        //            }
        //            else if (isItalic)
        //            {
        //                return new FontResolverInfo("OpenSans-Italic.ttf");
        //            }
        //            else
        //            {
        //                return new FontResolverInfo("OpenSans-Regular.ttf");
        //            }
        //        }
        //        return null;
        //    }
        //}
    }
}

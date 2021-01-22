using InvoicerNETCore.Helpers;
using InvoicerNETCore.Models;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using PdfSharpCore.Pdf.Security;
using System;

namespace InvoicerNETCore.Services.Impl
{
    public partial class PdfInvoice
    {
        public Document Pdf { get; private set; }
        public Invoice Invoice { get; private set; }

        private PageFormat InvoiceFormat
        {
            get
            {
                return Invoice.PageSize switch
                {
                    SizeOption.A4 => PageFormat.A4,
                    SizeOption.Legal => PageFormat.Legal,
                    SizeOption.Letter => PageFormat.Letter,
                    _ => throw new ArgumentException("Unable to find matching size."),
                };
            }
        }

        private Orientation InvoiceOrientation
        {
            get
            {
                return Invoice.PageOrientation switch
                {
                    OrientationOption.Landscape => Orientation.Landscape,
                    OrientationOption.Portrait => Orientation.Portrait,
                    _ => throw new ArgumentException("Unable to find matching orientation."),
                };
            }
        }

        private Border BorderLine
        {
            get
            {
                Border bottomLine = new Border
                {
                    Width = new Unit(0.5),
                    Color = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor)
                };
                return bottomLine;
            }
        }

        public PdfInvoice(Invoice invoice)
        {
            Pdf = new Document();
            Invoice = invoice;
        }

        public void Save(string filename, string password = null)
        {
            CreateDocument();

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = Pdf
            };
            renderer.RenderDocument();
            if (!string.IsNullOrEmpty(password))
                SetPassword(renderer, password);
            renderer.PdfDocument.Save(filename);
        }

        private void CreateDocument()
        {
            Pdf.DefaultPageSetup.PageFormat = InvoiceFormat;
            Pdf.DefaultPageSetup.Orientation = InvoiceOrientation;
            Pdf.DefaultPageSetup.TopMargin = 125;
            Pdf.Info.Title = Invoice.Title;

            DefineStyles();

            Pdf.AddSection();
            HeaderSection();
            AddressSection();
            BillingSection();
            PaymentSection();
            FooterSection();
        }


        private void SetPassword(PdfDocumentRenderer renderer, string password)
        {
            PdfSecuritySettings securitySettings = renderer.PdfDocument.SecuritySettings;
            securitySettings.OwnerPassword = password;
            securitySettings.UserPassword = password;
            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = false;
            securitySettings.PermitFullQualityPrint = false;
            securitySettings.PermitModifyDocument = false;
            securitySettings.PermitPrint = false;
        }

        private void DefineStyles()
        {
            Style style = Pdf.Styles["Normal"];
            style.Font.Name = "Calibri";

            style = Pdf.Styles.AddStyle("H1-20", "Normal");
            style.Font.Size = 20;
            style.Font.Bold = true;

            style = Pdf.Styles.AddStyle("H2-8", "Normal");
            style.Font.Size = 8;

            style = Pdf.Styles.AddStyle("H2-8-Blue", "H2-8");
            style.ParagraphFormat.Font.Color = Colors.Blue;

            style = Pdf.Styles.AddStyle("H2-8B", "H2-8");
            style.Font.Bold = true;

            style = Pdf.Styles.AddStyle("H2-9", "Normal");
            style.Font.Size = 9;

            style = Pdf.Styles.AddStyle("H2-9-Grey", "H2-9");
            style.Font.Color = Colors.Gray;

            style = Pdf.Styles.AddStyle("H2-9B", "H2-9");
            style.Font.Bold = true;

            style = Pdf.Styles.AddStyle("H2-9B-Inverse", "H2-9B");
            style.ParagraphFormat.Font.Color = Colors.White;

            style = Pdf.Styles.AddStyle("H2-9B-Color", "H2-9B");
            style.Font.Color = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);

            style = Pdf.Styles.AddStyle("H2-10", "Normal");
            style.Font.Size = 10;

            style = Pdf.Styles.AddStyle("H2-10B", "H2-10");
            style.Font.Bold = true;

            style = Pdf.Styles.AddStyle("H2-10B-Color", "H2-10B");
            style.Font.Color = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);
        }
    }
}

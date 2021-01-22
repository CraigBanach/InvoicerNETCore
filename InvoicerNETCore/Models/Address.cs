namespace InvoicerNETCore.Models
{
    public class Address
    {
        public string Title { get; set; }
        public string[] AddressLines { get; set; }
        public string VatNumber { get; set; }
        public string CompanyNumber { get; set; }

        public bool HasCompanyNumber { get { return !string.IsNullOrEmpty(CompanyNumber); } }
        public bool HasVatNumber { get { return !string.IsNullOrEmpty(VatNumber); } }

        public static Address Make(string title, string[] address, string company = null, string vat = null)
        {
            return new Address
            {
                Title = title,
                AddressLines = address,
                CompanyNumber = company,
                VatNumber = vat,
            };
        }
    }
}
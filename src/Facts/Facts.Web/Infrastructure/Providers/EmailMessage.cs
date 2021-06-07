namespace Facts.Web.Infrastructure.Providers
{
    public class EmailMessage
    {
        public string Id { get; set; } = null;

        public string Author { get; set; } = null!;

        public string Recipient { get; set; } = null!;

        public string AddressTo { get; set; } = null!;

        public string AddressFrom { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public bool IsHtml { get; set; }

        public string Body { get; set; } = null!;
    }
}
namespace UsersManagerAPI.DomainClasses.Models
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "your email";
        public string FromMailPassword { get; set; } = "your password";
        public string ToMailIds { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; } = true;

        //public List<string> Attachments { get; set; }
    }
}

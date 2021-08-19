namespace UsersManagerAPI.DomainClasses.Models
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "akdawood21@gmail.com";
        public string FromMailPassword { get; set; } = "4rgvevs-0dRD^&@Bx+)sfd";
        public string ToMailIds { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; } = true;

        //public List<string> Attachments { get; set; }
    }
}

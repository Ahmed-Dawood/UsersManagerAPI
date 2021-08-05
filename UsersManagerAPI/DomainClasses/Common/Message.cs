namespace UsersManagerAPI.DomainClasses.Common
{
    public static class Message
    {
        public static string Success = "Success";
        public static string ErrorFound = "ErrorFound";
        public static string UserAlreadyExist = "User already exist, please log in.";
        public static string VerifyMail = "User already exist, please verify your mail.";
        public static string InvalidUser = "Invalid user. Please sign up first.";
        public static string MailSent = "Mail Sent";
        public static string UserCreatedVerifyMail = "User created, check your mail. Click link and verify.";
        public static string UserRemoved = "User Removed.";
    }
}

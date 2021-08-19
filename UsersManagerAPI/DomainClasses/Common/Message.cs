namespace UsersManagerAPI.DomainClasses.Common
{
    public static class Message
    {
        public static string Success = "Success";
        public static string ErrorFound = "ErrorFound";
        public static string UserAlreadyExist = "User already exist, please log in.";
        public static string VerifyMailRegister = "User already exist, please check you mail box and verify your email.";
        public static string VerifyMailLogIn = "Please check you mail box and verify your email.";
        public static string InvalidUser = "Invalid user. Please sign up first.";
        public static string MailSent = "Mail Sent";
        public static string UserCreatedVerifyMail = "User created, check your mail. Click link and verify.";
        public static string UserRemoved = "User Removed.";
        public static string DuplicateEmail = "This Email is already used for another account.";
    }
}

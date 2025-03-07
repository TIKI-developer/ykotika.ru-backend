namespace Ykotika.Email
{
    public class EmailVerifierOptions
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required bool EnableSsl { get; set; }
        public required EmailCredentials Credentials { get; set; }
    }
}

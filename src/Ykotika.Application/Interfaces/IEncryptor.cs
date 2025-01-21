namespace Ykotika.Application.Interfaces
{
    public interface IEncryptor
    {
        public string Encrypt(string value);
        public string Decrypt(string value);
    }
}

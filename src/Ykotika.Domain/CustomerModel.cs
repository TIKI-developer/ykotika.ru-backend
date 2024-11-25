namespace Ykotika.Domain
{
    public class CustomerModel
    {
        public required Guid UserId { get; set; }
        public UserModel? User { get; set; }
    }
}

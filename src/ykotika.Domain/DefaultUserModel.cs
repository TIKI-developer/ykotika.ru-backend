namespace Ykotika.Domain
{
    public class DefaultUserModel : UserModel
    {
        public override UserRole Role => UserRole.Default;
    }
}

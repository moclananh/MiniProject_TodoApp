namespace Todo.Application.Exceptions.UserExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not exist")
        {

        }
    }
}

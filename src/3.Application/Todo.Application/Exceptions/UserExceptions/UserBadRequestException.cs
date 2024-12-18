using TodoApp.BuildingBlock.Exceptions;

namespace Todo.Application.Exceptions.UserExceptions
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException(string message) : base(message)
        {

        }
    }
}

namespace Todo.Application.Exceptions.TodoExceptions
{
    public class TodoBadRequestException : BadRequestException
    {
        public TodoBadRequestException(string message, string details) : base(message, details)
        {

        }
    }

}

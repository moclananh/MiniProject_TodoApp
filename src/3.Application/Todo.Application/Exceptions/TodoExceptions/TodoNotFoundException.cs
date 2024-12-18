namespace Todo.Application.Exceptions.TodoExceptions
{
    public class TodoNotFoundException : NotFoundException
    {
        public TodoNotFoundException(int Id) : base("Todo", Id)
        {

        }
    }
}

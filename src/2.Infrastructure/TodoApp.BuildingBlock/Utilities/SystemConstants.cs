
namespace TodoApp.BuildingBlock.Utilities
{
    public class SystemConstants
    {
        public class ModelStateResponses
        {
            public const string ModelStateInvalid = "Invalid data provided";
        }
        public class AuthenticateResponses
        {
            public const string IncorrectPassword = "Incorrect password.";
            public const string UserAuthenticated = "Authenticated successfully.";
            public const string UserRegistered = "Register successfully.";
            public const string EmailChecked = "Email is already registered. Please use a different email.";
            public const string UsernameChecked = "Username is already registered. Please use a different username.";
        }
        public class TodoMessageResponses
        {
            public const string TodoFetched = "Todo fetched successfully.";
            public const string TodoCreated = "Todo created successfully.";
            public const string TodoUpdated = "Todo updated successfully.";
            public const string TodoDeleted = "Todo deleted successfully.";
        }
        public class InternalMessageResponses
        {
            public const string InternalMessageError = "Error from server.";
            public const string DatabaseBadResponse = "Error when called store procedure. ";
        }
    }
}

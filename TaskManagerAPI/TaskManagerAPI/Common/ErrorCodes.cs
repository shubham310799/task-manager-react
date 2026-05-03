using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Common
{
    public class ErrorCodes
    {
        public static Error UserAlreadyExistsError = new Error
        {
            ErrorCode = "001",
            Message = "User with same email already exists"
        };

        public static Error LoginError = new Error
        {
            ErrorCode = "002",
            Message = "Email or password is incorrect"
        };

        public static Error UserNotFoundError = new Error
        {
            ErrorCode = "003",
            Message = "User not found"
        };

        public static Error SomethingWentWrong = new Error
        {
            ErrorCode = "004",
            Message = "Something went wrong"
        };

        public static Error InvalidTask = new Error
        {
            ErrorCode = "005",
            Message = "Invalid task"
        };

    }
}

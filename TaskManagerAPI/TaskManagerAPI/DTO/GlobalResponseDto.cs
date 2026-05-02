namespace TaskManagerAPI.DTO
{
    public class GlobalResponseDto<T>
    {
        public T Data { get; set; }
        public Error? Error { get; set; }
        public bool Success
        {
            get => Error == null;
        }
    }

    public class Error
    {
        public required string ErrorCode { get; set; }
        public required string Message { get; set; }
    }
}

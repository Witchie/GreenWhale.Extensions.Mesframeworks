namespace GreenWhale.Mesframeworks
{
    public class Result
    {
        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

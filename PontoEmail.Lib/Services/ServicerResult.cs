namespace PontoEmail.Lib.Services
{
    public class ServicerResult<T> where T : class
    {
        public string Message { get; }
        public bool IsOk { get; }
        public T Data { get; }

        public ServicerResult(T data)
        {
            IsOk = true;
            Data = data;
            Message = null;
        }

        public ServicerResult(string message)
        {
            Message = message;
            IsOk = false;
            Data = null;
        }


    }
}
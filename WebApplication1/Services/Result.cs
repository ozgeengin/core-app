using System;
using WebApplication1.Enums;

namespace WebApplication1.Services
{
    public class Result<T>
    {
        public ResultType ResultType { get; private set; }
        public Exception Exception { get; private set; }
        public T Data { get; set; }

        public Result(T data)
        {
            ResultType = ResultType.Success;
            Data = data;
        }
        
        public Result(Exception exception)
        {
            ResultType = ResultType.Fail;
            Exception = exception;
        }

    }
}

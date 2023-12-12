using System;

namespace APIServiceResult
{
    public sealed class ApiServiceResult<T>
    {
        public T Data { get; }

        public bool IsError { get; } = false;


        public string Msg { get; } = "";


        public ApiServiceResult(T obj, bool isError = false, string msg = "")
        {
            Data = obj;
            IsError = isError;
            Msg = msg;
        }
    }
}

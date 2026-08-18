using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Common
{
    internal class Result<T>
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        public T data { get; }

        public Result(bool _IsSuccess , string message ,T _data)
        {
            IsSuccess = _IsSuccess;
            Message = message;
            data = _data;
        }


        public static Result<T> Success(T obj, string message = "Success")
        {
            return new Result<T>(true, message,obj);
        }

        public static Result<T> Failure(string message = "Failure")
        {
            return new Result<T>(false, message, default);
        }
    }
}

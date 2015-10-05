using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core
{
    public class Result
    {
        public Result(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Result OnSuccess(Func<Result,Result> action)
        {
            if (this.Success)
                return action(this);
            else return this;
        }
        public Result OnSuccess(Action<Result> action)
        {
            if (this.Success) action(this);
            return this;
        }
        public Result OnError(Action<Result> action)
        {
            if (!this.Success) action(this);
            return this;                 
        }
    }
    public class Result<T>:Result
    {
        public T Value { get; private set; }
        public Result(bool success, string message, T value)
            :base (success,message)
        {
            this.Value = value;
        }
        public Result<T> OnSuccess(Func<Result<T>, Result<T>> action)
        {
            if (this.Success)
                return action(this);
            else return this;
        }
        public Result<T> OnSuccess(Action<Result<T>> action)
        {
            if (this.Success) action(this);
            return this;
        }
        public Result<T> OnError(Action<Result<T>> action)
        {
            if (!this.Success) action(this);
            return this;
        }
    }
}

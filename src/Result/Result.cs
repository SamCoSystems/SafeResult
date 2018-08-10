using Microsoft.AspNetCore.Mvc;

namespace SamCo.AspNetCore.SafeResult
{
    public abstract class Result
    {
        public ActionResult ErrorResult { get; private set; } = null;

        public bool Errored => ErrorResult != null;

        protected Result() { }

        protected Result(ActionResult errorResult)
        {
            ErrorResult = errorResult;
        }

        private class Success : Result { }

        private class Error : Result
        {
            public Error(ActionResult errorResult) : base(errorResult) { }
        }

        public static Result WithoutError => new Success();

        public static implicit operator Result(ActionResult errorResult) => new Error(errorResult);
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        private Result(T value)
        {
            Value = value;
        }

        private Result(ActionResult errorResult) : base(errorResult) { }

        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public static implicit operator Result<T>(ActionResult errorResult) => new Result<T>(errorResult);
    }
}

using System;
using System.Collections.Generic;

namespace Modules.Core.Domain.Logic
{
    public class Result
    {
        // Interfaces
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public string ErrorMsg
        {
            get
            {
                if (!IsFailure) throw new InvalidOperationException("InvalidOperation: Can't retrieve the error message from a success result");
                return _errorMsg;
            }
        }

        // Data
        private readonly string _errorMsg;

        // Constructors
        protected Result(bool isSuccess, string errorMsg = null)
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMsg))
                throw new InvalidOperationException(
                    "InvalidOperation: A result cannot be successful and contain an error");
            IsSuccess = isSuccess;
            _errorMsg = errorMsg;
        }

        // Factories
        public static Result Ok()
        {
            return new Result(true);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(true, null, value);
        }

        public static Result Fail(string errorMsg)
        {
            return new Result(false, errorMsg);
        }

        public static Result<T> Fail<T>(T error)
        {
            return new Result<T>(false, error);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
                if (result.IsFailure)
                    return result;
            return Ok();
        }
    }

    public class Result<T> : Result
    {
        // Interfaces
        public T Value
        {
            get
            {
                if (!IsSuccess) throw new InvalidOperationException("InvalidOperation: Can't retrieve the value from a failed result");
                return _value;
            }
        }

        public T Error
        {
            get
            {
                if (!IsFailure) throw new InvalidOperationException("InvalidOperation: Can't retrieve the error from a success result");
                return _value;
            }
        }

        // Data
        private readonly T _value;

        // Constructors
        protected internal Result(bool isSuccess, string errorMsg = null, T value = default(T)) : base(isSuccess, errorMsg)
        {
            if (!isSuccess && string.IsNullOrEmpty(errorMsg))
                throw new InvalidOperationException(
                    "InvalidOperation: A failing result needs to contain an error message");
            _value = value;
        }

        protected internal Result(bool isSuccess, T error = default(T)) : base(isSuccess)
        {
            if (!isSuccess && EqualityComparer<T>.Default.Equals(error, default(T)))
                throw new InvalidOperationException("InvalidOperation: A failing result needs to contain an error");
            _value = error;
        }

        // Operators
        public static explicit operator T(Result<T> result)
        {
            if (!result.IsSuccess) throw new InvalidOperationException("InvalidOperation: Can't retrieve the value from a failed result");
            return result.Value;
        }
    }
}
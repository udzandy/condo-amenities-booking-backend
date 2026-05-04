using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoAmenitiesBooking.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public object? Value { get; set; }

        public static Result Success(object? value = null)
            => new Result { IsSuccess = true, Value = value };

        public static Result Failure(string error)
            => new Result { IsSuccess = false, Error = error };
    }
}

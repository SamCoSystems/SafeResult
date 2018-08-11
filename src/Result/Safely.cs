using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SamCo.AspnetCore.SafeResult
{
    public static class Safely
    {
        public static Result<T> Call<T>(Func<T> f)
        {
            try 
            {
                return f();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        public static async Task<Result<T>> CallAsync<T>(Func<Task<T>> f)
        {
            try
            {
                return await f();
            }
            catch
            {
                return new StatusCodeResult(500);
            }

        }
    }
}
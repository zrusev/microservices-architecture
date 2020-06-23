namespace StoreApi.Services.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class QueryResult
    {
        private static readonly QueryResult SuccessField = new QueryResult { Succeeded = true };

        private readonly List<IdentityError> errors = new List<IdentityError>();

        public static QueryResult Success 
        { 
            get 
            { 
                return SuccessField; 
            } 
       }
        
        public bool Succeeded { get; protected set; }

        public IEnumerable<IdentityError> Errors 
        { 
            get 
            { 
                return this.errors; 
            } 
        }

        public static QueryResult Failed(params IdentityError[] errors)
        {
            var result = new QueryResult { Succeeded = false };

            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }
    }

    public class QueryResult<T> : QueryResult
        where T: class
    {
        public T Result { get; protected set; }

        public static QueryResult<T> Suceeded(T result)
        {
            var queryResult = new QueryResult<T>
            {
                Succeeded = true,
                Result = result
            };

            return queryResult;
        }
    }
}

namespace StoreApi.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Helpers;

    public static class QueryResultExtensions
    {
        public static IActionResult ToActionResult(this QueryResult result)
        {
            //Template specialization is not allowed in C#
            if (result.GetType().IsGenericType)
            {
                return QueryResultExtensions.ToActionResult((dynamic)result);
            }

            if (result.Succeeded)
            {
                return new OkResult();
            }

            if (result.Errors != null)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result.Errors));
            }

            return new BadRequestResult();
        }

        public static IActionResult ToActionResult<T>(this QueryResult<T> result)
            where T : class
        {
            if (result.Succeeded)
            {
                if (result.Result == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(result.Result);
            }

            if (result.Errors != null)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result.Errors));
            }

            return new BadRequestResult();
        }
    }
}

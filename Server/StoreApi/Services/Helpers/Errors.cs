namespace StoreApi.Services.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Collections.Generic;

    public static class Errors
    {
        public static ModelStateDictionary AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var e in errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }

            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string code, string description)
        {
            var modelState = new ModelStateDictionary();

            modelState.TryAddModelError(code, description);
            return modelState;
        }
    }
}

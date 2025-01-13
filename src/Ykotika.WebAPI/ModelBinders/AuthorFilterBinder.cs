using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class AuthorFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new AuthorFilterQueryParams
            {
                Status = query["status"],
                Name = query["name"],
                Surname = query["surname"],
                ContactSocial = query["contact"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class CategoryFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new CategoryFilterQueryParams
            {
                IsPublished = query["isPub"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

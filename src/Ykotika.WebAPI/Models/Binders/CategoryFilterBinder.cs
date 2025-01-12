using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models.Binders
{
    public class CategoryFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new CategoryFilterDto
            {
                IsPublished = bool.TryParse(query["isPub"], out var desc) && desc
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

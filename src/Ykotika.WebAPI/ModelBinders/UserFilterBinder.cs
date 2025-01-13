using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.ModelBinders
{
    public class UserFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new UserFilterDto
            {
                IsPublished = bool.TryParse(query["isPub"], out var desc) && desc
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.ModelBinders
{
    public class ProductFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new ProductFilterDto
            {
                UserId = Guid.TryParse(query["user"], out var userId) ? userId : null,
                ProductTypeId = Guid.TryParse(query["type"], out var productTypeId) ? productTypeId : null,
                IsPublished = bool.TryParse(query["isPub"], out var desc) && desc
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

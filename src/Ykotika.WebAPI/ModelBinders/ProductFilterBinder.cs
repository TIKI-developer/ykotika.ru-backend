using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class ProductFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new ProductFilterQueryParams
            {
                IsPublished = query["isPub"],
                UserId = query["user"],
                ProductTypeId = query["productType"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

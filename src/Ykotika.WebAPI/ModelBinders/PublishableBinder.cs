using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.QueryParams;

namespace Ykotika.WebAPI.ModelBinders
{
    public class PublishableBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new PublishableQueryParams
            {
                IsPublished = query["isPub"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

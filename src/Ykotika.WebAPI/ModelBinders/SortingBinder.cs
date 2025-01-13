using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class SortingBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new SortingQueryParams
            {
                SortBy = query["sortBy"],
                IsDescending = bool.TryParse(query["desc"], out var desc) && desc
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

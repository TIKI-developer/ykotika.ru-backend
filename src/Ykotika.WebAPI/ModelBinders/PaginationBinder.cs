using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class PaginationBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var query = bindingContext.HttpContext.Request.Query;

            var model = new PaginationQueryParams
            {
                Page = int.TryParse(query["page"], out var page) ? page : null,
                PageSize = int.TryParse(query["pageSize"], out var pageSize) ? pageSize : null
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

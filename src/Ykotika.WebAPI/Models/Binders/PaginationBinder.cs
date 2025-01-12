using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models.Binders
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

            var model = new PaginationQueryDto
            {
                Page = int.TryParse(query["page"], out var page) ? page : (int?)null,
                PageSize = int.TryParse(query["pageSize"], out var pageSize) ? pageSize : (int?)null
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

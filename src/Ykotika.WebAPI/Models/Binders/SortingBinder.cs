using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ykotika.WebAPI.Models.Binders
{
    public class SortingBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new SortingQueryDto
            {
                SortBy = query["page"],
                IsDescending = bool.TryParse(query["desc"], out var desc) && desc
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

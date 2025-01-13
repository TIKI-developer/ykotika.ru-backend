using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.ModelBinders
{
    public class AgreementFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var query = bindingContext.HttpContext.Request.Query;

            var model = new AgreementFilterQueryParams
            {
                UserId = query["userId"],
                OfferId = query["offerId"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

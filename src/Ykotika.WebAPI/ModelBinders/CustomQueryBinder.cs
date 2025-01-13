using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ykotika.WebAPI.ModelBinders
{
    public class CustomQueryBinder<T> : IModelBinder where T : class, new()
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var query = bindingContext.HttpContext.Request.Query;
            var model = new T();


            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

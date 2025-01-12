using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ykotika.WebAPI.Models.Binders
{
    public class CustomQueryBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(PaginationBinder) ||
                context.Metadata.ModelType == typeof(SortingBinder) || 
                context.Metadata.ModelType == typeof(CategoryFilterBinder))
            {
                var binderType = typeof(CustomQueryBinder<>).MakeGenericType(context.Metadata.ModelType);
                return new BinderTypeModelBinder(binderType);
            }
            return null;
        }
    }
}

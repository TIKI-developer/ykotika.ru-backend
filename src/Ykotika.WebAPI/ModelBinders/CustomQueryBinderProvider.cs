using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.Application.Models;
using Ykotika.WebAPI.Models;
using Ykotika.WebAPI.Models.Binders;

namespace Ykotika.WebAPI.ModelBinders
{
    public class CustomQueryBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(SortingQueryParams))
            {
                return new SortingBinder();
            }
            if (context.Metadata.ModelType == typeof(PaginationQueryParams))
            {
                return new PaginationBinder();
            }
            if (context.Metadata.ModelType == typeof(CategoryFilterDto))
            {
                return new CategoryFilterBinder();
            }

            return null;
        }
    }
}

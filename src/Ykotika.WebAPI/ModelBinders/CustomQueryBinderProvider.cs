using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ykotika.WebAPI.Controllers;

namespace Ykotika.WebAPI.ModelBinders
{
    public class CustomQueryBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType switch
            {
                Type t when t == typeof(SortingQueryParams) => new SortingBinder(),
                Type t when t == typeof(PaginationQueryParams) => new PaginationBinder(),
                Type t when t == typeof(CategoryFilterQueryParams) => new CategoryFilterBinder(),
                Type t when t == typeof(ProductFilterQueryParams) => new ProductFilterBinder(),
                Type t when t == typeof(ProductTypeFilterQueryParams) => new ProductTypeFilterBinder(),
                Type t when t == typeof(OfferFilterQueryParams) => new OfferFilterBinder(),
                Type t when t == typeof(AgreementFilterQueryParams) => new AgreementFilterBinder(),
                Type t when t == typeof(AuthorFilterQueryParams) => new AuthorFilterBinder(),
                Type t when t == typeof(FormFilterQueryParams) => new FormFilterBinder(),
                Type t when t == typeof(UserFilterQueryParams) => new UserFilterBinder(),
                _ => null,
            };
        }
    }
}

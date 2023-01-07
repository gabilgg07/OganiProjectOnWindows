using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Ogani.WebUI.AppCode.Binders;

namespace Ogani.WebUI.AppCode.Providers
{

    public class BooleanBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(bool))
                return new BinderTypeModelBinder(typeof(BooleanBinder));
            if (context.Metadata.ModelType == typeof(bool?))
                return new BinderTypeModelBinder(typeof(BooleanBinder));
            return null;
        }
    }
}


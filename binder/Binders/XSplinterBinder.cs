using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace binder.Binders
{
    public class XSplinterBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            
           if (!bindingContext.ModelMetadata.IsComplexType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            };

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }
            var elementType = bindingContext.ModelType.GetTypeInfo();
            var values = value.Split("x");
            var x = new TestModel();
            x.Monkey1 = values[0];
            x.Monkey2 = values[1];
            bindingContext.Model = x;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EstebanJimenezEFP6API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        //Especificamos cual es el clave:valor dentro de appsettings que queremos usar como ApiKey
        private readonly string _apiKey = "AnswerApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Aca validamos que en el body(en tipo json) del request vaya la info de la ApiKey
            //si no va la info presentamos un mensaje de error indicando que falta ApiKey y que no se
            //puede consumir el recurso.

            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Llamada no contiene informacion de seguridad..."
                };
                return;
                //si no hay info de seguridad sale de la funcion y muestra este mensaje

            }

            //si viene info de seguridad falta validar que sea la correcta
            //para esto lo primero es extraer el valor de Progra6ApiKey dentro de appsettings.json
            //para comparar contra lo que viene en el request

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            //queda comparar que las apikey sean iguales
            if (!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey invalida..."
                };
                return;
            }
            await next();



        }
    }
}

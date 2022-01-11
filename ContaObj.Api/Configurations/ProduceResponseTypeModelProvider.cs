using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ContaObj.Api.Configurations;

public class ProduceResponseTypeModelProvider : IApplicationModelProvider
{
    public int Order => 3;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        foreach (ControllerModel controller in context.Result.Controllers)
        {
            foreach (ActionModel action in controller.Actions)
            {
                ConfigureIfIsHttpAttribute(action);
            }
        }
    }

    private static void ConfigureIfIsHttpAttribute(ActionModel action)
    {
        if (action.Attributes.Any(p => p is HttpMethodAttribute))
        {   
            var verb = action.Attributes.OfType<HttpMethodAttribute>().Select(x => x.HttpMethods).FirstOrDefault()?.FirstOrDefault();
            ConfigureActionFromVerb(action, verb);
        }
    }

    private static void ConfigureActionFromVerb(ActionModel action, string? verb)
    {
        if (verb != null)
        {
            ConfigureCommon(action);
            switch (verb)
            {
                case "GET":
                    ConfigureGet(action);
                    break;
                case "POST":
                    ConfigurePost(action);
                    break;
                case "PUT":
                    ConfigurePut(action);
                    break;
                case "DELETE":
                    ConfigureDelete(action);
                    break;
                default:
                    break;
            }
        }
    }

    private static void ConfigureCommon(ActionModel action)
    {
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status500InternalServerError));
    }

    private static void ConfigureDelete(ActionModel action)
    {
        action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent));
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status400BadRequest));
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status404NotFound));
        
    }

    private static void ConfigurePut(ActionModel action)
    {
        if (HasReturnType(action))
        {
            Type returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments()[0];
            action.Filters.Add(new ProducesResponseTypeAttribute(returnType, StatusCodes.Status200OK));
        }
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status400BadRequest));
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status404NotFound));
    }
    private static void ConfigurePost(ActionModel action)
    {
        if (HasReturnType(action))
        {
            Type returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments()[0];
            action.Filters.Add(new ProducesResponseTypeAttribute(returnType, StatusCodes.Status201Created));
        }
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status400BadRequest));
    }

    private static void ConfigureGet(ActionModel action)
    {
        if (HasReturnType(action))
        {
            Type returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments()[0];
            action.Filters.Add(new ProducesResponseTypeAttribute(returnType, StatusCodes.Status200OK));
        }
        action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status404NotFound));
    }

    private static bool HasReturnType(ActionModel action)
    {
        return action.ActionMethod.ReturnType.GenericTypeArguments.Any() && action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments().Any();
    }

}

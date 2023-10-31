using FluentValidation.Results;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GeekStore.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected readonly INotificationService _notificationService;

        protected MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        protected ActionResult CreateResponse(object result = null)
        {
            if (!HasErrors())
                return Ok(result);

            return BadRequest(GetErrors());
        }

        protected ActionResult CreateResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
                AddValidationError(error.ErrorMessage);

            return CreateResponse();
        }

        protected ActionResult CreateResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddValidationError(error.ErrorMessage);

            return CreateResponse();
        }

        protected bool HasErrors() => _notificationService.GetNotifications().Any();

        protected void AddValidationError(string erro) => _notificationService.AddNotification("error", erro);

        protected void ClearErrors() => _notificationService.Clear();

        protected IEnumerable<Error> GetErrors()
        {
            return _notificationService.GetNotifications().Select(n => new Error(n.Key, n.Message));
        }
    }
}

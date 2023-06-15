using GeekStore.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Product.WebAPI.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected readonly INotificationService _notificationService;

        protected MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //protected ActionResult<T> GetResponse<T>(T objeto = default)
        //{
        //    return (ActionResult<T>) GetResponseObject(objeto as object);
        //}

        protected IActionResult GetResponse(object objeto = default)
        {
            return (IActionResult) GetResponseObject(objeto);
        }

        private object GetResponseObject(object objeto = default)
        {
            var n = _notificationService.GetNotifications();
            if(n.Count() > 0)
            {
                return BadRequest(new { errors = n.ToList() });
            }
            else
            {
                return Ok(objeto);                
            }
        }
    }
}

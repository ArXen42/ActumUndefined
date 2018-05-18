using Microsoft.AspNetCore.Mvc.Filters;

namespace Undefined.Scoring.WebApp.Model
{
	public class AllowCrossSiteJsonAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
			base.OnActionExecuting(context);
		}
	}
}
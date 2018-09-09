using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RestWebApiVariasEntidades.Filters
{
    public class BasicAuhtenticationAttribute : AuthorizationFilterAttribute
    {
        private const string TOKEN = "admin";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authrizationHeader = actionContext.Request.Headers.Authorization;

            if(authrizationHeader == null || authrizationHeader.Scheme != "Bearer" || authrizationHeader.Parameter != TOKEN)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}
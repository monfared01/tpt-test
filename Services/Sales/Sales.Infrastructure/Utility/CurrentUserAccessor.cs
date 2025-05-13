using Microsoft.AspNetCore.Http;

namespace Sales.Infrastructure.Utility
{
    public class CurrentUserAccessor : Application.Common.Interface.ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                try
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null &&
                        httpContext.Request.Headers.TryGetValue("x-identity-userid", out var userIdHeader) &&
                        Guid.TryParse(userIdHeader, out var userId))
                    {
                        return userId;
                    }
                }
                catch
                {
                    // Log error if needed
                }

                return Guid.Empty;
            }
        }
    }
}
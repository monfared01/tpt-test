using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;


namespace MainTest.Framework.ApiResponse
{
    public class ApiResponse : IActionResult
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public List<string> Messages { get; set; }

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode;

            await new ObjectResult(this).ExecuteResultAsync(context);
        }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }



    public static class ApiResponseExtenstions
    {
        public static ApiResponse<T> ToApiResponse<T>(this FluentResults.Result<T> result)
        {
            if (result.IsSuccess)
            {
                var response = new ApiResponse<T>
                {
                    HasError = false,
                    Message = result.Successes.FirstOrDefault()?.Message ?? string.Empty,
                    Messages = result.Successes.Select(s => s.Message).ToList(),
                    Data = result.Value,
                    HttpStatusCode = HttpStatusCode.OK
                };
                return response;
            }
            else
            {
                var response = new ApiResponse<T>
                {
                    HasError = true,
                    Message = result.Errors.FirstOrDefault()?.Message ?? string.Empty,
                    Messages = result.Errors.Select(e => e.Message).ToList(),
                    HttpStatusCode = GetStatusCodeFromErrors(result.Errors)
                };
                return response;
            }
        }
        
        public static Microsoft.AspNetCore.Mvc.IActionResult ToApiResponse(this Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            var messages = context.ModelState
                .Where(m => m.Value?.Errors.Count > 0)
                .SelectMany(p => p.Value?.Errors.Select(e => e.ErrorMessage) ?? new List<string>()).ToList();

            var response = new ApiResponse()
            {
                HasError = true,
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "One or more validation errors occurred.",
                Messages = messages
            };
            return response;
        }
        private static HttpStatusCode GetStatusCodeFromErrors(List<FluentResults.IError> errors)
        {
            // You can map specific errors to specific status codes here.
            // For now, we'll default to 400 (Bad Request).
            return HttpStatusCode.BadRequest;
        }
    }
}

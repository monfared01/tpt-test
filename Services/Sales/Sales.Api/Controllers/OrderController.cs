using MainTest.Framework.ApiResponse;
using MainTest.Framework.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Command.Order;
using Sales.Application.Common.Interface;
using Sales.Application.Query.Order;
using Sales.Application.Response;
using System.Net;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserAccessor currentUserAccessor;

        public OrderController(IMediator mediator, ICurrentUserAccessor currentUserAccessor)
        {
            _mediator = mediator;
            this.currentUserAccessor = currentUserAccessor;
        }


        [HttpGet]
        [Route("GetAllOrders")]
        [ProducesResponseType(typeof(ApiResponse<ListResponse<OrderResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ListResponse<OrderResponse>>), (int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<ListResponse<OrderResponse>>> GetAllOrders([FromQuery] GetAllOrdersQuery request)
        {
            var result = await _mediator.Send(request);
            return result.ToApiResponse();
        }

        [HttpGet]
        [Route("GetOrderById")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<OrderResponse>> GetOrderById([FromQuery] GetOrderByIdQuery request)
        {
            var result = await _mediator.Send(request);
            return result.ToApiResponse();
        }

        [HttpPost]
        [Route("CreateOrder")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<Guid>> CreateOrder([FromBody] CreateOrderCommand request)
        {
            var result = await _mediator.Send(request);
            return result.ToApiResponse();
        }
    }
}

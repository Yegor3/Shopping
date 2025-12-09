using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models.Results;
using Shopping.API.Mapping;
using Shopping.API.Models.Requests;
using Shopping.Platform.Service.Interfaces;
using ServiceRequestModel = Shopping.Platform.Service.Models.Requests;
using ServiceResultModel = Shopping.Platform.Service.Models.Results;

namespace Shopping.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IOrderService _orderService;
    private readonly OrderMapper _mapper;

    public OrderController(ILogger<ProductController> logger, IOrderService orderService)
    {
        _logger = logger;
        _mapper = new OrderMapper();
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest createOrderRequest)
    {
        ServiceRequestModel.CreateOrderRequest serviceRequest = _mapper.Map(createOrderRequest);
        ServiceResultModel.CreateOrderResult createOrderResult = _orderService.CreateOrder(serviceRequest);
        return Ok(createOrderResult);
    }

    [HttpPut("Close")]
    public IActionResult Close([FromQuery] CloseOrderRequest updateOrderRequest)
    {
        ServiceRequestModel.CloseOrderRequest serviceRequest= _mapper.Map(updateOrderRequest);
        _orderService.CloseOrder(serviceRequest);
        return Ok();
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetList([FromQuery] GetOrderListRequest getOrderListRequest)
    {
        ServiceRequestModel.GetOrderListRequest serviceRequest= _mapper.Map(getOrderListRequest);
        ServiceResultModel.GetOrderListResult getOrderListResult = await _orderService.GetOrderList(serviceRequest);
        return Ok(getOrderListResult);
    }

    [HttpGet("DetailsById")]
    public IActionResult GetById([FromQuery] GetOrderDetailsRequest getOrderDetailsRequest)
    {
        ServiceRequestModel.GetOrderDetailsRequest serviceRequest= _mapper.Map(getOrderDetailsRequest);
        ServiceResultModel.GetOrderDetailsResult getOrderDetailsResult = _orderService.GetOrderDetails(serviceRequest);
        return Ok(getOrderDetailsResult);
    }
}

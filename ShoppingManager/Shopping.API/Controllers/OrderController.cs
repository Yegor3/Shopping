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
    /// <summary>
    /// Crie um novo pedido.
    /// </summary>
    /// <param name="createOrderRequest"></param>
    /// <response code="200">Pedido criado com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest createOrderRequest)
    {
        ServiceRequestModel.CreateOrderRequest serviceRequest = _mapper.Map(createOrderRequest);
        ServiceResultModel.CreateOrderResult createOrderResult = _orderService.CreateOrder(serviceRequest);
        return Ok(createOrderResult);
    }

    /// <summary>
    /// Feche um pedido criado anteriormente.
    /// </summary>
    /// <param name="updateOrderRequest"></param>
    /// <response code="200">Pedido fechado com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpPut("Close")]
    public IActionResult Close([FromQuery] CloseOrderRequest updateOrderRequest)
    {
        ServiceRequestModel.CloseOrderRequest serviceRequest= _mapper.Map(updateOrderRequest);
        _orderService.CloseOrder(serviceRequest);
        return Ok();
    }

    /// <summary>
    /// Liste os pedidos existentes na plataforma.
    /// </summary>
    /// <param name="getOrderListRequest"></param>
    /// <response code="200">Listagem efetuada com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpGet("List")]
    public async Task<IActionResult> GetList([FromQuery] GetOrderListRequest getOrderListRequest)
    {
        ServiceRequestModel.GetOrderListRequest serviceRequest= _mapper.Map(getOrderListRequest);
        ServiceResultModel.GetOrderListResult getOrderListResult = await _orderService.GetOrderList(serviceRequest);
        return Ok(getOrderListResult);
    }

    /// <summary>
    /// Busque os detalhes de um pedido, incluindo seus produtos.
    /// </summary>
    /// <param name="getOrderDetailsRequest"></param>
    /// <response code="200">Detalhamento efetuado com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpGet("DetailsById")]
    public IActionResult GetById([FromQuery] GetOrderDetailsRequest getOrderDetailsRequest)
    {
        ServiceRequestModel.GetOrderDetailsRequest serviceRequest= _mapper.Map(getOrderDetailsRequest);
        ServiceResultModel.GetOrderDetailsResult getOrderDetailsResult = _orderService.GetOrderDetails(serviceRequest);
        return Ok(getOrderDetailsResult);
    }
}

using Microsoft.AspNetCore.Mvc;
using Shopping.API.Mapping;
using Shopping.API.Models.Requests;
using Shopping.Platform.Service.Interfaces;
using Shopping.Platform.Service.Models.Results;
using ServiceRequestModel = Shopping.Platform.Service.Models.Requests;
using ServiceResultModel = Shopping.Platform.Service.Models.Results;

namespace Shopping.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _mapper = new ProductMapper();
        _productService = productService;
    }

    /// <summary>
    /// Cria uma lista de produtos atrelados a um pedido.
    /// </summary>
    /// <param name="createProductListRequest"></param>
    /// <response code="200">Produtos criados com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] CreateProductListRequest createProductListRequest)
    {
        ServiceRequestModel.CreateProductListRequest serviceRequest= _mapper.Map(createProductListRequest);
        CreateProductListResult result = _productService.CreateProductList(serviceRequest);
        return Ok(result);
    }

    /// <summary>
    /// Deleta uma lista de produtos atrelados a um pedido.
    /// </summary>
    /// <param name="removeProductRequest"></param>
    /// <response code="200">Produto deletado com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] RemoveProductRequest removeProductRequest)
    {
        ServiceRequestModel.RemoveProductRequest serviceRequest= _mapper.Map(removeProductRequest);
        _productService.RemoveProductList(serviceRequest);
        return Ok();
    }
}

using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[Route("api/products")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { Message = "Invalid product ID." });
        }

        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetProductCommand>(id);
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
        {
            return NotFound(new { Message = "Product not found." });
        }

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(response)
        });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GeAlltProductsPaginated(CancellationToken cancellationToken,
                                                          [FromQuery] int pageNumber = 1,
                                                          [FromQuery] int pageSize = 10,
                                                          [FromQuery] string? order = null,
                                                          [FromQuery] GetAllProductsQuery? filter = null)
    {
        var query = new PaginatedQuery<GetAllProductsQuery, GetAllProductsResult>(pageNumber, pageSize, order, filter);

        var result = await _mediator.Send(query, cancellationToken);

        return OkPaginated(await PaginatedList<GetAllProductsResult>.CreateAsync(result, pageNumber, pageSize, cancellationToken));
    }

    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _mediator.Send(new GetProductCategoriesQuery());
        return Ok(categories);

    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsByCategory(CancellationToken cancellationToken, string category,
                                                           [FromQuery] int _page = 1,
                                                           [FromQuery] int _size = 10,
                                                           [FromQuery] string? _order = null)
    {
        var query = new GetProductsByCategoryQuery(category, _page, _size, _order);
        var result = await _mediator.Send(query);

        return OkPaginated(await PaginatedList<GetProductResult>.CreateAsync(result, _page, _size, cancellationToken));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await _mediator.Send(request, cancellationToken);

        if (response == null)
        {
            return NotFound(new { Message = "Product not found." });
        }

        return Ok(new ApiResponseWithData<UpdateProductResult>
        {
            Success = true,
            Message = "Product upadate successfully",
            Data = _mapper.Map<UpdateProductResult>(response)
        });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {

        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound(new { Message = "Product not found." });
        }

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully"
        });
    }
}
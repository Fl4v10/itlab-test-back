using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeItLab.domain.Exceptions;
using testeItLab.domain.Interfaces.Services;
using testeItLab.domain.Models;
using testeItLab.domain.ViewModels;

namespace testeItLab.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _service = productService;
        }


        [HttpPost]
        [Route("search")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductViewModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult<IEnumerable<ProductViewModel>> SearchProducts([FromBody]string term)
        {
            var list = _service.GetList();

            if (!string.IsNullOrEmpty(term))
                list = list.Where(w => w.Name.Contains(term));

            return Ok(_mapper.Map<IEnumerable<ProductViewModel>>(list.AsEnumerable()));
        }

        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            var product = await _service.GetAsync(id);
            if (product == null)
                throw new NotFoundEntityException<ProductViewModel>();

            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ProductViewModel>> SaveProduct([FromBody] ProductViewModel model)
        {
            var entity = _mapper.Map<Product>(model);
            var product = await _service.CreateAsync(entity);

            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ProductViewModel>> UpdateProduct(int id, [FromBody] ProductViewModel model)
        {
            var entity = _mapper.Map<Product>(model);
            var product = await _service.UpdateAsync(id, entity);

            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(304)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}

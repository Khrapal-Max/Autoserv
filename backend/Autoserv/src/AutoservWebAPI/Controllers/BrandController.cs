using ApplicationCore.Interfaces;
using ApplicationCore.ModelsDTO.Brand;
using Microsoft.AspNetCore.Mvc;

namespace AutoservWebAPI.Controllers
{
    /// <summary>
    /// BrandController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BrandController : ControllerBase
    {
        private readonly IBaseService<NewBrandDTO, BrandDTO> _brandService;

        /// <summary>
        /// Get constuctor BrandController
        /// </summary>
        /// <param name="brandService"></param>
        public BrandController(IBaseService<NewBrandDTO, BrandDTO> brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns></returns>
        /// <example>GET: /Brand</example>
        [HttpGet("", Name = nameof(GetAllBrands))]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        /// <summary>
        /// Get brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>GET: /Brand/1 </example>
        [HttpGet("{id}", Name = nameof(GetBrandById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandDTO>> GetBrandById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            if (brand == null)
            {
                return BadRequest($"Does not exist in the darabase with id {id}");
            }

            return Ok(brand);
        }

        /// <summary>
        /// Create new brand
        /// </summary>
        /// <param name="newBrandDTO"></param>
        /// <returns></returns>
        /// <example>POST: /Brand/ </example>
        [HttpPost(Name = nameof(CreateBrand))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandDTO>> CreateBrand([FromBody] NewBrandDTO newBrandDTO)
        {
            var brand = await _brandService.CreateAsync(newBrandDTO);

            return Ok(brand);
        }

        /// <summary>
        /// Update brand
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        /// <example>PUT: /Brand/5 </example>
        [HttpPut("{id}", Name = nameof(UpdateBrand))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandDTO>> UpdateBrand(int id, [FromBody] BrandDTO brandDTO)
        {
            if (id != brandDTO.Id)
            {
                return BadRequest($"Error data in operation with brand name: {brandDTO.Name}");
            }

            var brand = await _brandService.UpdateAsync(brandDTO);

            return Ok(brand);
        }

        /// <summary>
        /// Delete brand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>DELETE: /Brand/5 </example>
        [HttpDelete("{id}", Name = nameof(DeleteBrand))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteAsync(id);
            return NoContent();
        }
    }
}


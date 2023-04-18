using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logger;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dtos;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepository _repo;
       // private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaAPIController(IVillaRepository repo, IMapper mapper)
        {
            _repo = repo;
           // _logger = logging;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            //_logger.Log("Getting All Villas", "");
            try
            {
                var villa = await _repo.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaDto>>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id <= 0)
                {
                    //_logger.Log("Get Villa Error whit Id " + id, "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _repo.GetAsync(x => x.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] VillaCreateDto villaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _repo.GetAsync(x => x.Name.ToLower() == villaDto.Name.ToLower()) != null)
                {
                   // ModelState.AddModelError("CustomError", "Villa already Exists!");
                   _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (villaDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = villaDto;
                    return BadRequest(_response);
                }
                //if (villaDto.Id > 0)
                //{
                //    _logger.Log("Get Villa Error whit Id " + villaDto.Id, "");
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                Villa villa = _mapper.Map<Villa>(villaDto);
                await _repo.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtAction("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    //_logger.Log("Get Villa Error whit Id " + id, "");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _repo.GetAsync(x => x.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _repo.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] VillaUpdateDto villaDto)
        {
            try
            {
                if (villaDto == null || id != villaDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Villa villa = _mapper.Map<Villa>(villaDto);
                _repo.UpdateAsync(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPatch]
        public async Task<IActionResult> UpdatePartial(int id, JsonPatchDocument<VillaUpdateDto> patch)
        {
            if (patch == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _repo.GetAsync(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
            patch.ApplyTo(villaDto, ModelState);
            Villa model = _mapper.Map<Villa>(villaDto);
            _repo.UpdateAsync(model);
            await _repo.SaveAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}

using AutoMapper;
using MagicVilla_Service.IService;
using MagicVilla_VillaAPI.Models.Dtos;
using MagicVilla_VillaAPI.Models;
using Newtonsoft.Json;
using MagicVilla_Dtos.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new VillaNumberCreateVM();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }
        //public async Task<IActionResult> UpdateVillaNumber(int villaNumberId)
        //{
        //    var response = await _villaService.GetAsync<APIResponse>(villaNumberId);
        //    if (response != null && response.IsSuccess)
        //    {
        //        VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
        //        return View(_mapper.Map<VillaNumberUpdateDto>(model));
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _villaService.UpdateAsync<APIResponse>(model);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(IndexVillaNumber));
        //        }
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteVillaNumber(int villaNumberId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (villaNumberId != 0)
        //        {
        //            await _villaService.DeleteAsync<APIResponse>(villaNumberId);
        //            return RedirectToAction(nameof(IndexVillaNumber));
        //        }
        //    }
        //    return View(villaNumberId);
        //}
    }
}

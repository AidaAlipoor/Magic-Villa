﻿using Business.Dtos.VillaNumberDtos;
using Business.Repositories.VillaNumberRepository;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static WebAPI.Controllers.VillaAPIController;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberRepository _repository;
        public VillaNumberAPIController(IVillaNumberRepository villumberRepository)
        {
            _repository = villumberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var viewModels = await _repository.GetAsync();

            if (viewModels.Any() is false)
            { 
                ModelState.AddModelError("Error Message", "There is no villaNumber yet!");
                return BadRequest(ModelState);
            }

            return Ok(new APIResponse { Result = viewModels });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var viewModel = await _repository.GetAsync(id);

            return Ok(new APIResponse { Result = viewModel });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VillaNumberDto dto)
        {
            _repository.Insert(dto);

            if (dto is null)
                return BadRequest(dto);

            await _repository.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VillaNumberDto dto)
        {
            await _repository.UpdateAsync(dto.Id, dto);

            await _repository.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            await _repository.SaveChangesAsync();

            return Ok(new APIResponse { IsSuccess = true });
        }
    }
}

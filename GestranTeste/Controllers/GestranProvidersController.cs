using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestranTeste.Data;
using GestranTeste.Models;
using System.Net;
using System.Collections;
using Microsoft.AspNetCore.Diagnostics;

namespace GestranTeste.Controllers
{

    public class GestranProvidersController : Controller
    {
        private readonly GestranTesteContext _context;

        public GestranProvidersController(GestranTesteContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProviders")]
        public async Task<IActionResult> GetProviders()
        {
            try
            {
                return Ok(await _context.GestranProvider.Where(x => !x.IsDeleted).Include(x => x.AddressesProvider.Where(y => !y.IsDeleted)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("GetProviders")]
        public async Task<IActionResult> GetProviders(ProviderFilter filter)
        {
            try
            {
                var providers = await _context.GestranProvider.Include(x => x.AddressesProvider.Where(y => !y.IsDeleted)).ToListAsync();

                providers = filter.Id == 0 ? providers : providers.Where(x => x.Id == filter.Id).ToList();
                providers = providers.Where(x => x.IsDeleted == filter.IsDeleted).ToList();
                providers = String.IsNullOrEmpty(filter.Name) ? providers : providers.Where(x => x.Name == filter.Name).ToList();
                providers = String.IsNullOrEmpty(filter.CNPJ) ? providers : providers.Where(x => x.CNPJ == filter.CNPJ).ToList();
                providers = String.IsNullOrEmpty(filter.PhoneNumber) ? providers : providers.Where(x => x.PhoneNumber == filter.PhoneNumber).ToList();
                providers = String.IsNullOrEmpty(filter.EmailAddress) ? providers : providers.Where(x => x.EmailAddress == filter.EmailAddress).ToList();


                providers = String.IsNullOrEmpty(filter.Country) ? providers : providers.Where(x => x.AddressesProvider.Exists(y => y.Country == filter.Country)).ToList();

                providers = String.IsNullOrEmpty(filter.City) ? providers : providers.Where(x => x.AddressesProvider.Exists(y => y.City == filter.City)).ToList();
                providers = String.IsNullOrEmpty(filter.State) ? providers : providers.Where(x => x.AddressesProvider.Exists(y => y.State == filter.State)).ToList();
                providers = String.IsNullOrEmpty(filter.StreetName) ? providers : providers.Where(x => x.AddressesProvider.Exists(y => y.StreetName == filter.StreetName)).ToList();
                providers = String.IsNullOrEmpty(filter.ZipCode) ? providers : providers.Where(x => x.AddressesProvider.Exists(y => y.ZipCode == filter.ZipCode)).ToList();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("AddProviders")]
        public async Task<IActionResult> AddProviders(AddProvider addProvider)
        {
            try
            {
                var provider = new GestranProvider()
                {
                    Name = addProvider.Name,
                    CNPJ = addProvider.CNPJ,
                    PhoneNumber = addProvider.PhoneNumber,
                    EmailAddress = addProvider.EmailAddress,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                await _context.GestranProvider.AddAsync(provider);
                await _context.SaveChangesAsync();

                return Ok(provider);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPut]
        [Route("UpdateProvider/{id:int}")]
        public async Task<IActionResult> UpdateProvider([FromRoute] int id, UpdateProvider addProvider)
        {

            var provider = _context.GestranProvider.Find(id);

            if (provider != null && !provider.IsDeleted)
            {
                try
                {

                    provider.Name = String.IsNullOrEmpty(addProvider.Name) ? provider.Name : addProvider.Name;
                    provider.CNPJ = String.IsNullOrEmpty(addProvider.CNPJ) ? provider.CNPJ : addProvider.CNPJ;
                    provider.PhoneNumber = String.IsNullOrEmpty(addProvider.PhoneNumber) ? provider.PhoneNumber : addProvider.PhoneNumber;
                    provider.EmailAddress = String.IsNullOrEmpty(addProvider.EmailAddress) ? provider.EmailAddress : addProvider.EmailAddress;
                    provider.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return Ok(provider);
                }
                catch (Exception ex)
                {
                    BadRequest(ex);
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteProvider/{id:int}")]
        public async Task<IActionResult> DeleteProvider([FromRoute] int id)
        {
            try
            {
                var provider = _context.GestranProvider.Find(id);

                if (provider != null && !provider.IsDeleted)
                {
                    provider.UpdatedAt = DateTime.Now;
                    provider.IsDeleted = !provider.IsDeleted;
                    await _context.SaveChangesAsync();
                    return Ok(provider);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPut]
        [Route("RecoveryProvider/{id:int}")]
        public async Task<IActionResult> RecoveryProvider([FromRoute] int id)
        {
            try
            {
                var provider = _context.GestranProvider.Find(id);

                if (provider != null && provider.IsDeleted)
                {
                    provider.UpdatedAt = DateTime.Now;
                    provider.IsDeleted = !provider.IsDeleted;
                    await _context.SaveChangesAsync();
                    return Ok(provider);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            

        }

        //[HttpDelete]
        //[Route("ClearProvider")]
        //public async Task<IActionResult> ClearProvider()
        //{
        //    _context.RemoveRange(await _context.GestranProvider.ToListAsync());
        //    await _context.SaveChangesAsync();
        //    return Ok();
            
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestranTeste.Data;
using GestranTeste.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace GestranTeste.Controllers
{
    public class AddressesController : Controller
    {
        private readonly GestranTesteContext _context;

        public AddressesController(GestranTesteContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAddress")]
        public async Task<IActionResult> Address()
        {
            try
            {
                return Ok(await _context.Address.Where(x => !x.IsDeleted).ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost]
        [Route("GetAddress")]
        public async Task<IActionResult> GetAddress(AddressFilter filter)
        {

            try
            {
                var address = await _context.Address.Where(x => x.IsDeleted == filter.IsDeleted).ToListAsync();

                address = filter.Id == 0 ? address : address.Where(x => x.Id == filter.Id).ToList();
                address = String.IsNullOrEmpty(filter.Country) ? address : address.Where(x => x.Country == filter.Country).ToList();
                address = String.IsNullOrEmpty(filter.City) ? address : address.Where(x => x.City == filter.City).ToList();
                address = String.IsNullOrEmpty(filter.State) ? address : address.Where(x => x.State == filter.State).ToList();
                address = String.IsNullOrEmpty(filter.StreetName) ? address : address.Where(x => x.StreetName == filter.StreetName).ToList();
                address = String.IsNullOrEmpty(filter.ZipCode) ? address : address.Where(x => x.ZipCode == filter.ZipCode).ToList();
                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("AddAddress")]
        public async Task<IActionResult> AddAddress(AddAddress addAddress)
        {
            try
            {
                var provider = _context.GestranProvider.Find(addAddress.ProviderId);

                if (provider != null)
                {
                    var address = new Address()
                    {
                        City = addAddress.City,
                        Country = addAddress.Country,
                        State = addAddress.State,
                        StreetName = addAddress.StreetName,
                        ZipCode = addAddress.ZipCode,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        Provider = provider
                    };
                    await _context.Address.AddAsync(address);
                    await _context.SaveChangesAsync();

                    return Ok(address);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("UpdateAddress/{id:int}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, UpdateAddress updateAddress)
        {
            try
            {
                var address = _context.Address.Find(id);

                if (address != null && !address.IsDeleted)
                {
                    address.City = String.IsNullOrEmpty(updateAddress.City) ? address.City : updateAddress.City;
                    address.Country = String.IsNullOrEmpty(updateAddress.Country) ? address.Country : updateAddress.Country;
                    address.State = String.IsNullOrEmpty(updateAddress.State) ? address.State : updateAddress.State;
                    address.StreetName = String.IsNullOrEmpty(updateAddress.StreetName) ? address.StreetName : updateAddress.StreetName;
                    address.ZipCode = String.IsNullOrEmpty(updateAddress.ZipCode) ? address.ZipCode : updateAddress.ZipCode;

                    await _context.SaveChangesAsync();
                    return Ok(address);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteAddress/{id:int}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            try
            {
                var address = _context.Address.Find(id);

                if (address != null && !address.IsDeleted)
                {
                    address.UpdatedAt = DateTime.Now;
                    address.IsDeleted = !address.IsDeleted;
                    await _context.SaveChangesAsync();
                    return Ok(address);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("RecoveryAddress/{id:int}")]
        public async Task<IActionResult> RecoveryAddress([FromRoute] int id)
        {
            try
            {
                var address = _context.Address.Find(id);

                if (address != null && address.IsDeleted)
                {
                    address.UpdatedAt = DateTime.Now;
                    address.IsDeleted = !address.IsDeleted;
                    await _context.SaveChangesAsync();
                    return Ok(address);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        //[HttpDelete]
        //[Route("ClearAddress")]
        //public async Task<IActionResult> ClearAddress()
        //{
        //    _context.RemoveRange(await _context.Address.ToListAsync());
        //    await _context.SaveChangesAsync();
        //    return Ok();

        //}
    }
}

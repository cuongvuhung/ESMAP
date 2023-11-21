using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CBM_API.Entities;
using CBM_API.Ultilities;
using Novell.Directory.Ldap;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Minio.DataModel;

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : ControllerBase
    {
        public ApplicationDbContext _context;
        public ProvinceController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string? name, int? pageNumber, int? pageSize)
        {
            if (name == null) { name = string.Empty; }
            try
            {
                var item = await PaginatedList<Province>.CreateAsync(
                                    (from rec in _context.Provinces
                                  where rec.DeletedAt == null
                                  && (name == string.Empty || rec.Name == name)
                                  select rec).Include(x=>x.Substations)                                      
                                      ,pageNumber ?? 1,pageSize ?? 10);
                return Ok(new 
                {
                    totalItem = item.TotalItems,
                    totalPage = item.TotalPages,
                    items = item
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddItem(Province item)
        {
            try
            {
                Province? itemExist = await (from rec in _context.Provinces
                                            where rec.Name == item.Name
                                       select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    await _context.Provinces.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateItem(Province item)
        {
            try
            {
                Province? itemExist = await (from rec in _context.Provinces
                                             where rec.Id == item.Id
                                       select rec).FirstOrDefaultAsync();
                if (itemExist == null)
                {
                    return BadRequest();
                }
                else
                {
                    itemExist.UpdatedAt = DateTime.Now;
                    itemExist.UpdatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    itemExist.Name = item.Name;
                    _context.SaveChanges();
                    return Ok(item);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                Province? item = await (from rec in _context.Provinces
                                        where rec.Id == id
                                      select rec).FirstOrDefaultAsync();
                item.DeletedAt = DateTime.Now;
                item.DeletedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                _context.SaveChanges();
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


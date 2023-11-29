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
    public class SubstationController : ControllerBase
    {
        public ApplicationDbContext _context;
        public SubstationController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string? name, int? pageNumber, int? pageSize,int? provinceId)
        {
            if (name == null) { name = string.Empty; }
            if (provinceId == null) { provinceId = 0; }
            
            try
            {
                var item = await PaginatedList<Substation>.CreateAsync(
                                    (from rec in _context.Substations
                                     where rec.DeletedAt == null
                                     && (name == string.Empty || rec.Name == name)
                                     && (provinceId == 0 || rec.ProvinceId == provinceId)
                                     select rec).Include(x => x.Province)
                                     
                                      , pageNumber ?? 1, pageSize ?? 10);
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
        public async Task<IActionResult> AddItem(Substation item)
        {
            try
            {
                Substation? itemExist = await (from rec in _context.Substations
                                               where rec.Name == item.Name
                                       select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Substations.Add(item);
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
        [HttpPut]
        public async Task<IActionResult> UpdateItem(Substation item)
        {
            try
            {
                Substation? itemExist = await (from rec in _context.Substations
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
                    itemExist.ProvinceId = item.ProvinceId;
                    itemExist.Code = item.Code;
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
                Substation? item = await (from rec in _context.Substations
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


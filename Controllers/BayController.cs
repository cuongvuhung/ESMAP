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
    public class BayController : ControllerBase
    {
        public ApplicationDbContext _context;
        public BayController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string? name, int? substationId,int? pageNumber, int? pageSize)
        {
            if (name == null) { name = string.Empty; }
            if (substationId == null) { substationId = 0; }
            try
            {
                var item = await PaginatedList<Bay>.CreateAsync((from rec in _context.Bays
                                                                 where rec.DeletedAt == null
                                                                 && (name == string.Empty || rec.Name == name)
                                                                 && (substationId == 0 || rec.SubstationId == substationId)
                                                                 select rec)
                                                                 .Include(x=>x.Substation),
                                                                 pageNumber ?? 1, pageSize ?? 10);
                return Ok(new
                { 
                    totalItems= item.TotalItems,
                    totalPages= item.TotalPages,
                    items = item
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "admin")]
        [HttpGet("All")]
        public async Task<IActionResult> SearchAllItem(int? substationId)
        {
            try
            {
                var item = await (from rec in _context.Bays
                                  where rec.DeletedAt == null
                                  && rec.SubstationId == substationId
                                  select rec).ToListAsync();
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddItem(Bay item)
        {
            try
            {
                Bay? itemExist = await (from rec in _context.Bays
                                        where rec.Name == item.Name
                                       select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Bays.Add(item);
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
        public async Task<IActionResult> UpdateItem(Bay item)
        {
            try
            {
                Bay? itemExist = await (from rec in _context.Bays
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
                    itemExist.SubstationId = item.SubstationId;
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
                Bay? item = await (from rec in _context.Bays
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


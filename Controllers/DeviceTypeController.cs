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
    public class DeviceTypeController : ControllerBase
    {
        public ApplicationDbContext _context;
        public DeviceTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(int? manufactureId, string? name, int? pageNumber, int? pageSize)
        {
            if (name == null) { name = string.Empty; }
            if (manufactureId == null) {  manufactureId = 0; }
            Manufacture? manufacture = await (from rec in _context.Manufactures 
                                             where rec.Id == manufactureId
                                             select rec).FirstOrDefaultAsync();
            //if (manufacture == null) { manufacture = new Manufacture(0, "name"); }
            try
            {
                var item = await PaginatedList<DeviceType>.CreateAsync((from rec in _context.DeviceTypes
                                                                        where rec.DeletedAt == null
                                                                        && (name == string.Empty || rec.Name == name)
                                                                        && (manufactureId == 0 || rec.Manufactures.Contains(manufacture))
                                                                        select rec),
                                                                        pageNumber ?? 1, pageSize ?? 10);
                return Ok(new
                {
                    totalItems = item.TotalItems,
                    totalPages = item.TotalPages,
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
        public async Task<IActionResult> SearchAllItem()
        {
            try
            {
                var item = await (from rec in _context.DeviceTypes
                                  where rec.DeletedAt == null
                                  select rec).ToListAsync();
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "admin")]
        /*[HttpPost]
        public async Task<IActionResult> AddItem(string manufactureIdString,DeviceType item)
        {
            try
            {
                DeviceType? itemExist = await (from rec in _context.DeviceTypes
                                               where rec.Name == item.Name
                                               select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    var manufactureIDs = manufactureIdString.Split(' ');
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.DeviceTypes.Add(item);
                    _context.SaveChanges();
                    foreach(var manufactureId in manufactureIDs) 
                    {
                        try
                        {
                            _context.ManufactureDeviceType.Add(new ManufactureDeviceType(0, item.Id, Convert.ToInt32(manufactureId)));
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }                   
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
        public async Task<IActionResult> UpdateItem(DeviceType item)
        {
            try
            {
                DeviceType? itemExist = await (from rec in _context.DeviceTypes
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
                DeviceType? item = await (from rec in _context.DeviceTypes
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
        }*/
    }
}


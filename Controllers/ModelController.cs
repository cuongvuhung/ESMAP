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
    public class ModelController : ControllerBase
    {
        public ApplicationDbContext _context;
        public ModelController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string? name, int? manuFactureId, int? deviceTypeId, int? pageNumber, int? pageSize)
        {
            if (name == null) { name = string.Empty; }
            if (manuFactureId == null) { manuFactureId = 0; }
            if (deviceTypeId == null) { deviceTypeId = 0; }

            try
            {
                Manufacture? manufacture = await (from rec in _context.Manufactures
                                                 where rec.Id == manuFactureId select rec).FirstOrDefaultAsync();
                var item = await PaginatedList<Model>.CreateAsync((from rec in _context.Models
                                                                   where rec.DeletedAt == null
                                                                   && (name == string.Empty || rec.Name == name)
                                                                   && (manuFactureId ==0 || rec.Manufactures.Contains(manufacture?? new Manufacture()))
                                                                   && (deviceTypeId == 0 || rec.DeviceTypeId == deviceTypeId)
                                                                   select rec)
                                                                   //.Include(x=>x.Manufactures)
                                                                   .Include(y=>y.DeviceType)
                                                                   ,pageNumber ?? 1, pageSize ?? 10);
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
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody]Model item, [FromQuery]string? manufactureIdString)
        {
            try
            {
                Model? itemExist = await (from rec in _context.Models
                                          where rec.Name == item.Name
                                          select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Models.Add(item);
                    _context.SaveChanges();
                    if (manufactureIdString != null)
                    {
                        var manufactureIds = manufactureIdString.Split(' ');
                        foreach (var manufactureId in manufactureIds)
                        {
                            try
                            {
                                _context.ManufactureModel.Add(new ManufactureModel(0, item.Id, Convert.ToInt32(manufactureId)));
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }
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
        public async Task<IActionResult> UpdateItem([FromBody] Model item, [FromQuery] string? manufactureIdString)
        {
            try
            {
                
                Model? itemExist = await (from rec in _context.Models
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
                    itemExist.DeviceTypeId = item.DeviceTypeId;
                    
                    if (manufactureIdString != null)
                    {
                        var manufactureIds = manufactureIdString.Split(' ');
                        List<ManufactureModel> manufactureModels = await (from rec in _context.ManufactureModel
                                                                          where rec.ModelId == item.Id
                                                                          select rec).ToListAsync();
                        foreach (var manufactureModel in manufactureModels)
                        {
                            _context.ManufactureModel.Remove(manufactureModel);
                            _context.SaveChanges();
                        }

                        foreach (var manufactureId in manufactureIds)
                        {
                            try
                            {
                                _context.ManufactureModel.Add(new ManufactureModel(0, item.Id, Convert.ToInt32(manufactureId)));
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }

                        }
                    }
                    _context.SaveChanges();
                    return Ok(itemExist);
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
                Model? item = await (from rec in _context.Models
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


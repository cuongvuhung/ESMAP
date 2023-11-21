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
    public class ManufactureController : ControllerBase
    {
        public ApplicationDbContext _context;
        public ManufactureController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string? name, int? pageNumber, int? pageSize)
        {
            if (name == null) { name  = string.Empty; }
            try
            {
                var item = await PaginatedList<Manufacture>.CreateAsync((from rec in _context.Manufactures
                                                                         where rec.DeletedAt == null
                                                                         && (name==string.Empty ||  rec.Name==name)                                                                         
                                                                         select rec)
                                                                         .Include(x=>x.DeviceTypes)
                                                                         .Include(y=>y.Models)
                                                                         .Include(z=>z.Devices),
                                                                         pageNumber?? 1,pageSize?? 10);
                                      

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
        public async Task<IActionResult> AddItem([FromQuery]string deviceTypeIdString,[FromBody]Manufacture item)
        {
            try
            {
                var deviceTypeIds = deviceTypeIdString.Split(' ');
                Manufacture? itemExist = await (from rec in _context.Manufactures
                                            where rec.Name == item.Name
                                       select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Manufactures.Add(item);
                    _context.SaveChanges();
                    foreach (var deviceTypeId in deviceTypeIds)
                    {
                        try
                        {
                            _context.ManufactureDeviceType.Add(new ManufactureDeviceType(0, Convert.ToInt32(deviceTypeId), item.Id));
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
        public async Task<IActionResult> UpdateItem([FromQuery] string? deviceTypeIdString, [FromQuery] string? modelIdString, [FromBody] Manufacture item)
        {
            try
            {
                Manufacture? itemExist = await (from rec in _context.Manufactures
                                                where rec.Id == item.Id
                                       select rec).FirstOrDefaultAsync();
                if (itemExist == null)
                {
                    return BadRequest();
                }
                else
                {
                    if (deviceTypeIdString != null)
                    {
                        var deviceTypeIds = deviceTypeIdString.Split(' ');
                        List<ManufactureDeviceType> manufactureDeviceTypes = await (from rec in _context.ManufactureDeviceType
                                                                                    where rec.ManufactureId == item.Id
                                                                          select rec).ToListAsync();
                        foreach (var manufactureDeviceType in manufactureDeviceTypes)
                        {
                            _context.ManufactureDeviceType.Remove(manufactureDeviceType);
                            _context.SaveChanges();
                        }

                        foreach (var deviceTypeId in deviceTypeIds)
                        {
                            try
                            {
                                _context.ManufactureDeviceType.Add(new ManufactureDeviceType(0, Convert.ToInt32(deviceTypeId),item.Id));
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }
                        }
                    }
                    
                    if (modelIdString != null)
                    {
                        var modelIds = modelIdString.Split(' ');
                        List<ManufactureModel> manufactureModels = await (from rec in _context.ManufactureModel
                                                                                    where rec.ManufactureId == item.Id
                                                                                    select rec).ToListAsync();
                        foreach (var manufactureModel in manufactureModels)
                        {
                            _context.ManufactureModel.Remove(manufactureModel);
                            _context.SaveChanges();
                        }

                        foreach (var modelId in modelIds)
                        {
                            try
                            {
                                _context.ManufactureModel.Add(new ManufactureModel(0, Convert.ToInt32(modelId), item.Id));
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }
                        }
                    }


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
                Manufacture? item = await (from rec in _context.Manufactures
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


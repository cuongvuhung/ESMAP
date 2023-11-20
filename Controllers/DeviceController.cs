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
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.X509;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        public ApplicationDbContext _context;
        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem()
        {
            try
            {
                var item = await (from rec in _context.Devices
                                  where rec.DeletedAt == null
                                  select rec)
                                  .Include(x => x.DeviceType)
                                  .Include(y => y.Manufacture)
                                      .ToListAsync();

                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddItem(Device item)
        {
            try
            {
                Device? itemExist = await (from rec in _context.Devices
                                           where rec.Name == item.Name
                                       select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    itemExist.CreatedAt = DateTime.Now;
                    itemExist.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Devices.Add(item);
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
        public async Task<IActionResult> UpdateItem(Device item)
        {
            try
            {
                Device? itemExist = await (from rec in _context.Devices
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
                    itemExist.BayId = item.BayId;
                    itemExist.Phases = item.Phases;
                    itemExist.VoltageLevel = item.VoltageLevel;
                    itemExist.OperName = item.OperName;
                    itemExist.DeviceTypeId = item.DeviceTypeId;
                    itemExist.ManuFactureId = item.ManuFactureId;
                    itemExist.ModelId = item.ModelId;
                    itemExist.YearManuFacture = item.YearManuFacture;
                    itemExist.Serial = item.Serial;
                    itemExist.OperDate = item.OperDate;
                    itemExist.CurrentRate = item.CurrentRate;
                    itemExist.CurrentIknRate = item.CurrentIknRate;
                    itemExist.CurrentCut = item.CurrentCut;
                    itemExist.VoltageRate = item.VoltageRate;
                    itemExist.VoltageUmcov = item.VoltageUmcov;
                    itemExist.PowerRate = item.PowerRate;
                    itemExist.Ratio = item.Ratio;
                    itemExist.Wiring = item.Wiring;
                    itemExist.Img = item.Img;
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
                Device? item = await (from rec in _context.Devices
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


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
using Minio;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MCAIRController : ControllerBase
    {
        public ApplicationDbContext _context;
        public MinioClient _minio;
        public MCAIRController(ApplicationDbContext context, MinioClient minio)
        {
            _context = context;
            _minio = minio;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(int? deviceID,int? pageNumber, int? pageSize)
        {
            try
            {
                if (deviceID == null) { deviceID = 0; }

                var item = await PaginatedList<MCAIR>.CreateAsync((from rec in _context.MCAIRs
                                                                   where rec.DeletedAt == null
                                                                 && (deviceID == 0 || rec.DeviceId == deviceID)
                                                                 orderby rec.DateTest descending
                                                                 select rec), pageNumber ?? 1, pageSize ?? 10);
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
        public async Task<IActionResult> AddItem(MCAIR item)
        {
            try
            {
                MCAIRr? mCAIRr = await (from rec in _context.MCAIRrs select rec).FirstOrDefaultAsync();

                MCAIR? itemExist = await (from rec in _context.MCAIRs
                                          where 
                                        rec.DateTest == item.DateTest
                                        && rec.Id == item.Id
                                            select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    item.ScoreLevel1 = Calc.MCAIRScore1(item, mCAIRr);
                    item.ScoreLevel1 = Calc.MCAIRScore23(item);
                    item.TotalScore = Calc.MCAIRScore1(item, mCAIRr) + Calc.MCAIRScore23(item);
                    _context.MCAIRs.Add(item);
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
        public async Task<IActionResult> UpdateItem(MCAIR item)
        {
            try
            {
                MCAIRr? mCAIRr = await (from rec in _context.MCAIRrs select rec).FirstOrDefaultAsync();
                MCAIR? itemExist = await (from rec in _context.MCAIRs
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
                    itemExist.Id = item.Id;
                    itemExist.DeviceId = item.DeviceId;
                    itemExist.DateTest = item.DateTest;
                    itemExist.Outside = item.Outside;
                    itemExist.Temperature = item.Temperature;
                    itemExist.Pd = item.Pd;
                    itemExist.PdOnline = item.PdOnline;
                    itemExist.HistoryMain = item.HistoryMain;
                    itemExist.NumberYearOper = item.NumberYearOper;
                    itemExist.RContact = item.RContact;
                    itemExist.RIsolate = item.RIsolate;
                    itemExist.TimeCut = item.TimeCut;
                    itemExist.RIsolateClose = item.RIsolateClose;
                    itemExist.RIsolateCut = item.RIsolateCut;
                    itemExist.RIsolateMotor = item.RIsolateMotor;
                    itemExist.Air = item.Air;
                    itemExist.HightVoltageAC = item.HightVoltageAC;
                    itemExist.PdOnlineAnalysis = item.PdOnlineAnalysis;
                    itemExist.CutOnline = item.CutOnline;
                    itemExist.SpeedFlowCut = item.SpeedFlowCut;
                    itemExist.ScoreLevel1 = Calc.MCAIRScore1(item, mCAIRr); 
                    itemExist.ScoreLevel23 = Calc.MCAIRScore23(item);
                    itemExist.TotalScore = Calc.MCAIRScore1(item, mCAIRr) + Calc.MCAIRScore23(item);
                    itemExist.Note = item.Note;
                    itemExist.ReviewETC = item.ReviewETC;
                    itemExist.Img = item.Img;
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
                MCAIR? item = await (from rec in _context.MCAIRs
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
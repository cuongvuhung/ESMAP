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
    public class MCHGISController : ControllerBase
    {
        public ApplicationDbContext _context;
        public MinioClient _minio;
        public MCHGISController(ApplicationDbContext context, MinioClient minio)
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

                var item = await PaginatedList<MCHGIS>.CreateAsync((from rec in _context.MCHGISs
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
        public async Task<IActionResult> AddItem(MCHGIS item)
        {
            try
            {
                MCHGISr? mCHGISr = await (from rec in _context.MCHGISrs select rec).FirstOrDefaultAsync();
                MCHGIS? itemExist = await (from rec in _context.MCHGISs
                                           where 
                                        rec.DateTest == item.DateTest
                                        && rec.Id == item.Id
                                            select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    item.ScoreLevel1 = Calc.MCHGISScore1(item,mCHGISr);
                    item.ScoreLevel23 = Calc.MCHGISScore23(item);
                    item.TotalScore = Calc.MCHGISScore1(item, mCHGISr) + Calc.MCHGISScore23(item);


                    _context.MCHGISs.Add(item);
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
        public async Task<IActionResult> UpdateItem(MCHGIS item)
        {
            try
            {
                MCHGISr? mCHGISr = await (from rec in _context.MCHGISrs select rec).FirstOrDefaultAsync();
                MCHGIS? itemExist = await (from rec in _context.MCHGISs
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
                    itemExist.SF6Lost = itemExist.SF6Lost;
                    itemExist.HistoryMain = item.HistoryMain;
                    itemExist.NumberYearOper = item.NumberYearOper;
                    itemExist.RContact = item.RContact;
                    itemExist.RIsolate = item.RIsolate;
                    itemExist.TimeCut = item.TimeCut;
                    itemExist.RIsolateClose = item.RIsolateClose;
                    itemExist.RIsolateCut = item.RIsolateCut;
                    itemExist.PurifyHuminitySF6 = item.PurifyHuminitySF6;
                    itemExist.SF6Decay = item.SF6Decay;
                    itemExist.LostSF6 = item.LostSF6;
                    itemExist.RIsolateMotor = item.RIsolateMotor;
                    itemExist.HightVoltageAC = item.HightVoltageAC;
                    itemExist.CutOnline = item.CutOnline;
                    itemExist.RMotor = item.RMotor;
                    itemExist.SpeedFlowCut = item.SpeedFlowCut;
                    itemExist.SF6Analysis = item.SF6Analysis;
                    itemExist.PdAnalysis = item.PdAnalysis;
                    itemExist.ScoreLevel1 = Calc.MCHGISScore1(item, mCHGISr);
                    itemExist.ScoreLevel23 = Calc.MCHGISScore23(item);
                    itemExist.TotalScore = Calc.MCHGISScore1(item, mCHGISr) + Calc.MCHGISScore23(item);
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
                MCHGIS? item = await (from rec in _context.MCHGISs
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
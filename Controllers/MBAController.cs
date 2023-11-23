﻿using System;
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
    public class MBAController : ControllerBase
    {
        static IConfiguration _config;
        public ApplicationDbContext _context;
        public MinioClient _minio;
        public MBAController(ApplicationDbContext context, MinioClient minio,IConfiguration config)
        {
            _context = context;
            _minio = minio;
            _config = config;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(int? deviceID,int? pageNumber, int? pageSize)
        {
            try
            {
                if (deviceID == null) { deviceID = 0; }

                var item = await PaginatedList<MBA>.CreateAsync((from rec in _context.MBAs
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
        public async Task<IActionResult> AddItem(MBA item)
        {
            try
            {
                MBAr? mBAr = await (from rec in _context.MBArs select rec).FirstOrDefaultAsync();
                MBA? itemExist = await (from rec in _context.MBAs
                                        where 
                                        rec.DateTest == item.DateTest
                                        && rec.Id == item.Id
                                            select rec).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    item.ScoreLevel1 = Calc.MBAScore1(item, mBAr);
                    item.ScoreLevel23 = Calc.MBAScore23(item);
                    item.TotalScore = Calc.MBAScore1(item, mBAr) + Calc.MBAScore23(item);
                    _context.MBAs.Add(item);
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
        public async Task<IActionResult> UpdateItem(MBA item)
        {
            try
            {
                MBAr? mBAr = await (from rec in _context.MBArs select rec).FirstOrDefaultAsync();
                MBA? itemExist = await (from rec in _context.MBAs
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
                    itemExist.Oil = item.Oil;
                    itemExist.Oilair = item.Oilair;
                    itemExist.OilOLTC = item.OilOLTC;
                    itemExist.AirOLTC = item.AirOLTC;
                    itemExist.OldPaperIsolate = item.OldPaperIsolate;
                    itemExist.MotorOLTC = item.MotorOLTC;
                    itemExist.NM = item.NM;
                    itemExist.HistoryOper = item.HistoryOper;
                    itemExist.OldOper = item.OldOper;
                    itemExist.RIsolate = item.RIsolate;
                    itemExist.MagnetIsolate = item.MagnetIsolate;
                    itemExist.CoilIsolate = item.CoilIsolate;
                    itemExist.Ratio = item.Ratio;
                    itemExist.TgLost = item.TgLost;
                    itemExist.TgLostCapa = item.TgLostCapa;
                    itemExist.LowVoltage = item.LowVoltage;
                    itemExist.RLostCurrent = item.RLostCurrent;
                    itemExist.HardCD = item.HardCD;
                    itemExist.VoltageHightRate = item.VoltageHightRate;
                    itemExist.CurrentVoltage = item.CurrentVoltage;
                    itemExist.SensorPD = item.SensorPD;
                    itemExist.ScoreLevel1 = Calc.MBAScore1(item, mBAr);
                    itemExist.ScoreLevel23 = Calc.MBAScore23(item);
                    itemExist.TotalScore = Calc.MBAScore1(item, mBAr) + Calc.MBAScore23(item);
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
        [HttpPut("image/{id}")]
        public async Task<IActionResult> UpdateImgItem(int id, IFormFile file)
        {
            try
            {
                if (file.ContentType.ToString().Contains("image"))
                {
                    MBA? itemExist = await (from rec in _context.MBAs
                                            where rec.Id == id
                                            select rec).FirstOrDefaultAsync();
                    if (itemExist == null)
                    {
                        return BadRequest("Device not found!");
                    }
                    else
                    {
                        string fileExtName = file.FileName.ToString().Split(".")[file.FileName.ToString().Split(".").Length - 1];
                        string fileNameHashed = $"{Crypto.Hash(file.FileName + DateTime.Now)}.{fileExtName}";
                        string path = $"mba/{id}/";
                        string bucket = "cbm";
                        //Xoa file cu
                        try
                        {
                            string? oldfilename = (itemExist.Img + "").Split(bucket + "/")[1];// make it item.ImageURL not null and get olfile name
                            var foundfile = await _minio.StatObjectAsync(new StatObjectArgs().WithBucket(bucket).WithObject(oldfilename));
                            if (foundfile != null)
                            {
                                RemoveObjectArgs removeObjectArgs = new RemoveObjectArgs().WithBucket(bucket).WithObject(oldfilename);
                                await _minio.RemoveObjectAsync(removeObjectArgs);
                            }
                        }
                        catch { }
                        //Tim tao moi bucket
                        bool found = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
                        if (!found) { await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket)); }
                        //Tao file moi
                        try
                        {
                            PutObjectArgs putObjectArgs = new PutObjectArgs()
                                .WithBucket(bucket)
                                .WithObject(path + fileNameHashed)
                                .WithStreamData(file.OpenReadStream())
                                .WithObjectSize(file.Length)
                                .WithContentType(file.ContentType);
                            await _minio.PutObjectAsync(putObjectArgs);
                            var publicUrl = $"{_config["Minio:Protocol"]}://{_config["Minio:Host"]}/{bucket}/{path}{fileNameHashed}";
                            itemExist.Img = publicUrl;
                            itemExist.UpdatedAt = DateTime.Now;
                            itemExist.UpdatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                            await _context.SaveChangesAsync();
                            return Ok(itemExist);
                        }
                        catch (Exception e)
                        {
                            return BadRequest(e.Message);
                        }
                    }
                }
                else
                {
                    return BadRequest("Wrong image file");
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
                MBA? item = await (from rec in _context.MBAs
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
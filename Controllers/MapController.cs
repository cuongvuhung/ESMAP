using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESMAP.Entities;
using ESMAP.Ultilities;
using Novell.Directory.Ldap;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Minio.DataModel;
using System.Linq;

namespace ESMAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        public ApplicationDbContext _context;
        public MapController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Map(int stationId)
        {
            var station = await (from rec in _context.Stations
                                 where rec.Id == stationId
                                 select rec)
                                 .Include(a => a.Lines).ThenInclude(b=>b.Wires)
                                 .Include(a => a.Devices).ThenInclude(b => b.Typ)
                                 .FirstAsync();
            if (station == null) { return NotFound("No station"); }
            if (station.Devices == null) { return NotFound("No device"); }
            if (station.Lines == null) { return NotFound("No line"); }

            var deviceIds = await (from rec in _context.Devices
                                   where rec.StationId == stationId
                                   select rec.Id).ToListAsync();

            var wires = await (from rec in _context.Wires
                               where deviceIds.Contains(rec.SourceDeviceId ?? 0)
                               || deviceIds.Contains(rec.TargetDeviceId ?? 0)
                               select rec)
                               .Include(a => a.Line)
                               .OrderByDescending(odb => odb.LineId)
                               .ThenBy(db => db.SourceDeviceId)
                               .ThenBy(b=>b.TargetDeviceId)
                               .ToListAsync();

            if (wires == null) { return NotFound("No wire"); }
            /*            foreach (var wire in wires)
                        {
                            wire.SourceDevice = station.Devices.Find(a => a.Id == wire.SourceDeviceId);
                            wire.TargetDevice = station.Devices.Find(a => a.Id == wire.TargetDeviceId);
                        }
            */
            /*Console.WriteLine($"------------------------------------------------------");
            Console.WriteLine($"Station: {station.Name}");
            Console.WriteLine($"Line list: {station.Lines.Count()} item");
            foreach (var line in station.Lines)
            {
                Console.WriteLine($"  {line.Name} -> {line.Wires?[0].TargetDevice?.Name ?? ""}");
            }
            Console.WriteLine($"Device list: {station.Devices.Count()} item");
            foreach (var device in station.Devices)
            {
                Console.WriteLine($" {device.Typ?.Name?? ""}: {device.Name} ");
            }

            Console.WriteLine($"Wire list: {wires.Count()} item");
            foreach (var wire in wires)
            {
                Console.WriteLine($"  {wire.Line?.Name ?? ""} = {wire.SourceDevice?.Name ?? ""} ---> {wire.TargetDevice?.Name ?? ""}");
            }*/
            string fileContent = "";
            fileContent += "<mxGraphModel dx=\"1260\" dy=\"680\" grid=\"1\" gridSize=\"10\" guides=\"1\" tooltips=\"1\" connect=\"1\" arrows=\"1\" fold=\"1\" page=\"1\" pageScale=\"1\" pageWidth=\"826\" pageHeight=\"1169\" background=\"none\" math=\"0\" shadow=\"0\">\r\n  " +
                           "<root>\r\n    " +
                           "    <mxCell id=\"0\" />\r\n    " +
                           "    <mxCell id=\"1\" parent=\"0\" />\r\n";
            string MBA = "";
            string MC = "";
            string CS = "";
            string wire = "";
            string TD = "";
            string TU = "";
            string TI = "";
            string TC = "";
            fileContent += "</root>\r\n" +
                           "</mxGraphModel>";
            System.IO.File.WriteAllText($"So do tram {station.Name}.xml", fileContent);

            return Ok(station);

        }
        //[Authorize(Roles = "admin")]
        [HttpGet("All")]
        public async Task<IActionResult> SearchAllItem(int? substationId)
        {
            return BadRequest();
        }


    }
}


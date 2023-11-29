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
using System.Drawing.Printing;

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        public ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(int? id, string? name, int? pageSize, int? pageNumber)
        {
            try
            {
                if (name == null) { name = " "; }
                var item = await PaginatedList<Department>.CreateAsync((from rec in _context.Departments
                                                                     where rec.DeletedAt == null
                                                                     && (name == " " || rec.Description.Contains(name))
                                                                     && (id == null || rec.Id == id)
                                                                     select rec)
                                                                    , pageNumber ?? 1, pageSize ?? 10);

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
        public async Task<IActionResult> AddItem(Department item)
        {
            try
            {
                Department? itemExist = await (from department in _context.Departments
                                               where department.Name == item.Name
                                       select department).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest(); }
                else
                {
                    itemExist.CreatedAt = DateTime.Now;
                    itemExist.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    _context.Departments.Add(item);
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
        public async Task<IActionResult> UpdateItem(Department item)
        {
            try
            {
                Department? itemExist = await (from department in _context.Departments
                                               where department.Id == item.Id
                                       select department).FirstOrDefaultAsync();
                if (itemExist == null)
                {
                    return BadRequest();
                }
                else
                {
                    itemExist.UpdatedAt = DateTime.Now;
                    itemExist.UpdatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    itemExist.Name = item.Name;
                    itemExist.Description = item.Description;
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
                Department? item = await (from department in _context.Departments
                                          where department.Id == id
                                      select department).FirstOrDefaultAsync();
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


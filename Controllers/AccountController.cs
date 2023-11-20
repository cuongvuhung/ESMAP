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

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SearchItem()
        {
            try
            {
                var item = await (from account in _context.Accounts
                                      where account.DeletedAt == null
                                      select account)
                                      .Include(r => r.Roles)
                                      .Include(d => d.Department)
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
        public async Task<IActionResult> AddItem(Account item)
        {
            if (item.DepartmentID == null) { item.DepartmentID = 20; }
            if (item.Password == null) { item.Password = Crypto.Hash("string"); }
            try
            {
                Account? itemExist = await (from account in _context.Accounts
                       where account.Name == item.Name
                       select account).FirstOrDefaultAsync();
                if (itemExist != null) { return BadRequest() ; }
                else
                {
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                    await _context.Accounts.AddAsync(item);
                    await _context.SaveChangesAsync();
                    await _context.AccountRoles.AddAsync(new AccountRole(item.Id, 2));
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> UpdateItem(Account item,string? oldpw)
        {
            try
            {
                if (item.DepartmentID == null) { item.DepartmentID = 20; }
                item.Password = Crypto.Hash(item.Password +"");
                Account? itemExist = await (from account in _context.Accounts
                                     where account.Id == item.Id
                                     select account).FirstOrDefaultAsync();
                if (itemExist == null)
                {
                    return BadRequest();
                }
                else
                {
                    if (oldpw != null)
                    {
                        if (Crypto.Hash(oldpw) == itemExist.Password) 
                        {
                            itemExist.UpdatedAt = DateTime.Now;
                            itemExist.UpdatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                            itemExist.Password = Crypto.Hash(item.Password);
                            itemExist.Name = item.Name;
                            itemExist.Avatar = item.Avatar;
                            itemExist.Email = item.Email;
                            itemExist.DepartmentID = item.DepartmentID;
                            await _context.SaveChangesAsync();
                            return Ok(itemExist);
                        }
                        else { return Unauthorized(); }
                    }
                    else
                    {
                        itemExist.UpdatedAt = DateTime.Now;
                        itemExist.UpdatedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                        itemExist.Name = item.Name;
                        itemExist.Avatar = item.Avatar;
                        itemExist.Email = item.Email;
                        itemExist.DepartmentID = item.DepartmentID;
                        await _context.SaveChangesAsync();
                        return Ok(itemExist);
                    }
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
                Account? itemExist = await (from account in _context.Accounts
                                     where account.Id == id
                                     select account).FirstOrDefaultAsync();
                itemExist.DeletedAt = DateTime.Now;
                itemExist.DeletedBy = User.Claims.FirstOrDefault(ac => ac.Type == "Name")?.Value;
                await _context.SaveChangesAsync();
                return Ok(itemExist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


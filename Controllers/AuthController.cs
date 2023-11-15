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

namespace CBM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest body)
        {
            var response = new LoginResponse();
            try
            {
                var hashedPwd = Crypto.Hash(body.Password);
                Account user = await (from account in _context.Accounts
                                      where (account.Name == body.Name) && (account.Password == hashedPwd)
                                      select account)
                                      .Include(r => r.Roles)
                                      .Include(d => d.Department)
                                      .FirstAsync();
                response.Status = "200";
                response.Message = "Local Author";
                response.Account = user;
                response.AccessToken = Crypto.GenerateJwt(user);
                return Ok(response);
            } 
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            
                  
        }

        [HttpPost("login-ldap")]
        public async Task<IActionResult> LoginLdap(LoginRequest body)
        {
            var response = new LoginResponse();
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json").Build();


            try
            {
                LdapConnection cn = new LdapConnection();
                cn.Connect(config["LDAP:Host"],LdapConnection.DefaultPort);
                cn.Bind(LdapConnection.LdapV3, $"npcetc\\{body.Name}", $"{body.Password}");
                if (cn.Bound)
                {

                    var searchResult = cn.Search(config["LDAP:Base"], LdapConnection.ScopeSub,
                                                        $"(samaccountname={body.Name})", null, false);
                    var entry = searchResult.First();
                    var hashedPwd = Crypto.Hash(body.Password);
                    Account user = new Account();
                   
                    try
                    {
                        user = await (from account in _context.Accounts
                                      where account.Name == body.Name
                                      select account)
                                      .Include(r=>r.Roles)
                                      .FirstAsync();
                        user.FullName = entry.GetAttribute("cn").StringValue;
                        user.Password = hashedPwd;
                        string departmentName = entry.GetAttribute("department").StringValue;
                        Console.WriteLine(departmentName);
                        try
                        {
                            Department dept = await (from department in _context.Departments
                                                     where department.Name == departmentName
                                                     select department).FirstAsync();
                            user.DepartmentID = dept.Id;
                        }
                        catch
                        {
                            user.DepartmentID = 19;
                        }
                        await _context.SaveChangesAsync();
                        response.Status = "200";
                        response.Message = "LDAP Author";
                        response.Account = user;
                        response.AccessToken = Crypto.GenerateJwt(user);                        
                    }
                    catch
                    {
                        user.Name = body.Name;
                        user.Email = entry.GetAttribute("mail").StringValue;                        
                        user.FullName = entry.GetAttribute("cn").StringValue;
                        user.Password = hashedPwd;
                        //user.IsRegular = false;                        
                        string departmentName = entry.GetAttribute("department").StringValue;
                        try
                        {
                            Department dept = await (from department in _context.Departments
                                                     where department.Name == departmentName
                                                     select department).FirstAsync();
                            user.DepartmentID = dept.Id;
                        }
                        catch
                        {
                            user.DepartmentID = 19;
                        }                        
                        await _context.Accounts.AddAsync(user);
                        await _context.SaveChangesAsync();
                        AccountRole ar = new AccountRole();
                        ar.AccountId = user.Id;
                        ar.RoleId = 2;
                        Console.WriteLine(user.Id);
                        await _context.AccountRoles.AddAsync(ar);
                        await _context.SaveChangesAsync();
                        user = await (from account in _context.Accounts
                                      where account.Name == body.Name
                                      select account)
                                      .Include(r => r.Roles)
                                      .FirstAsync();
                        response.Status = "200";
                        response.Message = "LDAP Author and Insert new";
                        response.Account = user;
                        response.AccessToken = Crypto.GenerateJwt(user);
                    }
                }
                else
                {
                    return Unauthorized();
                }                                
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*[HttpPost("register")]
        public async Task<IActionResult> Register(Account acc)
        {
            try
            {
                acc.Password = Crypto.Hash(acc.Password);
                await _context.AddAsync(acc);
                await _context.SaveChangesAsync();
                Console.WriteLine($"{acc.Name} created!");
                return Ok(acc);
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }            
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var acc = await (from account in _context.Accounts
                                 where account.Email == email
                                 select account).FirstAsync();
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress ("AdminQLBA","quanlybepan@hotmail.com"));
                message.To.Add(new MailboxAddress(acc.Name, acc.Email));
                message.Subject = "Quản lý bếp ăn - Quên mật khẩu";
                BodyBuilder bodyBuilder = new BodyBuilder()
                {
                    HtmlBody =  $"<h1>NPCETC - QLBA</h1>" +
                                $"<h2>Chương trình quản lý bếp ăn</h2>" +
                                $"<h2>Công ty TNHH MTV Thí nghiệm điện miền bắc</h2>" +
                                $"<p>-----------------------------------------</p>" +
                                $"<h3>Bạn đã gửi yêu cầu reset mk về mặc định</h3>" +
                                $"<h3>Nếu không phải bạn xin vui lòng bỏ qua email này.</h3>" +
                                $"<p>-----------------------------------------</p>" +
                                $"<h3>Click vào <a href=\"" +
                                $"https://{config["Host"]}:{config["Port"]}/Auth/reset-token/{acc.Email}/{Crypto.Hash(acc.Password)}\"" +
                                $"> ĐÂY</a> để reset mật khẩu về mặc định!</h3>"+
                                $"<h3>Mật khẩu được reset chỉ là mật khẩu trên phần mềm quản lý bếp ăn. để reset mật khẩu tài khoản domain xin vui lòng liên hệ ban quản trị</h3>",
                    TextBody =  $"Url để reset mật khẩu về mặc định: " +
                                $"https://{config["Host"]}:{config["Port"]}/Auth/reset-token/{acc.Email}/{Crypto.Hash(acc.Password)}"
                               
                };
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    try
                    {
                        client.Connect(config["MailServer:Host"], 
                            Convert.ToInt32(config["MailServer:Port"]), 
                            SecureSocketOptions.StartTls);                        
                        client.Authenticate(config["MailServer:User"], 
                            config["MailServer:Password"]);
                        await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                    catch (Exception ex) 
                    {
                        return BadRequest(ex.Message);
                    }
                }
                Console.WriteLine($"A mail was send to {message.To}!");
                return Ok($"A mail was send to {message.To}!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("reset-password/{email}/{token}")]
        public async Task<IActionResult> UpdateNewPassword(string email,string token)
        {
            try
            {
                var acc = await (from account in _context.Accounts
                                 where account.Email == email
                                 select account).FirstAsync();
                if (token == Crypto.Hash(acc.Password))
                {
                    acc.Password = Crypto.Hash("Abcd@12345");
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"{acc.Email} reset password");
                    return Ok("NPCETC-QLBA: Tài khoản của bạn đã được reset về mật khẩu mặc định. Đây chỉ là mật khẩu của phần mềm quản lý bếp ăn.");
                } else { return BadRequest(); }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("new-password")]
        public async Task<IActionResult> UpdateNewPassword(NewPasswordRequest body)
        {
            try
            {
                var acc = await(from account in _context.Accounts
                                where account.Email == body.Email
                                select account).FirstAsync();
                acc.Password = Crypto.Hash(body.Password);
                await _context.SaveChangesAsync();
                return Ok(acc);
            }
            catch
            {
                return BadRequest();
            }
        }*/
    }
}


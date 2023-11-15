using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBM_API.Entities
{
    [Table("AccountRole")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public AccountRole(int accountid, int roleid)
        {
            AccountId = accountid;
            RoleId = roleid;
        }

        public AccountRole()
        {          
        }

    }
}

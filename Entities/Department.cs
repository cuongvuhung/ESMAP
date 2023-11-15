using Novell.Directory.Ldap;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Account>? Accounts { get; } 
        public Department(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Department()
        {
        }
    }
}

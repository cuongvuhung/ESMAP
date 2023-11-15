using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }               
        public string Name { get; set; }
        public List<Account>? Accounts { get; } = new();
        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Role()
        {
        }

    }
}

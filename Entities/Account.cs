using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string? Avatar { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? DepartmentID { get; set; }
        public Department? Department { get; set; }
        public List<Role>? Roles { get; } = new();
        
        public string? Telephone { get; set; }
        
        public Account(int id, string name, int departmentID, string password)//,  DateTime? deletedAt)
        {
            Id = id;
            Name = name;
            DepartmentID = departmentID;
            Password = password;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public Account()
        {
        }
        // JsonIgnore 
        [JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }
    }
}

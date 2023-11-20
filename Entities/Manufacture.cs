using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Manufactures")]
    public class Manufacture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        

        public List<DeviceType> DeviceTypes { get; set; }
        public List<Model> Models { get; set; }
        public List<Device> Devices { get; set; }


        public Manufacture(int id, string name)//,  DateTime? deletedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public Manufacture()
        {
        }
        // JsonIgnore 
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

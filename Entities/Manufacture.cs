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

        [JsonIgnore] public List<DeviceType>? DeviceTypes { get; set; }
        [JsonIgnore] public List<Model>? Models { get; set; }
        [JsonIgnore] public List<Device>? Devices { get; set; }


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
        [JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }
    }
}

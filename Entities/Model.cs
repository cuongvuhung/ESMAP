using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Models")]
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        
        public int? DeviceTypeId { get; set; }
        public DeviceType? DeviceType { get; set; }
        public List<Manufacture>? Manufactures { get; set; }
        public List<Device>? Devices { get; set; }

        public Model(int id, string name, int deviceTypeId)//,  DateTime? deletedAt)
        {
            Id = id;
            Name = name;
            DeviceTypeId = deviceTypeId;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public Model()
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

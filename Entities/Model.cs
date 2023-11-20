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
        public int DeviceTypeId { get; set; }

        public List<Manufacture> Manufactures { get; set; }
        public List<Device> Devices { get; set; }

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
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

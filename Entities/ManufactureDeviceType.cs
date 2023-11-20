using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("ManufactureDeviceType")]
    public class ManufactureDeviceType
    {
        [Key]
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }
        public int ManufactureId { get; set; }

        public ManufactureDeviceType(int id, int deviceTypeId, int manufactureId)
        {
            Id = id;
            DeviceTypeId = deviceTypeId;
            ManufactureId = manufactureId;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public ManufactureDeviceType()
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

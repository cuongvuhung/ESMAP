using Quartz.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BayId { get; set; }
        public string Phases { get; set; }
        public string VoltageLevel { get; set; }
        public string OperName { get; set; }
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public int ManuFactureId { get; set; }
        public Manufacture Manufacture { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int YearManuFacture { get; set; }
        public string Serial { get; set; } 
        public DateTime OperDate { get; set; }
        public string CurrentRate { get; set; }
        public string CurrentIknRate { get; set; }
        public string CurrentCut { get; set; }
        public string VoltageRate { get; set; }
        public string VoltageUmcov { get; set; }
        public string PowerRate { get; set; }
        public string Ratio { get; set; }
        public string Wiring { get; set; }
        public string Img { get; set; }

        
        

        public Device(int id, string name)//,  DateTime? deletedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public Device()
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

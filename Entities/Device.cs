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
        public int ManufactureId { get; set; }
        public int ModelId { get; set; }
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
        public string? Img { get; set; }

        //////////////////////////////////////
        public Bay? Bay { get; set; }
        public Model? Model { get; set; }
        public DeviceType? DeviceType { get; set; }
        public Manufacture? Manufacture { get; set; }
        //////////////////////////////////////
        public List<BDA>? BDAs { get; set; }
        public List<BDD>? BDDs { get; set; }
        public List<CL>? CLs { get; set; }
        public List<CSV>? CSVs { get; set; }
        public List<DCL>? DCLs { get; set; }
        public List<MBA>? MBAs { get; set; }
        public List<MCAIR>? MCAIRs { get; set; }
        public List<MCGIS>? MCGISs { get; set; }
        public List<MCHGIS>? MCHGISs { get; set; }
        public List<MCSF6>? MCSF6s { get; set; }

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
        [JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }
    }
}

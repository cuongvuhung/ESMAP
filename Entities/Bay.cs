using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("Bays")]
    public class Bay
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubstationId { get; set; }
        public Substation? Substation { get; set; }
        [JsonIgnore] public List<Device>? Devices { get; set; }
        

        public Bay(int id, string name)//,  DateTime? deletedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public Bay()
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

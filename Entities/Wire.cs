using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Subjects;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ESMAP.Entities
{
    [Table("Wires")]
    public class Wire
    {
        [Key]
        public int Id { get; set; }
        public int? SourceDeviceId { get; set; }
        public Device? SourceDevice { get; set; }
        public int? LineId { get; set; }
        public Line? Line { get; set; }
        public int? TargetDeviceId { get; set; }
        public Device? TargetDevice { get; set; }

        public Wire( int? sourceDeviceId, int? lineId, int? targetDeviceId)
        {
            SourceDeviceId = sourceDeviceId;
            LineId = lineId;
            TargetDeviceId = targetDeviceId;
        }

        public Wire()
        {
        }
        // JsonIgnore 
        /*[JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }*/
    }
}

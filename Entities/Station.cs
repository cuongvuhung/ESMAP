using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Subjects;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ESMAP.Entities
{
    [Table("StationLine")]
    public class StationLine
    {
        [Key]
        public int Id { get; set; }
        public int StationId { get; set; }
        public int LineId { get; set; }

        public StationLine(int stationId, int lineId)
        {
            StationId = stationId;
            LineId = lineId;
        }

        public StationLine()
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

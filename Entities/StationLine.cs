using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Subjects;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ESMAP.Entities
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public Station(string? name)
        {
            Name = name;
        }
        public List<Line>? Lines { get; }
        [JsonIgnore] public List<Device>? Devices { get; set; }

        public Station()
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

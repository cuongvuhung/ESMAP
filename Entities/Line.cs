using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Subjects;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ESMAP.Entities
{
    [Table("Lines")]
    public class Line
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Station>? Stations { get;}
        public List<Wire>? Wires { get; }
        public Line (string? name)
        {
            Name = name;
        }
        public Line()
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

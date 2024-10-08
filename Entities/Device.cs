﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Subjects;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ESMAP.Entities
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StationId { get; set; } 
        public Station? Station { get; }
        public int? TypId { get; set; } 
        public Typ? Typ { get; }


        public Device(string? name, int? stationId, int? typId)
        {
            Name = name;
            StationId = stationId;
            TypId = typId;
        }
        public Device()
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

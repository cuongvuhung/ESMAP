using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCSF6rs")]
    public class MCSF6r
    {
        [Key]
        public int Id { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? SF6Lost { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }

        public MCSF6r(int id, float? outside, float? temperature, float? pd, float? sF6Lost, float? historyMain, float? numberYearOper)
        {
            Id = id;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            SF6Lost = sF6Lost;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";
        }
        
        public MCSF6r()
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

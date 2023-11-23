using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCAIRrs")]
    public class MCAIRr
    {
        [Key]
        public int Id { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? PdTve { get; set; }
        public float? PdOnline { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }

        public MCAIRr(int id, float? outside, float? temperature, float? pd, float? pdTve, float? historyMain, float? numberYearOper)
        {
            Id = id;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdTve = pdTve;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";
        }
        public MCAIRr()
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

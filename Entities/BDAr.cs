using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("BDArs")]
    public class BDAr
    {
        [Key]
        public int Id { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? SecondaryVoltage { get; set; }
        public double? HistoryMain { get; set; }

        public BDAr(int id, double? outside, double? temperature, double? pd, double? secondaryVoltage, double? historyMain)
        {
            Id = id;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            SecondaryVoltage = secondaryVoltage;
            HistoryMain = historyMain;
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";            
        }
        public BDAr()
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

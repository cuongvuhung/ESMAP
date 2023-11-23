using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("CSVrs")]
    public class CSVr
    {
        [Key]
        public int Id { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? PdOnline { get; set; }
        public double? HistoryMain { get; set; }
        public double? NumberYearOper { get; set; }

        public CSVr(int id, double? outside, double? temperature, double? pd, double? pdOnline, double? historyMain, double? numberYearOper)
        {
            Id = id;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdOnline = pdOnline;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";
        }

        
        

                    
        public CSVr()
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

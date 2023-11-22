using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("CSVs")]
    public class CSV
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        
        public DateTime DateTest { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? PdOnline { get; set; }
        public double? HistoryMain { get; set; }
        public double? NumberYearOper { get; set; }
        public double? RIsolate { get; set; }
        public double? PdByIndeSource { get; set; }
        public double? PowerK { get; set; }
        public double? PdAnalysis { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public CSV(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? pdOnline, double? historyMain, double? numberYearOper, double? rIsolate, double? pdByIndeSource, double? powerK, double? pdAnalysis, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img, Device? device)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdOnline = pdOnline;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            RIsolate = rIsolate;
            PdByIndeSource = pdByIndeSource;
            PowerK = powerK;
            PdAnalysis = pdAnalysis;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        
        

                    
        public CSV()
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

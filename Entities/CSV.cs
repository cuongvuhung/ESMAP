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
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? PdOnline { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }
        public float? RIsolate { get; set; }
        public float? PdByIndeSource { get; set; }
        public float? PowerK { get; set; }
        public float? PdAnalysis { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public CSV(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? pdOnline, float? historyMain, float? numberYearOper, float? rIsolate, float? pdByIndeSource, float? powerK, float? pdAnalysis, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img, Device? device)
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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("CLs")]
    public class CL
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        
        public DateTime DateTest { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? HfctAndTev { get; set; }
        public double? HistoryMain { get; set; }
        public double? NumberYearOper { get; set; }
        public double? RIsolate { get; set; }
        public double? HightVoltageRes { get; set; }
        public double? HightVoltageResCase { get; set; }
        public double? PdDeep { get; set; }
        public double? TgLost { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public CL(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? hfctAndTev, double? historyMain, double? numberYearOper, double? rIsolate, double? hightVoltageRes, double? hightVoltageResCase, double? pdDeep, double? tgLost, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img, Device? device)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            HfctAndTev = hfctAndTev;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            RIsolate = rIsolate;
            HightVoltageRes = hightVoltageRes;
            HightVoltageResCase = hightVoltageResCase;
            PdDeep = pdDeep;
            TgLost = tgLost;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

                    
        public CL()
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

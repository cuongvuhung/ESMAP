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
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? HfctAndTev { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }
        public float? RIsolate { get; set; }
        public float? HightVoltageRes { get; set; }
        public float? HightVoltageResCase { get; set; }
        public float? PdDeep { get; set; }
        public float? TgLost { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public CL(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? hfctAndTev, float? historyMain, float? numberYearOper, float? rIsolate, float? hightVoltageRes, float? hightVoltageResCase, float? pdDeep, float? tgLost, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img, Device? device)
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

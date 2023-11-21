using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("BDDs")]
    public class BDD
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        
        public DateTime DateTest { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? HistoryMain { get; set; }
        public float? RIsolate { get; set; }
        public float? Ratio { get; set; }
        public float? ROneWayCoil { get; set; }
        public float? TgLost { get; set; }
        public float? VoltageHightRate { get; set; }
        public float? RatioCurrentSource { get; set; }
        public float? Chemical { get; set; }
        public float? OilIsolate { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public BDD(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? historyMain, float? rIsolate, float? ratio, float? rOneWayCoil, float? tgLost, float? voltageHightRate, float? ratioCurrentSource, float? chemical, float? oilIsolate, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img, Device? device)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            HistoryMain = historyMain;
            RIsolate = rIsolate;
            Ratio = ratio;
            ROneWayCoil = rOneWayCoil;
            TgLost = tgLost;
            VoltageHightRate = voltageHightRate;
            RatioCurrentSource = ratioCurrentSource;
            Chemical = chemical;
            OilIsolate = oilIsolate;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }
        public BDD()
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

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
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? HistoryMain { get; set; }
        public double? RIsolate { get; set; }
        public double? Ratio { get; set; }
        public double? ROneWayCoil { get; set; }
        public double? TgLost { get; set; }
        public double? VoltageHightRate { get; set; }
        public double? RatioCurrentSource { get; set; }
        public double? Chemical { get; set; }
        public double? OilIsolate { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public BDD(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? historyMain, double? rIsolate, double? ratio, double? rOneWayCoil, double? tgLost, double? voltageHightRate, double? ratioCurrentSource, double? chemical, double? oilIsolate, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img, Device? device)
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

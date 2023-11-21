using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("BDAs")]
    public class BDA
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        
        public DateTime DateTest { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? SecondaryVoltage { get; set; }
        public float? HistoryMain { get; set; }
        public float? RIsolate { get; set; }
        public float? Ratio { get; set; }
        public float? ROneWayCoil { get; set; }
        public float? TgLost { get; set; }
        public float? VoltageHightRate { get; set; }
        public float? RatioVoltageHight { get; set; }
        public float? OilIsolate { get; set; }
        public float? SensorWireLoop { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public BDA(int id, int deviceId, Device device, DateTime dateTest, float? outside, float? temperature, float? pd, float? secondaryVoltage, float? historyMain, float? rIsolate, float? ratio, float? rOneWayCoil, float? tgLost, float? voltageHightRate, float? ratioVoltageHight, float? oilIsolate, float? sensorWireLoop, float? scoreLevel1, float? scoreLevel23, string? note, string? reviewETC, string? img)
        {
            Id = id;
            DeviceId = deviceId;
            Device = device;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            SecondaryVoltage = secondaryVoltage;
            HistoryMain = historyMain;
            RIsolate = rIsolate;
            Ratio = ratio;
            ROneWayCoil = rOneWayCoil;
            TgLost = tgLost;
            VoltageHightRate = voltageHightRate;
            RatioVoltageHight = ratioVoltageHight;
            OilIsolate = oilIsolate;
            SensorWireLoop = sensorWireLoop;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";            
        }
        public BDA()
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

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
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? SecondaryVoltage { get; set; }
        public double? HistoryMain { get; set; }
        public double? RIsolate { get; set; }
        public double? Ratio { get; set; }
        public double? ROneWayCoil { get; set; }
        public double? TgLost { get; set; }
        public double? VoltageHightRate { get; set; }
        public double? RatioVoltageHight { get; set; }
        public double? OilIsolate { get; set; }
        public double? SensorWireLoop { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public BDA(int id, int deviceId, Device device, DateTime dateTest, double? outside, double? temperature, double? pd, double? secondaryVoltage, double? historyMain, double? rIsolate, double? ratio, double? rOneWayCoil, double? tgLost, double? voltageHightRate, double? ratioVoltageHight, double? oilIsolate, double? sensorWireLoop, double? scoreLevel1, double? scoreLevel23, string? note, string? reviewETC, string? img)
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

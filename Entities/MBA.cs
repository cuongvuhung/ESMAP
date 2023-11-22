using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MBAs")]
    public class MBA
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateTest { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? PdOnline { get; set; }
        public double? Oil { get; set; }
        public double? Oilair { get; set; }
        public double? OilOLTC { get; set; }
        public double? AirOLTC { get; set; }
        public double? OldPaperIsolate { get; set; }
        public double? MotorOLTC { get; set; }
        public double? NM { get; set; }
        public double? HistoryOper { get; set; }
        public double? OldOper { get; set; }
        public double? RIsolate { get; set; }
        public double? MagnetIsolate { get; set; }
        public double? CoilIsolate { get; set; }
        public double? Ratio { get; set; }
        public double? TgLost { get; set; }
        public double? TgLostCapa { get; set; }
        public double? LowVoltage { get; set; }
        public double? OLTC { get; set; }
        public double? Frequency { get; set; }
        public double? RLostCurrent { get; set; }
        public double? HardCD { get; set; }
        public double? VoltageHightRate { get; set; }
        public double? CurrentVoltage { get; set; }
        public double? SensorPD { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public MBA(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? pdOnline, double? oil, double? oilair, double? oilOLTC, double? airOLTC, double? oldPaperIsolate, double? motorOLTC, double? nM, double? historyOper, double? oldOper, double? rIsolate, double? magnetIsolate, double? coilIsolate, double? ratio, double? tgLost, double? tgLostCapa, double? lowVoltage, double? oLTC, double? frequency, double? rLostCurrent, double? hardCD, double? voltageHightRate, double? currentVoltage, double? sensorPD, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img, Device? device)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdOnline = pdOnline;
            Oil = oil;
            Oilair = oilair;
            OilOLTC = oilOLTC;
            AirOLTC = airOLTC;
            OldPaperIsolate = oldPaperIsolate;
            MotorOLTC = motorOLTC;
            NM = nM;
            HistoryOper = historyOper;
            OldOper = oldOper;
            RIsolate = rIsolate;
            MagnetIsolate = magnetIsolate;
            CoilIsolate = coilIsolate;
            Ratio = ratio;
            TgLost = tgLost;
            TgLostCapa = tgLostCapa;
            LowVoltage = lowVoltage;
            OLTC = oLTC;
            Frequency = frequency;
            RLostCurrent = rLostCurrent;
            HardCD = hardCD;
            VoltageHightRate = voltageHightRate;
            CurrentVoltage = currentVoltage;
            SensorPD = sensorPD;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        
        
        public MBA()
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

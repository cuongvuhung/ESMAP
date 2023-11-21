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
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? PdOnline { get; set; }
        public float? Oil { get; set; }
        public float? Oilair { get; set; }
        public float? OilOLTC { get; set; }
        public float? AirOLTC { get; set; }
        public float? OldPaperIsolate { get; set; }
        public float? MotorOLTC { get; set; }
        public float? NM { get; set; }
        public float? HistoryOper { get; set; }
        public float? OldOper { get; set; }
        public float? RIsolate { get; set; }
        public float? MagnetIsolate { get; set; }
        public float? CoilIsolate { get; set; }
        public float? Ratio { get; set; }
        public float? TgLost { get; set; }
        public float? TgLostCapa { get; set; }
        public float? LowVoltage { get; set; }
        public float? OLTC { get; set; }
        public float? Frequency { get; set; }
        public float? RLostCurrent { get; set; }
        public float? HardCD { get; set; }
        public float? VoltageHightRate { get; set; }
        public float? CurrentVoltage { get; set; }
        public float? SensorPD { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public MBA(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? pdOnline, float? oil, float? oilair, float? oilOLTC, float? airOLTC, float? oldPaperIsolate, float? motorOLTC, float? nM, float? historyOper, float? oldOper, float? rIsolate, float? magnetIsolate, float? coilIsolate, float? ratio, float? tgLost, float? tgLostCapa, float? lowVoltage, float? oLTC, float? frequency, float? rLostCurrent, float? hardCD, float? voltageHightRate, float? currentVoltage, float? sensorPD, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img, Device? device)
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

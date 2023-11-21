using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCSF6s")]
    public class MCSF6
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateTest { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? SF6Lost { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }
        public float? RIsolate { get; set; }
        public float? RContact { get; set; }
        public float? TimeCut { get; set; }
        public float? RIsolateClose { get; set; }
        public float? RIsolateCut { get; set; }
        public float? PurifyHuminitySF6 { get; set; }
        public float? SF6Decay { get; set; }
        public float? LostSF6 { get; set; }
        public float? RIsolateOneWayMotor { get; set; }
        public float? HightVoltageAC { get; set; }
        public float? CutOnline { get; set; }
        public float? RMotor { get; set; }
        public float? SpeedFlowCut { get; set; }
        public float? SF6Analysis { get; set; }
        public float? ScoreLevel1 { get; set; }
        public float? ScoreLevel23 { get; set; }
        public float? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public MCSF6(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? sF6Lost, float? historyMain, float? numberYearOper, float? rIsolate, float? rContact, float? timeCut, float? rIsolateClose, float? rIsolateCut, float? purifyHuminitySF6, float? sF6Decay, float? lostSF6, float? rIsolateOneWayMotor, float? hightVoltageAC, float? cutOnline, float? rMotor, float? speedFlowCut, float? sF6Analysis, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            SF6Lost = sF6Lost;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            RIsolate = rIsolate;
            RContact = rContact;
            TimeCut = timeCut;
            RIsolateClose = rIsolateClose;
            RIsolateCut = rIsolateCut;
            PurifyHuminitySF6 = purifyHuminitySF6;
            SF6Decay = sF6Decay;
            LostSF6 = lostSF6;
            RIsolateOneWayMotor = rIsolateOneWayMotor;
            HightVoltageAC = hightVoltageAC;
            CutOnline = cutOnline;
            RMotor = rMotor;
            SpeedFlowCut = speedFlowCut;
            SF6Analysis = sF6Analysis;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }
        
        public MCSF6()
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

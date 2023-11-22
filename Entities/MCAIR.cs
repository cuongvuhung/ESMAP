using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCAIRs")]
    public class MCAIR
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateTest { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? PdTve { get; set; }
        public double? PdOnline { get; set; }
        public double? HistoryMain { get; set; }
        public double? NumberYearOper { get; set; }
        public double? RIsolate { get; set; }
        public double? RContact { get; set; }
        public double? TimeCut { get; set; }
        public double? RIsolateClose { get; set; }
        public double? RIsolateCut { get; set; }
        public double? RIsolateMotor { get; set; }
        public double? Air { get; set; }
        public double? HightVoltageAC { get; set; }
        public double? PdOnlineAnalysis { get; set; }
        public double? CutOnline { get; set; }
        public double? SpeedFlowCut { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public MCAIR(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? pdTve, double? historyMain, double? numberYearOper, double? rIsolate, double? rContact, double? timeCut, double? rIsolateClose, double? rIsolateCut, double? rIsolateMotor, double? air, double? hightVoltageAC, double? pdOnlineAnalysis, double? cutOnline, double? speedFlowCut, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdTve = pdTve;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            RIsolate = rIsolate;
            RContact = rContact;
            TimeCut = timeCut;
            RIsolateClose = rIsolateClose;
            RIsolateCut = rIsolateCut;
            RIsolateMotor = rIsolateMotor;
            Air = air;
            HightVoltageAC = hightVoltageAC;
            PdOnlineAnalysis = pdOnlineAnalysis;
            CutOnline = cutOnline;
            SpeedFlowCut = speedFlowCut;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }
        public MCAIR()
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

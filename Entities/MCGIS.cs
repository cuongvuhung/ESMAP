using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCGISs")]
    public class MCGIS
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateTest { get; set; }
        public double? Outside { get; set; }
        public double? Temperature { get; set; }
        public double? Pd { get; set; }
        public double? PdTevUhf { get; set; }
        public double? PdOnline { get; set; }
        public double? SF6Lost { get; set; }
        public double? HistoryMain { get; set; }
        public double? NumberYearOper { get; set; }
        public double? RIsolate { get; set; }
        public double? RContact { get; set; }
        public double? TimeCut { get; set; }
        public double? RIsolateClose { get; set; }
        public double? RIsolateCut { get; set; }
        public double? PurifyHuminitySF6 { get; set; }
        public double? SF6Decay { get; set; }
        public double? LostSF6 { get; set; }
        public double? RIsolateOneWayMotor { get; set; }
        public double? HightVoltageAC { get; set; }
        public double? CutOnline { get; set; }
        public double? RMotor { get; set; }
        public double? SpeedFlowCut { get; set; }
        public double? SF6Analysis { get; set; }
        public double? PdAnalysis { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public MCGIS(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? pdTveUhf, double? pdOnline, double? sF6Lost, double? historyMain, double? numberYearOper, double? rIsolate, double? rContact, double? timeCut, double? rIsolateClose, double? rIsolateCut, double? purifyHuminitySF6, double? sF6Decay,double? lostSF6, double? rIsolateOneWayMotor, double? hightVoltageAC, double? cutOnline, double? rMotor, double? speedFlowCut, double? sF6Analysis, double? pdAnalysis, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            PdTevUhf = pdTveUhf;
            PdOnline = pdOnline;
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
            PdAnalysis = pdAnalysis;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        
        
        public MCGIS()
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

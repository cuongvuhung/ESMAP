using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("DCLs")]
    public class DCL
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
        public double? RContact { get; set; }
        public double? ROneWayMotor { get; set; }
        public double? VoltageACMotor { get; set; }
        public double? ScoreLevel1 { get; set; }
        public double? ScoreLevel23 { get; set; }
        public double? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? ReviewETC { get; set; }
        public string? Img { get; set; }
        public Device? Device { get; set; }

        public DCL(int id, int deviceId, DateTime dateTest, double? outside, double? temperature, double? pd, double? historyMain, double? rIsolate, double? rContact, double? rOneWayMotor, double? voltageACMotor, double? scoreLevel1, double? scoreLevel23, double? totalScore, string? note, string? reviewETC, string? img, Device? device)
        {
            Id = id;
            DeviceId = deviceId;
            DateTest = dateTest;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            HistoryMain = historyMain;
            RIsolate = rIsolate;
            RContact = rContact;
            ROneWayMotor = rOneWayMotor;
            VoltageACMotor = voltageACMotor;
            ScoreLevel1 = scoreLevel1;
            ScoreLevel23 = scoreLevel23;
            TotalScore = totalScore;
            Note = note;
            ReviewETC = reviewETC;
            Img = img;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }
        
        public DCL()
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

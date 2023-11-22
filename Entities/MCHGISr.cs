using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MCHGISrs")]
    public class MCHGISr
    {
        [Key]
        public int Id { get; set; }
        public float? Outside { get; set; }
        public float? Temperature { get; set; }
        public float? Pd { get; set; }
        public float? SF6Lost { get; set; }
        public float? HistoryMain { get; set; }
        public float? NumberYearOper { get; set; }

        public MCHGISr(int id, int deviceId, DateTime dateTest, float? outside, float? temperature, float? pd, float? sF6Lost, float? historyMain, float? numberYearOper, float? rIsolate, float? rContact, float? timeCut, float? rIsolateClose, float? rIsolateCut, float? purifyHuminitySF6, float? sF6Decay, float? lostSF6, float? rIsolateMotor, float? hightVoltageAC, float? cutOnline, float? rMotor, float? speedFlowCut, float? sF6Analysis, float? pdAnalysis, float? scoreLevel1, float? scoreLevel23, float? totalScore, string? note, string? reviewETC, string? img)
        {
            Id = id;
            Outside = outside;
            Temperature = temperature;
            Pd = pd;
            SF6Lost = sF6Lost;
            HistoryMain = historyMain;
            NumberYearOper = numberYearOper;
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";
        }

        
        

        
        
        public MCHGISr()
        {
        }
        // JsonIgnore 
        /*[JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }
*/
    }
}

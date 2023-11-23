using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("MBArs")]
    public class MBAr
    {
        [Key]
        public int Id { get; set; }
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

        public MBAr(int id, float? outside, float? temperature, float? pd, float? pdOnline, float? oil, float? oilair, float? oilOLTC, float? airOLTC, float? oldPaperIsolate, float? motorOLTC, float? nM, float? historyOper, float? oldOper)
        {
            Id = id;
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
            //CreatedAt = DateTime.Now;
            //CreatedBy = "System";
        }

        
        
        public MBAr()
        {
        }
        // JsonIgnore 
        /*[JsonIgnore] public DateTime? CreatedAt { get; set; }
        [JsonIgnore] public string? CreatedBy { get; set; }
        [JsonIgnore] public DateTime? DeletedAt { get; set; }
        [JsonIgnore] public string? DeletedBy { get; set; }
        [JsonIgnore] public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public string? UpdatedBy { get; set; }*/

    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CBM_API.Entities
{
    [Table("ManufactureModel")]
    public class ManufactureModel
    {
        [Key]
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int ManufactureId { get; set; }

        public ManufactureModel(int id, int modelId, int manufactureId)
        {
            Id = id;
            ModelId = modelId;
            ManufactureId = manufactureId;
            CreatedAt = DateTime.Now;
            CreatedBy = "System";
        }

        public ManufactureModel()
        {
        }
        // JsonIgnore 
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

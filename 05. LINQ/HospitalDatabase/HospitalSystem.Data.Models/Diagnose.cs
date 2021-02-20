namespace HospitalSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HospitalSystem.Data.Models.Common;

    public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DiagnoseNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.CommentsMaxLength)]
        public string Comments { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

    }
}

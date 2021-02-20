namespace HospitalSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HospitalSystem.Data.Models.Common;

    public class Visitation
    {
        [Key]
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(GlobalConstants.CommentsMaxLength)]
        public string Comments { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatiendId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

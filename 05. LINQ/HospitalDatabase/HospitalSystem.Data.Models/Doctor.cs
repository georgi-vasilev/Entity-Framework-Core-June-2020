namespace HospitalSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HospitalSystem.Data.Models.Common;

    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DoctorNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SpecialtyMaxLength)]
        public string Specialty { get; set; }

        public virtual ICollection<Visitation> Visations { get; set; }
    }
}

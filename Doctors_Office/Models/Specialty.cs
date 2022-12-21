using System.Collections.Generic;

namespace Doctors_Office.Models
{
  public class Specialty
  {
    public int SpecialtyId { get; set; }
    public int DoctorId { get; set; }
    public string SpecialtyName { get; set; }
    public virtual ICollection<DoctorSpecialty> JoinEntities { get; set; }

    public Specialty()
    {
      this.JoinEntities = new HashSet<DoctorSpecialty>();
    }
  }
}
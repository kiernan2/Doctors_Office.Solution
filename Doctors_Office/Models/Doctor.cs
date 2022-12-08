using System.Collections.Generic;

namespace Doctors_Office.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public virtual ICollection<DoctorPatient> JoinEntities { get; set; }

    public Doctor()
    {
      this.JoinEntities = new HashSet<DoctorPatient>();
    }
  }
}
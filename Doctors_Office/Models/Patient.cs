using System.Collections.Generic;

namespace Doctors_Office.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<DoctorPatient> JoinEntities{ get; set; }

    public Patient()
    {
      this.JoinEntities = new HashSet<DoctorPatient>();
    }
  }
}
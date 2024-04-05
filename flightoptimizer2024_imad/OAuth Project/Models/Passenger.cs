using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuth_Project.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        [Required]
        public string FullName{ get; set; }
        [Required]
        public string Type{ get; set; }
        public bool NeedsDoubleSpace { get; set; } = false;
        public int? FamilyId { get; set; }
        public Family? Family { get; set; }
        public int? age { get; set; }
        [NotMapped]
        public string? familyCode { get; set; }
        public Passenger()
        {
            
        }
        public Passenger(PassengerCSV csv)
        {
            //this.Id = csv.Id;
            this.FullName = csv.Id.ToString();
            if(csv.Type == "Enfant")
            {
                this.Type = "Child";
            }else if(csv.Type=="Adulte")
            {
                this.Type = "Adult";
            }
            //this.Type=csv.Type;
            this.familyCode = csv.FamilyCode;
            if(csv.RequiresExtraSeat == "Oui")
            {
                this.NeedsDoubleSpace = true;
            }else { this.NeedsDoubleSpace = false; }
            this.age= csv.Age;
          
        }
    }
    
}

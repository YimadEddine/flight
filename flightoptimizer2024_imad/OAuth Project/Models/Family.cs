using System.ComponentModel.DataAnnotations.Schema;

namespace OAuth_Project.Models
{
    public class Family
    {
        public int Id { get; set; }
        public List<Passenger> Members { get; set; }
        public int? MemberId { get; set; }
        [NotMapped]
        public string? FamilyCode { get; set; }
        public Family()
        {
            
        }

        public Family(string code)
        {
            this.FamilyCode = code;   
        }
    }
    
}

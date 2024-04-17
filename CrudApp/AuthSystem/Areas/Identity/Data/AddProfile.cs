using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;



namespace AuthSystem.Areas.Identity.Data
{
    public class AddProfile
    {
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public string ProfileUrl { get; set; }

        public string UserId { get; set; }
    }
}




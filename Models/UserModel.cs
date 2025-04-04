using System.ComponentModel.DataAnnotations;

namespace CRUD_Project.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }
    }
}

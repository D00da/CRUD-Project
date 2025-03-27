using System.ComponentModel.DataAnnotations;
using CRUD_Project.Enums;

namespace CRUD_Project.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        public string title { get; set; }

        public State status { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime? dateLimit { get; set; }
    }
}

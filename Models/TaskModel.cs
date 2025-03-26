using System.ComponentModel.DataAnnotations;
using CRUD_Project.Enums;

namespace CRUD_Project.Models
{
    public class TaskModel
    {
        [Key]
        public Guid Id { get; set; }

        public string title { get; set; }

        public State status => State.Unfinished;

        public DateTime dateCreated { get; set; }

        public DateTime? dateLimit { get; set; }
    }
}

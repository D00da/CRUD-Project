using CRUD_Project.Enums;

namespace CRUD_Project.DTOs
{
    public class TaskUpdateDTO
    {
        public string title { get; set; }
        public DateTime dateLimit { get; set; }
        public State status { get; set; }
    }
}

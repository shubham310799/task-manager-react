namespace TaskManagerAPI.Entities
{
    public class UserTask
    {
        public int UserTaskId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
    }
}

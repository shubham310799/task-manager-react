using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDt { get; set; }
        public bool IsVerified { get; set; }
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}

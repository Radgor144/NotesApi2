using System.ComponentModel.DataAnnotations;

namespace NoteApi__.NET_.Entities
{

    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public User User { get; set; } 
    }
}

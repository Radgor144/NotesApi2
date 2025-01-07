
using Microsoft.AspNetCore.Identity;

namespace NoteApi__.NET_.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}

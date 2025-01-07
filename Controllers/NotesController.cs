using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using NoteApi__.NET_.Entities;

namespace NoteApi__.NET_.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email); 

            var userNotes = await _context.Notes
                .Where(note => note.UserId == userId)
                .ToListAsync();

            ViewData["UserEmail"] = userEmail;

            return View(userNotes);
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null || note.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return NotFound();

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {

            note.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var note = await _context.Notes.FindAsync(id);

            if (note == null || note.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return NotFound();

            return View(note);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] Note note)
        {
            if (id != note.Id)
            {
                Console.WriteLine($"Id mismatch: {id} != {note.Id}");
                return NotFound();
            }

            var existingNote = await _context.Notes.FindAsync(id);

            if (existingNote == null)
            {
                Console.WriteLine("Note not found.");
                return NotFound();
            }

            if (existingNote.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                Console.WriteLine($"User mismatch: {existingNote.UserId} != {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
                return NotFound();
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null || note.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return NotFound();

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note != null && note.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

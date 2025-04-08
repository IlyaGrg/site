using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcComments.Data;
using MvcComments.Models;

namespace MvcComments.Controllers
{
    public class PostController : Controller
    {
        private readonly PagesCommentsContext _context;

        public PostController(PagesCommentsContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.OrderByDescending(p => p.PublicationDate).ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Перенаправляем на страницу Index
            }
            return View(post); // Возвращаем форму с ошибками валидации
        }
    }
}

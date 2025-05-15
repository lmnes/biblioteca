using Microsoft.AspNetCore.Mvc;
using BibliotecaVMC.Data;
using Microsoft.EntityFrameworkCore;
using BibliotecaVMC.Models;

namespace BibliotecaVMC.Controllers
{
    public class LivroController : Controller
    {
        private readonly AppDbContext _context;

        public LivroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Livro/Index
        public async Task<IActionResult> Index(string searchString)
        {
            var livros = from l in _context.Livros
                         select l;

            if (!string.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(l => l.Titulo.Contains(searchString) ||
                                          l.Autor.Contains(searchString) ||
                                          l.Categoria.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await livros.ToListAsync());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var livro = await _context.Livros.FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null) return NotFound();
            return View(livro);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,Categoria,AnoPublicacao")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Livro adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Erro ao adicionar o livro. Verifique os dados e tente novamente.";
            return View(livro);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null) return NotFound();
            return View(livro);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,Categoria,AnoPublicacao")] Livro livro)
        {
            if (id != livro.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Livro atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Livros.Any(e => e.Id == livro.Id))
                    {
                        TempData["ErrorMessage"] = "Livro não encontrado.";
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Erro ao atualizar o livro. Verifique os dados e tente novamente.";
            return View(livro);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var livro = await _context.Livros.FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null) return NotFound();
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                TempData["ErrorMessage"] = "Livro não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Livro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
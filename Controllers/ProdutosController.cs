using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjAspNetMvcDocker.Models;

namespace ProjAspNetMvcDocker.Controllers
{
    public class ProdutosController : Controller
    {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
            return View(produtos);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
           
            var produto = await _context.Produtos.Include(p => p.Categoria).
                FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null) 
                return NotFound();

            return View(produto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {

            if (ModelState.IsValid)
            {

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarregar categorias no caso de erro
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produto/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Produto produto)
        {
            if (id != produto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Produtos.Any(e => e.Id == produto.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            // Recarregar a lista de categorias em caso de erro
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using FreeCodeCamp.MVC.Data;
using FreeCodeCamp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FreeCodeCamp.MVC.Controllers;

public class ItemsController : Controller
{
    readonly FreeCodeCampContext _context;

    public ItemsController(FreeCodeCampContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _context.Items.Include(a => a.SerialNumber)
                                                        .Include(a => a.Category)
                                                        .Include(a => a.ItemClients!)
                                                        .ThenInclude(b => b.Client)
                                                        .ToListAsync();
        return View(items);
    }

    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id", "Price", "Name", "CategoryId")] Item item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        var item = await _context.Items.FindAsync(id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id", "Price", "Name", "CategoryId")] Item item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Items.FindAsync(id);
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Overview()
    {
        var item = new Item
        {
            Name = "Sample Item"
        };
        return View(item);
    }

    public IActionResult EditContent(int itemId)
    {
        return Content($"id={itemId}");
    }
}

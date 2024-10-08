﻿using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

public class TodosController(ITodosService todosService): Controller
{
    private readonly ITodosService _todosService = todosService;

    [HttpGet]
    public async Task<IActionResult> ShowTodos()
    {
        var todos = await _todosService.GetTodos();
        return View(todos);
    }

    [HttpPost]
    public async Task<IActionResult> AddTodo([FromForm]TodoInputViewModel newTodo)
    {
        await _todosService.AddTodo(newTodo);
        return RedirectToAction("ShowTodos");
    }
}

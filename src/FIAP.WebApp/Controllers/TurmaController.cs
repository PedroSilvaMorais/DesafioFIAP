using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FIAP.WebApp.Data;
using FIAP.WebApp.Models;
using FIAP.WebApp.Services;

namespace FIAP.WebApp.Controllers
{
    public class TurmaController : MainController
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        // GET: Turma
        public async Task<IActionResult> Index()
        {
              return View(await _turmaService.ObterTodos());
        }

        // GET: Turma/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaViewModel = await _turmaService.ObterById(id);
            if (turmaViewModel == null)
            {
                return NotFound();
            }

            return View(turmaViewModel);
        }

        // GET: Turma/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Curso_Id,NomeTurma,Ano")] TurmaViewModel turmaViewModel)
        {
            if (!ModelState.IsValid) return View(turmaViewModel);
            
            
            var resposta = await _turmaService.CriarTurma(turmaViewModel);

            if (ResponsePossuiErros(resposta.ResponseResult)) return View(turmaViewModel);
            
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Turma/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaViewModel = await _turmaService.ObterById(id);
            if (turmaViewModel == null)
            {
                return NotFound();
            }
            return View(turmaViewModel);
        }

        // POST: Turma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,curso_Id,nomeTurma,ano,isAtivo")] TurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(turmaViewModel);
            
            var resposta =  await _turmaService.AlterarById(turmaViewModel);

            if (ResponsePossuiErros(resposta.ResponseResult)) View(turmaViewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Turma/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaViewModel = await _turmaService.ObterById(id);
            if (turmaViewModel == null)
            {
                return NotFound();
            }

            return View(turmaViewModel);
        }

        // POST: Turma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turmaViewModel = await _turmaService.ObterById(id);
            if (turmaViewModel != null)
            {
                await _turmaService.ExcluirRegistro(turmaViewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool TurmaViewModelExists(int id)
        //{
        //  return _context.TurmaViewModel.Any(e => e.Id == id);
        //}
    }
}

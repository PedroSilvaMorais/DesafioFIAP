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
    public class AlunoViewModelsController : MainController
    {
        private readonly IAlunoService _alunoService;

        public AlunoViewModelsController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        // GET: AlunoViewModels
        public async Task<IActionResult> Index()
        {
              return View(await _alunoService.ObterTodos());
        }

        // GET: AlunoViewModels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunoViewModel = await _alunoService.ObterById(id);
            if (alunoViewModel == null)
            {
                return NotFound();
            }

            return View(alunoViewModel);
        }

        // GET: AlunoViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: AlunoViewModels/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Usuario,Senha,IsAtivo,DT_Cadastro,DT_Alteracao")] AlunoViewModel alunoViewModel)
        {
            if (!ModelState.IsValid) return View(alunoViewModel);


            var resposta = await _alunoService.CriarAluno(alunoViewModel);

            if (ResponsePossuiErros(resposta.ResponseResult)) return View(alunoViewModel);


            return RedirectToAction(nameof(Index));
            
        }

        //GET: AlunoViewModels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunoViewModel = await _alunoService.ObterById(id);
            if (alunoViewModel == null)
            {
                return NotFound();
            }
            return View(alunoViewModel);
        }

        //POST: AlunoViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Usuario,Senha,IsAtivo,DT_Cadastro,DT_Alteracao")] AlunoViewModel alunoViewModel)
        {
            if (id != alunoViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(alunoViewModel);

            var resposta = await _alunoService.AlterarById(alunoViewModel);

            if (ResponsePossuiErros(resposta.ResponseResult)) View(alunoViewModel);

            return RedirectToAction(nameof(Index));

        }

        //GET: AlunoViewModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunoViewModel = await _alunoService.ObterById(id);
            if (alunoViewModel == null)
            {
                return NotFound();
            }

            return View(alunoViewModel);
        }

        //POST: AlunoViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alunoViewModel = await _alunoService.ObterById(id);
            if (alunoViewModel != null)
            {
                await _alunoService.ExcluirRegistro(alunoViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool AlunoViewModelExists(int id)
        //{
        //    return _context.AlunoViewModel.Any(e => e.id == id);
        //}
    }
}

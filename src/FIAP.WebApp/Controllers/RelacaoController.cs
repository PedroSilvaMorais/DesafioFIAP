using FIAP.WebApp.Models;
using FIAP.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.WebApp.Controllers
{
    public class RelacaoController : MainController
    {
        private readonly IRelacaoService _relacaoService;

        private AlunoTurmaViewModel _alunoTurma = new AlunoTurmaViewModel();

        public RelacaoController(IRelacaoService turmaService)
        {
            _relacaoService = turmaService;
        }

        // GET: Relacao
        public async Task<IActionResult> Index()
        {
            _alunoTurma = await _relacaoService.ObterTodos();

            return View(_alunoTurma);
        }

        // GET: Relacao/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunoViewModel = await _relacaoService.RelacaoDeAlunos(id);
            if (alunoViewModel == null)
            {
                return NotFound();
            }

            return View(alunoViewModel);
        }

        // GET: Relacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AlunoTurmaViewModel alunoTurmaViewModel)
        {
            var resposta = await _relacaoService.SalvarRelacao(alunoTurmaViewModel);

            if (ResponsePossuiErros(resposta.ResponseResult))
            {
                _alunoTurma = await _relacaoService.ObterTodos();
                _alunoTurma.Aluno_Id = alunoTurmaViewModel.Aluno_Id;
                _alunoTurma.Turma_Id = alunoTurmaViewModel.Turma_Id;

                return View(_alunoTurma);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Relacao/Desaivar/5
        public async Task<IActionResult> Desativar(int id)
        {
            await _relacaoService.InativarRelacao(id);

            return RedirectToAction(nameof(Index));
        }


        // GET: Relacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Relacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Relacao/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

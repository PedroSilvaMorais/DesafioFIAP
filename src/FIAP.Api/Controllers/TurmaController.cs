using FIAP.Api.Data;
using FIAP.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Api.Controllers
{
    [Route("api/[controller]")]
    public class TurmaController : MainController
    {
        private readonly TurmaDB _db = new TurmaDB();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetAll()
        {
            return CustomResponse(await _db.ObterTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetById(int id)
        {
            return CustomResponse(await _db.ObterbyId(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAluno(int id, Turma turma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var retornoTurma = await _db.ObterbyNome(turma.NomeTurma);
            if (retornoTurma != null)
            {
                AdicionarErroProcessamento("Nome de turma já existe");
                return CustomResponse();
            }

            await _db.Alterar(turma);

            return CustomResponse();
        }

        [HttpPost]
        public async Task<ActionResult> PostAluno(Turma turma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);


            var retornoTurma = await _db.ObterbyNome(turma.NomeTurma);
            if (retornoTurma != null)
            {
                AdicionarErroProcessamento("Nome de turma já existe");
                return CustomResponse();
            }

            await _db.Inserir(turma);

            return CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task DeletarTurma(int id)
        {
            var turma = await _db.ObterbyId(id);

            if (turma == null) CustomResponse();

            await _db.Deletar(turma.Id);

            CustomResponse();
        }
    }
}

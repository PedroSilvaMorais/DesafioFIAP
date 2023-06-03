using FIAP.Api.Data;
using FIAP.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Api.Controllers
{
    [Route("api/[controller]")]
    public class RelacaoController : MainController
    {
        private readonly TurmaDB _turmaDB = new TurmaDB();

        private readonly AlunoDB _alunoDB = new AlunoDB();

        private readonly RelacaoDB _relacaoDB = new RelacaoDB();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoTurma>>> GetAll()
        {
            var listaALunos = await _alunoDB.GetAlunosAtivos();

            var listaTurmas = await _turmaDB.GetTurmasAtivas();

            var turmas = await _turmaDB.ObterTurmaQuantidade();


            return CustomResponse(new AlunoTurma
                                    {
                                        ListaAlunos = listaALunos,
                                        ListaTurmas= listaTurmas,
                                        Turmas = turmas
                                    });
        }

        [HttpPost]
        public async Task<ActionResult> Relacionar(AlunoTurma relacao)
        {
            var result = await _relacaoDB.PossuiRelacao(relacao);

            if (result.Count() > 0)
            {
                AdicionarErroProcessamento("Relacão já existente");
                return CustomResponse();
            }

            await _relacaoDB.Inserir(relacao.Turma_Id, relacao.Aluno_Id);

            return CustomResponse();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAlunosByTurma(int id)
        {
            var result = await _relacaoDB.RelacaoDeAlunos(id);

            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DesativarRelacao(int id)
        {
            await _relacaoDB.Desativar(id);

            return CustomResponse();
        }
    }
}

using FIAP.WebApp.Models;

namespace FIAP.WebApp.Services
{
    public interface IRelacaoService
    {
        Task<AlunoTurmaViewModel> ObterTodos();

        Task<RespostaRelacao> SalvarRelacao(AlunoTurmaViewModel relacao);

        Task<IEnumerable<RelacaoAlunosViewModel>> RelacaoDeAlunos(int Id);

        Task InativarRelacao(int id);
    }
}

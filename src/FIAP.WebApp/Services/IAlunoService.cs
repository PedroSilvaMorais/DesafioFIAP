using FIAP.WebApp.Models;

namespace FIAP.WebApp.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoViewModel>> ObterTodos();

        Task<AlunoViewModel> ObterById(int id);

        Task<RespostaAluno> AlterarById(AlunoViewModel aluno);

        Task<RespostaAluno> CriarAluno(AlunoViewModel aluno);

        Task<AlunoViewModel> ExcluirRegistro(AlunoViewModel aluno);

    }
}

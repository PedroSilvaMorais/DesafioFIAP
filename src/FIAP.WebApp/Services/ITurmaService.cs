using FIAP.WebApp.Models;

namespace FIAP.WebApp.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaViewModel>> ObterTodos();

        Task<TurmaViewModel> ObterById(int id);

        Task<RespostaTurma> AlterarById(TurmaViewModel turma);

        Task<RespostaTurma> CriarTurma(TurmaViewModel turma);

        Task<TurmaViewModel> ExcluirRegistro(TurmaViewModel turma);
    }
}

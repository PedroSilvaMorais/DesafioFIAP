namespace FIAP.WebApp.Models
{
    public class AlunoTurmaViewModel
    {
        public IEnumerable<Dado> ListaAlunos { get; set; }

        public IEnumerable<Dado> ListaTurmas { get; set; }

        public IEnumerable<TurmaQtdViewModel> Turmas { get; set; }

        public int Aluno_Id { get; set; }

        public int Turma_Id { get; set; }
    }

    public class Dado
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class RespostaRelacao
    {
        public AlunoTurmaViewModel TurmaAluno { get; set; }

        public ResponseResult ResponseResult { get; set; }
    }
}

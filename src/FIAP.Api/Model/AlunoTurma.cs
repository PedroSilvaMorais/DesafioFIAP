namespace FIAP.Api.Model
{
    public class AlunoTurma
    {
        public IEnumerable<Dado> ListaAlunos { get; set; }

        public IEnumerable<Dado> ListaTurmas { get; set; }

        public IEnumerable<TurmaQtd> Turmas { get; set; }

        public int Aluno_Id { get; set; }

        public int Turma_Id { get; set; }
    }

    public class Dado
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}

namespace FIAP.Api.Model
{
    public class Relacao
    {
        public int Id { get; set; }
        public int Aluno_Id { get; set; }

        public int Turma_Id { get; set; }

        public bool IsAtivo { get; set; }
    }
}

namespace FIAP.WebApp.Models
{
    public class AlunoViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public bool IsAtivo { get; set; }
    }

    public class RespostaAluno
    {
        public AlunoViewModel Aluno { get; set; }

        public ResponseResult ResponseResult { get; set; }
    }
}

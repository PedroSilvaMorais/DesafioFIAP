using System.ComponentModel.DataAnnotations;

namespace FIAP.WebApp.Models
{
    public class TurmaViewModel
    {
        public int Id { get; set; }

        public int Curso_Id { get; set; }

        public string NomeTurma { get; set; }

        public int Ano { get; set; }

        public bool IsAtivo { get; set; }
    }

    public class RespostaTurma
    {
        public TurmaViewModel Turma { get; set; }

        public ResponseResult ResponseResult { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FIAP.Api.Model
{
    public class Turma
    {
        public int Id { get; set; }

        public int Curso_Id { get; set; }

        public string NomeTurma { get; set; }
        [Range(2023, 2100)]
        public int Ano { get; set; }

        public bool IsAtivo { get; set; }
    }
}

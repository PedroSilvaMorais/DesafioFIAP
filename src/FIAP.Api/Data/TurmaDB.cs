using Dapper;
using FIAP.Api.Model;
using Microsoft.Data.SqlClient;

namespace FIAP.Api.Data
{
    public class TurmaDB : ContextDB
    {

        public async Task<IEnumerable<Turma>> ObterTodos()
        {
            var query = string.Format("SELECT * FROM TB_TURMAS WHERE IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Turma>(query);

                return result;
            }
        }

        public async Task<IEnumerable<TurmaQtd>> ObterTurmaQuantidade()
        {
            var query = string.Format("SELECT A.ID, A.NOMETURMA AS Nome, COUNT(A.ID) AS Qtd FROM TB_TURMAS A INNER JOIN TB_ALUNOS_TURMAS B ON A.ID = B.TURMA_ID WHERE B.IsAtivo = 1 GROUP BY A.ID, A.NOMETURMA");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<TurmaQtd>(query);

                return result;
            }
        }

        public async Task<Turma> ObterbyId(int id)
        {
            var query = string.Format($"SELECT * FROM TB_TURMAS WHERE ID = {id} AND IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Turma>(query);

                return result.FirstOrDefault();
            }
        }

        public async Task<Turma> ObterbyNome(string nome)
        {
            var query = string.Format($"SELECT * FROM TB_TURMAS WHERE NOMETURMA LIKE '{nome}' AND IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Turma>(query);

                return result.FirstOrDefault();
            }
        }

        public async Task<Turma> Inserir(Turma aluno)
        {
            var query = string.Format("INSERT INTO TB_TURMAS(CURSO_ID, NOMETURMA, ANO) VALUES (@Curso_Id, @NomeTurma, @Ano)");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Turma>(query, aluno);

                return aluno;
            }
        }

        public async Task<Turma> Alterar(Turma aluno)
        {
            var query = string.Format("UPDATE TB_TURMAS SET CURSO_ID = @Curso_Id, NOMETURMA = @NomeTurma, ANO = @Ano, IsAtivo = @IsAtivo WHERE ID = @Id");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync<Aluno>(query, aluno);

                return aluno;
            }
        }

        public async Task Deletar(int id)
        {
            var query = string.Format($"DELETE FROM TB_TURMAS WHERE ID = {id}");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync<Aluno>(query);
            }
        }

        public async Task<IEnumerable<Dado>> GetTurmasAtivas()
        {
            var query = string.Format("SELECT ID, NOMETURMA AS NOME FROM TB_TURMAS WHERE IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Dado>(query);

                return result;
            }
        }
    }
}

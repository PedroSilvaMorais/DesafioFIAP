using Dapper;
using FIAP.Api.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FIAP.Api.Data
{
    public class AlunoDB : ContextDB
    {
        public async Task<IEnumerable<Aluno>> ObterTodos()
        {
            var query = string.Format("SELECT * FROM TB_ALUNOS WHERE IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Aluno>(query);

                return result;
            }
        }

        public async Task<Aluno> ObterbyId(int id)
        {
            var query = string.Format($"SELECT * FROM TB_ALUNOS WHERE ID = {id} AND IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Aluno>(query);

                return result.FirstOrDefault();
            }
        }

        public async Task<Aluno> Inserir(Aluno aluno)
        {
            var query = string.Format("INSERT INTO TB_ALUNOS(NOME, USUARIO, SENHA) VALUES (@Nome, @Usuario, @Senha)");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Aluno>(query, aluno);

                return aluno;
            }
        }

        public async Task<Aluno> Alterar(Aluno aluno)
        {
            var query = string.Format("UPDATE TB_ALUNOS SET NOME = @Nome, USUARIO = @Usuario, SENHA = @Senha, IsAtivo = @IsAtivo WHERE ID = @Id");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync<Aluno>(query, aluno);

                return aluno;
            }
        }

        public async Task Deletar(int id)
        {
            var query = string.Format($"DELETE FROM TB_ALUNOS WHERE ID = {id}");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync<Aluno>(query);
            }
        }

        public async Task<IEnumerable<Dado>> GetAlunosAtivos()
        {
            var query = string.Format("SELECT ID, NOME FROM TB_ALUNOS WHERE IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Dado>(query);

                return result;
            }
        }
    }
}

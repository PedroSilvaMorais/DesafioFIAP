using Dapper;
using FIAP.Api.Model;
using Microsoft.Data.SqlClient;

namespace FIAP.Api.Data
{
    public class RelacaoDB : ContextDB
    {
        public async Task<IEnumerable<Relacao>> PossuiRelacao(AlunoTurma rel)
        {
            var query = string.Format("SELECT * FROM TB_ALUNOS_TURMAS WHERE ALUNO_ID = @Aluno_Id AND TURMA_ID = @Turma_Id AND IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                var result = await sqlConn.QueryAsync<Relacao>(query, rel);

                return result;
            }
        }

        public async Task Inserir (int turma_Id, int aluno_Id)
        {
            var query = string.Format($"INSERT INTO TB_ALUNOS_TURMAS (TURMA_ID, ALUNO_ID) VALUES ({turma_Id}, {aluno_Id})");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync(query);

                return;
            }
        }

        public async Task<IEnumerable<RelacaoAlunos>> RelacaoDeAlunos(int id)
        {
            var query = string.Format($"SELECT A.ID, B.NOME, B.USUARIO, A.IsAtivo FROM TB_ALUNOS_TURMAS A INNER JOIN TB_ALUNOS B ON A.ALUNO_ID = B.ID WHERE A.TURMA_ID = {id} AND A.IsAtivo = 1");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                return await sqlConn.QueryAsync<RelacaoAlunos>(query);
            }
        }

        public async Task Desativar(int id)
        {
            var query = string.Format($"UPDATE TB_ALUNOS_TURMAS SET IsAtivo = 0 WHERE ID = {id}");

            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                await sqlConn.QueryAsync(query);
                return;
            }
        }
    }
}

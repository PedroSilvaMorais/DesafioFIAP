using FIAP.WebApp.Models;
using System.Text;
using System.Text.Json;

namespace FIAP.WebApp.Services
{
    public class AlunoService : Service, IAlunoService
    {
        private readonly HttpClient _httpClient;

        public AlunoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RespostaAluno> AlterarById(AlunoViewModel aluno)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(aluno),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PutAsync("https://localhost:7195/api/Aluno/" + aluno.Id, content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!TratarErrosResponse(response))
            {
                return new RespostaAluno
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return new RespostaAluno
            {
                Aluno = JsonSerializer.Deserialize<AlunoViewModel>(await response.Content.ReadAsStringAsync(), options)
            };
        }

        public async Task<RespostaAluno> CriarAluno(AlunoViewModel aluno)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(aluno),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PostAsync("https://localhost:7195/api/Aluno", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!TratarErrosResponse(response))
            {
                return new RespostaAluno
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return new RespostaAluno
            {
                Aluno = JsonSerializer.Deserialize<AlunoViewModel>(await response.Content.ReadAsStringAsync(), options)
            };
        }

        public async Task<AlunoViewModel> ExcluirRegistro(AlunoViewModel aluno)
        {
            await _httpClient.DeleteAsync("https://localhost:7195/api/Aluno/" + aluno.Id);

            return aluno;
        }

        public async Task<AlunoViewModel> ObterById(int id)
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Aluno/" + id);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<AlunoViewModel>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<IEnumerable<AlunoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Aluno");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<IEnumerable<AlunoViewModel>>(await response.Content.ReadAsStringAsync(), options);
        }


    }
}

using FIAP.WebApp.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FIAP.WebApp.Services
{
    public class RelacaoService : Service, IRelacaoService
    {
        private readonly HttpClient _httpClient;

        public RelacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InativarRelacao(int id)
        {
            await _httpClient.DeleteAsync("https://localhost:7195/api/Relacao/" + id);

            return;
        }

        public async Task<AlunoTurmaViewModel> ObterTodos()
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Relacao");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<AlunoTurmaViewModel>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<IEnumerable<RelacaoAlunosViewModel>> RelacaoDeAlunos(int Id)
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Relacao/" + Id);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<IEnumerable<RelacaoAlunosViewModel>>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<RespostaRelacao> SalvarRelacao(AlunoTurmaViewModel relacao)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(relacao),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PostAsync("https://localhost:7195/api/Relacao", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!TratarErrosResponse(response))
            {
                return new RespostaRelacao
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return new RespostaRelacao
            {
                TurmaAluno = relacao
            };
        }
    }
}

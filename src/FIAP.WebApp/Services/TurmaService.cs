using FIAP.WebApp.Models;
using System.Text;
using System.Text.Json;

namespace FIAP.WebApp.Services
{
    public class TurmaService : Service, ITurmaService
    {
        private readonly HttpClient _httpClient;

        public TurmaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RespostaTurma> AlterarById(TurmaViewModel turma)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(turma),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PutAsync("https://localhost:7195/api/turma/" + turma.Id, content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!TratarErrosResponse(response))
            {
                return new RespostaTurma
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return new RespostaTurma
            {
                Turma = JsonSerializer.Deserialize<TurmaViewModel>(await response.Content.ReadAsStringAsync(), options)
            };
        }

        public async Task<RespostaTurma> CriarTurma(TurmaViewModel turma)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(turma),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PostAsync("https://localhost:7195/api/Turma", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!TratarErrosResponse(response))
            {
                return new RespostaTurma
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return new RespostaTurma
            {
                Turma = turma
            };
        }

        public async Task<TurmaViewModel> ExcluirRegistro(TurmaViewModel turma)
        {
            await _httpClient.DeleteAsync("https://localhost:7195/api/Turma/" + turma.Id);

            return turma;
        }

        public async Task<TurmaViewModel> ObterById(int id)
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Turma/" + id);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<TurmaViewModel>(await response.Content.ReadAsStringAsync(),options);
        }

        public async Task<IEnumerable<TurmaViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Turma");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<IEnumerable<TurmaViewModel>>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}

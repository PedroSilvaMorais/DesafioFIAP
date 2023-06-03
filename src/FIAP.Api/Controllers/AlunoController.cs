using FIAP.Api.Data;
using FIAP.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FIAP.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlunoController : MainController
    {
        private readonly AlunoDB _db = new AlunoDB();

        [HttpGet]
        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _db.ObterTodos();
        }

        [HttpGet("{id}")]
        public async Task<Aluno> GetById(int id)
        {
            return await _db.ObterbyId(id);
        }

        [HttpPut("{id}")]
        public async Task<Aluno> UpdateAluno(int id, Aluno aluno)
        {
            return await _db.Alterar(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> PostAluno(Aluno aluno)
        {
            var pwdHashed = getHash(aluno.Senha);

            if (!await IsPassWordStrong(aluno.Senha))
            {
                AdicionarErroProcessamento("A senha deve ter no minimo 8 caracteres contendo números, 1 letra maiúscula e caracteres especiais.");
                return CustomResponse();
            }

            aluno.Senha = pwdHashed;

            return CustomResponse(await _db.Inserir(aluno));
        }

        [HttpDelete("{id}")]
        public async Task DeletarAluno(int id)
        {
            await _db.Deletar(id);
        }

        private async Task<bool> IsPassWordStrong(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        private string getHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

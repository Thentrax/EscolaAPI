using Microsoft.AspNetCore.Mvc;
using EscolaAPI.Models;
using EscolaAPI.Context;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly EscolaContext _context;
        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _context.Aluno.ToList();
            if (alunos == null)
            {
                return NotFound("Nenhum aluno encontrado");
            }
            else{
                return Ok(alunos);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            if (aluno == null)
            {
                return BadRequest("Aluno inválido");
            }
            else{
                _context.Aluno.Add(aluno);
                _context.SaveChanges();
                return Ok("Aluno cadastrado com sucesso");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno aluno)
        {
            var alunoUpdate = _context.Aluno.FirstOrDefault(a => a.Id == id);
            if (alunoUpdate == null)
            {
                return NotFound("Aluno não encontrado");
            }
            else{
                alunoUpdate.Nome = aluno.Nome;
                alunoUpdate.CPF = aluno.CPF;
                alunoUpdate.Email = aluno.Email;
                _context.SaveChanges();
                return Ok("Aluno atualizado com sucesso");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alunoDelete = _context.Aluno.FirstOrDefault(a => a.Id == id);
            if (alunoDelete == null)
            {
                return NotFound("Aluno não encontrado");
            }
            else{
                _context.Aluno.Remove(alunoDelete);
                _context.SaveChanges();
                return Ok("Aluno excluído com sucesso");
            }
        }
    }
}
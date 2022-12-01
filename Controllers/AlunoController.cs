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
        public IActionResult GetAluno()
        {
            var alunos = _context.Aluno.ToList();
            if (alunos.Count == 0)
            {
                return NotFound("Nenhum aluno encontrado");
            }
            else{
                return Ok(alunos);
            }
        }

        [HttpPost]
        public IActionResult PostAluno(Aluno aluno, int turmaId)
        {

            if (aluno == null)
            {
                return BadRequest("Dados de aluno inválidos");
            }
            var alunoExistente = _context.Aluno.FirstOrDefault(a => a.CPF == aluno.CPF);
            if (alunoExistente != null)
            {
                return BadRequest("CPF já cadastrado");
            }
            else{
                _context.Aluno.Add(aluno);
                _context.SaveChanges();

                MatriculaController matriculaController = new MatriculaController(_context);
                matriculaController.PostMatricula(aluno.Id, turmaId);

                return Ok("Aluno cadastrado com sucesso");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutAluno(int id, Aluno aluno)
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
        public IActionResult DeleteAluno(int id)
        {
            var aluno = _context.Aluno.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            else{
                _context.Aluno.Remove(aluno);
                _context.SaveChanges();
                return Ok("Aluno excluído com sucesso");
            }
        }
    }
}
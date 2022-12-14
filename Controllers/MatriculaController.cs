using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Models;
using EscolaAPI.Context;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly EscolaContext _context;
        public MatriculaController(EscolaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostMatricula(int alunoId, int turmaId)
        {
            var aluno = _context.Aluno.FirstOrDefault(a => a.Id == alunoId);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            
            var turma = _context.Turma.Include(t => t.Alunos).FirstOrDefault(t => t.Id == turmaId);
            if (turma.Alunos.Contains(aluno))
            {
                return BadRequest("Aluno já matriculado");
            }
            if (turma.Alunos.Count >= 5)
            {
                return BadRequest("Turma lotada");
            }

            turma.Alunos.Add(aluno);
            _context.SaveChanges();

            return Ok("Matrícula realizada com sucesso");
        }

        [HttpGet]
        public IActionResult GetMatricula()
        {
            var matriculas = (
                from turma in _context.Turma
                from matricula in turma.Alunos
                join aluno in _context.Aluno on matricula.Id equals aluno.Id
                select new
                {
                    Matricula = aluno.Id,
                    Aluno = aluno.Nome,
                    Turma = turma.Nome
                }
            ).ToList();
            if (matriculas.Count == 0)
            {
                return NotFound("Nenhuma matrícula encontrada");
            }
            else{
                return Ok(matriculas);
            }
        }

        [HttpDelete]
        public IActionResult DeleteMatricula(int alunoId, int turmaId)
        {
            var aluno = _context.Aluno.FirstOrDefault(a => a.Id == alunoId);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            var turma = _context.Turma.Include(t => t.Alunos).FirstOrDefault(t => t.Id == turmaId);
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            turma.Alunos.Remove(turma.Alunos.Where(a => a.Id == alunoId).FirstOrDefault());
            _context.SaveChanges();

            return Ok("Matrícula cancelada com sucesso");
        }
    }
}
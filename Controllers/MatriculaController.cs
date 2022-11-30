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
        public IActionResult Post(int alunoId, int turmaId)
        {
            Aluno aluno = new Aluno { Id = alunoId };
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            _context.Aluno.Add(aluno);
            _context.Aluno.Attach(aluno);

            Turma turma = new Turma { Id = turmaId };
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }
            _context.Turma.Add(turma);
            _context.Turma.Attach(turma);

            turma.Alunos.Add(aluno);
            _context.SaveChanges();

            return Ok("Matrícula realizada com sucesso");
        }

        [HttpGet]
        public IActionResult Get()
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
            if (matriculas == null)
            {
                return NotFound("Nenhuma matrícula encontrada");
            }
            else{
                return Ok(matriculas);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int alunoId, int turmaId)
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
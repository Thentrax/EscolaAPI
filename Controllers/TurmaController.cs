using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Models;
using EscolaAPI.Context;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly EscolaContext _context;
        public TurmaController(EscolaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTurma()
        {
            var turmas = _context.Turma.ToList();
            if (turmas.Count == 0)
            {
                return NotFound("Nenhuma turma encontrada");
            }
            else{
                return Ok(turmas);
            }
        }

        [HttpPost]
        public IActionResult PostTurma(Turma turma)
        {
            if (turma == null)
            {
                return BadRequest("Turma inválida");
            }
            else{
                _context.Turma.Add(turma);
                _context.SaveChanges();
                return Ok("Turma cadastrada com sucesso");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutTurma(int id, Turma turma)
        {
            var turmaUpdate = _context.Turma.FirstOrDefault(t => t.Id == id);
            if (turmaUpdate == null)
            {
                return NotFound("Turma não encontrada");
            }
            else{
                turmaUpdate.AnoLetivo = turma.AnoLetivo;
                _context.SaveChanges();
                return Ok("Turma atualizada com sucesso");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurma(int id)
        {
            var turmaDelete = _context.Turma.Include(t => t.Alunos).FirstOrDefault(t => t.Id == id);
            if (turmaDelete == null)
            {
                return NotFound("Turma não encontrada");
            }
            if (turmaDelete.Alunos.Count != 0)
            {
                return BadRequest("Turma possui alunos cadastrados");
            }
            else{
                _context.Turma.Remove(turmaDelete);
                _context.SaveChanges();
                return Ok("Turma excluída com sucesso");
            }
        }
    }
}
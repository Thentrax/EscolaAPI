using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.Models
{
    public class Aluno
    {
        public Aluno(){
            this.Turmas = new HashSet<Turma>();
        }
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual ICollection<Turma> Turmas { get; set; }
    }
}
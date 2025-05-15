using System.ComponentModel.DataAnnotations;

namespace BibliotecaVMC.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres.")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O autor não pode ter mais de 100 caracteres.")]
        public required string Autor { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50, ErrorMessage = "A categoria não pode ter mais de 50 caracteres.")]
        public required string Categoria { get; set; }

        [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "O ano deve estar entre 1 e 9999.")]
        public int AnoPublicacao { get; set; }
    }
}
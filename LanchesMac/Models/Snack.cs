﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Snacks")]
    public class Snack
    {
        [Key]
        public int SnackId { get; set; }

        [StringLength(80, MinimumLength = 10,  ErrorMessage = "O {0} deve ter no minimo {1} e no máximo {2}")]
        [Required(ErrorMessage = "O nome do lanche deve ser informado.")]
        [Display(Name = "Nome do lanche")]
        public string Name { get; set; }

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no minimo {1} e no máximo {2}")]
        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Nome do lanche")]
        public string ShortDescription { get; set; }

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no minimo {1} e no máximo {2}")]
        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Nome do lanche")]
        public string DetailedDescription { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche.")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Price { get; set; }

        [Display(Name ="Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="O {0} deve ter no máximo {1} caracteres")]
        public string ImageUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Prefereido?")]
        public bool IsFavoriteSnack { get; set; }

        [Display(Name = "Estoque")]
        public bool InStock { get; set; }

        //Definir relacionamento entre Snack e Category.
        public int CategoryId { get; set; } // Mapeado como chave estrangeira.
        public virtual Category Category { get; set; } // Propriedade de navegação.

    }
}

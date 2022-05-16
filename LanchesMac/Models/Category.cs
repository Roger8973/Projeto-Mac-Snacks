using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categories")]
    public class Category
    {
        //Definindo as propriedades com Data Anotations
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100, ErrorMessage ="O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage ="Informe o nome da categoria.")]
        [Display(Name ="Nome")]
        public string CategoryName { get; set; }


        [StringLength(100, ErrorMessage = "O tamanho máximo é 200 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição da categoria.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        //Criar relacionamento 1 Category para muitos Snacks
        public List<Snack> snacks { get; set; }
    }
}

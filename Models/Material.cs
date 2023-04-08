using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestVegaMVC.Models
{
  public class Material
  {
    [Key]
    public int Id { get; set; }

    [Display(Name = "Fornecedor")]
    public int IdSupplier { get; set; }

    [ForeignKey("IdSupplier")]
    [Display(Name = "Fornecedor")]
    public Supplier? Supplier { get; set; }

    [Required(ErrorMessage = "O código do material é obrigatório")]
    [MaxLength(20, ErrorMessage = "O código do material não pode ter mais que 20 caracteres")]
    [Display(Name = "Código")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "O nome do material é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome do material não pode ter mais que 50 caracteres")]
    [Display(Name = "Nome")]
    public string? Name { get; set; }

    [MaxLength(300, ErrorMessage = "A descrição do material não pode ter mais que 300 caracteres")]
    [Display(Name = "Descrição")]
    public string? Description { get; set; }

    [MaxLength(20, ErrorMessage = "O código fiscal do material não pode ter mais que 20 caracteres")]
    [Display(Name = "Código Fiscal")]
    public string? FiscalCode { get; set; }

    [MaxLength(50, ErrorMessage = "A espécie do material não pode ter mais que 50 caracteres")]
    [Display(Name = "Espécie")]
    public string? Specie { get; set; }

    [Required(ErrorMessage = "A data de criação do material é obrigatória")]
    [Display(Name = "Criado em")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "O criador do material é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome do criador do material não pode ter mais que 50 caracteres")]
    [Display(Name = "Criado por")]
    public string? CreatedBy { get; set; }

    [Display(Name = "Atualizado em")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(50, ErrorMessage = "O nome do atualizador do material não pode ter mais que 50 caracteres")]
    [Display(Name = "Atualizado por")]
    public string? UpdatedBy { get; set; }
  }
}
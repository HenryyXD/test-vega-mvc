using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestVegaMVC.Models
{
  public class Supplier
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo nome é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo Nome deve ter no máximo 50 caracteres.")]
    [Display(Name = "Nome")]
    public string? Name { get; set; }

    [RegularExpression(@"^\(\d{2}\)( |)(\d|)( |)\d{4}(-|)\d{4}$", ErrorMessage = "Número de telefone inválido")]
    [Display(Name = "Telefone")]
    public string? PhoneNumber { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.[a-z]+(\.[a-z]+)?$", ErrorMessage = "Email inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Campo CNPJ é obrigatório")]
    [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}|(\d{14})$", ErrorMessage = "CNPJ inválido")]
    public string? CNPJ { get; set; }

    [Required(ErrorMessage = "Campo CEP é obrigatório")]
    [RegularExpression(@"^[0-9]{5}-[0-9]{3}|([0-9]{8})$", ErrorMessage = "CEP inválido")]
    public string? CEP { get; set; }

    public string? QRCode { get; } = "%CNPJ% - %CEP% / %DATACADASTRO%";

    [NotMapped]
    public string? FormattedQRCode
    {
        get
        {
            return QRCode?.Replace("%CNPJ%", CNPJ)
                          .Replace("%CEP%", CEP)
                          .Replace("%DATACADASTRO%", CreatedAt.ToShortDateString());
        }
    }


    [StringLength(255, ErrorMessage = "O campo Endereço deve ter no máximo 255 caracteres.")]
    [Display(Name = "Endereço")]
    public string? Address { get; set; }

    [Display(Name = "Criado em")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Criado por")]
    [Required(ErrorMessage = "Campo Criado por é obrigatório")]
    public string? CreatedBy { get; set; }

    [Display(Name = "Atualizado em")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "Atualizado por")]
    public string? UpdatedBy { get; set; }

    public ICollection<Material>? Materials { get; set; }
  }
}
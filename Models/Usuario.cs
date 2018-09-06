namespace BDVendaDireta.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Produto = new HashSet<Produto>();
        }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe o seu nome", AllowEmptyStrings = false)]
        [StringLength(60)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a sua senha", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string Senha { get; set; }

        [Display(Name = "Total Vendido")]
        public decimal Receita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}

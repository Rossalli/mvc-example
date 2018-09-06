namespace BDVendaDireta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Produto")]
    public partial class Produto
    {
        public int ProdutoId { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public bool Vendido { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

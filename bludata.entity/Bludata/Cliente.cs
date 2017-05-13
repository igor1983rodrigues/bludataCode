namespace bludata.entity.Bludata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cliente", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        [Column("codigo")]
        public int ClienteCodigo { get; set; }

        [Required]
        [Column("nome", Order = 1)]
        [Display(Name = "Nome")]
        [StringLength(64)]
        public string ClienteNome { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "UF")]
        [Column("uf")]
        public string UfCodigo { get; set; }

        [Required]
        [StringLength(14)]
        [Display(Name = "CNPJ")]
        [Column("CNPJ")]
        public string ClienteCnpj { get; set; }

        [NotMapped]
        public Uf Uf { get; set; }
    }
}

namespace bludata.entity.Bludata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("uf", Schema = "dbo")]
    public partial class Uf
    {
        [Key]
        [StringLength(2)]
        [Column("codigo")]
        public string UfCodigo { get; set; }

        [Required]
        [Column("nome")]
        [StringLength(64)]
        public string UfNome { get; set; }

        [Column("cadastro", TypeName = "tinyint")]
        public bool UfCadastro { get; set; }
    }
}

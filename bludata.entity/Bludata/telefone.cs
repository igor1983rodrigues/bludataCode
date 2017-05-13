namespace bludata.entity.Bludata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("telefone", Schema = "dbo")]
    public partial class Telefone
    {
        [Key]
        [Column("codigo")]
        public int TelefoneCodigo { get; set; }

        [Required]
        [Column("ddd")]
        [StringLength(3)]
        public string TelefoneDdd { get; set; }

        [Required]
        [Column("numero")]
        [StringLength(9)]
        public string TelefoneNumero { get; set; }

        [Column("tipo_telefone")]
        public int TipoTelefoneCodigo { get; set; }

        [Column("pessoa")]
        public int PessoaCodigo { get; set; }

        public Pessoa Pessoa { get; set; }

        public TipoTelefone TipoTelefone { get; set; }
    }
}

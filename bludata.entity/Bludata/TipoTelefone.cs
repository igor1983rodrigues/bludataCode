namespace bludata.entity.Bludata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tipo_telefone", Schema = "dbo")]
    public class TipoTelefone
    {
        [Key]
        [Column("codigo")]
        public int TipoTelefoneCodigo { get; set; }

        [Required]
        [Column("descricao")]
        [StringLength(32)]
        public string TipoTelefoneDescricao { get; set; }
    }
}

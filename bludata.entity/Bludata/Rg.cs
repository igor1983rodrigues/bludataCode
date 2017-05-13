namespace bludata.entity.Bludata
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("rg", Schema = "dbo")]
    public class Rg
    {
        [Key]
        [Column("codigo")]
        public int PessoaCodigo { get; set; }

        [Column("rg")]
        public int RgNumero { get; set; }

        [Required]
        [Column("orgao")]
        [StringLength(64)]
        public string RgOrgao { get; set; }

        [Required]
        [Column("dt_expedicao")]
        public DateTime RgDataExpedicao { get; set; }

        [Required]
        [Column("uf")]
        [StringLength(2)]
        public string UfCodigo { get; set; }

        [NotMapped]
        public Pessoa Pessoa { get; set; }

        [NotMapped]
        public Uf Uf { get; set; }
    }
}

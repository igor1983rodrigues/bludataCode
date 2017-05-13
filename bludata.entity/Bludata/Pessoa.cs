namespace bludata.entity.Bludata
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("pessoa", Schema = "dbo")]
    public class Pessoa
    {
        [Key]
        [Column("codigo")]
        public int PessoaCodigo { get; set; }

        [Required]
        [Column("nome", Order = 1)]
        [Display(Name ="Nome")]
        [StringLength(64)]
        public string PessoaNome { get; set; }

        [Required]
        [Column("cpf")]
        [Display(Name = "CPF")]
        [StringLength(11)]
        public string PessoaCpf { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [Column("dt_nascimento")]
        public DateTime PessoaDataNascimento { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [Column("dt_cadastro")]
        public DateTime PessoaDataCadastro { get; set; }

        [Required]
        [Column("uf")]
        [Display(Name = "UF")]
        [StringLength(2)]
        public string UfCodigo { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        [Column("cliente")]
        public int ClienteCodigo { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }

        [NotMapped]
        public Uf Uf { get; set; }

        [NotMapped]
        public Rg Rg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiControleEstoque.Models
{
    public partial class Estoque
    {
        public Estoque() 
        {

        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        [StringLength(200)]
        public string Name { get; set; }

        [Column("Quantidade")]
        public int Quantidade { get; set; }
    }
}

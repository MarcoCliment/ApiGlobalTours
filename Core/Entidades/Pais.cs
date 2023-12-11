using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Pais
    {
        [Key]
        public int PaisId { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }
    }
}
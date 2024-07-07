using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationExamenPOO.Models
{
    public class Paises
    {
        [StringLength(3, ErrorMessage = "El id del país debe tener 3 caracteres")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "El id del país solo puede contener exactamente 3 números")]
        public string Idpais { get; set; }
        [StringLength(40, ErrorMessage ="Nombre de pais no valido")] public string NombrePais { get; set; }
    }
}
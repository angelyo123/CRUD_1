using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationExamenPOO.Models
{
    public class Consumidor
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        [StringLength(5, ErrorMessage = "El id debe tener 5 caracteres")]
        public string idConsumidor { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(40, ErrorMessage = "El nombre debe tener un máximo de 40 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(40, ErrorMessage = "El apellido debe tener un máximo de 40 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(250, ErrorMessage = "La dirección debe tener un máximo de 250 caracteres")]
        public string Direccion { get; set; }

        [StringLength(3, ErrorMessage = "El id del país debe tener 3 caracteres")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "El id del país solo puede contener exactamente 3 números")]
        public string IdPais { get; set; }

        [Required(ErrorMessage ="el email no puede estar vacio")]
        [EmailAddress(ErrorMessage = "El email no es una dirección de correo electrónico válida")]
        public string Email { get; set; }
    }
}
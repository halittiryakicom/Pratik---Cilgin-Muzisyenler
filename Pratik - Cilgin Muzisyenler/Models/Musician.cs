using System.ComponentModel.DataAnnotations;

namespace Pratik___Cilgin_Muzisyenler.Models
{
    public class Musician
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Müzisyen isimi gereklidir.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Müzisyen isimi 3 ile 30 karakter arasında olmalı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Müzisyen mesleği gereklidir.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Müzisyen mesleği 3 ile 30 karakter arasında olmalı")]
        public string Job { get; set; }
        [Required(ErrorMessage = "Müzisyen eğlenceli özelliği gereklidir.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Müzisyen eğlenceli özelliği karakter arasında olmalı")]
        public string FunProperty { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi.Models.Dto
{
    public class VillaDto
    {
        public Guid Id { get; set; }
        //[Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}

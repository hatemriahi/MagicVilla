using MagicVilla_WebApi.Models.Dto;

namespace MagicVilla_WebApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto{ Id = new Guid("1a004ff8-0413-4d42-9b83-9128dc54d3f7"), Name = "Villa Tunis" },
            new VillaDto{ Id = new Guid("beee5cc5-d153-4622-9baf-590c1bb7430c"), Name = "Villa Marsa" },
        };
    }
}

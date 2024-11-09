﻿using MagicVilla_Web.Models.Dtos.VillaNumberDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels.villaNumberViewModels
{
    public class VillaNumberDeleteViewModel
    {
        public VillaNumberDeleteViewModel()
        {
            VillaNumber = new VillaNumDto();
        }

        public VillaNumDto VillaNumber { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}

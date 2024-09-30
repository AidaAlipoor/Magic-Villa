﻿namespace Business.ViewModels
{
    public sealed class VillaNumberViewModel
    {
        public int Id { get; set; }
        public int VillaId { get; set; }
        public int VillaNumber { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public  VillaViewModel Villa { get; set; }
    }
}

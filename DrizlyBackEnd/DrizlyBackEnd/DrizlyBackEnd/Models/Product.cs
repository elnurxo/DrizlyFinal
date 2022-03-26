using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 85)]
        public string Name { get; set; }
        [StringLength(maximumLength: 800)]
        public string Desc { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }    
        public bool IsPacket { get; set; }
        public int? ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }
        public int? ProductCountId { get; set; }
        public ProductCount ProductCount { get; set; }
        public int TypeProductId { get; set; }
        public TypeProduct TypeProduct { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 100)]
        public decimal? Abv { get; set; }
        public int? SweetDryScaleId { get; set; }
        public SweetDryScale sweetDryScale { get; set; }
        public string CodePref { get; set; }
        public int CodeNum { get; set; }
        public int? LiquorFlavorId { get; set; }
        public LiquorFlavor LiquorFlavor { get; set; }
        public int CategoryId { get; set; }
        public LiquorColor LiquorColor { get; set; }
        public int? LiquorColorId { get; set; }
        public WineFoodPairing WineFoodPairing { get; set; }
        public int? WineFoodPairingId { get; set; }
    }
}

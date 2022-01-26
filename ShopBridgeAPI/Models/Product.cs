//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBridgeAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Product Name Required")]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Description Required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MinLength(4)]
        public string Description { get; set; }

        [Required(ErrorMessage ="Product Price Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        public decimal Price { get; set; }
    }
}

﻿namespace Pronia.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductPhoto> ProductPhotos { get; set;}
        public List<Tag> Tags { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
﻿namespace MVC.Entity
{
    public class Course : IEntity
    {
        public int Id { get; set; } 
        public string ImageUrl { get; set; } 
        public string Description { get; set; } 
        public double Price { get; set; }

    }
}
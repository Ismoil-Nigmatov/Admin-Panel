﻿namespace MVC.Entity
{
    public class Homework : IEntity
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public Task Task { get; set; }
    }
}

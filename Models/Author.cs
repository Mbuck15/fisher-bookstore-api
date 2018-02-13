using System;

namespace Fisher.Bookstore.Api.Models
{

    public class Author
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime DateOfBirth  {get; set;}
        public int NumberOfWorksPublished {get; set;}
        public string Publisher {get; set;}
    }
}
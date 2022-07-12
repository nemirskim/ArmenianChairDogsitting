using ArmenianChairDogsitting.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Tests;

public class ClientsTestCaseSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        //var clients = new List<Client>
        //{
        //    new Client
        //    {
        //        Id = 3,
        //        Name = "Mikhal",
        //        LastName = "Yankowski",
        //        Dogs = new()
        //            { new() { Id = 5, Name = "Puck" }},
        //        IsDeleted = false
        //    },

        //    new Client
        //    {
        //        Id = 4,
        //        Name = "Katerina",
        //        LastName = "Sharikova",
        //        Dogs = new()
        //            { new() { Id = 65, Name = "Boraks" }},
        //        IsDeleted = false
        //    }
        //};

        var newClient = new Client
        {
            Id = 5,
            Name = "Elizaveta",
            LastName = "Korosten",
            //Dogs = new()
                    //{ new() { Id = 65, Name = "Boraks" }},
        };

        var expected = new Client
        {
            Id = 5,
            Name = "Elizaveta",
            LastName = "Korosten",
            IsDeleted = false
        };    
        
        yield return new object[] {newClient, expected};
    }
}

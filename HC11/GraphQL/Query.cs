using HC11.Models;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC11.GraphQL
{
    public class Query
    {
        [UsePaging]
        public string[] Letters => new[]
                    {
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "g",
                "h",
                "i",
                "j",
                "k",
                "l"
            };
        [Authorize]
        [UseProjection]
        [HotChocolate.Types.UseFiltering]
        public IQueryable<TestHC11> GetTest()
        {
            var test = new List<TestHC11>();
            test.Add(new TestHC11() { 
            Id=1,
            Name="Prashant"
            });
            test.Add(new TestHC11()
            {
                Id = 2,
                Name = "kumar"
            });
            test.Add(new TestHC11()
            {
                Id = 3,
                Name = "Jacob"
            });
            test.Add(new TestHC11()
            {
                Id = 4,
                Name = "Michelle"
            });
            test.Add(new TestHC11()
            {
                Id = 5,
                Name = "Test"
            });
            return test.AsQueryable();
        }
    }

}

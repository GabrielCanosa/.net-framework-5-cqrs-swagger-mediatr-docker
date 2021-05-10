using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerizarWebApi.Repository
{
    public class Repository
    {
        public List<Todo> todoList { get; } = new List<Todo>()
        {
            new Todo { Id = 1, Name= "Wake up", Completed = true },
            new Todo { Id = 2, Name = "Breakfast", Completed = true },
            new Todo { Id = 3, Name = "Work", Completed = false },
            new Todo { Id = 4, Name = "Relax", Completed = false }
        };
    }
}

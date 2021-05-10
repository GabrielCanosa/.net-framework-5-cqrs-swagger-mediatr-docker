using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Commands
{
    public static class AddTodo
    {
        // Request / Command
        public record Command(string Name, bool Completed) : IRequest<int>;

        // Handler
        public class Handler : IRequestHandler<Command, int>
        {
            //public Repository.Repository Repository { get; }
            //public Handler(Repository.Repository repository)
            //{
            //    Repository = repository;
            //}

            public ApplicationDbContext.ApplicationDbContext DbContext { get; }
            public Handler(ApplicationDbContext.ApplicationDbContext dbContext)
            {
                DbContext = dbContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                DbContext.Todos.Add(new Todo
                {
                     Name = request.Name, Completed = request.Completed
                });
                DbContext.SaveChanges();

                return DbContext.Todos.FirstOrDefault(x => x.Name == request.Name).Id;
            }
        }

        // Response

    }
}

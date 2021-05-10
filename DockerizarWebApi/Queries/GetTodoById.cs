using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Queries
{
    public static class GetTodoById
    {
        // Query / Command
        // All the data we need to execute
        public record Query(int id) : IRequest<Response>;

        // Handler
        // All the business logic to execute. return a response
        public class Handler : IRequestHandler<Query, Response>
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

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                // All the business logic
                var todo = DbContext.Todos.FirstOrDefault(x => x.Id == request.id);
                return todo == null ? null : new Response(todo.Id, todo.Name, todo.Completed);
            }
        }

        // Response
        // The data we want to return
        public record Response(int id, string Name, bool Completed);
    }
}

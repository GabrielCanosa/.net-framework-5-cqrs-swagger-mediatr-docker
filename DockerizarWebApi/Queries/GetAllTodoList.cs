using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Queries
{
    public static class GetAllTodoList
    {
        public record Query() : IRequest<Response>;

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
                var list = DbContext.Todos.ToList();
                return list.Count > 0 ? new Response(list) : null;
            }
        }

        public record Response(List<Todo> todoList);
    }
}

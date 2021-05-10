using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Commands
{
    public static class DeleteAllTodoList
    {
        public record Command() : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
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

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                List<Todo> listPreview = DbContext.Todos.ToList();
                DbContext.Todos.RemoveRange(listPreview);
                DbContext.SaveChanges();
                List<Todo> list = DbContext.Todos.ToList();
                return list.Count > 0 ? new Response(false) : new Response(true);
            }
        }

        public record Response(bool deleted);
    }
}

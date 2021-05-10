using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Commands
{
    public static class UpdateTodo
    {
        public record Command(Todo todo) : IRequest<Response>;

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
                Todo todo = DbContext.Todos.FirstOrDefault(x => x.Id == request.todo.Id);
                todo.Name = request.todo.Name;
                todo.Completed= request.todo.Completed;
                DbContext.SaveChanges();
                return todo == null ? null : new Response(todo);
            }
        }

        public record Response(Todo todo);
    }
}

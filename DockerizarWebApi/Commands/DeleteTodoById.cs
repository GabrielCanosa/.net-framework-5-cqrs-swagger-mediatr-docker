using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Commands
{
    public static class DeleteTodoById
    {
        public record Command(int id) : IRequest<Response>;

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
                Todo todo = DbContext.Todos.FirstOrDefault(x => x.Id == request.id);
                DbContext.Todos.Remove(todo);
                DbContext.SaveChanges();
                List<Todo> todoList = DbContext.Todos.ToList();
                return todoList.Count > 0 ? new Response(todoList) : null;
            }
        }

        public record Response(List<Todo> todoList);
    }
}

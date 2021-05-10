using DockerizarWebApi.Commands;
using DockerizarWebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerizarWebApi.Controllers
{
    [Route("/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public IMediator Mediator { get; }
        public TodoController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodoList()
        {
            var response = await Mediator.Send(new GetAllTodoList.Query());
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var response = await Mediator.Send(new GetTodoById.Query(id));
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodo.Command command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> RemoveTodoById(int id)
        {
            var response = await Mediator.Send(new DeleteTodoById.Command(id));
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAllTodoList()
        {
            var response = await Mediator.Send(new DeleteAllTodoList.Command());
            return response.deleted ? Ok(response) : NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo(UpdateTodo.Command command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

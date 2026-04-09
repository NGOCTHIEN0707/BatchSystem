using MediatR;

namespace BatchSystem.Application.Commands.Lines.Create
{
    public class CreateLineCommand : IRequest<bool>
    {
        public string? LineName { get; set; }
        public string? LineCode { get; set; }

    }
}

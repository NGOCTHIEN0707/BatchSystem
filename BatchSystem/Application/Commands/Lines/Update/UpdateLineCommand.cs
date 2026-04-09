using MediatR;

namespace BatchSystem.Application.Commands.Lines.Update
{
    public class UpdateLineCommand : IRequest<bool>
    {
        public string LineId { get; set; }
        public string? LineName { get; set; }
        public string? LineCode { get; set; }
    }
}

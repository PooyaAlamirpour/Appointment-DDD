using ErrorOr;
using MediatR;

namespace Appointment.Application.Common.Messages
{
    internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : IErrorOr
    {
    }
}
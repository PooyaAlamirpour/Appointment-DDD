using ErrorOr;
using MediatR;

namespace Charisma.Application.Common.Messages
{
    internal interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : IErrorOr
    {
    }
}
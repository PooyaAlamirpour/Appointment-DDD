using ErrorOr;
using FluentValidation;

namespace Appointment.Application.Common.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
            Error error) => rule.WithErrorCode(error.Code).WithMessage(error.Description);
    }
}
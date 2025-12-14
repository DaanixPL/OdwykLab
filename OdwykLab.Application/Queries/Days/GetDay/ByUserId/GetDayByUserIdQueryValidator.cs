using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Queries.Days.GetDay.ByUserId
{
    public class GetDayByUserIdQueryValidator : AbstractValidator<GetDayByUserIdQuery>
    {
        public GetDayByUserIdQueryValidator()
        {
            RuleFor(x => x.userId)
                .RequiredField(nameof(GetDayByUserIdQuery.userId));
        }
    }
}

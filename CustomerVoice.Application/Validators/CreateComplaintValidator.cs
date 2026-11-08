using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.DTOs.Complaints;
using FluentValidation;


namespace CustomerVoice.Application.Validators
{
    public class CreateComplaintValidator
        : AbstractValidator<CreateComplaintDto>
    {
        public CreateComplaintValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام الزامی است.")
                .MaximumLength(100)
                .WithMessage("نام نباید بیشتر از ۱۰۰ کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(100)
                .WithMessage("نام خانوادگی نباید بیشتر از ۱۰۰ کاراکتر باشد.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("شماره موبایل الزامی است.")
                .MaximumLength(20)
                .WithMessage("شماره موبایل معتبر نیست.");

            RuleFor(x => x.LandlineNumber)
                .MaximumLength(20)
                .WithMessage("تلفن ثابت نباید بیشتر از ۲۰ کاراکتر باشد.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("فرمت ایمیل معتبر نیست.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("انتخاب پروژه الزامی است.");

            RuleFor(x => x.ServiceTypeId)
                .GreaterThan(0)
                .WithMessage("انتخاب نوع خدمات الزامی است.");

            RuleForEach(x => x.DissatisfactionReasonIds)
                .GreaterThan(0)
                .WithMessage("شناسه علت نارضایتی معتبر نیست.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("شرح شکایت الزامی است.")
                .MinimumLength(10)
                .WithMessage("شرح شکایت باید حداقل ۱۰ کاراکتر باشد.")
                .MaximumLength(4000)
                .WithMessage("شرح شکایت نباید بیشتر از ۴۰۰۰ کاراکتر باشد.");

            RuleFor(x => x.SuggestedSolution)
                .MaximumLength(4000)
                .WithMessage("راه‌حل پیشنهادی نباید بیشتر از ۴۰۰۰ کاراکتر باشد.");
        }
    }
}
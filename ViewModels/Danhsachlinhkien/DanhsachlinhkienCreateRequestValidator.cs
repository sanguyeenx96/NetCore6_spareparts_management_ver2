using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.System.User;

namespace ViewModels.Danhsachlinhkien
{
    public class DanhsachlinhkienCreateRequestValidator : AbstractValidator<DanhsachlinhkienCreateRequest>
    {
        public DanhsachlinhkienCreateRequestValidator()
        {
            RuleFor(x => x.Tenlinhkien).NotEmpty().WithMessage("Part name is required");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model name is required");
            RuleFor(x => x.Malinhkien).NotEmpty().WithMessage("Model name is required");

        }
    }
}

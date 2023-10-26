using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

        public class RouteInstruction
        {
            public int Stage { get; set; }
            public string Description { get; set; } = string.Empty;

        }

    }

    public class TrailDtoValidator: AbstractValidator<TrailDto> 
    {
        public TrailDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length");
            RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter the time it takes to cover this trail.");
            RuleFor(x => x.Route).Must(x => x.Count > 0).WithMessage($"Please enter at least one route instruction");
            RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
        }
    }

    public class RouteInstructionValidator: AbstractValidator<TrailDto.RouteInstruction>
    {
        public RouteInstructionValidator()
        {
            RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        }
    }
}

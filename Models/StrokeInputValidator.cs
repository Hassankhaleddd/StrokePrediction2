namespace StrokePrediction.Models
{
    public class StrokeInputValidator : AbstractValidator<StrokeInput>
    {
           public StrokeInputValidator()
        {
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");

            RuleFor(x => x.Age).InclusiveBetween(0, 120).WithMessage("Age must be between 0 and 120.");

            RuleFor(x => x.Hypertension).Must(x => x == 0 || x == 1)
                  .WithMessage("Hypertension must be 0 (no Hypertension) or 1 (has Hypertension).");


            RuleFor(x => x.HasHeartDisease).Must(x => x == 0 || x == 1)
                .WithMessage("Heart Disease must be 0 (no heart diseases) or 1 (has heart diseases).");

            RuleFor(x => x.EverMarried).NotNull().WithMessage("Marital status is required.");

            RuleFor(x => x.WorkType).NotEmpty().WithMessage("Work Type is required.");

            RuleFor(x => x.Residence_type).NotEmpty().WithMessage("Residence Type is required.");

            RuleFor(x => x.avg_glucose_level).GreaterThan(0).WithMessage("Glucose Level must be greater than 0.");

            RuleFor(x => x.Bmi).GreaterThan(0).WithMessage("BMI must be greater than 0.");

            RuleFor(x => x.smoking_status).NotEmpty().WithMessage("Smoking Status is required.");
        }
    }
  }

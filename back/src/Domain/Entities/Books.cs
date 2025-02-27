using FluentValidation;

namespace Domain.Entities
{
    public class Books : Entity<Books>
    { 
        public Books(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }

        public override bool IsValid()
        {
            var validator = new InlineValidator<Books>();
            validator.RuleFor(book => book.Title)
                     .NotEmpty().WithMessage("O título é obrigatório.")
                     .Length(10, 100).WithMessage("O título deve ter entre 10 e 100 caracteres.");

            validator.RuleFor(book => book.Author)
                    .NotEmpty().WithMessage("O autor é obrigatório.")
                    .Length(10, 100).WithMessage("O autor deve ter entre 10 e 100 caracteres.");

            validator.RuleFor(book => book.Description)                        
                        .MaximumLength(1024).WithMessage("A descrição deve ter no m[aximo 1024 caracteres.");

            ValidationResult = validator.Validate(this);

            return ValidationResult.IsValid;            
        }
    }
}

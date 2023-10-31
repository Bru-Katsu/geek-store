using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Customer.Domain.Customers
{
    public class Customer : Entity
    {
        protected Customer() { }
        public Customer(Guid userId, string name, string surname, DateTime birthday, string document, string email)
        {
            Id = Guid.NewGuid();
            UserId = userId;

            Name = name;
            Surname = surname;
            Birthday = birthday;
            Document = document;

            Addresses = new List<CustomerAddress>();
            Email = email;
        }

        public Guid UserId { get; private set; }

        public string Name { get; private set; }
        public string Surname { get; private set; }

        public void SetName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public DateTime Birthday { get; private set; }
        public void SetBirthday(DateTime date)
        {
            Birthday = date;
        }

        public string Document { get; private set; }
        public void SetDocument(string document)
        {
            Document = document;
        }

        public virtual ICollection<CustomerAddress> Addresses { get; set; }
        public void AddAddress(CustomerAddress address)
        {
            Addresses.Add(address);
        }

        public void RemoveAddress(CustomerAddress address)
        {
            if (!Addresses.Contains(address))
                throw new InvalidOperationException("Endereço não existe!");
            
            Addresses.Remove(address);
        }

        public string ProfileImage { get; private set; }
        public void ChangeProfileImage(string profileImage)
        {
            ProfileImage = profileImage;
        }

        public string Email { get; set; }
        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerEntityValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CustomerEntityValidator : AbstractValidator<Customer>
        {
            public CustomerEntityValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Nome não pode ficar vazio!")
                    .MaximumLength(255)
                    .WithMessage("O nome deve conter no máximo 255 caracteres!"); ;

                RuleFor(x => x.Surname)
                    .NotEmpty()
                    .WithMessage("Sobrenome não pode ficar vazio!")
                    .MaximumLength(255)
                    .WithMessage("O sobrenome deve conter no máximo 255 caracteres!");

                RuleFor(x => x.Birthday)
                    .LessThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data de nascimento inválida!");

                RuleFor(x => x.Document)
                    .NotEmpty()                    
                    .WithMessage("Infome o documento de identificação!")
                    .MaximumLength(100)
                    .WithMessage("O documento de identificação deve conter no máximo 100 caracteres!");

                RuleFor(x => x.ProfileImage)
                    .MaximumLength(300)
                    .WithMessage("O link de imagem do perfil deve conter no máximo 300 caracteres!");

                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Endereço de e-mail inválido!")
                    .NotEmpty()
                    .WithMessage("O e-mail não pode ficar em branco!")
                    .MaximumLength(512)
                    .WithMessage("O e-mail deve conter no máximo 512 caracteres!");
            }
        }
    }
}

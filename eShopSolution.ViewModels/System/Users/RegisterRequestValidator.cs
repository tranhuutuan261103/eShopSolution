﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required").MaximumLength(200).WithMessage("First name is limited to 200 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required").MaximumLength(200).WithMessage("Last name is limited to 200 characters");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is not null").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format is not correct");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot be greater than 100 years");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("Password is at least 6 characters");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Confirm password is not match");
        }
    }
}

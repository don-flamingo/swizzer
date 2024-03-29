﻿using Prism.Commands;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Mapper;
using Swizzer.Client.Services;
using Swizzer.Client.Validators;
using Swizzer.Shared.Common.Domain.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Swizzer.Client.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly RegisterViewModelValidator _registerViewModelValidator;

        private string _email;
        private string _password;
        private string _repeatePassword;
        private string _name;
        private string _surname;

        private string _error;

        public string Email { get => _email; set => SetPropertyAndValidate(ref _email, value); }
        public string Password { get => _password; set => SetPropertyAndValidate(ref _password, value); }
        public string RepeatPassword { get => _repeatePassword; set => SetPropertyAndValidate(ref _repeatePassword, value); }
        public string Name { get => _name; set => SetPropertyAndValidate(ref _name, value); }
        public string Surname { get => _surname; set => SetPropertyAndValidate(ref _surname, value); }
        public string Error { get => _error; set => SetProperty(ref _error, value); }

        public ICommand RegisterCommand { get; private set; }
        public ICommand GoToLoginCommand { get; private set; }

        public RegisterViewModel(INavigationService navigationService,
            RegisterViewModelValidator registerViewModelValidator,
            IViewModelFacade viewModelFacade) : base(viewModelFacade)
        {
            this._navigationService = navigationService;
            this._registerViewModelValidator = registerViewModelValidator;

            GoToLoginCommand = new DelegateCommand(GoToLogin, CanGoToLogin);
            RegisterCommand = new DelegateCommand(Register, CanRegister)
                .ObservesProperty(() => Error);
        }

        public void SetPropertyAndValidate<TValue>(ref TValue storage, TValue value, [CallerMemberName] string propertyName = null)
        {
            SetProperty(ref storage, value, propertyName);
            CanRegister();
        }

        private void GoToLogin()
        {
            _navigationService.GoToAsync<LoginViewModel>();
        }

        private bool CanGoToLogin()
        {
            return true;
        }

        private async void Register()
        {
            var registerCommand = MapTo<UserRegisterCommand>(this);
            await DispatchCommandAsync(registerCommand);
        }

        private bool CanRegister()
        {
            var errors =  _registerViewModelValidator.Validate(this);
            Error = errors.Errors.FirstOrDefault()?.ErrorMessage;
            
            return errors.IsValid;
        }
    }
}

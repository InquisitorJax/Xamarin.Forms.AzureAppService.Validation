﻿using Autofac;
using FluentValidation;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Validation.Shared;

namespace Validation.Client.XForms.Custom
{
    public class MainPageViewModel : BindableBase
    {
        private TodoItem _model;

        public MainPageViewModel()
        {
            SaveCommand = DelegateCommand.FromAsyncHandler(SaveAsync);
            Model = new TodoItem();
        }

        public TodoItem Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public ICommand SaveCommand { get; private set; }

        private async Task SaveAsync()
        {
            if (Validate())
            {
                bool saved = await SaveModelToServer();
                if (saved)
                {
                    Model = new TodoItem();
                }
            }
        }

        private async Task<bool> SaveModelToServer()
        {
            bool saved = true;
            var service = Wibci.IoC.Resolve<IMobileServiceClient>();
            try
            {
                Model.Message = "saving....";
                await service.GetTable<TodoItem>().InsertAsync(Model);
                Model.Message = null;
            }
            catch (Exception ex)
            {
                saved = false;
                Model.Message = $"Something went wrong with the service call :( {ex.Message}";
            }
            return saved;
        }

        /// <summary>
        /// Make sure the model is valid before sending to the server
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            object validationInstance;
            Wibci.IoC.TryResolveNamed(nameof(ITodoItem), typeof(IValidator), out validationInstance);
            var validator = (IValidator)validationInstance;

            var validationResult = validator.Validate(Model);

            IList<string> propertiesWithErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                propertiesWithErrors.Add(error.PropertyName);
            }

            //The UI validation is listening to property change events
            //actively raise event for those properties which might be in default invalid state (user hasn't actioned a change from the UI)
            Model.RequestValidation(propertiesWithErrors);

            return validationResult.Errors.Count == 0;
        }
    }
}
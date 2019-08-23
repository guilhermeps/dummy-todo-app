using System;
using System.Collections.Generic;
using DummyTodoApp.WebApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DummyTodoApp.WebApi.StartupFilters
{
    public class SettingsValidationStartupFilter : IStartupFilter
    {
        readonly IEnumerable<IValidatable> validatableObjects;

        public SettingsValidationStartupFilter(IEnumerable<IValidatable> validate) => validatableObjects = validate;

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var objectToBeValidated in validatableObjects)
                objectToBeValidated.Validate();

            return next;
        }
    }
}
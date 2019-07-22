using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimCoDesktopUI.Helpers;
using TimCoDesktopUI.ViewModels;

namespace TimCoDesktopUI
{
    class Bootstrapper :BootstrapperBase
    {
        private SimpleContainer _container=new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAPIHelper, APIHelper>();

            GetType().Assembly.GetTypes() //get all types in the problem
                .Where(type => type.IsClass) //limit to only classes
                .Where(type => type.Name.EndsWith("ViewModel")) //limit to only classes which end with "ViewModel"
                .ToList() //make a list
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType)); //which each entry, get service,key,implementation.
            //TODO makes it harder to unit test; might change to interfaces later.

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}

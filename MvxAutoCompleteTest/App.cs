using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MvxAutoCompleteTest.ViewModels;
using System;

namespace MvxAutoCompleteTest
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart(new CustomAppStart());
        }
    }

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {

        public void Start(object hint = null)
        {
            ShowViewModel<TestViewModel>();
        }
    }
}
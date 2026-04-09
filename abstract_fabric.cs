using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    class abstract_fabric
    {
        public interface IButton
        {
            void Render();
            void OnClick();
        }

        public interface ICheckbox
        {
            void Render();
            void Check();
        }

        // Конкретные продукты для Windows
        public class WindowsButton : IButton
        {
            public void Render() => Console.WriteLine("Отрисовка Windows-кнопки");
            public void OnClick() => Console.WriteLine("Клик по Windows-кнопке");
        }

        public class WindowsCheckbox : ICheckbox
        {
            public void Render() => Console.WriteLine("Отрисовка Windows-чекбокса");
            public void Check() => Console.WriteLine("Windows-чекбокс отмечен");
        }

        // Конкретные продукты для Mac
        public class MacButton : IButton
        {
            public void Render() => Console.WriteLine("Отрисовка Mac-кнопки");
            public void OnClick() => Console.WriteLine("Клик по Mac-кнопке");
        }

        public class MacCheckbox : ICheckbox
        {
            public void Render() => Console.WriteLine("Отрисовка Mac-чекбокса");
            public void Check() => Console.WriteLine("Mac-чекбокс отмечен");
        }

        // Абстрактная фабрика
        public interface IGUIFactory
        {
            IButton CreateButton();
            ICheckbox CreateCheckbox();
        }

        // Конкретные фабрики
        public class WindowsFactory : IGUIFactory
        {
            public IButton CreateButton() => new WindowsButton();
            public ICheckbox CreateCheckbox() => new WindowsCheckbox();
        }

        public class MacFactory : IGUIFactory
        {
            public IButton CreateButton() => new MacButton();
            public ICheckbox CreateCheckbox() => new MacCheckbox();
        }

        // Клиентский код
        public class Application
        {
            private IButton _button;
            private ICheckbox _checkbox;

            public Application(IGUIFactory factory)
            {
                _button = factory.CreateButton();
                _checkbox = factory.CreateCheckbox();
            }

            public void RenderUI()
            {
                _button.Render();
                _checkbox.Render();
            }

            public void HandleUserInput()
            {
                _button.OnClick();
                _checkbox.Check();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    class buildercs
    {
        public class Computer
        {
            public string CPU { get; set; }
            public string RAM { get; set; }
            public string Storage { get; set; }
            public string GPU { get; set; }
            public string Motherboard { get; set; }
            public List<string> Peripherals { get; set; } = new List<string>();

            public void ShowSpecs()
            {
                Console.WriteLine($"Компьютер:\n" +
                                 $"  CPU: {CPU}\n" +
                                 $"  RAM: {RAM}\n" +
                                 $"  Storage: {Storage}\n" +
                                 $"  GPU: {GPU}\n" +
                                 $"  Motherboard: {Motherboard}\n" +
                                 $"  Peripherals: {string.Join(", ", Peripherals)}");
            }
        }

        // Абстрактный строитель
        public interface IComputerBuilder
        {
            IComputerBuilder SetCPU(string cpu);
            IComputerBuilder SetRAM(string ram);
            IComputerBuilder SetStorage(string storage);
            IComputerBuilder SetGPU(string gpu);
            IComputerBuilder SetMotherboard(string motherboard);
            IComputerBuilder AddPeripheral(string peripheral);
            Computer Build();
        }

        // Конкретный строитель
        public class ComputerBuilder : IComputerBuilder
        {
            private Computer _computer = new Computer();

            public IComputerBuilder SetCPU(string cpu)
            {
                _computer.CPU = cpu;
                return this; // Возвращаем this для цепочки вызовов
            }

            public IComputerBuilder SetRAM(string ram)
            {
                _computer.RAM = ram;
                return this;
            }

            public IComputerBuilder SetStorage(string storage)
            {
                _computer.Storage = storage;
                return this;
            }

            public IComputerBuilder SetGPU(string gpu)
            {
                _computer.GPU = gpu;
                return this;
            }

            public IComputerBuilder SetMotherboard(string motherboard)
            {
                _computer.Motherboard = motherboard;
                return this;
            }

            public IComputerBuilder AddPeripheral(string peripheral)
            {
                _computer.Peripherals.Add(peripheral);
                return this;
            }

            public Computer Build()
            {
                // Можно добавить валидацию
                if (string.IsNullOrEmpty(_computer.CPU))
                    throw new InvalidOperationException("CPU обязателен");

                var result = _computer;
                _computer = new Computer(); // Сброс для следующего строительства
                return result;
            }
        }

        // Директор (опционально) - управляет процессом строительства
        public class ComputerDirector
        {
            public Computer BuildGamingComputer(IComputerBuilder builder)
            {
                return builder
                    .SetCPU("Intel i9-13900K")
                    .SetRAM("32GB DDR5")
                    .SetStorage("1TB NVMe SSD")
                    .SetGPU("NVIDIA RTX 4090")
                    .SetMotherboard("Z790")
                    .AddPeripheral("Mechanical Keyboard")
                    .AddPeripheral("Gaming Mouse")
                    .Build();
            }

            public Computer BuildOfficeComputer(IComputerBuilder builder)
            {
                return builder
                    .SetCPU("Intel i5-13400")
                    .SetRAM("16GB DDR4")
                    .SetStorage("512GB SSD")
                    .SetGPU("Integrated")
                    .SetMotherboard("B760")
                    .AddPeripheral("Standard Keyboard")
                    .Build();
            }
        }

        // Пример с текстовым строителем
        public class TextBuilder
        {
            private StringBuilder _sb = new StringBuilder();

            public TextBuilder AddHeader(string text)
            {
                _sb.AppendLine($"=== {text} ===");
                return this;
            }

            public TextBuilder AddParagraph(string text)
            {
                _sb.AppendLine(text);
                _sb.AppendLine();
                return this;
            }

            public TextBuilder AddList(params string[] items)
            {
                foreach (var item in items)
                {
                    _sb.AppendLine($"  • {item}");
                }
                _sb.AppendLine();
                return this;
            }

            public string Build() => _sb.ToString();
        }
    }        
}

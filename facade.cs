using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    public class CookingSystem
    {
        public void PrepareIngredients(string dish)
        {
            Console.WriteLine($"  Подготовка ингредиентов для {dish}");
        }

        public void Cook(string dish)
        {
            Console.WriteLine($"  Приготовление {dish}");
        }

        public void Plate(string dish)
        {
            Console.WriteLine($"  Сервировка {dish}");
        }
    }

    // Сложная подсистема 2: Система оплаты
    public class PaymentSystem
    {
        public bool ProcessPayment(string customer, double amount)
        {
            Console.WriteLine($"  Обработка платежа {customer} на сумму {amount} руб.");
            return true;
        }

        public void PrintReceipt(string customer, double amount)
        {
            Console.WriteLine($"  Печать чека для {customer} на сумму {amount} руб.");
        }
    }

    // Сложная подсистема 3: Система доставки
    public class DeliverySystem
    {
        public void AssignCourier(string address)
        {
            Console.WriteLine($"  Назначение курьера для доставки по адресу: {address}");
        }

        public void TrackDelivery(int orderId)
        {
            Console.WriteLine($"  Отслеживание заказа #{orderId}");
        }
    }

    // Сложная подсистема 4: Система уведомлений
    public class NotificationSystem
    {
        public void SendSMS(string phone, string message)
        {
            Console.WriteLine($"  SMS на {phone}: {message}");
        }

        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"  Email на {email}: {message}");
        }
    }

    // ФАСАД: Упрощает работу со всеми подсистемами
    public class RestaurantFacade
    {
        private CookingSystem _cooking;
        private PaymentSystem _payment;
        private DeliverySystem _delivery;
        private NotificationSystem _notification;

        public RestaurantFacade()
        {
            _cooking = new CookingSystem();
            _payment = new PaymentSystem();
            _delivery = new DeliverySystem();
            _notification = new NotificationSystem();
        }

        // Простой метод для заказа с доставкой
        public void OrderDelivery(string customer, string dish, string address,
                                  string phone, string email, double price)
        {
            Console.WriteLine($"\n=== Оформление заказа для {customer} ===");

            // 1. Приготовление
            Console.WriteLine("1. Приготовление блюда:");
            _cooking.PrepareIngredients(dish);
            _cooking.Cook(dish);
            _cooking.Plate(dish);

            // 2. Оплата
            Console.WriteLine("\n2. Оплата заказа:");
            if (_payment.ProcessPayment(customer, price))
            {
                _payment.PrintReceipt(customer, price);
            }

            // 3. Доставка
            Console.WriteLine("\n3. Доставка:");
            _delivery.AssignCourier(address);

            // 4. Уведомления
            Console.WriteLine("\n4. Уведомления:");
            _notification.SendSMS(phone, $"Ваш заказ '{dish}' принят!");
            _notification.SendEmail(email, $"Заказ #{new Random().Next(1000, 9999)} оформлен");

            Console.WriteLine("=== Заказ успешно оформлен ===\n");
        }

        // Простой метод для забора самовывозом
        public void OrderPickup(string customer, string dish, double price)
        {
            Console.WriteLine($"\n=== Самовывоз для {customer} ===");

            _cooking.PrepareIngredients(dish);
            _cooking.Cook(dish);
            _cooking.Plate(dish);
            _payment.ProcessPayment(customer, price);
            _payment.PrintReceipt(customer, price);

            Console.WriteLine($"Заказ '{dish}' готов к выдаче!\n");
        }
    }
}

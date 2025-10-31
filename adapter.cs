using System;

namespace PaymentAdapterLab
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
        void RefundPayment(double amount);
    }

    public class InternalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($" Внутренняя система: обработка платежа {amount} тг.");
        }

        public void RefundPayment(double amount)
        {
            Console.WriteLine($" Внутренняя система: возврат {amount} тг.");
        }
    }

    public class ExternalPaymentSystemA
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine($" External A: оплата {amount} тг успешно проведена.");
        }

        public void MakeRefund(double amount)
        {
            Console.WriteLine($" External A: возврат {amount} тг выполнен.");
        }
    }

    public class ExternalPaymentSystemB
    {
        public void SendPayment(double amount)
        {
            Console.WriteLine($" External B: отправка платежа на {amount} тг.");
        }

        public void ProcessRefund(double amount)
        {
            Console.WriteLine($" External B: возврат {amount} тг выполнен.");
        }
    }

    public class PaymentAdapterA : IPaymentProcessor
    {
        private ExternalPaymentSystemA _systemA;

        public PaymentAdapterA(ExternalPaymentSystemA systemA)
        {
            _systemA = systemA;
        }

        public void ProcessPayment(double amount) => _systemA.MakePayment(amount);
        public void RefundPayment(double amount) => _systemA.MakeRefund(amount);
    }

    public class PaymentAdapterB : IPaymentProcessor
    {
        private ExternalPaymentSystemB _systemB;

        public PaymentAdapterB(ExternalPaymentSystemB systemB)
        {
            _systemB = systemB;
        }

        public void ProcessPayment(double amount) => _systemB.SendPayment(amount);
        public void RefundPayment(double amount) => _systemB.ProcessRefund(amount);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(" Система оплаты ");
            Console.Write("Введите сумму: ");
            double amount = double.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("\nВыберите систему оплаты:");
            Console.WriteLine("1 - Внутренняя");
            Console.WriteLine("2 - External System A");
            Console.WriteLine("3 - External System B");
            Console.Write("Ваш выбор: ");
            int choice = int.Parse(Console.ReadLine() ?? "1");

            IPaymentProcessor processor = choice switch
            {
                1 => new InternalPaymentProcessor(),
                2 => new PaymentAdapterA(new ExternalPaymentSystemA()),
                3 => new PaymentAdapterB(new ExternalPaymentSystemB()),
                _ => new InternalPaymentProcessor()
            };

            processor.ProcessPayment(amount);

            Console.WriteLine("\nХотите выполнить возврат? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Введите сумму возврата: ");
                double refund = double.Parse(Console.ReadLine() ?? "0");
                processor.RefundPayment(refund);
            }

            Console.WriteLine("\n Операция завершена.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class Cheque : Receipt
    {
        int amount;
        public static int MaxAmount => 99999;
        public int Amount
        {
            get => amount;
            set
            {
                if (value <= 0)
                    amount = 1;
                else if (value > MaxAmount)
                    amount = MaxAmount;
                else
                    amount = value;
            }
        }
        public override object Init()
        {
            Receipt receipt = (Receipt)base.Init();
            int newAmount = IInit.RandomObj.Next(1, MaxAmount);
            Cheque cheque = new Cheque(receipt.Number, receipt.Company, newAmount);
            return cheque;
        }
        public override int CompareTo(object obj)        // реализация интерфейса IComparable
        {
            int baseComparation = base.CompareTo(obj);
            if (baseComparation == 0 && obj is Cheque cheque)  // номера и организации чеков одинаковые
                return this.Amount.CompareTo(cheque.Amount);
            return baseComparation;
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is Cheque cheque)
                return this.Amount == cheque.Amount;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() + Amount;
        }
        public override object Clone()
        {
            Cheque clonedCheque = new Cheque(Number, Company, Amount);
            clonedCheque.isCloned = true;
            return clonedCheque;
        }
        public Cheque(int number = 1, string company = "ПАО Сбербанк", int amount = 1) : base(number, company)
        {
            Amount = amount;
        }
        public override string ToString()
        {
            string strCheque = $"Чек №{Number}. Организация: {Company}. Сумма: {Amount} руб. ";
            if (isCloned)
                strCheque += "(клон) ";
            return strCheque;
        }
        public new void Show()              // перегрузка метода Show класса Receipt
        {
            Console.Write($"Чек №{Number}. Организация: {Company}. Сумма: {Amount} руб. ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
        public override void ShowVirtual()  // переопределённый метод вывода
        {
            Console.Write($"Чек №{Number}. Организация: {Company}. Сумма: {Amount} руб. ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
    }
}

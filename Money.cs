using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class Money : IInit
    {
        int rubles, kopeks;
        static int MaxRubles => 9999;
        public int Rubles
        {
            get => rubles;
            set
            {
                if (value < 0)
                    rubles = 0;  // число рублей не может быть отрицательным
                else
                    rubles = value;
            }
        }
        public int Kopeks
        {
            get => kopeks;
            set
            {
                if (value < 0)
                    kopeks = 0;  // число копеек не может быть отрицательным
                else
                {
                    rubles += value / 100;
                    kopeks = value % 100;
                }
            }
        }
        public Money(int rubles = 0, int kopeks = 0)
        {
            Rubles = rubles;
            Kopeks = kopeks;
        }
        public object Init()
        {
            int newRubles = IInit.RandomObj.Next(0, MaxRubles);
            int newKopeks = IInit.RandomObj.Next(0, 99);
            Money money = new Money(newRubles, newKopeks);
            return money;
        }
        public override string ToString()
        {
            return $"{Rubles} руб. {Kopeks} коп. ";
        }
        public void Show()
        {
            Console.WriteLine($"{Rubles} руб. {Kopeks} коп. ");
        }
    }
}

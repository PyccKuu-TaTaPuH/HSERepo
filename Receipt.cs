using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class Receipt : Document
    {
        public static string[] Companies => new string[]{"ПАО Сбербанк", "ПАО ВТБ", "ПАО Альфа-Банк", "ПАО Банк ФК Открытие",
                                                         "АО Тинькофф Банк", "DNS", "Магнит", "Пятёрочка", "Семья", "Cитилинк"};
        public string Company { get; set; }
        public Document BaseDocument        // возвращает объект базового класса Document
        {
            get => new Document(Number);
        }
        public Receipt(int number = 1, string company = "ПАО Сбербанк") : base(number)
        {
            Company = company;
        }
        public override object Init()
        {
            Document document = (Document)base.Init();
            int index = IInit.RandomObj.Next(0, Companies.Length);
            string newCompany = Companies[index];
            Receipt receipt = new Receipt(document.Number, newCompany);
            return receipt;
        }
        public override int CompareTo(object obj)        // реализация интерфейса IComparable
        {
            int baseComparation = base.CompareTo(obj);
            if (baseComparation == 0 && obj is Receipt receipt)        // номера квитанций одинаковые
                return String.Compare(this.Company, receipt.Company);  // сравниваем названия компаний
            return baseComparation;
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is Receipt receipt)
                return string.Equals(this.Company, receipt.Company);
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() + Array.IndexOf(Companies, Company) + 1;
        }
        public override object Clone()
        {
            Receipt clonedReceipt = new Receipt(Number, Company);
            clonedReceipt.isCloned = true;
            return clonedReceipt;
        }
        public override string ToString()
        {
            string strReceipt = $"Квитанция №{Number}. Организация: {Company} ";
            if (isCloned)
                strReceipt += "(клон) ";
            return strReceipt;
        }
        public new void Show()              // перегрузка метода Show класса Receipt
        {
            Console.Write($"Квитанция №{Number}. Организация: {Company} ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
        public override void ShowVirtual()  // переопределённый метод вывода
        {
            Console.Write($"Квитанция №{Number}. Организация: {Company} ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
    }
}

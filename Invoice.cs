using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class Invoice : Document
    {
        string product;
        int count, price;
        public static string[] Products => new string[] {"автомобиль", "газета", "диван", "книга", "компьютерная мышь", "кресло",
                                                         "ноутбук", "подшипник", "сейф", "смартфон", "стеллаж", "стол", "стул", "шкаф"};
        public static int MaxCount => 9999;
        public static int MaxPrice => 99999;
        public string Product
        {
            get => product;
            set
            {
                product = value.ToLower();
            }
        }
        public int Count
        {
            get => count;
            set
            {
                if (value <= 0)
                    count = 1;
                else if (value > MaxCount)
                    count = MaxCount;
                else
                    count = value;
            }
        }
        public int Price
        {
            get => price;
            set
            {
                if (value <= 0)
                    price = 1;
                else if (value > MaxPrice)
                    price = MaxPrice;
                else
                    price = value;
            }
        }
        public Document BaseDocument        // возвращает объект базового класса Document
        {
            get => new Document(Number);
        }
        public Invoice(int number = 1, string product = "", int count = 1, int price = 1) : base(number)
        {
            Product = product;
            Count = count;
            Price = price;
        }
        public override object Init()
        {
            Document document = (Document)base.Init();
            // выбор продукта по индексу из списка заданных
            int index = IInit.RandomObj.Next(0, Products.Length);
            string newProduct = Products[index];
            // установка количества и цены продукции
            int newCount = IInit.RandomObj.Next(1, MaxCount);
            int newPrice = IInit.RandomObj.Next(1, MaxPrice);
            Invoice invoice = new Invoice(document.Number, newProduct, newCount, newPrice);
            return invoice;
        }
        public override int CompareTo(object obj)        // реализация интерфейса IComparable
        {
            int baseComparation = base.CompareTo(obj);
            if (baseComparation == 0 && obj is Invoice invoice)  // номера накладных одинаковые
            {
                int productComparation = String.Compare(this.Product, invoice.Product);  // сравниваем названия продуктов
                if (productComparation == 0)                                             // названия продуктов одинаковые
                {
                    int priceComparation = this.Price.CompareTo(invoice.Price);          // сравниваем цены на продукт
                    if (priceComparation == 0)                                           // цены на продукт одинаковые
                        return this.Count.CompareTo(invoice.Count);                      // сравниваем количество продукции
                    return priceComparation;
                }
                return productComparation;
            }
            return baseComparation;
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is Invoice invoice)
                return string.Equals(this.Product, invoice.Product) && 
                       this.Count == invoice.Count && this.Price == invoice.Price;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() + Array.IndexOf(Products, Product) + Price * Count + 1;
        }
        public override object Clone()
        {
            Invoice clonedInvoice = new Invoice(Number, Product, Count, Price);
            clonedInvoice.isCloned = true;
            return clonedInvoice;
        }
        public override string ToString()
        {
            string strInvoice = $"Накладная №{Number}. Наименование товара: {Product}, количество: {Count}, цена: {Price} руб. ";
            if (isCloned)
                strInvoice += "(клон) ";
            return strInvoice;
        }
        public new void Show()              // перегрузка метода Show класса Document
        {
            Console.Write($"Накладная №{Number}. Наименование товара: {Product}, количество: {Count}, цена: {Price} руб. ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
        public override void ShowVirtual()  // переопределённый метод вывода
        {
            Console.Write($"Накладная №{Number}. Наименование товара: {Product}, количество: {Count}, цена: {Price} руб. ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
    }
}

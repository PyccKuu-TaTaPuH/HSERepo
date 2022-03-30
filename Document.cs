using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class Document : IComparable, ICloneable, IInit
    {
        int number;
        protected bool isCloned = false;        // по умолчанию объект класса не является клоном
        public static int MaxNumber => 99999;
        public int Number
        {
            get => number;
            set
            {
                if (value <= 0)
                    number = 1;
                else if (value > MaxNumber)
                    number = MaxNumber;
                else
                    number = value;
            }
        }
        public Document(int number = 1)
        {
            Number = number;
        }
        public virtual object Init()            // реализация интерфейса IInit
        {
            int newNumber = IInit.RandomObj.Next(1, MaxNumber);
            Document document = new Document(newNumber);
            return document;
        }
        public virtual int CompareTo(object obj)        // реализация интерфейса IComparable
        {
            Document otherDocument = (Document)obj;
            return this.Number.CompareTo(otherDocument.Number);
        }
        public override bool Equals(object obj)
        {
            if (obj is Document document)
                return this.Number == document.Number;
            return false;
        }
        public override int GetHashCode()
        {
            return Number;
        }
        public object ShallowCopy()           // поверхностное копированиеы
        {
            return this.MemberwiseClone();
        }
        public virtual object Clone()           // глубокое клонирование
        {
            Document clonedDocument = new Document(Number);
            clonedDocument.isCloned = true;
            return clonedDocument;
        }
        public override string ToString()
        {
            string strDoc = $"Документ №{Number} ";
            if (isCloned)
                strDoc += "(клон) ";
            return strDoc;
        }
        public void Show()
        {
            Console.Write($"Документ №{Number} ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
        public virtual void ShowVirtual()
        {
            Console.Write($"Документ №{Number} ");
            if (isCloned)
                Console.Write("(клон) ");
            Console.WriteLine();
        }
    }
}
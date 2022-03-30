using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class SortByNumber : IComparer
    {
        // сортировка документов по номеру
        int IComparer.Compare(object obj1, object obj2)
        {
            Document document1 = (Document)obj1;
            Document document2 = (Document)obj2;
            return document1.Number.CompareTo(document2.Number);
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassLibrary
{
    public class SortByAmount : IComparer
    {
        // сортировка чеков по сумме
        int IComparer.Compare(object obj1, object obj2)
        {
            Cheque cheque1 = (Cheque)obj1;
            Cheque cheque2 = (Cheque)obj2;
            return cheque1.Amount.CompareTo(cheque2.Amount);
        }
    }
}

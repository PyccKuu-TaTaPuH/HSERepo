using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentClassLibrary;

namespace Lab_11
{
    public class TestCollections
    {
        List<Invoice> invoiceList = new List<Invoice>();
        List<string> stringList = new List<string>();
        SortedDictionary<Document, Invoice> documentDict = new SortedDictionary<Document, Invoice>();
        SortedDictionary<string, Invoice> stringDict = new SortedDictionary<string, Invoice>();
        public Invoice First
        {
            get
            {
                Invoice firstInList = invoiceList[0];
                Invoice firstElem = new Invoice(firstInList.Number, firstInList.Product, 
                                                firstInList.Count, firstInList.Price);
                return firstElem;
            }
        }
        public Invoice Middle
        {
            get
            {
                int count = invoiceList.Count;
                Invoice midddleInList = invoiceList[count / 2];
                Invoice midddleElem = new Invoice(midddleInList.Number, midddleInList.Product,
                                                  midddleInList.Count, midddleInList.Price);
                return midddleElem;
            }
        }
        public Invoice Last
        {
            get
            {
                int count = invoiceList.Count;
                Invoice lastInList = invoiceList[count - 1];
                Invoice lastElem = new Invoice(lastInList.Number, lastInList.Product,
                                               lastInList.Count, lastInList.Price);
                return lastElem;
            }
        }
        public TestCollections()
        {
            int elemCount = 1000;  //
            for (int i = 0; i < elemCount; i++)
            {
                Invoice invoice;
                Document baseDocument;
                bool isExist;
                do
                {
                    invoice = (Invoice)new Invoice().Init();  // генерация элемента с помощью интерфейса IInit
                    baseDocument = invoice.BaseDocument;
                    // проверка, был ли ранее создан такой элемент
                    isExist = invoiceList.Contains(invoice) || documentDict.ContainsKey(baseDocument);
                } while (isExist);
                string strInvoice = invoice.ToString();
                // добавление нового элемента в коллекции
                invoiceList.Add(invoice);
                stringList.Add(strInvoice);
                documentDict.Add(baseDocument, invoice);
                stringDict.Add(strInvoice, invoice);
            }
            invoiceList.Sort();
            stringList.Sort();
        }
        public void AddElem(Invoice elem)
        {
            bool elemExist = invoiceList.Contains(elem);  // проверка, содержится ли введённый элемент в коллекциях
            if (!elemExist)                               // элемент не содержится в коллекциях
            {
                // добавление в коллекции
                string strElem = elem.ToString();
                Document baseElem = elem.BaseDocument;
                invoiceList.Add(elem);
                stringList.Add(strElem);
                documentDict.Add(baseElem, elem);
                stringDict.Add(strElem, elem);
            }
        }
        public void DeleteElem(Invoice elem)
        {
            string strElem = elem.ToString();
            Document baseElem = elem.BaseDocument;
            invoiceList.Remove(elem);
            stringList.Remove(strElem);
            documentDict.Remove(baseElem);
            stringDict.Remove(strElem);
        }
        public bool ContainsInInvoiceList(Invoice invoice)
        {
            return invoiceList.Contains(invoice);
        }
        public bool ContainsInStringList(Invoice invoice)
        {
            string strInvoice = invoice.ToString();
            return stringList.Contains(strInvoice);
        }
        public bool ContainsKeyInDocumentDict(Invoice invoice)
        {
            Document baseDoc = invoice.BaseDocument;
            return documentDict.ContainsKey(baseDoc);
        }
        public bool ContainsKeyInStringDict(Invoice invoice)
        {
            string strInvoice = invoice.ToString();
            return stringDict.ContainsKey(strInvoice);
        }
        public bool ContainsValueInDocumentDict(Invoice invoice)
        {
            return documentDict.ContainsValue(invoice);
        }
    }
}

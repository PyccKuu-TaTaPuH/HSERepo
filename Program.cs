using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentClassLibrary;

namespace Lab_11
{
    class Program
    {
        #region Вспомогательные функции
        static string[] ClassNames = new string[] { "Document", "Receipt", "Cheque", "Invoice" };
        static int ReadInt(string name = "числовое значение", int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            string prompt = $"Введите {name} (целое число от {minValue} до {maxValue}): "; // подсказка для удобства ввода
            bool correctRead;
            int n = 0; // временное значение
            do
            {
                try
                {
                    Console.Write(prompt);
                    n = int.Parse(Console.ReadLine());
                    correctRead = (Math.Abs(n) >= minValue) && (Math.Abs(n) <= maxValue); // выполнение ограничения
                    if (!correctRead)
                        Console.WriteLine("Введённое целое число не принадлежит указанному отрезку. Попробуйте снова");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введённое значение не является целым числом. Попробуйте снова");
                    correctRead = false;
                }
            } while (!correctRead);
            return n;
        }
        static string СhooseFromList(string[] valueList, string name = "строку")
        {
            int valueCount = valueList.Length;
            for (int i = 0; i < valueCount; i++)
                Console.WriteLine($"{i + 1} - {valueList[i]}");
            Console.WriteLine($"Выберите {name} из указанных выше.");
            int index = ReadInt("номер варианта", 1, valueCount);
            return valueList[index - 1];
        }
        static Document CreateDocument()
        {
            int number = ReadInt("номер документа", 1, Document.MaxNumber);
            return new Document(number);
        }
        static Receipt CreateReceipt()
        {
            int number = ReadInt("номер квитанции", 1, Document.MaxNumber);
            string company = СhooseFromList(Receipt.Companies, "организацию");
            return new Receipt(number, company);
        }
        static Cheque CreateCheque()
        {
            int number = ReadInt("номер чека", 1, Document.MaxNumber);
            string company = СhooseFromList(Receipt.Companies, "организацию");
            int amount = ReadInt("сумму чека", 1, Cheque.MaxAmount);
            return new Cheque(number, company, amount);
        }
        static Invoice CreateInvoice()
        {
            int number = ReadInt("номер накладной", 1, Document.MaxNumber);
            string product = СhooseFromList(Invoice.Products, "товар");
            int count = ReadInt("количество товара", 1, Invoice.MaxCount);
            int price = ReadInt("цену товара", 1, Invoice.MaxPrice);
            return new Invoice(number, product, count, price);
        }
        static Document CreateElem(string name)
        {
            Document elem;
            switch (name)
            {
                case "Receipt":
                    elem = CreateReceipt();
                    break;
                case "Cheque":
                    elem = CreateCheque();
                    break;
                case "Invoice":
                    elem = CreateInvoice();
                    break;
                default:
                    elem = CreateDocument();
                    break;
            }
            return elem;
        }
        static Document GenerateElem(string name)
        {
            Document elem;
            switch (name)
            {
                case "Receipt":
                    elem = (Receipt)new Receipt().Init();
                    break;
                case "Cheque":
                    elem = (Cheque)new Cheque().Init();
                    break;
                case "Invoice":
                    elem = (Invoice)new Invoice().Init();
                    break;
                default:
                    elem = (Document)new Document().Init();
                    break;
            }
            return elem;
        }
        static bool IsFull(ICollection collection)
        {
            return collection.Count > 0;
        }
        static void PrintResult(string okMessage = "", string failMessage = "", bool success = true)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(okMessage + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(failMessage + "\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
        #region Вывод текстовых меню
        static void PrintStartMenu()
        {
            Console.WriteLine("Выберите класс коллекции для работы:");
            Console.WriteLine("1 - Hashtable");
            Console.WriteLine("2 - Dictionary<Document, Invoice>");
            Console.WriteLine("0 - Выход\n");
        }
        static void PrintWorkMenu(string type = "Dictionary<Document, Invoice>")
        {
            Console.WriteLine($"Выберите операцию с коллекцией {type}:");
            Console.WriteLine("1 - Вывести все элементы с помощью цикла foreach");
            Console.WriteLine("2 - Добавить элемент");
            Console.WriteLine("3 - Удалить элемент");
            Console.WriteLine("4 - Найти элемент");
            // по 3 запроса на коллекцию
            if (type == "Hashtable")
            {
                Console.WriteLine("5 - Найти количество квитанций от заданной организации");
                Console.WriteLine("6 - Вывести чеки на сумму, меньшую заданной");
                Console.WriteLine("7 - Найти наибольшую сумму чека от заданной организации");
            }
            else
            {
                Console.WriteLine("5 - Найти количество накладных на заданный товар");
                Console.WriteLine("6 - Вывести накладные c суммарной стоимостью товара, меньшей заданной");
                Console.WriteLine("7 - Найти количество заданного товара");
            }
            Console.WriteLine("0 - Назад\n");
        }
        static void PrintCreateMenu()
        {
            Console.WriteLine($"Выберите способ создания объекта:");
            Console.WriteLine("1 - С помощью интерфейса IInit");
            Console.WriteLine("2 - Ввод вручную");
        }
        #endregion
        #region Организация работы
        static void Work(ref Hashtable hashtable)
        {
            bool running = true;
            do  // запуск цикла работы с хэш-таблицей
            {
                PrintWorkMenu("Hashtable");
                int answer = ReadInt("номер ответа", 0, 7);
                switch (answer)
                {
                    case 1:
                        PrintElems(hashtable);
                        break;
                    case 2:
                        AddElem(ref hashtable);
                        break;
                    case 3:
                        DeleteElem(ref hashtable);
                        break;
                    case 4:
                        FindElem(hashtable, CreateElem(СhooseFromList(ClassNames, "класс объекта")));
                        break;
                    case 5:
                        CountReceiptsFromCompany(hashtable, СhooseFromList(Receipt.Companies));
                        break;
                    case 6:
                        PrintChequesForLessAmount(hashtable, ReadInt("сумму", 1, Cheque.MaxAmount));
                        break;
                    case 7:
                        FindMaxСhequeAmountFromCompany(hashtable, СhooseFromList(Receipt.Companies));
                        break;
                    default:
                        running = false;
                        break;
                }
            } while (running);
        }
        static void Work(ref Dictionary<Document, Invoice> keyValuePairs)
        {
            bool running = true;
            do  // запуск цикла работы с хэш-таблицей
            {
                PrintWorkMenu("Dictionary<Document, Invoice>");
                int answer = ReadInt("номер ответа", 0, 7);
                switch (answer)
                {
                    case 1:
                        PrintElems(keyValuePairs);
                        break;
                    case 2:
                        AddElem(ref keyValuePairs);
                        break;
                    case 3:
                        DeleteElem(ref keyValuePairs);
                        break;
                    case 4:
                        FindElem(keyValuePairs, CreateInvoice());
                        break;
                    case 5:
                        CountInvoicesForProduct(keyValuePairs, СhooseFromList(Invoice.Products));
                        break;
                    case 6:
                        PrintInvoicesWithLargerCost(keyValuePairs, ReadInt("стоимость товара", 1, 
                                                                           Invoice.MaxPrice * Invoice.MaxCount));
                        break;
                    case 7:
                        CountProductFromInvoices(keyValuePairs, СhooseFromList(Invoice.Products));
                        break;
                    default:
                        running = false;
                        break;
                }
            } while (running);
        }
        static void Run(Hashtable hashtable, Dictionary<Document, Invoice> keyValuePairs)
        {
            bool running = true;
            do // начало главного цикла
            {
                PrintStartMenu();
                int collectionNumber = ReadInt("номер ответа", 0, 2);
                switch (collectionNumber)
                {
                    case 1:
                        Work(ref hashtable);
                        break;
                    case 2:
                        Work(ref keyValuePairs);
                        break;
                    default:
                        Console.WriteLine("Завершение работы с коллекциями\n");
                        running = false;
                        break;
                }
            } while (running);
        }
        #endregion
        #region Основные функции
        static void AddElem(ref Hashtable hashtable)
        {
            string className = СhooseFromList(ClassNames, "класс объекта");
            PrintCreateMenu();
            int answer = ReadInt("номер ответа", 1, 2);
            Document elem;
            bool elemExist;
            do
            {
                if (answer == 1)
                    elem = GenerateElem(className);
                else
                    elem = CreateElem(className);
                elemExist = hashtable.Contains(elem);
                if (answer == 2 && elemExist)
                    Console.WriteLine("Данный объект уже есть в коллекции. Попробуйте снова");
            } while (elemExist);
            Console.WriteLine($"Созданный объект: {elem}");
            hashtable.Add(elem, elem.GetHashCode());
            PrintResult("Объект успешно добавлен в коллекцию");

        }
        static void AddElem(ref Dictionary<Document, Invoice> keyValuePairs)
        {
            PrintCreateMenu();
            int answer = ReadInt("номер ответа", 1, 2);
            Invoice elem;
            Document baseDoc;
            bool elemExist;
            do
            {
                if (answer == 1)
                    elem = (Invoice)new Invoice().Init();
                else
                    elem = CreateInvoice();
                baseDoc = elem.BaseDocument;
                elemExist = keyValuePairs.ContainsValue(elem) || keyValuePairs.ContainsKey(baseDoc);
                if (answer == 2 && elemExist)
                    Console.WriteLine("Данный объект или его ключ уже есть в коллекции. Попробуйте снова");
            } while (elemExist);
            Console.WriteLine($"Созданный объект: {elem}");
            keyValuePairs.Add(baseDoc, elem);
            PrintResult("Объект успешно добавлен в коллекцию");
        }
        static void DeleteElem(ref Hashtable hashtable)
        {
            string className = СhooseFromList(ClassNames, "класс объекта");
            Document elem = CreateElem(className);
            Console.WriteLine($"Выбран объект {elem}");
            bool elemExist = hashtable.Contains(elem);
            if (elemExist)
                hashtable.Remove(elem);
            PrintResult($"Объект успешно удалён из коллекции",
                        $"Объекта нет в коллекции", elemExist);
        }
        static void DeleteElem(ref Dictionary<Document, Invoice> keyValuePairs)
        {
            Invoice elem = CreateInvoice();
            bool elemExist = keyValuePairs.ContainsValue(elem);
            Console.WriteLine($"Выбран объект {elem}");
            if (elemExist)
            {
                Document baseDoc = elem.BaseDocument;
                keyValuePairs.Remove(baseDoc);
            }
            PrintResult($"Объект успешно удалён из коллекции",
                        $"Объекта нет в коллекции", elemExist);
        }
        static void PrintElems(Hashtable hashtable)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Document document in hashtable.Keys)
                document.ShowVirtual();
            PrintResult(failMessage: "Коллекция пуста", success: IsFull(hashtable));
        }
        static void PrintElems(Dictionary<Document, Invoice> keyValuePairs)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Invoice invoice in keyValuePairs.Values)
                invoice.ShowVirtual();
            PrintResult(failMessage: "Коллекция пуста", success: IsFull(keyValuePairs));
        }
        static void FindElem(Hashtable hashtable, Document elem)
        {
            bool elemFound = hashtable.Contains(elem);
            PrintResult("Значение найдено", "Значение не найдено", elemFound);
        }
        static void FindElem(Dictionary<Document, Invoice> keyValuePairs, Invoice elem)
        {
            bool elemFound = keyValuePairs.ContainsValue(elem);
            PrintResult("Значение найдено", "Значение не найдено", elemFound);
        }
        #endregion
        #region Запросы
        static void CountReceiptsFromCompany(Hashtable hashtable, string company)
        {
            int count = 0;
            foreach (Document document in hashtable.Keys)
            {
                if (document is Receipt receipt)  // данный элемент - квитанция или чек
                    if (String.Compare(receipt.Company, company) == 0)  // квитанция от данной организации
                        count++;
            }
            PrintResult($"Количество квитанций и чеков от организации {company}: {count}",
                        $"Квитанции и чеки от организации {company} не найдены", count > 0);
        }
        static void PrintChequesForLessAmount(Hashtable hashtable, int amount)
        {
            bool found = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Document document in hashtable.Keys)
            {
                if (document is Cheque cheque)    // данный элемент - чек
                    if (cheque.Amount < amount)       // чек на меньшую сумму
                    {
                        found = true;
                        cheque.ShowVirtual();
                    }
            }
            PrintResult($"Все чеки на сумму меньше {amount} распечатаны",
                        $"Чеки на сумму меньше {amount} не найдены", found);
        }
        static void FindMaxСhequeAmountFromCompany(Hashtable hashtable, string company)
        {
            int maxAmount = 0;
            foreach (Document document in hashtable.Keys)
            {
                if (document is Cheque cheque)    // данный элемент - чек
                    if (String.Compare(cheque.Company, company) == 0)  // чек от данной организации
                    {
                        int curAmount = cheque.Amount;
                        if (curAmount > maxAmount)
                            maxAmount = curAmount;
                    }
            }
            PrintResult($"Наибольшая сумма чека от организации {company}: {maxAmount}",
                        $"Чеки от организации {company} не найдены", maxAmount > 0);
        }
        static void CountInvoicesForProduct(Dictionary<Document, Invoice> keyValuePairs, string product)
        {
            int count = 0;
            foreach (Invoice invoice in keyValuePairs.Values)
            {
                if (String.Compare(invoice.Product, product.ToLower()) == 0)  // накладная на данный товар
                    count++;
            }
            PrintResult($"Количество накладных на товар {product}: {count}",
                        $"Накладные на товар {product} не найдены", count > 0);
        }
        static void PrintInvoicesWithLargerCost(Dictionary<Document, Invoice> keyValuePairs, int cost)
        {
            bool found = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Invoice invoice in keyValuePairs.Values)
            {
                int curCost = invoice.Price * invoice.Count;
                if (curCost > cost)  // накладная на данный товар
                {
                    found = true;
                    invoice.ShowVirtual();
                }
            }
            PrintResult($"Все накладные с суммарной стоимостью товара больше {cost} распечатаны",
                        $"Накладные с суммарной стоимостью товара больше {cost} не найдены", found);
        }
        static void CountProductFromInvoices(Dictionary<Document, Invoice> keyValuePairs, string product)
        {
            int count = 0;
            foreach (Invoice invoice in keyValuePairs.Values)
            {
                if (String.Compare(invoice.Product, product.ToLower()) == 0)  // накладная на данный товар
                    count += invoice.Count;
            }
            PrintResult($"Количество товара {product}: {count}",
                        $"Накладные на товар {product} не найдены", count > 0);
        }
        #endregion
        static void Main(string[] args)
        {
            // Работа с коллекциями .NET
            Hashtable documentHashtable = new Hashtable();
            Dictionary<Document, Invoice> invoiceDict = new Dictionary<Document, Invoice>();
            Run(documentHashtable, invoiceDict);
            // Клонирование коллекции Hashtable
            Hashtable hashtableClone = new Hashtable();
            foreach (Document document in documentHashtable.Keys)
            {
                Document docClone = (Document)document.Clone();
                hashtableClone.Add(docClone, docClone.GetHashCode());
            }
            Console.WriteLine("Элементы коллекции-клона Hashtable:");
            PrintElems(hashtableClone);
            // Клонирование коллекции Dictionary<Document, Invoice>
            Dictionary<Document, Invoice> dictClone = new Dictionary<Document, Invoice>();
            foreach (Invoice invoice in invoiceDict.Values)
            {
                Invoice invoiceClone = (Invoice)invoice.Clone();
                dictClone.Add(invoiceClone.BaseDocument, invoiceClone);
            }
            Console.WriteLine("Элементы коллекции-клона Dictionary<Document, Invoice>:");
            PrintElems(dictClone);
            // Работа с коллекцией TestCollections
            TestCollections testCollections = new TestCollections();
            Invoice[] checkInvoices = new Invoice[4];
            checkInvoices[0] = testCollections.First;                          // первый элемент коллекции  
            checkInvoices[1] = testCollections.Middle;                         // центральный элемент коллекции
            checkInvoices[2] = testCollections.Last;                           // последний элемент коллекции
            checkInvoices[3] = new Invoice(2903, "настольная лампа", 5, 680);  // не входит в коллекцию
            Stopwatch stopwatch = new Stopwatch();                             // счётчик времени
            bool isExist;
            for (int i = 0; i < 4; i++)
            {
                Invoice curElem = checkInvoices[i];
                Console.WriteLine("");
                curElem.ShowVirtual();
                // Поиск в коллекции List<Invoice>
                Console.WriteLine("Поиск в коллекции List<Invoice>");
                stopwatch.Start();
                isExist = testCollections.ContainsInInvoiceList(curElem);
                stopwatch.Stop();
                Console.WriteLine($"Время поиска: {stopwatch.ElapsedTicks}");
                PrintResult("Значение найдено", "Значение не найдено", isExist);
                // Поиск в коллекции List<string>
                Console.WriteLine("Поиск в коллекции List<string>");
                stopwatch.Restart();
                isExist = testCollections.ContainsInStringList(curElem);
                stopwatch.Stop();
                Console.WriteLine($"Время поиска: {stopwatch.ElapsedTicks}");
                PrintResult("Значение найдено", "Значение не найдено", isExist);
                // Поиск ключа в коллекции SortedDictionary<Document, Invoice>
                Console.WriteLine("Поиск ключа в коллекции SortedDictionary<Document, Invoice>");
                stopwatch.Restart();
                isExist = testCollections.ContainsKeyInDocumentDict(curElem);
                stopwatch.Stop();
                Console.WriteLine($"Время поиска: {stopwatch.ElapsedTicks}");
                PrintResult("Ключ найден", "Ключ не найден", isExist);
                // Поиск ключа в коллекции SortedDictionary<string, Invoice>
                Console.WriteLine("Поиск ключа в коллекции SortedDictionary<string, Invoice>");
                stopwatch.Restart();
                isExist = testCollections.ContainsKeyInStringDict(curElem);
                stopwatch.Stop();
                Console.WriteLine($"Время поиска: {stopwatch.ElapsedTicks}");
                PrintResult("Ключ найден", "Ключ не найден", isExist);
                // Поиск значения в коллекции SortedDictionary<Document, Invoice>
                Console.WriteLine("Поиск значения в коллекции SortedDictionary<Document, Invoice>");
                stopwatch.Restart();
                isExist = testCollections.ContainsValueInDocumentDict(curElem);
                stopwatch.Stop();
                Console.WriteLine($"Время поиска: {stopwatch.ElapsedTicks}");
                PrintResult("Значение найдено", "Значение не найдено", isExist);
                stopwatch.Reset();
            }
        }
    }
}

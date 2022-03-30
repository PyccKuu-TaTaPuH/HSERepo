using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentClassLibrary;
using Lab_11;

namespace UnitTestCollections
{
    [TestClass]
    public class UnitTestCollections
    {
        [TestMethod]
        public void TestAddNewElem()               // тест добавления нового элемента
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice newElem = new Invoice(203, "шифоньер", 5, 740);
            // Act
            collections.AddElem(newElem);  // добавление в конец
            Invoice last = collections.Last;
            // Assert
            Assert.AreEqual(newElem, last);
        }
        [TestMethod]
        public void TestAddExistElem()             // тест добавления уже существующего элемента
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice lastBeforeAdding = collections.Last;
            Invoice existElem = collections.First;
            // Act
            collections.AddElem(existElem);  // добавление в конец
            Invoice lastAfterAdding = collections.Last;
            // Assert
            // добавление уже существующего элемента не должно изменить коллекцию
            Assert.AreEqual(lastBeforeAdding, lastAfterAdding);
        }
        [TestMethod]
        public void TestDeleteElem()               // тест удаления элемента
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice lastBeforeDelete = collections.Last;  // последний элемент коллекции
            // Act
            collections.DeleteElem(lastBeforeDelete);
            Invoice lastAfterDelete = collections.Last;   // после удаления последний элемент должен измениться
            // Assert
            Assert.AreNotEqual(lastBeforeDelete, lastAfterDelete);
        }
        [TestMethod]
        public void TestFindElemInInvoiceList()    // тест поиска элемента в списке объектов Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = false;
            Invoice elem = new Invoice(203, "шифоньер", 5, 740);  // элемента нет в коллекции
            // Act
            bool isFound = collections.ContainsInInvoiceList(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindElemInStringList()     // тест поиска элемента в списке строк
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.Middle;  // средний элемент коллекции
            // Act
            bool isFound = collections.ContainsInStringList(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindKeyInDocumentDict()    // тест поиска ключа в словаре пар Document-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.First;  // первый элемент коллекции
            // Act
            bool isFound = collections.ContainsKeyInDocumentDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindKeyInStringDict()      // тест поиска ключа в словаре пар строка-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.Middle;  // средний элемент коллекции
            // Act
            bool isFound = collections.ContainsKeyInStringDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindValueInDocumentDict()  // тест поиска значения в словаре пар Document-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = false;
            Invoice elem = new Invoice(918, "настольная лампа", 24, 360);  // элемента нет в коллекции
            // Act
            bool isFound = collections.ContainsValueInDocumentDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
    }
}

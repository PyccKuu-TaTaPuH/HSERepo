using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentClassLibrary;
using Lab_11;

namespace UnitTestCollections
{
    [TestClass]
    public class UnitTestCollections
    {
        [TestMethod]
        public void TestAddNewElem()               // ���� ���������� ������ ��������
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice newElem = new Invoice(203, "��������", 5, 740);
            // Act
            collections.AddElem(newElem);  // ���������� � �����
            Invoice last = collections.Last;
            // Assert
            Assert.AreEqual(newElem, last);
        }
        [TestMethod]
        public void TestAddExistElem()             // ���� ���������� ��� ������������� ��������
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice lastBeforeAdding = collections.Last;
            Invoice existElem = collections.First;
            // Act
            collections.AddElem(existElem);  // ���������� � �����
            Invoice lastAfterAdding = collections.Last;
            // Assert
            // ���������� ��� ������������� �������� �� ������ �������� ���������
            Assert.AreEqual(lastBeforeAdding, lastAfterAdding);
        }
        [TestMethod]
        public void TestDeleteElem()               // ���� �������� ��������
        {
            // Arrange
            TestCollections collections = new TestCollections();
            Invoice lastBeforeDelete = collections.Last;  // ��������� ������� ���������
            // Act
            collections.DeleteElem(lastBeforeDelete);
            Invoice lastAfterDelete = collections.Last;   // ����� �������� ��������� ������� ������ ����������
            // Assert
            Assert.AreNotEqual(lastBeforeDelete, lastAfterDelete);
        }
        [TestMethod]
        public void TestFindElemInInvoiceList()    // ���� ������ �������� � ������ �������� Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = false;
            Invoice elem = new Invoice(203, "��������", 5, 740);  // �������� ��� � ���������
            // Act
            bool isFound = collections.ContainsInInvoiceList(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindElemInStringList()     // ���� ������ �������� � ������ �����
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.Middle;  // ������� ������� ���������
            // Act
            bool isFound = collections.ContainsInStringList(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindKeyInDocumentDict()    // ���� ������ ����� � ������� ��� Document-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.First;  // ������ ������� ���������
            // Act
            bool isFound = collections.ContainsKeyInDocumentDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindKeyInStringDict()      // ���� ������ ����� � ������� ��� ������-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = true;
            Invoice elem = collections.Middle;  // ������� ������� ���������
            // Act
            bool isFound = collections.ContainsKeyInStringDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
        [TestMethod]
        public void TestFindValueInDocumentDict()  // ���� ������ �������� � ������� ��� Document-Invoice
        {
            // Arrange
            TestCollections collections = new TestCollections();
            bool expectedResult = false;
            Invoice elem = new Invoice(918, "���������� �����", 24, 360);  // �������� ��� � ���������
            // Act
            bool isFound = collections.ContainsValueInDocumentDict(elem);
            // Assert
            Assert.AreEqual(expectedResult, isFound);
        }
    }
}

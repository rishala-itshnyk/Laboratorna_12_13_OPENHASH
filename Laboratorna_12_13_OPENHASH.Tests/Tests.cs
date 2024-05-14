namespace Laboratorna_12_13_OPENHASH.Tests;

public class Tests
{
    private OpenHashTable hashTable;

    [SetUp]
    public void Setup()
    {
        hashTable = new OpenHashTable();
    }

    [Test]
    public void AddStudent_SearchExistingStudent_ReturnsTrue()
    {
        // Arrange
        string surname = "Smith";
        int[] grades = { 85, 90, 92 };

        // Act
        hashTable.Add(surname, grades);

        // Assert
        Assert.IsTrue(hashTable.Search(surname, out int[] resultGrades));
        Assert.AreEqual(grades, resultGrades);
    }

    [Test]
    public void AddStudent_DeleteStudent_SearchDeletedStudent_ReturnsFalse()
    {
        // Arrange
        string surname = "Johnson";
        int[] grades = { 78, 80, 85 };

        // Act
        hashTable.Add(surname, grades);
        hashTable.Delete(surname);

        // Assert
        Assert.IsFalse(hashTable.Search(surname, out _));
    }

    [Test]
    public void AddStudent_DeleteNonExistingStudent_ReturnsFalse()
    {
        // Arrange
        string nonExistingSurname = "NonExisting";

        // Act & Assert
        Assert.IsFalse(hashTable.Delete(nonExistingSurname));
    }
}
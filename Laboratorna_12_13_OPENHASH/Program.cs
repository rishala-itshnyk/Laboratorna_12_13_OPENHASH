using System;
using System.Collections.Generic;
using System.Linq;

// Клас для зберігання студентських даних
public class StudentData
{
    public string Surname { get; set; }
    public int[] Grades { get; set; }

    public StudentData(string surname, int[] grades)
    {
        Surname = surname;
        Grades = grades;
    }
}

// Клас відкритої хеш-таблиці з ланцюжками
public class OpenHashTable
{
    private const int Size = 100; // Розмір хеш-таблиці
    private List<StudentData>[] table;

    public OpenHashTable()
    {
        table = new List<StudentData>[Size];
    }

    // Хеш-функція для обчислення індексу
    private int HashFunction(string surname)
    {
        return surname.Length % Size;
    }

    // Додавання студента до хеш-таблиці
    public void Add(string surname, int[] grades)
    {
        int index = HashFunction(surname);
        if (table[index] == null)
            table[index] = new List<StudentData>();

        table[index].Add(new StudentData(surname, grades));
    }

    // Пошук студента в хеш-таблиці
    public bool Search(string surname, out int[] grades)
    {
        int index = HashFunction(surname);
        if (table[index] != null)
        {
            foreach (var student in table[index])
            {
                if (student.Surname == surname)
                {
                    grades = student.Grades;
                    return true;
                }
            }
        }
        grades = null;
        return false;
    }

    // Видалення студента з хеш-таблиці
    public bool Delete(string surname)
    {
        int index = HashFunction(surname);
        if (table[index] != null)
        {
            var student = table[index].FirstOrDefault(s => s.Surname == surname);
            if (student != null)
            {
                table[index].Remove(student);
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        OpenHashTable hashTable = new OpenHashTable();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Пошук студента");
            Console.WriteLine("3. Видалити студента");
            Console.WriteLine("4. Вихід");
            Console.Write("Виберіть опцію: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Введено некоректну опцію. Спробуйте ще раз.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent(hashTable);
                    break;
                case 2:
                    SearchStudent(hashTable);
                    break;
                case 3:
                    DeleteStudent(hashTable);
                    break;
                case 4:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Введено некоректну опцію. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void AddStudent(OpenHashTable hashTable)
    {
        Console.Write("Введіть прізвище студента: ");
        string surname = Console.ReadLine();

        int[] grades = new int[3];
        for (int i = 0; i < grades.Length; i++)
        {
            Console.Write($"Введіть оцінку {i + 1}: ");
            if (!int.TryParse(Console.ReadLine(), out grades[i]))
            {
                Console.WriteLine("Введено некоректну оцінку. Спробуйте ще раз.");
                return;
            }
        }

        hashTable.Add(surname, grades);
        Console.WriteLine("Студент доданий успішно.");
    }

    static void SearchStudent(OpenHashTable hashTable)
    {
        Console.Write("Введіть прізвище студента для пошуку: ");
        string surname = Console.ReadLine();

        if (hashTable.Search(surname, out int[] grades))
        {
            Console.WriteLine($"Знайдено студента: {surname}");
            Console.WriteLine($"Оцінки: {string.Join(", ", grades)}");
        }
        else
        {
            Console.WriteLine($"Студента з прізвищем {surname} не знайдено.");
        }
    }

    static void DeleteStudent(OpenHashTable hashTable)
    {
        Console.Write("Введіть прізвище студента для видалення: ");
        string surname = Console.ReadLine();

        if (hashTable.Delete(surname))
        {
            Console.WriteLine($"Студент {surname} видалений успішно.");
        }
        else
        {
            Console.WriteLine($"Студента з прізвищем {surname} не знайдено.");
        }
    }
}

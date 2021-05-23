using System;
using System.IO;
using System.Collections.Generic;

namespace student_app
{
    public class subjects
    {
        public subjects(string path)//creates a file with names and IDs of items that are in the system
        {
            using (StreamWriter sw = new StreamWriter(path + "SbjctList"))
            {
                sw.WriteLine("101 C#");
                sw.WriteLine("102 Java");
                sw.WriteLine("103 Python");
            }
        }
    }
    class student
    {
        public student(string path, int student_number, string first_name, string last_name, int[] marks)//add new stident
        {
            using (StreamWriter sw = new StreamWriter(path + student_number))
            {
                using (StreamReader sr = new StreamReader(path + "SbjctList"))
                {
                    sw.WriteLine(student_number);
                    sw.WriteLine(first_name);
                    sw.WriteLine(last_name);
                    int i = 0;
                    while (true)
                    {
                        try
                        {
                            sw.WriteLine(sr.ReadLine() + "  " + marks[i]);
                            i++;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
            string[] text = new string[1];
            if (File.Exists(path + "StdList"))
            {
                int i = 0;
                using (StreamReader sr = new StreamReader(path + "StdList"))
                {
                    text[i] = sr.ReadLine();
                    while (text[i] != null)
                    {
                        try
                        {
                            i++;
                            Array.Resize(ref text, text.Length + 1);
                            text[i] = sr.ReadLine();
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(path + "StdList"))
                {
                    i = 0;
                    while (i != text.Length)
                    {
                        try
                        {
                            if (text[i] != null)
                                sw.WriteLine(text[i]);
                            i++;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    sw.WriteLine(student_number);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path + "StdList"))
                {
                    sw.WriteLine(student_number);
                }
            }
        }

        class Program
        {
            static public string getInfoId(int id, string path, int j)//displays a specific line in the student file (as an example: displays id)
            {
                string result = "";
                using (StreamReader sr = new StreamReader(path + id))
                {
                    int i = 0;
                    while (i < j)
                    {
                        sr.ReadLine();
                        i++;
                    }
                    result = sr.ReadLine();
                    return (result);
                }
            }
            static public void showInfo(int id, string path)//displays all information about a student by ID to the console
            {
                using (StreamReader sr = new StreamReader(path + id))
                {
                    string o = "o";
                    Console.WriteLine("Student number: " + sr.ReadLine());
                    Console.WriteLine("First name: " + sr.ReadLine());
                    Console.WriteLine("Last name: " + sr.ReadLine());
                    Console.WriteLine("Score:");
                    while (o != null)
                    {
                        o = sr.ReadLine();
                        Console.WriteLine(o);
                    }
                }
            }
            static public int[] takeId(string path)//returns all id from the list of students
            {
                int i = 0;
                int[] id = new int[1];
                using (StreamReader sr = new StreamReader(path))
                {
                    
                    id[i] = Convert.ToInt32(sr.ReadLine());
                    while (id[i] != 0)
                    {
                        try
                        {
                            i++;
                            Array.Resize(ref id, id.Length + 1);
                            id[i] = Convert.ToInt32(sr.ReadLine());
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                return (id);
            }
            static public int check_int(int from, int to)//checks a number within certain limits
            {
                int Num;
                while (true)
                {
                    try
                    {
                        Num = Convert.ToInt32(Console.ReadLine());
                        if ((from <= Num) && (to >= Num))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a number in this range:" + " from " + from + " to " + to);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error, enter only numbers");
                    }
                }
                return (Num);
            }
            static public void sortIdList(string path)//sorts all IDs from smallest to largest
            {
                int[] numbers = takeId(path + "StdList");
                Array.Sort(numbers);
                File.Delete(path + "StdList");
                using (StreamWriter sw = new StreamWriter(path + "StdList"))
                {
                    int i = 1;
                    while(i < numbers.Length)
                    {
                        sw.WriteLine(numbers[i]);
                        i++;
                    }
                }
                
            }
            static public int auto_id(string path)//sets automatic ID by brute-force ID
            {
                int student_number = 200;
                while (true)
                {
                    if (File.Exists(path + student_number))
                    {
                        student_number++;
                    }
                    else
                    {
                        break;
                    }
                }
                return (student_number);
            }
            static void Main(string[] args)
            {
                string path = @"C:\Users\1392659\Desktop\Student_Info\";
                string first_name = "";
                string last_name = "";
                string sub;
                int student_number = 200;
                int[] marks = new int[3];
                string[] firstName = {"Viktoria", "Iris","Sasha", "Ulya", "Denis", "Alena" };//students with some test data
                string[] lastName = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis" };
                int q = 0;
                Directory.CreateDirectory(path);
                new subjects(path);
                if (!File.Exists(path + "StdList"))
                {
                    while (q!=6)
                    {
                        marks[0] = new Random().Next(1, 100);
                        marks[1] = new Random().Next(1, 100);
                        marks[2] = new Random().Next(1, 100);
                        new student(path, student_number, firstName[q],lastName[q] , marks) ;
                        q++;
                        student_number++;

                    }
                }
                Console.WriteLine("Choose one of the options:");
                Console.WriteLine("add new student");
                Console.WriteLine("del student");
                Console.WriteLine("list");
                Console.WriteLine("sort");
                Console.WriteLine("search");
                Console.WriteLine("score");
                string answer = Console.ReadLine();//You need to carefully enter words, otherwise an error will appear if you make a typo
                marks = new int[0];
                switch (answer)
                {
                    case ("add new student"):
                        student_number = auto_id(path);
                        Console.WriteLine("The student is assigned an automatic ID: " + student_number);
                        Console.WriteLine("enter student first name:");
                        first_name = Console.ReadLine();
                        Console.WriteLine("enter student last name");
                        last_name = Console.ReadLine();
                        using (StreamReader sr = new StreamReader(path + "SbjctList"))
                        {
                            sub = sr.ReadLine();
                            while (sub != null)
                            {
                                Array.Resize(ref marks, marks.Length + 1);
                                Console.WriteLine("Enter the number of points for the subject" + sub.Remove(0, 3));
                                marks[marks.Length - 1] = check_int(0, 100);
                                sub = sr.ReadLine();
                            }
                            new student(path, student_number, first_name, last_name, marks);
                            sortIdList(path);
                        }
                        break;
                    case ("del student"):
                        Console.WriteLine("Enter the number of the student you want to remove");
                        while (true)
                            try
                            {
                                student_number = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Enter only numbers");
                            }
                        File.Delete(path + student_number);
                        int[] id = takeId(path + "StdList");
                        int i = 0;
                        while (i != id.Length)//overwrites file from ID without remote ID
                        {
                            try
                            {
                                if (id[i] == student_number)
                                {
                                    id[i] = 0;
                                }
                                i++;
                            }
                            catch
                            {
                                break;
                            }
                        }
                        using (StreamWriter sw = new StreamWriter(path + "StdList"))
                        {
                            i = 0;
                            while (i != id.Length - 1)
                            {
                                try
                                {
                                    if (id[i] != 0)
                                    {

                                        sw.WriteLine(id[i]);
                                    }
                                    i++;
                                }
                                catch
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    case ("list"):
                        id = takeId(path + "StdList");
                        i = 0;
                        while (i != id.Length - 1)
                        {
                            if (File.Exists(path + id[i]))
                            {
                                showInfo(id[i], path);
                            }
                            i++;
                        }
                        break;
                    case ("sort"):
                        id = takeId(path + "StdList");
                        string[] name = new string[0];
                        i = 0;
                        Console.WriteLine("sort students by:");
                        Console.WriteLine("first name");
                        Console.WriteLine("last name");
                        Console.WriteLine("student number");
                        answer = Console.ReadLine();
                        while (i != id.Length - 1)
                        {
                            using (StreamReader sr = new StreamReader(path + id[i]))//
                            {
                                if (answer == "last name")
                                {
                                    sr.ReadLine();
                                    sr.ReadLine();
                                }
                                else if (answer == "first name")
                                {
                                    sr.ReadLine();
                                }
                                else if (answer == "student number")
                                {

                                }
                                Array.Resize(ref name, name.Length + 1);
                                name[i] = sr.ReadLine();
                            }
                            i++;
                        }
                        string[] name2 = new string[name.Length];
                        int[] id2 = new int[id.Length];
                        int j = 0;
                        i = 0;
                        Array.Copy(name, name2, name.Length);
                        Array.Sort(name);
                        while (j != name.Length)
                        {
                            if (name[j] == name2[i])
                            {
                                id2[j] = i;
                                j++;
                                i = 0;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        i = 0;
                        while (i != id.Length - 1)
                        {
                            showInfo(id[id2[i]], path);
                            i++;
                        }
                        break;
                    case ("search"):
                        i = 0;
                        int count = 0;
                        string result = "";
                        name = new string[0];
                        id = takeId(path + "StdList");
                        Console.WriteLine("student search by:");
                        Console.WriteLine("first name");
                        Console.WriteLine("last name");
                        Console.WriteLine("student number");
                        answer = Console.ReadLine();
                        if(answer == "student number")
                        {
                            sortIdList(path);
                            id = takeId(path + "StdList");
                            Console.WriteLine("Enter number of student");
                            int number = Convert.ToInt32(Console.ReadLine());
                            int resultId = 0;
                            int less = 0;
                            int more = id.Length;
                            int mid = 0;
                            while (true)//binary search
                            {
                                mid = (less + more) / 2;
                                if (id[mid] == number)
                                {
                                    resultId = mid;
                                    Console.WriteLine("Id number in the queue is " + resultId);
                                    showInfo(id[resultId],path);
                                    break;
                                }
                                else if (id[mid] < number)
                                {
                                    less = mid + 1;
                                }
                                else if (id[mid] > number)
                                {
                                    more = mid - 1;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter " + answer + " of student");
                            string target = Console.ReadLine();
                            if (answer == "first name")
                            {
                                count = 1;
                            }
                            else
                            {
                                count = 2;
                            }
                            while (i < id.Length)//Implement linear search
                            {

                                result = getInfoId(id[i], path, count);
                                if (target == result)
                                {
                                    showInfo(id[i], path);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    case ("score"):
                        i = 0;
                        int[] score = new int[0];

                        id = takeId(path + "StdList");
                        Console.WriteLine("Write the ID of the subject");
                        string sbjct = Console.ReadLine();
                        int string_of_subject = 3;

                        while (i < id.Length-1)
                        {
                            string test = getInfoId(id[i], path,string_of_subject);
                            if (test.Contains(sbjct))
                            {
                                test = test.Remove(0, test.Length - 3);
                                Array.Resize(ref score, score.Length + 1);
                                score[i] = Convert.ToInt32(test);
                                i++;
                            }
                            else
                            {
                                string_of_subject++;
                            }
                        }
                        i = 0;
                        int max_score = -1;
                        int max_id = -1;
                        while (i < id.Length - 1)
                        {
                            if (max_score < score[i])
                            {
                                max_score = score[i];
                                max_id = i;
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        Console.WriteLine("This student has the highest("+ max_score +") score in this subject:");
                        showInfo(id[max_id],path);
                        break;

                }
            }
        }
    }
}
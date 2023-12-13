using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Events_StudentExam
{
	public delegate void ExamDelegate(string t);
	class Student
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BD { get; set; }
		public int studentMark { get; set; }
		public void Exam(string exam_work)

		{
			Console.WriteLine($"студент {LastName} выполнил {exam_work} c оценкой {studentMark}");
		}
	}
	class Teacher
	{
		Random rnd = new Random();
		SortedList<int, ExamDelegate> examSortedEvents = new SortedList<int, ExamDelegate>();

		//Генерируется случайное значение для установки делегатов в случайном порядке. Подписка и отписка в случайном порядке
		public event ExamDelegate examEvent
		{

			add
			{

				for (int key; ;)
				{
					key = rnd.Next();
					if (!examSortedEvents.ContainsKey(key))
					{
						examSortedEvents.Add(key, value);
						break;
					}
				}
			}
			remove
			{
				try
				{
					examSortedEvents.RemoveAt(examSortedEvents.
					IndexOfValue(value));
				}
				catch (Exception ex)
				{
					//Console.WriteLine($"Студентов больше нет: {ex.Message}");
				}

			}
		}
		//Вызов событий
		public void Exam(string task)
		{
			foreach (int item in examSortedEvents.Keys)
			{
				if (examSortedEvents[item] != null)
				{
					examSortedEvents[item](task);
				}
			}
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Random rndMark = new Random();
			List<Student> abiturients = new List<Student>
				 {
					new Student{FirstName="Иван", LastName="Пупкин", BD=new DateTime(2000,1,1), studentMark = 100},
					new Student{FirstName="Петр", LastName="Зайцев", BD=new DateTime(2014,3,10), studentMark = 100},
					new Student{FirstName="Джим", LastName="Рейнор", BD=new DateTime(2205,4,23), studentMark = 100},
					new Student{FirstName="Сара", LastName="Керриган", BD=new DateTime(2200,8,13), studentMark = 100},
					new Student{FirstName="Волтер", LastName="Уайт", BD=new DateTime(1970,7,19), studentMark = 100}
				};
			Teacher teacher = new Teacher();
			int rounds = 3;

			foreach (Student gays in abiturients)
			{
				teacher.examEvent += gays.Exam;
			}
			for (int i = 0; i < rounds; i++)
			{
				Console.WriteLine($"Итог тура №{i + 1}");
				foreach (Student item in abiturients)
				{
					{
						if (item.studentMark >= 7)
						{
							item.studentMark = rndMark.Next(4, 12);
						}
						else
						{
							teacher.examEvent -= item.Exam;
						}
					}
				}
				teacher.Exam($"Задание №{i + 1}");
			}
		}
	}
}


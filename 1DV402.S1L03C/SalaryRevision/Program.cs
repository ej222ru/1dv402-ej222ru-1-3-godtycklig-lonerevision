using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace SalaryRevision
{
	class Program
	{
		// Declare a resource manager to retrieve resources in all class methods.
		static ResourceManager rm;

		static void Main(string[] args)
		{
			// Create a resource manager to retrieve resources.
			rm = new ResourceManager("SalaryRevision.Strings", Assembly.GetExecutingAssembly());

			int noOfSalaries = 0;
			int[] salaries;
			do
			{
				noOfSalaries = ReadInt(rm.GetString("NoOfSalaries_Prompt"));
				salaries = ReadSalaries(noOfSalaries);
				ViewResult(salaries);

			} while (IsContinuing());
		}
		/// <summary>
		/// Prints a message and expects a key respons from the user.
		/// </summary>
		/// <returns>Returns true if user input is anything but ESC</returns>
		private static bool IsContinuing()
		{
			viewMessage(rm.GetString("Continue_Prompt"));
			ConsoleKeyInfo cki = Console.ReadKey(true);
			Console.Clear();
			return (cki.Key != ConsoleKey.Escape);
		}
		/// <summary>
		/// Read input from user/screen which must be an integer.
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		private static int ReadInt(string prompt)
		{
			int ret = 0;
			string input=""; 
			do
			{
				try
				{
					Console.Write(prompt);
					input = Console.ReadLine();
					ret = int.Parse(input);

					if (ret < 2)
					{
						viewMessage(rm.GetString("Error2_Message"), ConsoleColor.Red);
					}
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), input), ConsoleColor.Red);
				}
			} while (ret < 2);
			return ret;
		}
		/// <summary>
		/// Reads the number of salaries previously given by the user.
		/// Salaries must be given as integer values
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		private static int[] ReadSalaries(int count)
		{
			int[] iaSalaries = new int[count];
			for (int i=0; i<count; i++)
			{
				iaSalaries[i] = ReadInt(string.Format(rm.GetString("Salary_Prompt"), i + 1));
			}
			return iaSalaries;
		}

		private static void viewMessage(string message, ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
		{
			Console.WriteLine("");
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("");
		}
		/// <summary>
		/// Displays calculated values for median, average and dispersion for the salaries given as an integer array parameter.
		/// Also displays the salaries in the parameter
		/// </summary>
		/// <param name="salaries"></param>
		private static void ViewResult(int[] salaries)
		{
			int median = salaries.Median();
			Double average = salaries.Average();
			int dispersion = salaries.Dispersion();

			Console.WriteLine("");
			Console.WriteLine(rm.GetString("Divider_String"));
			Console.WriteLine(String.Format("{0,-15}{1, 9:c0}", rm.GetString("MedianSalary_Text"), median));
			Console.WriteLine(String.Format("{0,-15}{1, 9:c0}", rm.GetString("AverageSalary_Text"), average));
			Console.WriteLine(String.Format("{0,-15}{1, 9:c0}", rm.GetString("SalaryDistribution_Text"), dispersion));
			Console.WriteLine(rm.GetString("Divider_String")); 
			Console.WriteLine("");

			// print given salaries, three in each row
			int noOfSalaries = salaries.Length;
			int rows = noOfSalaries / 3;
			int lastRow = noOfSalaries % 3;
			int item = 0;
			for (int i=0; i<rows; i++)
			{
				Console.WriteLine(String.Format("{0,8}{1,8}{2,8}", salaries[item], salaries[item+1], salaries[item+2]));
				item +=2;
			}
			if (lastRow > 0)
			{
				if (lastRow == 1)
					Console.WriteLine(String.Format("{0,8}", salaries[item+1]));
				else if (lastRow == 2)
					Console.WriteLine(String.Format("{0,8}{1,8}", salaries[item+1], salaries[item+2]));
			}
		}
	}

	static class MyExtensions
	{
		/// <summary>
		/// Calculates dispersion for given values in the integer array parameter
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int Dispersion(this int[] source)
		{
			int ret = 0;
			int maxValue = source.Max();
			int minValue = source.Min();
			return ret = maxValue - minValue;
		}
		/// <summary>
		/// Calculates median for given values in the integer array parameter
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int Median(this int[] source)
		{
			int median = 0;
			int noOfElements = source.Length;
			int[] sortedSource = new int[noOfElements];
			// Input parameter source must not be altered. 
			// Create a copy of the array to do the math
			Array.Copy(source, sortedSource, source.Length);
			Array.Sort(sortedSource);

			if ((noOfElements % 2) == 0)
			{
				// even numbers, caluculate median as an average of the two
				median = (sortedSource[sortedSource.Length/2 - 1] + sortedSource[sortedSource.Length/2]) / 2;
			}
			else
				median = sortedSource[noOfElements/2];   // index of array starts at 0
			return median;
		}
	}
}

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
			do
			{
				int[] salaries;
				try
				{
					noOfSalaries = ReadInt(rm.GetString("NoOfSalaries_Prompt"));
					if (noOfSalaries < 2)
					{
						throw new ArgumentException(rm.GetString("Error2_Message"));
					}

					salaries = ReadSalaries(noOfSalaries);
					ViewResult(salaries);
				}
				catch (ArgumentException exception)
				{
					viewMessage(string.Format(exception.Message, noOfSalaries), ConsoleColor.Red);
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), noOfSalaries), ConsoleColor.Red);
				}
			} while ((noOfSalaries < 2) || IsContinuing());
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
		/// 
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		private static int ReadInt(string prompt)
		{
			int ret = 0;
			string input="";
			bool getInput = true;

			while (getInput)
			{
				try
				{
					Console.Write(prompt);
					input = Console.ReadLine();
					ret = int.Parse(input);
					getInput = false;
				}
				catch (FormatException)
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), input), ConsoleColor.Red);
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), input), ConsoleColor.Red);
				}
			}

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
			int[] salaries = new int[count];
			for (int i=0; i<count; i++)
			{
				try
				{
					salaries[i] = ReadInt(string.Format(rm.GetString("Salary_Prompt"), i + 1));
					if (salaries[i] < 0)
					{
						throw new ArgumentException(rm.GetString("Error_Message"));
					}
				}
				catch (ArgumentException exception)
				{
					viewMessage(string.Format(exception.Message, salaries[i]), ConsoleColor.Red);
					i--;
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), salaries[i]), ConsoleColor.Red);
					i--;
				}
			}
			return salaries;
		}

		private static void viewMessage(string message, ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
		{
			Console.WriteLine("");
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
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
			int fullRows = noOfSalaries / 3;
			int lastRow = noOfSalaries % 3;
			int item = 0;
			for (int i = 0; i < fullRows; i++)
			{
				Console.WriteLine(String.Format("{0,8}{1,8}{2,8}", salaries[item], salaries[item + 1], salaries[item + 2]));
				item += 3;
			}
			if (lastRow == 1)
				Console.WriteLine(String.Format("{0,8}", salaries[item]));
			else if (lastRow == 2)
				Console.WriteLine(String.Format("{0,8}{1,8}", salaries[item], salaries[item + 1]));
		}
	}
}

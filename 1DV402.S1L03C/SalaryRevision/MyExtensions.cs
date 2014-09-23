using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryRevision
{
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
				median = (sortedSource[sortedSource.Length / 2 - 1] + sortedSource[sortedSource.Length / 2]) / 2;
			}
			else
				median = sortedSource[noOfElements / 2];   // index of array starts at 0
			return median;
		}
	}
}

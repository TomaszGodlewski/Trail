using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permutation 
{

	public static IEnumerable<T[]> getPermutations<T>(T[] items) 
	{
		int[] work = new int[items.Length];
		for (int i = 0; i < work.Length; i++) 
		{
			work[i] = i;
		}
		foreach (int[] index in getIntPermutations(work, 0, work.Length)) 
		{
			T[] result = new T[index.Length];
			for (int i = 0; i < index.Length; i++) result[i] = items[index[i]];
			yield return result;
		}
	}

	public static IEnumerable<int[]> getIntPermutations(int[] index, int offset, int len) 
	{
		if (len == 1) 
		{
			yield return index;
		} 
		else if (len == 2) 
		{
			yield return index;
			swap(index, offset, offset + 1);
			yield return index;
			swap(index, offset, offset + 1);
		} 
		else 
		{
			foreach (int[] result in getIntPermutations(index, offset + 1, len - 1)) 
			{
				yield return result;
			}
			for (int i = 1; i < len; i++) 
			{
				swap(index, offset, offset + i);
				foreach (int[] result in getIntPermutations(index, offset + 1, len - 1)) 
				{
					yield return result;
				}
				swap(index, offset, offset + i);
			}
		}
	}

	private static void swap(int[] index, int offset1, int offset2) 
	{
		int temp = index[offset1];
		index[offset1] = index[offset2];
		index[offset2] = temp;
	}

}

using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace BionicVisionVR.Backend.Resources
{
    /// <summary>
    /// Static container class for generating random number lists
    /// </summary>
    public static class RandomizedList
    {
        /// <summary>
        /// Creates a random array of [size] ints from [start] to [step*size]
        /// </summary>
        /// <param name="size">Size of array</param>
        /// <param name="start">Lowest value of array</param>
        /// <param name="step">Step amount between values</param>
        /// <returns></returns>
        public static int[] GetRandomizedList(int size, int start=0, int step=1)
        {
            int[] numList = new int[size];
            for (int i = start; i < size; i++)
            {
                numList[i] = i * step; 
            }

            return RandomizeList(numList); 
        }

        private static int[] RandomizeList(int[] numList)
        {
            int[] randomOrder = new int[numList.Length];
            
            for (int i = 0; i < numList.Length; i++)
                randomOrder[i] = (int) Random.Range(0, 100f);
        
            Array.Sort(randomOrder, numList);
            return numList; 
        }
    }
}
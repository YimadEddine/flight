using OAuth_Project.Models;
using System.Collections.Generic;

namespace OAuth_Project.Helper_Classes
{
    public static class BookingHelper
    {
        public static bool isCloseSeat()
        {
            return true;
        }

        public static int hasChildren(Family family)
        {
            int counter = 0;
            foreach (Passenger p in family.Members)
            {
                if (p.Type == "Child")
                {
                    counter++;
                }
            }
            return counter;
        }
        //public static 
        public static List<Tuple<int, int>> FindCloseFalseIndices(bool[,] matrix, int maxDistance)
        {
            List<Tuple<int, int>> closeFalseIndices = new List<Tuple<int, int>>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!matrix[i, j]) // Check if current value is false
                    {
                        // Search for close false neighbors
                        for (int neighborRow = i - maxDistance; neighborRow <= i + maxDistance; neighborRow++)
                        {
                            for (int neighborCol = j - maxDistance; neighborCol <= j + maxDistance; neighborCol++)
                            {
                                // Check for valid neighbor index and false value
                                if (neighborRow >= 0 && neighborRow < matrix.GetLength(0) &&
                                    neighborCol >= 0 && neighborCol < matrix.GetLength(1) &&
                                    !matrix[neighborRow, neighborCol])
                                {
                                    closeFalseIndices.Add(Tuple.Create(neighborRow, neighborCol)); // Only add neighbor index
                                }
                            }
                        }
                    }
                }
            }

            return closeFalseIndices;
        }

        public static List<Tuple<int, int>> FindAvailableSeats(bool[,] matrix, int numberOfSeats)
        {
            List < Tuple<int, int> > result = new List<Tuple<int, int>> ();
           int counter = 0;
            for (int column = 1; column <= 33; column++)
            {
                for (int row = 1; row <= 6; row++)
                {

                }
            }return null;
        }
    }
}

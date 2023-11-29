using System.Text;
using BenchmarkDotNet.Attributes;

namespace BigONetCore
{
    public class BigOBenchmarks
    {
        readonly int[] OrderedNumbers =
        {
            1, 2, 4, 6, 10, 14, 15, 19, 20, 34, 36, 38, 40, 42, 50, 52, 55, 60, 61, 62, 63, 67, 70, 78, 80, 82, 84, 86, 88, 90, 92, 93, 99, 101, 105, 107, 110, 115,118
        };

        readonly int[] UnorderedNumbers =
        {
            2, 23, -575, 1, -400, 8, 44, -90, 0, 4, 180, -32, 323, 73, 59, 663, 35, 45, -67, 28, 3, 5, -5, 52, 7, 62, -20, 9, 92, 63, -342, 12, 53, 200, 234, 756, 456
        };

        [Benchmark(Description = "O(1)")]
        public void BigO_1()
        {
            var sum = Sum(3, 3);
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }

        [Benchmark(Description = "O(log n)")]
        public void BigO_LogN()
        {
            BinarySearch(OrderedNumbers, 52);
        }

        static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                {
                    return mid;
                }
                else if (array[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }

        [Benchmark(Description = "O(n)")]
        public void BigO_N()
        {
            SearchValue(38);
        }

        public bool SearchValue(int searchedNumber)
        {
            foreach (var element in OrderedNumbers)
            {
                if (element == searchedNumber)
                    return true;
            }

            return false;
        }

        [Benchmark(Description = "O(n log n)")]
        public void BigO_NLogN()
        {
            Ordering();
        }

        public void Ordering()
        {
            var numbersMergeSort = MergeSort(UnorderedNumbers.ToArray<int>(),
                0, UnorderedNumbers.Length - 1).ToList<int>();

            var sbMergeSort = new StringBuilder();
            for (int count = 0; count < numbersMergeSort.Count; count++)
            {
                sbMergeSort.Append(numbersMergeSort[count].ToString() + "==>");
            }
        }

        public static int[] MergeSort(int[] inputItems, int lowerBound, int upperBound)
        {
            if (lowerBound < upperBound)
            {
                int middle = (lowerBound + upperBound) / 2;

                MergeSort(inputItems, lowerBound, middle);
                MergeSort(inputItems, middle + 1, upperBound);

                int[] leftArray = new int[middle - lowerBound + 1];
                int[] rightArray = new int[upperBound - middle];

                Array.Copy(inputItems, lowerBound, leftArray, 0, middle - lowerBound + 1);
                Array.Copy(inputItems, middle + 1, rightArray, 0, upperBound - middle);

                int i = 0;
                int j = 0;
                for (int count = lowerBound; count < upperBound + 1; count++)
                {
                    if (i == leftArray.Length)
                    {
                        inputItems[count] = rightArray[j];
                        j++;
                    }
                    else if (j == rightArray.Length)
                    {
                        inputItems[count] = leftArray[i];
                        i++;
                    }
                    else if (leftArray[i] <= rightArray[j])
                    {
                        inputItems[count] = leftArray[i];
                        i++;
                    }
                    else
                    {
                        inputItems[count] = rightArray[j];
                        j++;
                    }
                }
            }
            return inputItems;
        }


        [Benchmark(Description = "O(n^2)")]
        public void BigO_N2()
        {
            NestedLoop();
        }

        public static void NestedLoop()
        {
            var array = new int[20000];
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        [Benchmark(Description = "O(2^n)")]
        public void BigO_2n()
        {
            Fibonacci(13);
        }


        public static int Fibonacci(int n)
        {
            var result = 0;
            if (n <= 1)
            {
                result = n;
            }
            else
            {
                result = Fibonacci(n - 1) + Fibonacci(n - 2);
            }

            return result;
        }
    }
}
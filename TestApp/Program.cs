﻿using System;
using System.Linq;

public class Solution
{
    public static int[] FindMissingAndRepeatedValues(int[][] grid)
    {
        int twice = 0;
        int missing = 0;
        SortedSet<int> result = [];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (!result.Contains(grid[i][j]))
                {
                    result.Add(grid[i][j]);
                }
                else
                {
                    twice = grid[i][j];
                }
            }
        }

        for (int i = 0; i <= (grid.Length * grid.Length); i++)
        {
            if (!result.Contains(i)) missing = i;
        }

        return [twice, missing];
    }
    public static int LengthOfLongestSubstring(string s)
    {
        int maxL = 0;
        int start = 0;
        HashSet<char> set = [];

        for (int end = 0; end < s.Length; end++)
        {
            while (set.Contains(s[end]))
            {
                set.Remove(s[start]);
                start++;
            }
            set.Add(s[end]);
            maxL = Math.Max(maxL, end - start + 1);
        }

        return maxL;
    }
    public static int MaxPerformance(int n, int[] speed, int[] efficiency, int k)
    {
        int mod = 1000000007;

        List<(int efficiency, int speed)> engineers = new();
        for (int i = 0; i < n; i++)
        {
            engineers.Add((efficiency[i], speed[i]));
        }

        engineers.Sort((a,b) => b.efficiency.CompareTo(a.efficiency));

        PriorityQueue<int, int> minHeap = new();
        long totalSpeed = 0;
        long maxPerformance = 0;

        foreach (var engineer in engineers)
        {
            int currSpeed = engineer.speed;

            totalSpeed += currSpeed;
            minHeap.Enqueue(currSpeed, currSpeed);

            if (minHeap.Count > k)
            {
                totalSpeed -= minHeap.Dequeue();
            }

            maxPerformance = Math.Max(maxPerformance, totalSpeed * engineer.efficiency);
        }

        return (int)(maxPerformance % (mod));
    }
    public static long ColoredCells(int n)
    {
        return 2L * n * (n - 1) + 1;
    }
    public int CountUnguarded(int m, int n, int[][] guards, int[][] walls)
    {
        int[,] matris = new int[m, n];
        int unprotected = 0;

        foreach (var wall in walls)
        {
            matris[wall[0], wall[1]] = -1;
        }

        foreach (var guard in guards)
        {
            matris[guard[0], guard[1]] = 1;
        }

        foreach (var guard in guards)
        {
            int x = guard[0], y = guard[1];

            for (int i = y + 1; i < n && matris[x, i] != -1; i++)
            {
                if (matris[x, i] != 1)
                {
                    matris[x, i] = 2;
                }
            }

            for (int i = y - 1; i >= 0 && matris[x, i] != -1; i--)
            {
                if (matris[x, i] != 1)
                {
                    matris[x, i] = 2;
                }
            }

            for (int i = x + 1; i < m && matris[i, y] != -1; i++)
            {
                if (matris[i, y] != 1)
                {
                    matris[i, y] = 2;
                }
            }

            for (int i = x - 1; i >= 0 && matris[i, y] != -1; i--)
            {
                if (matris[i, y] != 1)
                {
                    matris[i, y] = 2;
                }
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matris[i, j] + " ");
                if (matris[i, j] == 0) unprotected++;
            }
        }

        return unprotected;
    }
    
    static string ternary = "";
    public static bool CheckPowersOfThree(int n)
    {
        ternary += (n % 3).ToString();
        n /= 3;
        if (n > 0)
            CheckPowersOfThree(n);
        return !ternary.Contains('2');
    }
    public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int[] packed = [.. nums1, .. nums2];

        if (packed.Length <= 1)
        {
            return packed[0];
        }

        Array.Sort(packed);

        return packed.Length % 2 == 0 ? (double)(((double)packed[packed.Length / 2 - 1] + (double)packed[packed.Length / 2]) / 2) : (double) packed[(packed.Length - 1) / 2];
    }

    //public static int MaxEqualRowsAfterFlips(int[][] matrix)
    //{

    //}

    public static int[] ApplyOperations(int[] nums)
    {

        for (int i = 0; i < nums.Length; i++)
        {
            if (i + 1 < nums.Length)
            {
                if ((nums[i] == nums[i + 1]))
                {
                    nums[i] *= 2;
                    nums[i + 1] = 0;
                }
            }
        }

        List<int> nums_temp = new(nums);
        List<int> finalized = new();
        int zeroCounter = 0;

        for (int i = 0; i < nums_temp.Count; i++)
        {
            if (nums_temp[i] == 0)
            {
                zeroCounter++;
            }
            else
            {
                finalized.Add(nums_temp[i]);
            }
        }

        for (int i = 0; i < zeroCounter; i++)
        {
            finalized.Add(0);
        }

        nums_temp.Clear();

        Console.WriteLine(string.Join(", ", finalized));
        return [.. finalized];
    }

    public static int MinimumDeletions(int[] nums)
    {
        if (nums.Length <= 1) return 1;

        int n = nums.Length;
        int minIdx = 0, maxIdx = 0;

        // Define min and max indexes.
        for (int i = 0; i < n; i++)
        {
            if (nums[i] < nums[minIdx]) minIdx = i;
            if (nums[i] > nums[maxIdx]) maxIdx = i;
        }

        int leftIndex = Math.Min(minIdx, maxIdx);
        int rightIndex = Math.Max(minIdx, maxIdx);

        int removeFromLeft = rightIndex + 1;
        int removeFromRight = n - leftIndex;
        int removeFromBothSides = (leftIndex + 1) + (n - rightIndex);

        return Math.Min(removeFromLeft, Math.Min(removeFromRight, removeFromBothSides));
    }

    public static bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        // RoomData oluşturuluyor.
        Dictionary<int, List<int>> roomData = new Dictionary<int, List<int>>();

        // Ziyaret edilen odaları takip eden HashSet (daha hızlı kontrol için)
        HashSet<int> visited = new HashSet<int>();

        // İlk olarak sadece 0 numaralı odayı ziyaret edebiliriz.
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(0);
        visited.Add(0);

        // Odaları geziyoruz.
        while (queue.Count > 0)
        {
            int room = queue.Dequeue(); // Şu anki odayı al

            // Odanın içindeki anahtarları al
            foreach (int key in rooms[room])
            {
                if (!visited.Contains(key)) // Eğer bu oda daha önce ziyaret edilmemişse
                {
                    visited.Add(key);
                    queue.Enqueue(key);
                }
            }
        }

        // Ziyaret edilen oda sayısı toplam oda sayısına eşitse, hepsine ulaşabiliyoruz.
        return visited.Count == rooms.Count;
    }

    public static char[][] RotateTheBox(char[][] box)
    {
        int m = box.Length;  // Satır sayısı
        int n = box[0].Length;  // Sütun sayısı
        char[][] rotatedBox = new char[n][];  // Yeni matrisin boyutu transpoze olacak şekilde

        // Her satır için yeni bir sütun oluşturuyoruz
        for (int i = 0; i < n; i++)
        {
            rotatedBox[i] = new char[m];  // Her sütun, eski satırlara denk geliyor
        }

        // Kutuyu döndürme işlemi
        for (int i = 0; i < m; i++)  // Eski satırlar
        {
            int emptySlot = n - 1;  // Boşlukları sağa kaydırmak için başlangıçta en sağdaki sütun

            for (int j = n - 1; j >= 0; j--)  // Eski sütunlar
            {
                if (box[i][j] == '#')  // Taşlar sağa kaydırılmaz, olduğu gibi kalmalı
                {
                    rotatedBox[j][emptySlot] = '#';
                    emptySlot--;  // Sonraki boşluğu bir sola kaydır
                }
                else if (box[i][j] == '*')  // İnciler de taşlar gibi sabit kalmalı
                {
                    rotatedBox[j][emptySlot] = '*';
                    emptySlot--;  // Sonraki boşluğu bir sola kaydır
                }
                else  // Boşluk, ileride bir taş ya da inci tarafından dolabilir
                {
                    rotatedBox[j][emptySlot] = '.';
                }

                // Sınır kontrolü ekleyin: emptySlot 0'dan küçük olmamalı
                if (emptySlot < 0)
                {
                    break;  // Eğer emptySlot 0'a düşerse, döngüyü kırın
                }
            }
        }

        // RotatedBox'u ekrana yazdırmak (Test amaçlı)
        Console.WriteLine("Rotated Box:");
        for (int i = 0; i < rotatedBox.Length; i++)
        {
            for (int j = 0; j < rotatedBox[i].Length; j++)
            {
                Console.Write(rotatedBox[i][j] + " ");
            }
            Console.WriteLine();
        }

        return rotatedBox;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(FindMissingAndRepeatedValues([[1, 3], [2, 2]]));
    }
}
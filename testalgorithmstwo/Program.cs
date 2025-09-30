using System;
using System.Diagnostics;

public class Program
{
    class OptA
    {
        public string[] A;
        public int n;
        public OptA(int cap) { A = new string[cap]; n = 0; }
        public bool Insert(string s) { if (n >= A.Length) return false; A[n++] = s; return true; }
        public int Search(string s) { for (int i = 0; i < n; i++) if (A[i] == s) return i; return -1; }
        public bool Delete(string s)
        {
            int i = Search(s);
            if (i < 0) return false;
            for (int j = i; j < n - 1; j++) A[j] = A[j + 1];
            n--;
            return true;
        }
    }

    class OptB
    {
        public string[] A;
        public int n;
        public OptB(int cap) { A = new string[cap]; n = 0; }
        public bool Insert(string s)
        {
            if (n >= A.Length) return false;
            int i = 0;
            while (i < n && string.CompareOrdinal(A[i], s) < 0) i++;
            for (int j = n - 1; j >= i; j--) A[j + 1] = A[j];
            A[i] = s; n++;
            return true;
        }
        public int Search(string s)
        {
            int l = 0, r = n - 1;
            while (l <= r)
            {
                int m = (l + r) >> 1;
                int c = string.CompareOrdinal(A[m], s);
                if (c == 0) return m;
                if (c > 0) r = m - 1; else l = m + 1;
            }
            return -1;
        }
        public bool Delete(string s)
        {
            int i = Search(s);
            if (i < 0) return false;
            for (int j = i; j < n - 1; j++) A[j] = A[j + 1];
            n--;
            return true;
        }
    }

    static void Main()
    {
        int N = 100_000;
        string[] names = new string[N];
        for (int i = 0; i < N; i++) names[i] = "Name" + i.ToString("D7");

        var rnd = new Random(42);
        int searchReps = 10_000;
        string[] searchKeys = new string[searchReps];
        for (int i = 0; i < searchReps; i++) searchKeys[i] = names[rnd.Next(N)];

        int delCount = 1_000;
        string[] delKeys = new string[delCount];
        for (int i = 0; i < delCount; i++) delKeys[i] = names[rnd.Next(N)];

        var sw = new Stopwatch();

        // ===== Option A =====
        Console.WriteLine("=== Option A (unsorted) ===");
        var a = new OptA(N);

        sw.Restart();
        for (int i = 0; i < N; i++) a.Insert(names[i]);
        sw.Stop();
        Console.WriteLine("Insert took " + sw.ElapsedMilliseconds + " ms");

        sw.Restart();
        int tmp = 0;
        for (int i = 0; i < searchReps; i++) tmp ^= a.Search(searchKeys[i]);
        sw.Stop();
        Console.WriteLine("Search took " + sw.ElapsedMilliseconds + " ms");

        sw.Restart();
        for (int i = 0; i < delCount; i++) a.Delete(delKeys[i]);
        sw.Stop();
        Console.WriteLine("Delete took " + sw.ElapsedMilliseconds + " ms");

        // ===== Option B =====
        Console.WriteLine();
        Console.WriteLine("=== Option B (sorted) ===");
        var b = new OptB(N);

        sw.Restart();
        for (int i = 0; i < N; i++) b.Insert(names[i]);
        sw.Stop();
        Console.WriteLine("Insert took " + sw.ElapsedMilliseconds + " ms");

        sw.Restart();
        tmp = 0;
        for (int i = 0; i < searchReps; i++) tmp ^= b.Search(searchKeys[i]);
        sw.Stop();
        Console.WriteLine("Search took " + sw.ElapsedMilliseconds + " ms");

        sw.Restart();
        for (int i = 0; i < delCount; i++) b.Delete(delKeys[i]);
        sw.Stop();
        Console.WriteLine("Delete took " + sw.ElapsedMilliseconds + " ms");

        if (tmp == 123456789) Console.WriteLine("ignore");
    }
}
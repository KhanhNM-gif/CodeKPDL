using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKPDL
{
    class Program
    {
        static void Main(string[] args)
        {
            Day day = new Day("input.txt");
            day.CalMean();
            day.CalMedian();
            day.CalSD();
            day.CalV(2,5);
            day.CalHeSoTuongQuan();

            Console.ReadKey();
        }
    }

    class Day
    {

        public static int LAM_TRON = 2;
        List<float> x { get; set; }
        List<float> xs { get; set; }
        List<float> vx { get; set; }

        float sdXp;
        float sdX
        {
            get { return sdXp; }
            set { sdXp = (float)Math.Round(value, LAM_TRON); }
        }

        List<float> y { get; set; }
        List<float> ys { get; set; }
        List<float> vy { get; set; }

        float sdYp { get; set; }
        float sdY
        {
            get { return sdYp; }
            set { sdYp = (float)Math.Round(value, LAM_TRON); }
        }

        float meanXp { get; set; }
        float meanX
        {
            get { return meanXp; }
            set { meanXp = (float)Math.Round(value, LAM_TRON); }
        }
        int n { get; set; }

        float medianXp;
        float medianX
        {
            get { return medianXp; }
            set { medianXp = (float)Math.Round(value, LAM_TRON); }
        }

        float meanYp;
        float meanY
        {
            get { return meanYp; }
            set { meanYp = (float)Math.Round(value, LAM_TRON); }
        }

        float medianYp;
        float medianY
        {
            get { return medianYp; }
            set { medianYp = (float)Math.Round(value, LAM_TRON); }
        }

        float HeSoTuongQuanp;
        float HeSoTuongQuan
        {
            get { return HeSoTuongQuanp; }
            set { HeSoTuongQuanp = (float)Math.Round(value, LAM_TRON); }
        }
        public Day(string filepath)
        {
            int line = 0;
            foreach (var s in File.ReadLines(filepath))
            {

                string[] str = s.Trim().Split(' ');
                if (str.Count() > 0)
                {
                    if (line == 0) n = int.Parse(str[0]);

                    if (line == 1)
                    {
                        x = new List<float>();
                        for (int i = 0; i < str.Count(); i++)
                            x.Add(float.Parse(str[i]));
                    }

                    if (line == 2)
                    {
                        y = new List<float>();
                        for (int i = 0; i < str.Count(); i++)
                            y.Add(float.Parse(str[i]));
                    }
                    if(line == 3)
                    {
                        LAM_TRON = int.Parse(str[0]);
                    }
                    line++;
                }
            }
        }

        public void CalMean()
        {
            meanX = x.Sum() / n;
            meanY = y.Sum() / n;

            Console.WriteLine($"mean X: {meanX}");
            Console.WriteLine($"mean Y: {meanY}");
        }

        public void CalMedian()
        {
            xs = x.OrderBy(x => x).ToList();
            Console.WriteLine($"X sort: {string.Join(", ", xs)}");

            ys = y.OrderBy(y => y).ToList();
            Console.WriteLine($"Y sort: {string.Join(", ", ys)}");


            if (n % 2 == 0)
            {
                medianX = (xs[n / 2 - 1] + xs[n / 2]) / 2;
                medianY = (ys[n / 2 - 1] + ys[n / 2]) / 2;
            }
            else
            {
                medianX = (xs[n+1 / 2]);
                medianY = (ys[n+1 / 2]);
            }

            Console.WriteLine($"median X: {medianX}");
            Console.WriteLine($"median Y: {medianY}");
        }

        public void CalSD()
        {
            sdX = (float)Math.Sqrt(x.Sum(x => (x - meanX) * (x - meanX)) / n);
            Console.WriteLine($"Do lech chuan X: {sdX}");

            sdY = (float)Math.Sqrt(y.Sum(y => (y - meanY) * (y - meanY)) / n);
            Console.WriteLine($"Do lech chuan Y: {sdY}");
        }

        public void CalV(int min = 0, int max = 1)
        {
            vx = new List<float>();
            vy = new List<float>();

            float minx = x.Min();
            float maxx = x.Max();
            foreach (var item in x)
                vx.Add(((item - minx) / (maxx - minx)) * (max - min) + min);
            Console.WriteLine($"\nX Chuan hoa min - max:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{x[i]}]={Math.Round(vx[i], LAM_TRON)} \n"); }

            float miny = y.Min();
            float maxy = y.Max();
            foreach (var item in y)
                vy.Add(((item - miny) / (maxy - miny)) * (max - min) + min);
            Console.WriteLine($"\nY Chuan hoa min - max:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{y[i]}]={Math.Round(vy[i], LAM_TRON)} \n"); }

            vx.Clear();
            foreach (var item in x)
                vx.Add((item - meanX) / sdX);
            Console.WriteLine($"\nX Chuan hoa z-sc:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{x[i]}]={Math.Round(vx[i], LAM_TRON)} \n"); }


            vy.Clear();
            foreach (var item in y)
                vy.Add((item - meanY) / sdY);
            Console.WriteLine($"\nY Chuan hoa z-sc:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{y[i]}]={Math.Round(vy[i], LAM_TRON)} \n"); }


            vx.Clear();
            float sdXa = x.Sum(x => Math.Abs(x - meanX)) / n;
            foreach (var item in y)
                vx.Add((item - meanX) / sdXa);
            Console.WriteLine($"\nX Chuan hoa z-sc Lech chuan tuyet doi:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{x[i]}]={Math.Round(vx[i], LAM_TRON)} \n"); }


            vy.Clear();
            float sdYa = y.Sum(y => Math.Abs(y - meanY)) / n;
            foreach (var item in y)
                vy.Add((item - meanY) / sdYa);
            Console.WriteLine($"\nY Chuan hoa z-sc Lech chuan tuyet doi:");
            for (int i = 0; i < n; i++) { Console.Write($"v'[{y[i]}]={Math.Round(vy[i], LAM_TRON)} \n"); }
        }

        public void CalHeSoTuongQuan()
        {
            HeSoTuongQuan = 0;
            for (int i = 0; i < n; i++)
                HeSoTuongQuan += (x[i] - meanX) * (y[i] - meanY);

            HeSoTuongQuan = HeSoTuongQuan / (n * sdX * sdY);

            Console.WriteLine($"\nHe So Tuong Quan: {HeSoTuongQuan}");
        }
    }
}

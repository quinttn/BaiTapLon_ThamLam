using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balo
{
    internal class Balo
    {
        static void nhap(out int n, out int[] a, out int[] b, out int M, out string[] tenDoVat)
        {
            n = 4; // Số lượng đồ vật
            M = 37; // Kích thước của ba lô

            a = new int[] { 0, 15, 10, 2, 4 };  // Mảng chứa trọng lượng của từng đồ vật
            b = new int[] { 0, 30, 25, 2, 6 };  // Mảng chứa giá trị của từng đồ vật
            tenDoVat = new string[] { "", "A", "B", "C", "D" }; // Tên các đồ vật

            Console.WriteLine("\n\t\tDanh sach cac loai do vat");

            // In tiêu đề bảng
            Console.WriteLine("| {0,-10} | {1,-11} | {2,-7} | {3,-7} |", "Loai do vat", "Trong luong", "Gia tri", "Don gia");
            Console.WriteLine("+-------------+-------------+---------+---------+");

            // In thông tin các đồ vật
            for (int i = 1; i <= n; i++)
            {
                float donGia = (float)b[i] / a[i]; // Tính đơn giá
                Console.WriteLine("| {0,-10}  | {1,-11} | {2,-7} | {3,-8:0.00}|", tenDoVat[i], a[i], b[i], donGia);
            }

            Console.WriteLine($"\nKich thuoc balo M = {M}");
        }

        static void swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        static void sx(int n, int[] a, int[] b, out int[] id)
        {
            id = new int[n + 1];  // Mảng chứa chỉ số của các đồ vật
            float[] t = new float[n + 1];  // Mảng chứa đơn giá

            for (int i = 1; i <= n; i++)
            {
                id[i] = i;  // Gán chỉ số ban đầu cho từng đồ vật
                t[i] = (float)b[i] / a[i];  // Tính đơn giá của đồ vật thứ i
            }

            // Sắp xếp giảm dần theo đơn giá
            for (int i = 1; i <= n; i++)
            {
                for (int j = i + 1; j <= n; j++)
                {
                    if (t[i] < t[j])
                    {
                        swap(ref a[i], ref a[j]);
                        swap(ref id[i], ref id[j]);
                        swap(ref b[i], ref b[j]);
                        swap(ref t[i], ref t[j]);
                    }
                }
            }
        }

        static void balo(int n, int[] a, int[] b, int M, string[] tenDoVat)
        {
            int[] id;
            sx(n, a, b, out id);

            int tkt = 0, tgt = 0;
            for (int i = 1; i <= n; i++)
            {
                int count = 0;  // Đếm số lần chọn đồ vật i
                while (tkt + a[i] <= M) // Lấy số lượng tối đa của đồ vật hiện tại sao cho tổng trọng lượng không vượt quá M
                {
                    count++;
                    tgt += b[i];  // Cộng giá trị của đồ vật vào tổng giá trị
                    tkt += a[i];  // Cộng trọng lượng của đồ vật vào tổng trọng lượng
                }

                if (count > 0)
                {
                    Console.WriteLine($"Chon {count} vat {tenDoVat[id[i]]} co trong luong = {a[i]} va gia tri = {b[i]}");
                }
            }

            Console.WriteLine($"\nTong trong luong = {tkt}");
            Console.WriteLine($"\nTong gia tri = {tgt}");
        }

        static void Main(string[] args)
        {
            int n, M;
            int[] a, b;
            string[] tenDoVat;
            nhap(out n, out a, out b, out M, out tenDoVat);
            Console.WriteLine();
            balo(n, a, b, M, tenDoVat);
        }
    }
}

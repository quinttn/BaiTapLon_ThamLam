using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaChonHoatDong
{
    internal class LuaChonHoatDong
    {
        // Lớp để lưu trữ thông tin của một hoạt động
        class HoatDong
        {
            public int Start { get; set; }  // Thời gian bắt đầu 
            public int Finish { get; set; } // Thời gian kết thúc
            public string TenHD { get; set; } // Tên của hoạt động
        }

        class Program
        {
            static void Main()
            {
                int n = 8; // Số lượng hoạt động

                HoatDong[] hd = new HoatDong[n];

                hd[0] = new HoatDong { TenHD = "A1", Start = 1, Finish = 4 };
                hd[1] = new HoatDong { TenHD = "A2", Start = 2, Finish = 9 };
                hd[2] = new HoatDong { TenHD = "A3", Start = 7, Finish = 15 };
                hd[3] = new HoatDong { TenHD = "A4", Start = 5, Finish = 8 };
                hd[4] = new HoatDong { TenHD = "A5", Start = 10, Finish = 18 };
                hd[5] = new HoatDong { TenHD = "A6", Start = 16, Finish = 17 };
                hd[6] = new HoatDong { TenHD = "A7", Start = 21, Finish = 27 };
                hd[7] = new HoatDong { TenHD = "A8", Start = 23, Finish = 30 };

                Console.WriteLine("\n\t Danh sach cac hoat dong");
                Console.WriteLine("| {0,-13} | {1,-10} | {2,-8} |", "Ten hoat dong", "Start Time", "End Time");
                Console.WriteLine("+---------------+------------+----------+");

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("| {0,-13} | {1,-10} | {2,-8} |", hd[i].TenHD, hd[i].Start, hd[i].Finish);
                }

                LuaChonHoatDong(hd, n);
                Console.ReadKey();
            }

            static void HoanVi(ref HoatDong a, ref HoatDong b)
            {
                HoatDong t = a;
                a = b;
                b = t;
            }

            // Hàm sắp xếp các hoạt động theo thời gian kết thúc tăng dần
            static void SapXepHoatDong(HoatDong[] hd, int n)
            {
                for (int i = 0; i < n; i++)
                    for (int j = i + 1; j < n; j++)
                    {
                        if (hd[i].Finish > hd[j].Finish)
                            HoanVi(ref hd[i], ref hd[j]);
                    }
            }

            // Hàm lựa chọn các hoạt động
            static void LuaChonHoatDong(HoatDong[] hd, int n)
            {
                // Sắp xếp các hoạt động theo thời gian kết thúc
                SapXepHoatDong(hd, n);
                int i = 0;
                Console.Write("\nCac hoat dong duoc chon la: ");

                // In hoạt động đầu tiên
                Console.Write(hd[i].TenHD);

                // Duyệt qua các hoạt động còn lại và chọn những hoạt động không trùng thời gian
                for (int j = 1; j < n; j++)
                {
                    if (hd[j].Start >= hd[i].Finish)
                    {
                        Console.Write(", " + hd[j].TenHD);
                        i = j;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
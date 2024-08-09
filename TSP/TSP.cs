using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    internal class TSP
    {
        static int soDiem = 5; // Số điểm cố định
        static double[,] maTranKhoangCach; // Ma trận khoảng cách giữa các điểm
        static bool[] daThamQuan; // Mảng đánh dấu các điểm đã được tham quan
        static double chiPhiTong; // Tổng chi phí của hành trình

        static void Main()
        {
            // Khai báo tọa độ các điểm
            double[,] toaDo = new double[,]
            {
            { 0, 0 },   // Điểm 1
            { 1, 2 },   // Điểm 2
            { 2, 4 },   // Điểm 3
            { 3, 1 },   // Điểm 4
            { 4, 3 }    // Điểm 5
            };

            Console.WriteLine("\nDanh sach cac diem va toa do:");
            for (int i = 0; i < soDiem; i++)
            {
                Console.WriteLine($"Diem {i + 1}: ({toaDo[i, 0]}, {toaDo[i, 1]})");
            }

            // Tạo ma trận khoảng cách
            maTranKhoangCach = new double[soDiem, soDiem];
            for (int i = 0; i < soDiem; i++)
            {
                for (int j = 0; j < soDiem; j++)
                {
                    // Tính khoảng cách giữa hai điểm
                    maTranKhoangCach[i, j] = tinhKhoangCach(toaDo[i, 0], toaDo[i, 1], toaDo[j, 0], toaDo[j, 1]);
                }
            }

            Console.WriteLine("\n\t Ma tran khoang cach");
            for (int i = 0; i < soDiem; i++)
            {
                for (int j = 0; j < soDiem; j++)
                {
                    Console.Write($"{maTranKhoangCach[i, j]:F2}\t");
                }
                Console.WriteLine();
            }

            // Khởi tạo mảng đánh dấu các điểm đã được tham quan
            daThamQuan = new bool[soDiem];

            Console.WriteLine("\nDuong di co chi phi thap nhat:");
            duongDiChiPhiThapNhat(0);

            Console.WriteLine($"\nChi phi toi thieu la {chiPhiTong:F2}");

            Console.ReadLine();
        }

        // Hàm tính khoảng cách giữa hai điểm (x1, y1) và (x2, y2)
        static double tinhKhoangCach(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        // Hàm tìm điểm có chi phí thấp nhất từ điểm hiện tại
        static int chiPhiThapNhat(int diem)
        {
            int diemGanNhat = -1; // Khởi tạo biến lưu điểm gần nhất
            double chiPhiThapNhat = double.MaxValue; // Khởi tạo chi phí thấp nhất

            // Duyệt qua tất cả các điểm để tìm điểm gần nhất chưa được tham quan
            for (int i = 0; i < soDiem; i++)
            {
                // Kiểm tra điểm chưa được tham quan và có khoảng cách nhỏ hơn chi phí thấp nhất hiện tại
                if (maTranKhoangCach[diem, i] != 0 && !daThamQuan[i] && maTranKhoangCach[diem, i] < chiPhiThapNhat)
                {
                    chiPhiThapNhat = maTranKhoangCach[diem, i]; // Cập nhật chi phí thấp nhất
                    diemGanNhat = i; // Cập nhật điểm gần nhất
                }
            }

            // Cập nhật tổng chi phí nếu tìm thấy điểm kế tiếp
            if (diemGanNhat != -1)
            {
                chiPhiTong += chiPhiThapNhat;
            }

            return diemGanNhat;
        }

        // Hàm tìm đường đi có chi phí thấp nhất từ điểm hiện tại
        static void duongDiChiPhiThapNhat(int diem)
        {
            daThamQuan[diem] = true; // Đánh dấu điểm hiện tại đã được tham quan
            Console.Write($"{diem + 1} ---> "); 

            int diemKeTiep = chiPhiThapNhat(diem); // Tìm điểm kế tiếp có chi phí thấp nhất
            if (diemKeTiep == -1) // Nếu không tìm thấy điểm kế tiếp
            {
                diemKeTiep = 0; // Quay lại điểm xuất phát
                Console.Write($"{diemKeTiep + 1}");
                chiPhiTong += maTranKhoangCach[diem, diemKeTiep]; // Cập nhật tổng chi phí
                return; // Kết thúc hành trình
            }

            duongDiChiPhiThapNhat(diemKeTiep); // Đệ quy để tìm đường đi tiếp theo
        }
    }
}
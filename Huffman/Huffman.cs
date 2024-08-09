using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Huffman
    {
        class Node
        {
            public char Ch { get; set; }  // Ký tự
            public int Freq { get; set; } // Tần số xuất hiện của ký tự
            public Node Left { get; set; } // Con trái
            public Node Right { get; set; } // Con phải
        }

        private static List<Node> heap = new List<Node>(); // Heap để xây dựng cây Huffman
        private static Dictionary<char, string> huffmanCodes = new Dictionary<char, string>(); // Bảng mã Huffman
        private static Dictionary<string, char> reverseHuffmanCodes = new Dictionary<string, char>(); // Bảng mã Huffman đảo ngược

        static void Main()
        {
            int n = 5; // Số lượng ký tự

            var input = new (char, int)[]
            {
            ('A', 9),
            ('B', 15),
            ('C', 10),
            ('D', 6),
            ('E', 7)
            };

            Console.WriteLine("\nBang tan suat xuat hien");
            Console.WriteLine("| Ky tu | Tan suat |");
            Console.WriteLine("+-------+----------+");
            foreach (var item in input)
            {
                Console.WriteLine($"| {item.Item1,-5} | {item.Item2,-8} |");
            }

            // Thêm các nút vào heap
            foreach (var item in input)
            {
                Node temp = new Node { Ch = item.Item1, Freq = item.Item2 };
                Insert(temp);
            }

            // Xử lý trường hợp chỉ có 1 ký tự
            if (n == 1)
            {
                Console.WriteLine($"Ky tu {heap[0].Ch} ma 0");
                return;
            }

            // Xây dựng cây Huffman
            for (int i = 0; i < n - 1; i++)
            {
                Node left = DeleteMin();
                Node right = DeleteMin();
                Node temp = new Node
                {
                    Ch = '\0', // Ký tự rỗng cho nút nội bộ
                    Freq = left.Freq + right.Freq, // Tần số là tổng của tần số hai nút con
                    Left = left, // Con trái
                    Right = right // Con phải
                };
                Insert(temp);
            }

            // Xây dựng bảng mã Huffman từ cây Huffman
            Node tree = DeleteMin();
            BuildHuffmanCodes(tree, "");
            BuildReverseHuffmanCodes();

            Console.WriteLine("\nBang ma Huffman");
            Console.WriteLine("| Ky tu | Ma Huffman |");
            Console.WriteLine("+-------+------------+");
            foreach (var kvp in huffmanCodes)
            {
                Console.WriteLine($"| {kvp.Key,-5} | {kvp.Value,-11}|");
            }

            // Chuỗi cần nén
            string text = "ABABBCBBDEEEABABBAEEDDCCABBBCDEEDCBCCCCDBBBCAAA";
            Console.WriteLine($"\nChuoi ky tu can nen F: {text}");

            // Nén chuỗi
            string encodedText = Encode(text);
            Console.WriteLine($"\nChuoi da duoc nen F(output): {encodedText}");
        }

        // Hàm thêm nút vào heap
        private static void Insert(Node element)
        {
            heap.Add(element);
            int now = heap.Count - 1;
            // Đẩy phần tử lên trên heap để duy trì tính chất của heap
            while (now > 0 && heap[(now - 1) / 2].Freq > element.Freq)
            {
                heap[now] = heap[(now - 1) / 2];
                now = (now - 1) / 2;
            }
            heap[now] = element;
        }

        // Hàm xóa phần tử nhỏ nhất khỏi heap
        private static Node DeleteMin()
        {
            Node minElement = heap[0];
            Node lastElement = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                int now = 0, child;
                // Đẩy phần tử cuối cùng xuống dưới heap để duy trì tính chất của heap
                while (now * 2 + 1 < heap.Count)
                {
                    child = now * 2 + 1;
                    if (child + 1 < heap.Count && heap[child + 1].Freq < heap[child].Freq)
                        child++;
                    if (lastElement.Freq > heap[child].Freq)
                        heap[now] = heap[child];
                    else
                        break;
                    now = child;
                }
                heap[now] = lastElement;
            }

            return minElement;
        }

        // Hàm xây dựng bảng mã Huffman
        private static void BuildHuffmanCodes(Node node, string code)
        {
            // Nếu là lá thì lưu mã vào bảng mã
            if (node.Left == null && node.Right == null)
            {
                huffmanCodes[node.Ch] = code;
                return;
            }

            // Nếu không thì tiếp tục duyệt cây
            if (node.Left != null)
                BuildHuffmanCodes(node.Left, code + "0");
            if (node.Right != null)
                BuildHuffmanCodes(node.Right, code + "1");
        }

        // Hàm xây dựng bảng mã Huffman đảo ngược
        private static void BuildReverseHuffmanCodes()
        {
            foreach (var kvp in huffmanCodes)
            {
                reverseHuffmanCodes[kvp.Value] = kvp.Key;
            }
        }

        // Hàm mã hóa chuỗi bằng mã Huffman
        private static string Encode(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in text)
            {
                if (huffmanCodes.TryGetValue(ch, out string code))
                {
                    sb.Append(code);
                }
                else
                {
                    throw new ArgumentException($"Character '{ch}' does not have a Huffman code.");
                }
            }
            return sb.ToString();
        }

        // Hàm giải mã chuỗi mã Huffman
        //private static string Decode(string encodedText)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string code = "";
        //    foreach (char bit in encodedText)
        //    {
        //        code += bit;
        //        if (reverseHuffmanCodes.TryGetValue(code, out char ch))
        //        {
        //            sb.Append(ch);
        //            code = "";
        //        }
        //    }
        //    return sb.ToString();
        //}
    }
}
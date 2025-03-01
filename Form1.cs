using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//注释测试11111111
namespace WindowsFormsApp2
{
    public class DetectDLL
    {
        private const string dllPath = @"D:\Program Files (x86)\Microsoft Visual Studio\Project\TEST\20240607opencv_dll_test\opencv_dll_test\x64\Release\opencv_dll_test.dll";

        /// <summary>
        /// 读取输入参数JSON
        /// </summary>
        /// <param name="JsonPath"></param>
        /// <returns></returns>
        [DllImport(dllPath, EntryPoint = "GetInparamsStr", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern string GetInparamsStr(string JsonPath = "./init_para.json");

        /// <summary>
        /// 缺陷检测
        /// </summary>
        /// <param name="InParamsStr"></param>
        /// <param name="ImgPath"></param>
        /// <returns></returns>
        [DllImport(dllPath, EntryPoint = "DefectDetect", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DefectDetect(string InParamsStr, string ImgPath);
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int number = 2;
            //byte[] pBuf = BitConverter.GetBytes(number);
            //foreach (byte b in pBuf)
            //{
            //    Console.WriteLine(b);
            //}
            //string test = "123123";
            //char[] separators = { ',' };
            //string[] tests = test.Split(separators);
            //Console.WriteLine(tests);


            Thread thread = new Thread(ThreadTest);
            thread.Start();
        }
        private void ThreadTest()
        {
            try
            {
                string msg = "./init_para.json";
                int result = DetectDLL.DefectDetect(msg, @"D:/Program Files (x86)/Microsoft Visual Studio/Project/TEST/D_2_2_123456.bmp");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string InkjetStr(int number) {
            int ge = number % 10;
            int shi = (number % 100) / 10;
            int bai = (number / 100) % 10;
            return $"3{bai} 3{shi} 3{ge}";
        }

        /// <summary>
        /// 获取bit指定位数值
        /// </summary>
        /// <param name="val"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static uint getbit(uint val, int index)
        {
            uint temp = 1;
            val &= temp << index;
            val >>= index;
            return val;
        }

        /// <summary>
        /// 设置bit位数
        /// </summary>
        /// <param name="val"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public static void set_bit(ref uint val, int index, uint value)
        {
            if (value > 0)
            {
                val = val | (value << index);
            }
            else
            {
                uint temp = 1;
                val = val & (~(temp << index));
            }
        }

        private void TestBit() {
            uint val = 0, uBuf = 0;
            byte[] pBuf = new byte[2];
            set_bit(ref val, 0, 1);
            set_bit(ref val, 1, 0);
            set_bit(ref val, 2, 0);
            set_bit(ref val, 3, 0);
            set_bit(ref val, 4, 0);
            set_bit(ref val, 5, 0);
            set_bit(ref val, 6, 0);
            set_bit(ref val, 7, 0);
            pBuf[0] = (byte)val;
            uBuf = BitConverter.ToUInt16(pBuf, 0);
            uint result1 = getbit(uBuf, 0);
            uint result2 = getbit(uBuf, 1);
            uint result3 = getbit(uBuf, 2);
            uint result4 = getbit(uBuf, 3);
            uint result5 = getbit(uBuf, 4);
            uint result6 = getbit(uBuf, 5);
            uint result7 = getbit(uBuf, 6);
            uint result8 = getbit(uBuf, 7);
            Console.WriteLine($"{result1}, {result2}, {result3}, {result4}, {result5}, {result6}, {result7}, {result8}");
        }
    }
}

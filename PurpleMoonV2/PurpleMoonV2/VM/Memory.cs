using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.VM
{
    public static class Memory
    {
        // properties
        public const int Size = 4096;
        public static byte[] Data { get; private set; }

        // initialize
        public static void Initialize()
        {
            // initialize ram
            Data = new byte[Size];
        }

        // clear
        public static void Clear() { Data = new byte[Size]; Fill(0x00); }

        // fill
        public static void Fill(byte b)
        {
            for (int i = 0; i < Size; i++) { Data[i] = b; }
        }

        // write byte
        public static bool Write(int addr, byte b)
        {
            if (addr >= Size) { return false; }
            else
            {
                Data[addr] = b;
                return true;
            }
        }

        // write array
        public static bool WriteArray(int addr, byte[] data, int len)
        {
            if (addr + len >= Size) { return false; }
            else
            {
                for (int i = 0; i < len; i++) { Data[addr + i] = data[i]; }
                return true;
            }
        }

        // write list
        public static bool WriteList(int addr, List<byte> data)
        {
            if (addr + data.Count >= Size) { return false; }
            else
            {
                for (int i = 0; i < data.Count; i++) { Data[addr + i] = data[i]; }
                return true;
            }
        }

        // read byte
        public static byte Read(int addr)
        {
            if (addr >= Size) { return 0x00; }
            else
            {
                byte b = Data[addr];
                return b;
            }
        }

        // read array
        public static byte[] ReadArray(int addr, int len)
        {
            if (addr + len >= Size) { return new byte[1] { 0x00 }; }
            else
            {
                byte[] data = new byte[len];
                for (int i = 0; i < len; i++) { data[i] = Data[addr + i]; }
                return data;
            }
        }

        // read list
        public static List<byte> ReadList(int addr, int len)
        {
            List<byte> data = new List<byte>();
            if (addr + len >= Size) { return data; }
            else
            {
                for (int i = 0; i < len; i++) { data.Add(Data[addr + i]); }
                return data;
            }
        }
    }
}

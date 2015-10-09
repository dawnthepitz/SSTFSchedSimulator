using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLApp;

namespace SSTFSchedSimulator
{
    class DiskSpace
    {
        public int diskAddress;
        public bool isTraversed = false;
        public DiskSpace(int diskAddress)
        {
            this.diskAddress = Math.Abs(diskAddress);
        }
    }

    public class SSTFSolver
    {
        static DiskSpace[] SSTF(int start, DiskSpace[] input)
        {
            DiskSpace[] SSTFQueue = new DiskSpace[input.Length + 1];
            SSTFQueue[0] = new DiskSpace(start);
            for (int i = 0; i < input.Length; i++)
            {
                SSTFQueue[i + 1] = findShortest(SSTFQueue[i].diskAddress, input);
            }
            return SSTFQueue;
        }
        static bool allIsTraversed(DiskSpace[] input){
            foreach(DiskSpace disk in input){
                if(!disk.isTraversed)return false;
            }
            return true;
        }
        static DiskSpace findShortest(int current, DiskSpace[] input)
        {
            int i = 0, sr = 0;
            for (i = 0; i < input.Length; i++)
            {
                sr = i;
                if (!input[i].isTraversed) break;
            }
            for (; i < input.Length; i++)
            {
                if (Math.Abs(input[i].diskAddress - current) < Math.Abs(input[sr].diskAddress - current) & (!input[i].isTraversed)) sr = i;
            }
            input[sr].isTraversed = true;
            return input[sr];
        }
        static DiskSpace[] toDiskArray(int[] input)
        {
            DiskSpace[] diskSpace = new DiskSpace[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                diskSpace[i] = new DiskSpace(input[i]);
            }
            return diskSpace;
        }
        static void output(DiskSpace[] disks){
            foreach(DiskSpace disk in disks)
                Console.WriteLine(disk.diskAddress);
        }
        static void plot(DiskSpace[] disk)
        {
            string address="[ ";
            string swinger="[ ";
            for (int i = 0; i < disk.Length; i++)
            {
                address += disk[i].diskAddress + " ";
                swinger += "-" + i + " ";
            }
            address += " ]";
            swinger += "]";
            MLApp.MLApp matlab = new MLApp.MLApp();

            matlab.Execute("plot("+address+","+swinger+")");
        }
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter Initital Address");
            int initialAddress=int.Parse(Console.ReadLine());;
            Console.WriteLine("How many do you want to put");
            int length=int.Parse(Console.ReadLine());
            Console.WriteLine("Input all Addresses to Swing");
            int[] diskSet=new int[length];
            for(int i=0;i<length;i++){
                diskSet[i]=int.Parse(Console.ReadLine());
            }
            Console.WriteLine("SSTF Result");
            output(SSTF(initialAddress, toDiskArray(diskSet)));
            plot(SSTF(initialAddress,toDiskArray(diskSet)));
            Console.ReadLine();
        }
    }
}

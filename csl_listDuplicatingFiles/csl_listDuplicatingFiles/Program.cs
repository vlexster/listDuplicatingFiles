using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace csl_listDuplicatingFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            Console.WriteLine("Please specify folder to process:");
            path = Console.ReadLine();
            while (!Directory.Exists(path)) //proverka dali ima takawa directoriq
            {
                Console.WriteLine("Non-Existent folder! Please specify another one:");
                path = Console.ReadLine();
            };
            
            string[] fileList = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            switch (fileList.Length){
                //Ako nqma elementi, to nqma i unikalni...
                case 0:
                    Console.WriteLine("No files in the specified folder!...");
                    break;
                //Ako e samo edin element, o4ewidno 6te e unikalen w masiwa...
                case 1:
                    Console.WriteLine("[" + Path.GetFileName(fileList[0]) + "]");
                    break;

                default:
                    List<string> unique_files = new List<string>();
                    unique_files.Add(Path.GetFileName(fileList[0])); //pyrwiqt element e winagi unikalen za nowiq listarray
                    for (int i = 1; i < fileList.Length; i++)
                    {
                        bool unique = true; //declaration i assignment na bulewata promenliwa
                        for (int j=0; j<i; j++) // prowerka na wsi4ki elementi predi towa
                        {
                            if (!UniqueFile(fileList[i], fileList[j])) unique = false;
                        }
                        if (unique == true) unique_files.Add(Path.GetFileName(fileList[i]));
                    }
                    Console.Write("[");
                    foreach (string unique_entry in unique_files) Console.Write(unique_entry + ", ");
                    Console.Write("]");
                    break;
            }
            Console.ReadLine(); 
        }
        static string[] AlreadyExists(string[] array)
        {
            List<string> result = new List<string>();
            for (int i=0;i<array.Length+1;i++)
            {
                foreach (string entry in result)
                {
                    if (UniqueFile(array[i], entry)) result.Add(array[i]);
                }
            }
            return result.ToArray();
        }
        static bool UniqueFile(string path1, string path2)
        {
            byte[] file1= File.ReadAllBytes(path1);
            byte[] file2= File.ReadAllBytes(path2);
            if (file1.Length != file2.Length) return true;
            else
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i]) return true;
                } return false;
            }
        }
    }
}

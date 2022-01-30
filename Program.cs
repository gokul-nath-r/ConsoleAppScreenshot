using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleAppScreenshot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Screenshot Segregator");
            // give the folder path for origin
            Console.WriteLine("input folder");
            string input_folder = Console.ReadLine();
            Console.WriteLine(input_folder);
            Console.WriteLine("Output folder path");
            string output_folder = Console.ReadLine();

            try
            {
                //getting the files in the directory
                DirectoryInfo d = new DirectoryInfo(input_folder);
                if (d.Exists)
                {
                    // getting the jpg files
                    FileInfo[] Files = d.GetFiles("*.jpg");
                    string[] Files1 = System.IO.Directory.GetFiles(input_folder); 
                    List<string> Images_lst = new List<string>();
                    HashSet<string> App_lst = new HashSet<string>();

                    // for adding the files name in the txt file
                    //using (StreamWriter sw = File.CreateText(@"F:\MY\Next phase\C sharp\List_of_images"))
                    //{
                    //    foreach (FileInfo file in Files)
                    //    {
                    //        //  Images_lst.Add(file.Name);
                    //    }
                    //    Images_lst.Sort();
                    //    foreach (var item in App_lst)
                    //    {
                    //        // writing the file names in txt file
                    //        Console.WriteLine(item);
                    //        //sw.WriteLine(item);
                    //    }
                    //}

                    foreach (FileInfo file in Files)
                    {
                        App_lst.Add(GetAppName(file.Name));
                    }
                    

                    Console.WriteLine(DirCreation(output_folder, App_lst,Files1));
                    foreach (string item in Files1)
                    {
                        foreach (var appname in App_lst)
                        {
                            string filename = Path.GetFileName(item);
                            if (filename.Contains(appname))
                            {
                                string subdir1 = Path.Combine(output_folder, appname);
                                File.Copy(item, Path.Combine(subdir1,filename),true);
                            }
                           
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static string GetAppName(string name)
        {
            string hello = "Other";
            string[] uniquenames = name.Split('_');
            if (uniquenames.Length > 2)
            {
                string appname = uniquenames[2];
                hello = appname.Substring(0, appname.IndexOf("."));
                //Console.WriteLine(hello);
            }
            return hello;
        }

        public static bool DirCreation(string folderpath, HashSet<string> lst_app, string[] files)
        {
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            foreach (var item in lst_app)
            {
                string subdir = System.IO.Path.Combine(folderpath, item);
                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                }
            }

            //code working fine till the abv part,
            //check and update how to copy the files from that dir to here!
            //foreach (string file in files)
            //{
            //    foreach (var item in lst_app)
            //    {
            //        if (file.Contains(item))
            //        {
            //            string fileName = System.IO.Path.GetFileName(file);
            //            string subdir = fileName = System.IO.Path.Combine(folderpath, item);
            //            string destdir = System.IO.Path.Combine(subdir, fileName);
            //            System.IO.File.Copy(file, destdir, true);
            //        }
            //    }
                
            //}
            
            return true;
        }
    }
}

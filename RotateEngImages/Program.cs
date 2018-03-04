using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
namespace RotateEngImages
{
    class RotateEngImages
    {
        static void Main(string[] args)
        {
            //ar 1, folder for iages, arg 2 path to imageMagick software on local machine
           // string[] filePaths = Directory.GetFiles(@"\\ledadata3.ledalite.com\EngPublic\CONTROLLEDPDFs\images\", "*.png");
            //string[] filePaths = Directory.GetFiles(args[0], "*.png");
            string ImageMagickPath = args[1];
            string fileFilter = args[2];

            if (fileFilter == "")
                fileFilter = "*.png";

            DirectoryInfo dir = new DirectoryInfo(args[0]);

            FileInfo[] files = dir.GetFiles(fileFilter).OrderByDescending(p => p.CreationTime).ToArray();

            
            foreach (FileInfo file in files)
            {       
                Console.WriteLine(file.FullName);
                try
                {
                 //   Console.WriteLine(filePaths[i]);
                    Image img;
                    img = System.Drawing.Image.FromFile(file.FullName);
                    int width = img.Width;
                    int height = img.Height;
                    img.Dispose();

                    if (width > height)
                    {
                        Console.WriteLine("going to rotate" + file.Name + " width: " + width.ToString() + " height: " + height.ToString());
                        
                        string rotateCommand = "\"C:\\Program Files (x86)\\ImageMagick-6.8.6-Q16\\convert.exe\" " + file.FullName + " -rotate 90 " + file.FullName;
                        Console.WriteLine(rotateCommand);

                        
                        Process p = new Process();
                        // Redirect the output stream of the child process.
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.FileName = ImageMagickPath;//"C:\\Program Files (x86)\\ImageMagick-6.8.6-Q16\\convert.exe";
                        //  p.StartInfo.Arguments = filePaths[i] + " -rotate 90 " + filePaths[i];
                        p.StartInfo.Arguments = file.FullName + " -rotate 90 " + file.FullName;

                        p.Start();
                        // Do not wait for the child process to exit before
                        // reading to the end of its redirected stream.
                        // p.WaitForExit();
                        // Read the output stream first and then wait.
                        string output = p.StandardOutput.ReadToEnd();
                        p.WaitForExit();
                        Console.WriteLine(output);
                        p.Dispose();
                         
                    }
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }


            }

        }
    }
}

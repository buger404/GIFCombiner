using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace GIFCombiner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("combined")) Directory.CreateDirectory("combined");
            foreach(string file in args)
            {
                Console.WriteLine("Processing: " + file);
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(file);
                FrameDimension fd = new FrameDimension(bitmap.FrameDimensionsList[0]);
                Bitmap convert = new Bitmap(bitmap.Width * bitmap.GetFrameCount(fd), bitmap.Height);
                Graphics g = Graphics.FromImage((Image)convert);
                for(int i = 0;i < bitmap.GetFrameCount(fd); i++)
                {
                    bitmap.SelectActiveFrame(fd, i);
                    g.DrawImage(bitmap, new Point(i * bitmap.Width, 0));
                }
                string filename = file.Split('\\')[file.Split('\\').Length - 1].Split('.')[0];
                convert.Save("combined\\" + filename + ".png");
                Console.WriteLine("Succeed: " + filename + ".png");
                bitmap.Dispose();
                g.Dispose();
                convert.Dispose();
            }
            if(args.Length == 0)
            {
                Console.WriteLine("Please drag file(s) upon this .exe file to continue.");
                Console.ReadLine();
            }
            else {
                Console.WriteLine("Finished all tasks, see the result in \\combined\\");
                Console.ReadLine();
            }
        }
    }
}

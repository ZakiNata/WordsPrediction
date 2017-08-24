using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordsPrediction
{
    static class Program
    {
        public static xmlStruct xml;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = "./xmlStruct.xml";
            try
            {
                string text = System.IO.File.ReadAllText(path);
                xml = XmlConvert.DeserializeObject<xmlStruct>(text);
            }
            catch (System.IO.FileNotFoundException)
            {
                FileStream fs = File.Create(path);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        internal static bool addToTheXml(string text)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] words = text.Split(delimiterChars);

            for (int i=0;i<words.Length-1;i++)
            {
                addWordToXml(words[i], words[i + 1]);
            }

            return true;
        }

        private static void addWordToXml(string v1, string v2)
        {
            if(wordExistsInXml(v1))
            {

            }
            else
            {

            }
        }

        private static bool wordExistsInXml(string v1)
        {
            return true;
        }
    }
}

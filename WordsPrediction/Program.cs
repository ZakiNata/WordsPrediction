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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        internal static bool addToTheXml(string text)
        {
            string path = "./xmlStruct.xml";
            try
            {
                string Filetext = System.IO.File.ReadAllText(path);
                xml = XmlConvert.DeserializeObject<xmlStruct>(Filetext);
            }
            catch (System.IO.FileNotFoundException)
            {
                FileStream fs = File.Create(path);
            }

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
                addRelatedWord(v1, v2);
            }
            else
            {

            }
        }

        private static void addRelatedWord(string v1, string v2)
        {
            foreach (WordsPrediction.xmlStruct.wordTag word in xml.words)
            {
                if (word.word.Equals(v1))
                {
                    foreach (WordsPrediction.xmlStruct.wordTag.WordProp prop in word.wp)
                    {
                        if (prop.rWord.Equals(v2))
                            prop.count = prop.count + 1;
                    }
                    WordsPrediction.xmlStruct.wordTag.WordProp temp = new xmlStruct.wordTag.WordProp(v2, 1);
                    xmlStruct.wordTag.WordProp.addWordProp(word.wp,temp);

                }
            }
        }

        //private static bool wordExistsInXmlRelated(string v1, string v2)
        //{
        //    foreach (WordsPrediction.xmlStruct.wordTag word in xml.words)
        //    {
        //        if (word.word.Equals(v1))
        //        {
        //            foreach(WordsPrediction.xmlStruct.wordTag.WordProp prop in word.wp)
        //            {
        //                if (prop.rWord.Equals(v2))
        //                    return true;
        //            }
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        private static bool wordExistsInXml(string v1)
        {
            foreach(WordsPrediction.xmlStruct.wordTag word in xml.words)
            {
                if(word.word.Equals(v1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

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

            char[] delimiterChars = { ' ', ',', '.', ':', '\t','\n','\r'};

            string[] words = text.Split(delimiterChars);

            string ElementToRemove = "";
            words = words.Where(val => val != ElementToRemove).ToArray();

            for (int i=0;i<words.Length-1;i++)
            {
                addWordToXml(words[i], words[i + 1]);
            }

            string xmlString = XmlConvert.SerializeObject(xml);

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(xmlString);
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
                addNewWord(v1, v2);
            }
        }

        private static void addNewWord(string v1, string v2)
        {
            xmlStruct.wordTag.WordProp wp = new xmlStruct.wordTag.WordProp(v2, 1);
            xmlStruct.wordTag wt = new xmlStruct.wordTag();
            wt.word = v1;
            xmlStruct.wordTag.WordProp[] temp = { wp };
            wt.wp = temp;
            if (xml.words != null)
            {
                xml.words = xml.words.Concat(Enumerable.Repeat(wt, 1)).ToArray();
            }
            else
            {
                xmlStruct.wordTag[] temp2 = { wt };
                xml.words = temp2;
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
                        {
                            prop.count = prop.count + 1;
                            return;
                        }
                    }
                    WordsPrediction.xmlStruct.wordTag.WordProp temp = new xmlStruct.wordTag.WordProp(v2, 1);
                    word.wp = word.wp.Concat(Enumerable.Repeat(temp, 1)).ToArray();
                    //xmlStruct.wordTag.WordProp.addWordProp(word.wp,temp);

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
            if (xml.words == null)
                return false;
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

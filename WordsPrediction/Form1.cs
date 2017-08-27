using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordsPrediction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            if (text == "")
            {
                textBox2.Text = "No Suggestions found";
                return;
            }

            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '\r' };
            string[] words = text.Split(delimiterChars);
            string ElementToRemove = "";
            words = words.Where(val => val != ElementToRemove).ToArray();
            string word = words[words.Length - 1];
            //textBox2.Text = word;
            xmlStruct xml = null;
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
            showRelatedWords(xml, word);
        }

        private void showRelatedWords(xmlStruct xml, string word)
        {
            if (xml.words == null)
            {
                textBox2.Text = "No Suggestions found";
                return;
            }
            foreach (WordsPrediction.xmlStruct.wordTag w in xml.words)
            {
                if (w.word.Equals(word))
                {
                    textBox2.Text = calculateRelatedWords(w.wp);
                    return;
                }
            }
            textBox2.Text = "No Suggestions found";
        }

        private string calculateRelatedWords(xmlStruct.wordTag.WordProp[] wp)
        {
            string result = "";
            //MyClass[] sorted = myClassArray.OrderBy(c => c.Name).ToArray();

            //xmlStruct.wordTag.WordProp[] wpSorted = wp.OrderBy(w => w.count).ToArray();

            wp = wp.OrderByDescending(w => w.count).ToArray();

            foreach (xmlStruct.wordTag.WordProp wps in wp)
            {
                result += wps.rWord + Environment.NewLine;
            }
            return result;
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}

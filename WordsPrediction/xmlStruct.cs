using System;
using System.Linq;

namespace WordsPrediction
{
    public class xmlStruct
    {
        public wordTag[] words;
        public class wordTag
        {
            public string word;
            public WordProp[] wp;

            public class WordProp
            {
                public string rWord;
                public int count;

                public WordProp()
                { }
                public WordProp(string w,int c)
                {
                    rWord = w;
                    count = c;
                }

                internal static void addWordProp(WordProp[] wp, WordProp temp)
                {
                    wp = wp.Concat(Enumerable.Repeat(temp, 1)).ToArray();
                }
            }
        }
    }
}


 
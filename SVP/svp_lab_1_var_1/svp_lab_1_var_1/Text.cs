using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace svp_lab_1_var_1
{
    public class Text
    {
        char[] t; //массив символов храним здесь

        public Text(RichTextBox rtb) //размерность массива из формы
        {
            
            string txt = rtb.Text;
            t = txt.ToCharArray();
        }                 

        public string counter_words(TextBox sym) //количество слов в тексте
        {
            int count = 0;            
            char symbool = sym.Text[0];
            if (Char.IsLetter(symbool))
            {
                for (int i = 0; i < t.Length; i++)
                {
                    if (i == 0 && t[i]==symbool) count++;
                    if(i != 0 && t[i]==symbool && (t[i-1] == '\0' || 
                        t[i - 1] == '\n' || t[i - 1] == ' ')) count++;
                }
            }
            return Convert.ToString(count);
        }

        public string counter_quote() //количество цитат
        {
            int count = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == '"') count++;                
            } 
            count /= 2;
            return Convert.ToString(count);
        }

        public List<string> counter_phones() //количество телефонных номеров
        {
            List<string> phone_base = new List<string>();
            char[] number = new char[13];
            
            
            bool is_number = false;

            for(int i = 0; i < t.Length; i++)
            {
                if(t[i] == '+' && (t.Length > i + 13))
                {
                    is_number = true;
                    number[0] = t[i];
                    for(int j = i + 1, k = 1; j < i + 13; j++, k++)
                    {
                        if (Char.IsDigit(t[j]))
                        {
                            is_number = true;
                            number[k] = t[j];
                        }
                        else {is_number = false; break; }
                        
                    }    
                    if(is_number == true)
                    {
                        
                        phone_base.Add(new string(number));
                    }
                    
                }
            }
            return phone_base;
        }
    }
}

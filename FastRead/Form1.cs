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

namespace FastRead
{
    public partial class Form1 : Form
    {
        bool isStart = false;
        int currentIndex = 0;
        int currentWordIndex = 0;
        string currentWord;
        int textSize = 0;
        string[] words;
        float wordInMinute;
        public Form1()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            isStart = !isStart;
            
            button1.Text = isStart ? "Остановить" : "Запустить";
            textSize = richTextBox1.Text.Length;
            timer1.Enabled = isStart;
            words = richTextBox1.Text.Split(' ');
            wordInMinute = Convert.ToInt32(WordInMinute.Value);
        }

        private void ColorWord(string text, Color color)
        {
            int position_save = (currentWordIndex > 0 && currentIndex > words[currentWordIndex - 1].Length) ? currentIndex - words[currentWordIndex-1].Length : 0; // сохраняем позицию курсора из начально


            richTextBox1.SelectionStart = (currentIndex > 0) ? position_save-1 : position_save;
            richTextBox1.SelectionLength = (currentWordIndex > 0) ? words[currentWordIndex - 1].Length:10;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionStart = position_save;

            string str = text;

            int i = currentIndex;

            //выделение цветом
            i = richTextBox1.Text.IndexOf(str, i);
            richTextBox1.SelectionStart = i;
            richTextBox1.SelectionLength = str.Length;
            richTextBox1.SelectionColor = color;
            richTextBox1.SelectionStart = position_save; // ставим как было
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentWordIndex < words.Length-1)
            {
                currentWord = words[currentWordIndex];
                ColorWord(currentWord, Color.Red);
                currentIndex += currentWord.Length+1;
                int time = Convert.ToInt32(Convert.ToDouble(words[currentWordIndex + 1].Length) / (wordInMinute*6 / 60) * 1000);
                
                timer1.Interval = time + (currentWord.Contains("\n") ? time*2 : 0);
                currentWordIndex++;
            }
            else
            {
                isStart = false;
            }
            //if (currentWordIndex % 100 == 0)
            //{
            //    richTextBox1.ScrollToCaret();
            //}
        }

        void GetText()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            using (FileStream fstream = File.OpenRead(openFileDialog.FileName))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.UTF8.GetString(array);
                richTextBox1.Text = textFromFile;
                //int wordAmmount = 0;
                //for(var i = 0; i< richTextBox1.Text.Length; i++)
                //{
                //    if(richTextBox1.Text[i] == ' ')
                //    {
                //        wordAmmount++;
                //    }
                //    if(wordAmmount == 8)
                //    {
                //    }
                //}
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetText();
        }
    }
}

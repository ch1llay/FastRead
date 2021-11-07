using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        float wordInLine;
        float secForLine = 1;
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
            wordInLine = richTextBox1.Lines[0].Split(' ').Length;
            wordInMinute = WordInMinute.Value;
        }

        private void ColorWord(string text, Color color)
        {
            int position_save = richTextBox1.SelectionStart; // сохраняем позицию курсора из начально


            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = textSize;
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
            richTextBox1.SelectionColor = Color.Black; // чужое красим в черное
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentWordIndex < words.Length-1)
            {
                currentWord = words[currentWordIndex];
                ColorWord(currentWord, Color.Red);
                currentIndex += currentWord.Length;
                int time = Convert.ToInt32(wordInMinute / currentWord.Length * 1000 * words[currentWordIndex + 1].Length);
                timer1.Interval = time;
                currentWordIndex++;
            }
            else
            {
                isStart = false;
            }
        }
    }
}

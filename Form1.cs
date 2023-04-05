using System.Drawing.Text;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace AndreevaTestTask
{
    public partial class Form1 : Form
    {
        private PrivateFontCollection fontCollection = new PrivateFontCollection();
        bool arrayLoaded = false;
        int even = 0;
        int odd = 0;

        Image countActive = Image.FromFile("01.png");
        Image countInactive = Image.FromFile("03.png");
        Image countHover = Image.FromFile("02.png");
        public Form1()
        {
            InitializeComponent();

            fontCollection.AddFontFile("Montserrat-ExtraBoldItalic.ttf");
            fontCollection.AddFontFile("Montserrat-Medium.ttf");

            label1.Font = new Font(fontCollection.Families[0], 18, FontStyle.Italic);
            label2.Font = label1.Font;

            textBox1.Font = new Font(fontCollection.Families[1], 16);
            textBox2.Font = textBox1.Font;
        }

        private void ArrayCheck(string text)
        {
            string[] Dividers = new string[] { " ", ",", ", ", "  ", " ,", " , " };
            var filetext = text.Split(Dividers, StringSplitOptions.RemoveEmptyEntries);
            List<int> Ints = new List<int>();
            bool result = true;
            foreach (string s in filetext)
            {
                if (int.TryParse(s, out int parsed) == true)
                {
                    Ints.Add(parsed);
                    if (parsed % 2 == 0) even++;
                    else odd++;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            if (result == false || Ints.Count < 2)
            {
                if (Ints.Count == 1)
                {
                    MessageBox.Show("Ошибка! Массив должен содержать минимум два числа.");
                    Ints.Clear();
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Ошибка! Заданный текст не является перечислением элементов массива натуральных чисел. Введите данные повторно.");
                    textBox1.Clear();
                    textBox1.Focus();
                    Ints.Clear();
                }
            }
            else
            {
                textBox1.Text = string.Join(", ", Ints);
                arrayLoaded = true;
                label1.Focus();
                AvailabilityCheck();
            }
        }

        private void AvailabilityCheck()
        {
            if (arrayLoaded == true)
            {
                button1.Enabled = true;
                button1.BackgroundImage = countActive;
            }
            else
            {
                button1.Enabled = false;
                button1.BackgroundImage = countInactive;
            }
        }

        private void EnterResult()
        {
            if (even != 0 || odd != 0)
            {
                textBox2.Text = $"Четных: {even}, нечетных: {odd}";
            }
        }

        private void EnterTextbox()
        {
            even = 0;
            odd = 0;
            arrayLoaded = false;
            textBox2.Clear();
            AvailabilityCheck();
            ArrayCheck(textBox1.Text);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = countHover;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = countActive;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnterResult();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) EnterTextbox();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (arrayLoaded == false) EnterTextbox();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arrayLoaded = false;
            AvailabilityCheck();
        }
    }
}
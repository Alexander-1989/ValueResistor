using System;
using System.Drawing;
using System.Windows.Forms;

namespace Маркировка_резисторов
{
    public partial class Form1 : Form
    {
        private readonly IniFile INI = new IniFile();

        private readonly float[] variationOne = { 1F, 2F, 5F, 10F };
        private readonly float[] variationTwo = { 0.05F, 0.1F, 0.25F, 0.5F, 1F, 2F, 5F, 10F };
        private readonly float[] factor = { 0.01F, 0.1F, 1F, 10F, 100F, 1000F, 10000F, 100000F, 1000000F, 10000000F };

        private readonly string[] valueColors = { "Black", "Brown", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray", "White" };
        private readonly string[] factorColors = { "Silver", "Gold", "Black", "Brown", "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        private readonly string[] variationColorsOne = { "Brown", "Red", "Gold", "Silver" };
        private readonly string[] variationColorsTwo = { "Gray", "Purple", "Blue", "Green", "Brown", "Red", "Gold", "Silver" };

        public Form1()
        {
            InitializeComponent();

            cb1.Items.AddRange(valueColors);
            cb2.Items.AddRange(valueColors);
            cb3.Items.AddRange(factorColors);
            cb4.Items.AddRange(variationColorsOne);
            cb5.Items.AddRange(valueColors);
            cb6.Items.AddRange(valueColors);
            cb7.Items.AddRange(valueColors);
            cb8.Items.AddRange(factorColors);
            cb9.Items.AddRange(variationColorsTwo);

            ReadParameters();
            ShowResistance();

            cb1.DrawItem += ComboBox_DrawItem;
            cb2.DrawItem += ComboBox_DrawItem;
            cb3.DrawItem += ComboBox_DrawItem;
            cb4.DrawItem += ComboBox_DrawItem;
            cb5.DrawItem += ComboBox_DrawItem;
            cb6.DrawItem += ComboBox_DrawItem;
            cb7.DrawItem += ComboBox_DrawItem;
            cb8.DrawItem += ComboBox_DrawItem;
            cb9.DrawItem += ComboBox_DrawItem;

            cb1.SelectedValueChanged += (s, e) => ShowResistance();
            cb2.SelectedValueChanged += (s, e) => ShowResistance();
            cb3.SelectedValueChanged += (s, e) => ShowResistance();
            cb4.SelectedValueChanged += (s, e) => ShowResistance();
            cb5.SelectedValueChanged += (s, e) => ShowResistance();
            cb6.SelectedValueChanged += (s, e) => ShowResistance();
            cb7.SelectedValueChanged += (s, e) => ShowResistance();
            cb8.SelectedValueChanged += (s, e) => ShowResistance();
            cb9.SelectedValueChanged += (s, e) => ShowResistance();

            tabPage1.Paint += TabPage1_Paint;
            tabPage2.Paint += TabPage2_Paint;
        }

        private void ReadParameters()
        {
            try
            {
                Location = new Point(INI.Parse("Location", "X"), INI.Parse("Location", "Y"));
                tabControl.SelectedIndex = INI.Parse("General", "TabPage");
                cb1.SelectedIndex = INI.Parse("General", "ComboBox1");
                cb2.SelectedIndex = INI.Parse("General", "ComboBox2");
                cb3.SelectedIndex = INI.Parse("General", "ComboBox3");
                cb4.SelectedIndex = INI.Parse("General", "ComboBox4");
                cb5.SelectedIndex = INI.Parse("General", "ComboBox5");
                cb6.SelectedIndex = INI.Parse("General", "ComboBox6");
                cb7.SelectedIndex = INI.Parse("General", "ComboBox7");
                cb8.SelectedIndex = INI.Parse("General", "ComboBox8");
                cb9.SelectedIndex = INI.Parse("General", "ComboBox9");
            }
            catch (Exception)
            {
                cb1.SelectedIndex = 2;
                cb2.SelectedIndex = 5;
                cb3.SelectedIndex = 6;
                cb4.SelectedIndex = 3;
                cb5.SelectedIndex = 2;
                cb6.SelectedIndex = 5;
                cb7.SelectedIndex = 6;
                cb8.SelectedIndex = 5;
                cb9.SelectedIndex = 7;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                INI.Write("Location", "X", Location.X);
                INI.Write("Location", "Y", Location.Y);
                INI.Write("General", "TabPage", tabControl.SelectedIndex);
                INI.Write("General", "ComboBox1", cb1.SelectedIndex);
                INI.Write("General", "ComboBox2", cb2.SelectedIndex);
                INI.Write("General", "ComboBox3", cb3.SelectedIndex);
                INI.Write("General", "ComboBox4", cb4.SelectedIndex);
                INI.Write("General", "ComboBox5", cb5.SelectedIndex);
                INI.Write("General", "ComboBox6", cb6.SelectedIndex);
                INI.Write("General", "ComboBox7", cb7.SelectedIndex);
                INI.Write("General", "ComboBox8", cb8.SelectedIndex);
                INI.Write("General", "ComboBox9", cb9.SelectedIndex);
            }
            catch (Exception) { }
        }

        private string GetDenomination(float value)
        {
            if (value < 1000)
            {
                return string.Format("{0} Ом", value);
            }
            else if (value >= 1000 && value < 1000000)
            {
                return string.Format("{0} кОм", value / 1000);
            }
            else
            {
                return string.Format("{0} МОм", value / 1000000);
            }
        }

        private void ShowResistance()
        {
            float value = (10 * cb1.SelectedIndex + cb2.SelectedIndex) * factor[cb3.SelectedIndex];
            label1.Text = string.Format("{0}     Погрешность ±{1} %", GetDenomination(value), variationOne[cb4.SelectedIndex]);

            value = (100 * cb5.SelectedIndex + 10 * cb6.SelectedIndex + cb7.SelectedIndex) * factor[cb8.SelectedIndex];
            label2.Text = string.Format("{0}    Погрешность ±{1} %", GetDenomination(value), variationTwo[cb9.SelectedIndex]);

            Refresh();
        }

        private void PrintRectangle(ComboBox comboBox, Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromName(comboBox.Text)))
            {
                graphics.FillRectangle(brush, 140, comboBox.Top, 50, 22);
            }
        }

        private void TabPage1_Paint(object sender, PaintEventArgs e)
        {
            PrintRectangle(cb1, e.Graphics);
            PrintRectangle(cb2, e.Graphics);
            PrintRectangle(cb3, e.Graphics);
            PrintRectangle(cb4, e.Graphics);
        }

        private void TabPage2_Paint(object sender, PaintEventArgs e)
        {
            PrintRectangle(cb5, e.Graphics);
            PrintRectangle(cb6, e.Graphics);
            PrintRectangle(cb7, e.Graphics);
            PrintRectangle(cb8, e.Graphics);
            PrintRectangle(cb9, e.Graphics);
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            string text = (sender as ComboBox).Items[e.Index].ToString();
            e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds);
            using (SolidBrush brush = new SolidBrush(Color.FromName(text)))
            {
                e.Graphics.FillRectangle(brush, e.Bounds.X + 50, e.Bounds.Y, e.Bounds.Width - 50, e.Bounds.Height);
            }
        }
    }
}
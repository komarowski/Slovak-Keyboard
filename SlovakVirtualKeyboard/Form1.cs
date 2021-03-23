using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SlovakVirtualKeyboard
{
    public partial class Form1 : Form
    {
        public static bool globalShift = false;
        public static bool globalAlt = false;
        public static List<string> buttonNames = typeof(CustomKeys).GetFields().Select(field => field.Name).ToList();

        public void FinishWord(string word)
        {
            string text = textBox1.Text;
            var index = Math.Max(text.LastIndexOf('\n'), text.LastIndexOf(' '));
            if (index == -1)
            {
                textBox1.Text = word;
            }
            else
            {
                textBox1.Text = $"{text.Substring(0, index + 1)}{word}";
            }
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        internal void SwitchFlag(ref bool flag)
        {
            if (flag) flag = false;
            else flag = true;
        }

        internal void PrintSimpleKey(SimpleKey key, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (globalShift) textBox1.AppendText(char.ToString(key.secondSymbol));
            else textBox1.AppendText(char.ToString(key.firstSymbol));
        }

        internal void PrintSimpleKey(SimpleKey key)
        {
            if (globalShift) textBox1.AppendText(char.ToString(key.secondSymbol));
            else textBox1.AppendText(char.ToString(key.firstSymbol));
        }

        internal void PrintComplexKey(СomplexKey key, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (!globalAlt)
            {
                if (globalShift) textBox1.AppendText(char.ToString(key.secondSymbol));
                else textBox1.AppendText(char.ToString(key.firstSymbol));
            }
            else
            {
                if (globalShift) textBox1.AppendText(char.ToString(key.fourthSymbol));
                else textBox1.AppendText(char.ToString(key.thirdSymbol));
            }           
        }

        internal void PrintComplexKey(СomplexKey key)
        {
            if (!globalAlt)
            {
                if (globalShift) textBox1.AppendText(char.ToString(key.secondSymbol));
                else textBox1.AppendText(char.ToString(key.firstSymbol));
            }
            else
            {
                if (globalShift) textBox1.AppendText(char.ToString(key.fourthSymbol));
                else textBox1.AppendText(char.ToString(key.thirdSymbol));
            }
        }

        internal void ChangeKeyMode()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                var buttonName = button.Name;
                if (buttonNames.Contains(buttonName))
                {
                    var typeKey = typeof(CustomKeys).GetField(buttonName).FieldType.ToString();
                    if (typeKey == "SlovakVirtualKeyboard.SimpleKey")
                    {
                        ChangeSimpleKeyMode(button, buttonName);
                    }
                    else if (typeKey == "SlovakVirtualKeyboard.СomplexKey")
                    {
                        ChangeComplexKeyMode(button, buttonName);
                    }
                }
            }
        }

        internal void ChangeSimpleKeyMode(Button button, string buttonName)
        {
            var key = (SimpleKey)typeof(CustomKeys).GetField(buttonName).GetValue(null);
            if (globalShift)
            {
                button.Text = key.secondSymbol.ToString();
            }
            else
            {
                button.Text = key.firstSymbol.ToString();
            }
        }

        internal void ChangeComplexKeyMode(Button button, string buttonName)
        {
            var key = (СomplexKey)typeof(CustomKeys).GetField(buttonName).GetValue(null);
            if (globalShift)
            {
                if (globalAlt) button.Text = key.fourthSymbol.ToString();
                else button.Text = key.secondSymbol.ToString();
            }
            else
            {
                if (globalAlt) button.Text = key.thirdSymbol.ToString();
                else button.Text = key.firstSymbol.ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            KeyUp += new KeyEventHandler(Form1_KeyUp);
            ParseTxt.ReadSlovakWords();
            textBox1.Select();
        }      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift)
            {
                globalShift = true;
                ChangeKeyMode();
            }         
            if (e.Modifiers == Keys.Alt)
            {
                globalAlt = true;
                ChangeKeyMode();
            }
            if (e.Shift && e.Alt)
            {
                globalAlt = true;
                globalShift = true;
                ChangeKeyMode();
            }
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                if (listBox1.Items.Count > 0) FinishWord(listBox1.Items[0].ToString());
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetData(DataFormats.Text, (Object)textBox1.Text);
                return;
            }

            if (e.KeyCode == Keys.Q) PrintSimpleKey(CustomKeys.button1, e);
            if (e.KeyCode == Keys.W) PrintSimpleKey(CustomKeys.button2, e);
            if (e.KeyCode == Keys.E) PrintComplexKey(CustomKeys.button3, e);
            if (e.KeyCode == Keys.R) PrintComplexKey(CustomKeys.button4, e);
            if (e.KeyCode == Keys.T) PrintComplexKey(CustomKeys.button5, e);
            if (e.KeyCode == Keys.Y) PrintComplexKey(CustomKeys.button6, e);
            if (e.KeyCode == Keys.U) PrintComplexKey(CustomKeys.button7, e);
            if (e.KeyCode == Keys.I) PrintComplexKey(CustomKeys.button8, e);
            if (e.KeyCode == Keys.O) PrintComplexKey(CustomKeys.button9, e);
            if (e.KeyCode == Keys.P) PrintSimpleKey(CustomKeys.button10, e);
            if (e.KeyCode == Keys.OemOpenBrackets) PrintSimpleKey(CustomKeys.button11, e);
            if (e.KeyCode == Keys.OemCloseBrackets) PrintSimpleKey(CustomKeys.button12, e);
            if (e.KeyCode == Keys.Oem5) PrintSimpleKey(CustomKeys.button51, e);
            if (e.KeyCode == Keys.A) PrintComplexKey(CustomKeys.button13, e);
            if (e.KeyCode == Keys.S) PrintComplexKey(CustomKeys.button14, e);
            if (e.KeyCode == Keys.D) PrintComplexKey(CustomKeys.button15, e);
            if (e.KeyCode == Keys.F) PrintSimpleKey(CustomKeys.button16, e);
            if (e.KeyCode == Keys.G) PrintSimpleKey(CustomKeys.button17, e);
            if (e.KeyCode == Keys.H) PrintSimpleKey(CustomKeys.button18, e);
            if (e.KeyCode == Keys.J) PrintSimpleKey(CustomKeys.button19, e);
            if (e.KeyCode == Keys.K) PrintSimpleKey(CustomKeys.button20, e);
            if (e.KeyCode == Keys.L) PrintComplexKey(CustomKeys.button21, e);
            if (e.KeyCode == Keys.Oem1) PrintComplexKey(CustomKeys.button22, e);
            if (e.KeyCode == Keys.Oem7) PrintSimpleKey(CustomKeys.button23, e);
            if (e.KeyCode == Keys.OemQuestion) PrintSimpleKey(CustomKeys.button24, e);
            if (e.KeyCode == Keys.OemPeriod) PrintSimpleKey(CustomKeys.button25, e);
            if (e.KeyCode == Keys.Oemcomma) PrintSimpleKey(CustomKeys.button26, e);
            if (e.KeyCode == Keys.M) PrintSimpleKey(CustomKeys.button28, e);
            if (e.KeyCode == Keys.N) PrintComplexKey(CustomKeys.button29, e);
            if (e.KeyCode == Keys.B) PrintSimpleKey(CustomKeys.button30, e);
            if (e.KeyCode == Keys.V) PrintSimpleKey(CustomKeys.button31, e);
            if (e.KeyCode == Keys.C) PrintComplexKey(CustomKeys.button32, e);
            if (e.KeyCode == Keys.X) PrintSimpleKey(CustomKeys.button33, e);
            if (e.KeyCode == Keys.Z) PrintComplexKey(CustomKeys.button34, e);

            if (e.KeyCode == Keys.D1) PrintSimpleKey(CustomKeys.button35, e);
            if (e.KeyCode == Keys.D2) PrintSimpleKey(CustomKeys.button36, e);
            if (e.KeyCode == Keys.D3) PrintSimpleKey(CustomKeys.button37, e);
            if (e.KeyCode == Keys.D4) PrintSimpleKey(CustomKeys.button38, e);
            if (e.KeyCode == Keys.D5) PrintSimpleKey(CustomKeys.button39, e);
            if (e.KeyCode == Keys.D6) PrintSimpleKey(CustomKeys.button40, e);
            if (e.KeyCode == Keys.D7) PrintSimpleKey(CustomKeys.button41, e);
            if (e.KeyCode == Keys.D8) PrintSimpleKey(CustomKeys.button42, e);
            if (e.KeyCode == Keys.D9) PrintSimpleKey(CustomKeys.button43, e);
            if (e.KeyCode == Keys.D0) PrintSimpleKey(CustomKeys.button44, e);
            if (e.KeyCode == Keys.OemMinus) PrintSimpleKey(CustomKeys.button45, e);
            if (e.KeyCode == Keys.Oemplus) PrintSimpleKey(CustomKeys.button46, e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                globalShift = false;
                ChangeKeyMode();
            }
            if (e.KeyCode == Keys.Menu)
            {
                globalAlt = false;
                ChangeKeyMode();
            }
        }
        
        private void button52_Click(object sender, EventArgs e)
        {
            SwitchFlag(ref globalShift);
            ChangeKeyMode();
        }
        private void button50_Click(object sender, EventArgs e)
        {
            SwitchFlag(ref globalAlt);
            ChangeKeyMode();
        }

        private void button1_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button1); }
        private void button2_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button2); }
        private void button3_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button3); }
        private void button4_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button4); }
        private void button5_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button5); }
        private void button6_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button6); }
        private void button7_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button7); }
        private void button8_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button8); }
        private void button9_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button9); }
        private void button10_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button10); }
        private void button11_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button11); }
        private void button12_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button12); }
        private void button51_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button51); }
        private void button13_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button13); }
        private void button14_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button14); }
        private void button15_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button15); }
        private void button16_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button16); }
        private void button17_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button17); }
        private void button18_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button18); }
        private void button19_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button19); }
        private void button20_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button20); }
        private void button21_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button21); }
        private void button22_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button22); }
        private void button23_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button23); }
        private void button24_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button24); }
        private void button25_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button25); }
        private void button26_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button26); }
        private void button28_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button28); }
        private void button29_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button29); }
        private void button30_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button30); }
        private void button31_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button31); }
        private void button32_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button32); }
        private void button33_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button33); }
        private void button34_Click(object sender, EventArgs e) { PrintComplexKey(CustomKeys.button34); }

        private void button35_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button35); }
        private void button36_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button36); }
        private void button37_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button37); }
        private void button38_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button38); }
        private void button39_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button39); }
        private void button40_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button40); }
        private void button41_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button41); }
        private void button42_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button42); }
        private void button43_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button43); }
        private void button44_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button44); }
        private void button45_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button45); }
        private void button46_Click(object sender, EventArgs e) { PrintSimpleKey(CustomKeys.button46); }

        private void button47_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            textBox1.Text = text.Remove(text.Length - 1);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(" ");
        }

        private void button48_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Environment.NewLine);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ParseTxt.exitFile)
            {
                var lastWord = textBox1.Text;
                if (lastWord.Contains("\n"))
                {
                    lastWord = textBox1.Text.Split('\n').Last();
                }
                if (lastWord.Contains(" "))
                {
                    lastWord = textBox1.Text.Split(' ').Last();
                }
                listBox1.Items.Clear();
                listBox1.BeginUpdate();
                var anotate = ParseTxt.SearchStartWith(lastWord);
                foreach (var item in anotate)
                {
                    listBox1.Items.Add(item);
                }
                listBox1.EndUpdate();
            }          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
            {
                var word = listBox1.SelectedItem.ToString();
                FinishWord(word);
                textBox1.Focus();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            var newForm = new AboutBox1();
            newForm.Show();
        }
    }
}

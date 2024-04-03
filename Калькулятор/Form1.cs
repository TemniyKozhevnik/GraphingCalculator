using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Калькулятор
{
    public partial class Form1 : Form
    {
        private double FirstValue;
        private double SecondValue;
        private char symbol;
        private bool IsZero = false;
        private bool IsErrorDegree = false;
        public static bool IsError = false;
        private bool IsSin = false;
        private bool IsCos = false;
        private bool IsTan = false;
        private bool IsAsin = false;
        private bool IsAcos = false;
        private bool IsAtan = false;
        private bool IsPi = false;
        private bool Ise = false;
        private bool IsLn = false;
        private byte OpenPanel = 0;
        private byte OpenINV = 0;
        private bool BraketClose = false;
        private bool IsRad = true;
        int j = 0;
        List<string> actions = new List<string>(15);
        private byte NumOfActions = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void HideErrors()
        {
            if (IsZero)
            {
                division0.Hide();
                IsZero = false;
            }
            if (IsErrorDegree)
            {
                errordegree.Hide();
                IsErrorDegree = false;
            }
            if (IsError)
            {
                error.Hide();
                IsError = false;
            }
            if (IsSin)
                labelsin.Hide();
            if (IsCos)
                labelcos.Hide();
            if (IsTan)
                labeltan.Hide();
            if (IsAsin)
                labelasin.Hide();
            if (IsAcos)
                labelacos.Hide();
            if (IsAtan)
                labelatan.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            division0.Hide();
            button2.Hide();
            button4.Hide();
            errordegree.Hide();
            error.Hide();
            panel1.Hide();
            labelsin.Hide();
            labelasin.Hide();
            labelcos.Hide();
            labelacos.Hide();
            labeltan.Hide();
            labelatan.Hide();
            labelln.Hide();
            RAD.Hide();
            asin.Hide();
            acos.Hide();
            atg.Hide();
            listBox1.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && 
                (e.KeyChar != '-' || textBox1.Text.Length != 0 && !IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan) && 
                (e.KeyChar != '(' && e.KeyChar != ')' || !IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan) &&
                (e.KeyChar != ',' || !IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void number1_Click(object sender, EventArgs e)
        { 
            HideErrors();
            textBox1.Text += "1";
        }

        private void Action(char ch)
        {
            NumOfActions++;
            if (NumOfActions > 15)
            {
                actions.RemoveAt(0);
                j++;
            }
            switch (ch) 
            {
                case '+':
                    actions.Add($"{FirstValue} + {SecondValue} = {textBox1.Text}");
                    break;
                case '-':
                    actions.Add($"{FirstValue} - {SecondValue} = {textBox1.Text}");
                    break;
                case '*':
                    actions.Add($"{FirstValue} * {SecondValue} = {textBox1.Text}");
                    break;
                case '/':
                    actions.Add($"{FirstValue} / {SecondValue} = {textBox1.Text}");
                    break;
                case '^':
                    actions.Add($"{FirstValue} ^ {SecondValue} = {textBox1.Text}");
                    break;
                case 's':
                    actions.Add($"sin({FirstValue}) = {textBox1.Text}");
                    break;
                case 'c':
                    actions.Add($"cos({FirstValue}) = {textBox1.Text}");
                    break;
                case 't':
                    actions.Add($"tg({FirstValue}) = {textBox1.Text}");
                    break;
                case 'S':
                    actions.Add($"asin({FirstValue}) = {textBox1.Text}");
                    break;
                case 'C':
                    actions.Add($"acos({FirstValue}) = {textBox1.Text}");
                    break;
                case 'T':
                    actions.Add($"atg({FirstValue}) = {textBox1.Text}");
                    break;
                case 'l':
                    actions.Add($"ln({FirstValue}) = {textBox1.Text}");
                    break;
            }
            listBox1.Items.Clear();
            for (int i = NumOfActions - 1 - j; i >= 0; i--)
                listBox1.Items.Add(i % 15 + 1 + ". " + actions[i]);
        }

        private void equality_Click(object sender, EventArgs e)
        {
            HideErrors();
            BraketClose = false;
            division0.Hide();
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (textBox1.Text != String.Empty && textBox1.Text != "-")
            {
                if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
                    SecondValue = Convert.ToDouble(textBox1.Text);
                switch (symbol)
                {
                    case '+':
                        textBox1.Text = Convert.ToString(FirstValue + SecondValue);
                        Action('+');
                        break;
                    case '-':
                        textBox1.Text = Convert.ToString(FirstValue - SecondValue);
                        Action('-');
                        break;
                    case '*':
                        textBox1.Text = Convert.ToString(FirstValue * SecondValue);
                        Action('*');
                        break;
                    case '/':
                        if (SecondValue != 0)
                        {
                            textBox1.Text = Convert.ToString(FirstValue / SecondValue);
                            Action('/');
                        }
                        else
                        {
                            division0.Show();
                            IsZero = true;
                            textBox1.Text = String.Empty;
                        }
                        break;
                    case '^':
                        if (!(FirstValue < 0 && Math.Abs(SecondValue) < 1 && Math.Abs(SecondValue) > 0))
                        {
                            textBox1.Text = Convert.ToString(Math.Pow(FirstValue, SecondValue));
                            Action('^');
                        }
                        else
                        {
                            errordegree.Show();
                            IsErrorDegree = true;
                            textBox1.Text = String.Empty;
                        }
                        break;
                    case 's':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        if (IsRad)
                            textBox1.Text = Convert.ToString(Math.Round(Math.Sin(FirstValue), 6));
                        else
                            textBox1.Text = Convert.ToString(Math.Round(Math.Sin(FirstValue / 180 * Math.PI), 6));
                        IsSin = false;
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('s');
                        break;
                    case 'S':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        if (Math.Abs(FirstValue) > 1)
                            IsError = true;
                        else
                        {
                            if (IsRad)
                                textBox1.Text = Convert.ToString(Math.Round(Math.Asin(FirstValue), 6));
                            else
                                textBox1.Text = Convert.ToString(Math.Round(Math.Asin(FirstValue) * 180 / Math.PI, 6));
                            IsError = false;
                        }
                        IsAsin = false;
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('S');
                        break;
                    case 'c':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        if (IsRad)
                            textBox1.Text = Convert.ToString(Math.Round(Math.Cos(FirstValue), 6));
                        else
                            textBox1.Text = Convert.ToString(Math.Round(Math.Cos(FirstValue / 180 * Math.PI), 6));
                        IsCos = false;
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('c');
                        break;
                    case 'C':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        if (Math.Abs(FirstValue) > 1)
                            IsError = true;
                        else
                        {
                            if (IsRad)
                                textBox1.Text = Convert.ToString(Math.Round(Math.Acos(FirstValue), 6));
                            else
                                textBox1.Text = Convert.ToString(Math.Round(Math.Acos(FirstValue) * 180 / Math.PI, 6));
                            IsError = false;
                        }
                        IsAcos = false;
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('T');
                        break;
                    case 't':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        IsTan = false;
                        if (IsRad && Math.Abs(Math.Cos(Math.Round(FirstValue, 6))) <= 0.0001 || !IsRad && Math.Abs(Math.Cos(Math.Round(FirstValue * Math.PI / 180, 6))) <= 0.0001)
                        {
                            division0.Show();
                            IsZero = true;
                            textBox1.Text = "";
                        }
                        else
                        {
                            if (IsRad)
                                textBox1.Text = Convert.ToString(Math.Round(Math.Tan(FirstValue), 6));
                            else
                                textBox1.Text = Convert.ToString(Math.Round(Math.Tan(FirstValue / 180 * Math.PI), 6));
                        }
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('t');
                        break;
                    case 'T':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        if (IsRad)
                            textBox1.Text = Convert.ToString(Math.Round(Math.Atan(FirstValue), 6));
                        else
                            textBox1.Text = Convert.ToString(Math.Round(Math.Atan(FirstValue) * 180 / Math.PI, 6));
                        IsAtan = false;
                        if (IsError)
                        {
                            error.Show();
                            textBox1.Text = "";
                        }
                        if (textBox1.Text != "")
                            Action('T');
                        break;
                    case 'l':
                        try
                        {
                            FirstValue = Module1.Calc(Module1.ToPostfix(textBox1.Text));
                            if (double.IsNaN(FirstValue))
                                throw new Exception();
                        }
                        catch
                        {
                            IsError = true;
                        }
                        IsLn = false;
                        if (IsError || FirstValue <= 0)
                        {
                            error.Show();
                            IsError = true;
                            textBox1.Text = "";
                        }
                        else
                        {
                            textBox1.Text = Convert.ToString(Math.Round(Math.Log(FirstValue), 6));
                            Action('l');
                        }
                        break;
                }
            }
            symbol = ' ';
        }

        private void subtraction_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                if (textBox1.Text != String.Empty)
                {
                    HideErrors();
                    if (textBox1.Text != String.Empty && textBox1.Text != "-")
                    {
                        FirstValue = Convert.ToDouble(textBox1.Text);
                        textBox1.Text = string.Empty;
                    }
                    symbol = '-';
                }
                else
                {
                    textBox1.Text += "-";
                }
            }
            else
                textBox1.Text += " - ";

        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    textBox1.Text = string.Empty;
                }
                symbol = '*';
            }
            else 
                textBox1.Text += " * ";
        }

        private void division_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    textBox1.Text = string.Empty;
                }
                symbol = '/';
            }
            else
                textBox1.Text += " / ";
        }

        private void percent_Click(object sender, EventArgs e)
        {
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    SecondValue = FirstValue / 100.0;
                    textBox1.Text = Convert.ToString(SecondValue);
                }
            }
            else if (textBox1.Text != String.Empty)
                textBox1.Text += " / 100";
        }

        private void degree_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    textBox1.Text = string.Empty;
                }
                symbol = '^';
            }
            else
                textBox1.Text += " ^ ";
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise) 
                Ise = false;
            HideErrors();
            if (IsSin)
                labelsin.Hide();
            if (IsCos)
                labelcos.Hide();
            if (IsTan)
                labeltan.Hide();
            if (IsLn)
                labelln.Hide();
            if (IsAsin)
                labelasin.Hide();
            if (IsAtan)
                labelatan.Hide();
            if (IsAcos) 
                labelacos.Hide();
            textBox1.Text = string.Empty;
        }

        private void wash_Click(object sender, EventArgs e)
        {
            HideErrors();
            if (textBox1.Text.Length >= 1)
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
        }

        private int count(char ch)
        {
            int kol = 0;
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] == ch)
                    kol++;
            }
            return kol;
        }

        private bool IsOverflow()
        {
            if (textBox1.Text.Length >= 9)
                return true;
            return false;
        }
        private void dot_Click(object sender, EventArgs e)
        {
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                if (count(',') == 0 && textBox1.Text != String.Empty && textBox1.Text != "-")
                    textBox1.Text += ",";
            }
            else
                textBox1.Text += ",";
        }

        private void number0_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "0";
        }

        private void number3_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "3";
        }

        private void number2_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "2";
        }

        private void number6_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "6";
        }

        private void number5_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "5";
        }

        private void number4_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "4";
        }

        private void number9_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "9";
        }

        private void number8_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "8";
        }

        private void number7_Click(object sender, EventArgs e)
        {
            HideErrors();
            textBox1.Text += "7";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void addition_Click(object sender, EventArgs e)
        {
            if (IsPi)
                IsPi = false;
            if (Ise)
                Ise = false;
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    textBox1.Text = string.Empty;
                }
                symbol = '+';
            }
            else
                textBox1.Text += " + ";
        }

        private void division0_Click(object sender, EventArgs e)
        {

        }

        private void errordegree_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ToLowSize()
        {
            number0.Height = 35;
            number0.Top = 425;
            number0.Font = new Font(number0.Font.FontFamily, 16);
            number1.Height = 35;
            number1.Top = 385;
            number1.Font = new Font(number1.Font.FontFamily, 16);
            number2.Height = 35;
            number2.Top = 385;
            number2.Font = new Font(number2.Font.FontFamily, 16);
            number3.Height = 35;
            number3.Top = 385;
            number3.Font = new Font(number3.Font.FontFamily, 16);
            number4.Height = 35;
            number4.Top = 345;
            number4.Font = new Font(number4.Font.FontFamily, 16);
            number5.Height = 35;
            number5.Top = 345;
            number5.Font = new Font(number5.Font.FontFamily, 16);
            number6.Height = 35;
            number6.Top = 345;
            number6.Font = new Font(number6.Font.FontFamily, 16);
            number7.Height = 35;
            number7.Top = 305;
            number7.Font = new Font(number7.Font.FontFamily, 16);
            number8.Height = 35;
            number8.Top = 305;
            number8.Font = new Font(number8.Font.FontFamily, 16);
            number9.Height = 35;
            number9.Top = 305;
            number9.Font = new Font(number9.Font.FontFamily, 16);
            wash.Height = 35;
            wash.Top = 425;
            wash.Font = new Font(wash.Font.FontFamily, 16);
            dot.Height = 35;
            dot.Top = 425;
            dot.Font = new Font(dot.Font.FontFamily, 16);
            equality.Height = 35;
            equality.Top = 425;
            equality.Font = new Font(equality.Font.FontFamily, 16);
            addition.Height = 35;
            addition.Top = 385;
            addition.Font = new Font(addition.Font.FontFamily, 16);
            subtraction.Height = 35;
            subtraction.Top = 345;
            subtraction.Font = new Font(subtraction.Font.FontFamily, 16);
            multiplication.Height = 35;
            multiplication.Top = 305;
            multiplication.Font = new Font(multiplication.Font.FontFamily, 16);
            division.Height = 35;
            division.Top = 265;
            division.Font = new Font(division.Font.FontFamily, 16);
            percent.Height = 35;
            percent.Top = 265;
            percent.Font = new Font(percent.Font.FontFamily, 16);
            delete.Height = 35;
            delete.Top = 265;
            delete.Font = new Font(delete.Font.FontFamily, 16);
            brackets.Height = 35;
            brackets.Top = 265;
            brackets.Font = new Font(brackets.Font.FontFamily, 15);
        }
        private void ToNormalSize()
        {
            number0.Height = 48;
            number0.Top = 410;
            number0.Font = new Font(number0.Font.FontFamily, 20);
            number1.Height = 48;
            number1.Top = 350;
            number1.Font = new Font(number0.Font.FontFamily, 20);
            number2.Height = 48;
            number2.Top = 350;
            number2.Font = new Font(number0.Font.FontFamily, 20);
            number3.Height = 48;
            number3.Top = 350;
            number3.Font = new Font(number0.Font.FontFamily, 20);
            number4.Height = 48;
            number4.Top = 290;
            number4.Font = new Font(number0.Font.FontFamily, 20);
            number5.Height = 48;
            number5.Top = 290;
            number5.Font = new Font(number0.Font.FontFamily, 20);
            number6.Height = 48;
            number6.Top = 290;
            number6.Font = new Font(number0.Font.FontFamily, 20);
            number7.Height = 48;
            number7.Top = 230;
            number7.Font = new Font(number0.Font.FontFamily, 20);
            number8.Height = 48;
            number8.Top = 230;
            number8.Font = new Font(number0.Font.FontFamily, 20);
            number9.Height = 48;
            number9.Top = 230;
            number9.Font = new Font(number0.Font.FontFamily, 20);
            wash.Height = 48;
            wash.Top = 410;
            wash.Font = new Font(wash.Font.FontFamily, 20);
            dot.Height = 48;
            dot.Top = 410;
            dot.Font = new Font(wash.Font.FontFamily, 20);
            equality.Height = 48;
            equality.Top = 410;
            equality.Font = new Font(wash.Font.FontFamily, 20);
            addition.Height = 48;
            addition.Top = 350;
            addition.Font = new Font(wash.Font.FontFamily, 20);
            subtraction.Height = 48;
            subtraction.Top = 290;
            subtraction.Font = new Font(wash.Font.FontFamily, 20);
            multiplication.Height = 48;
            multiplication.Top = 230;
            multiplication.Font = new Font(wash.Font.FontFamily, 20);
            division.Height = 48;
            division.Top = 170;
            division.Font = new Font(wash.Font.FontFamily, 20);
            percent.Height = 48;
            percent.Top = 170;
            percent.Font = new Font(wash.Font.FontFamily, 20);
            delete.Height = 48;
            delete.Top = 170;
            delete.Font = new Font(wash.Font.FontFamily, 20);
            brackets.Height = 48;
            brackets.Top = 170;
            brackets.Font = new Font(wash.Font.FontFamily, 18);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            HideErrors();
            button1.Hide();
            button2.Show();
            panel1.Show();
            ToLowSize();
        }

        private void sin_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelsin.Show();
            IsSin = true;
            symbol = 's';
        }

        private void π_Click(object sender, EventArgs e)
        {
            HideErrors();
            if ((IsSin || IsCos || IsTan || IsLn || IsAsin || IsAcos || IsAtan) || (!IsPi && textBox1.Text.Length == 0))
            {
                textBox1.Text += Convert.ToString(Math.PI);
                IsPi = true;
                Ise = true;
            }
        }

        private void e_Click(object sender, EventArgs e)
        {
            HideErrors();
            if ((IsSin || IsCos || IsTan || IsLn || IsAsin || IsAcos || IsAtan) || (!Ise && textBox1.Text.Length == 0))
            {
                textBox1.Text += Convert.ToString(Math.E);
                IsPi = true;
                Ise = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void sqrt_Click(object sender, EventArgs e)
        {
            if (!IsSin && !IsCos && !IsTan && !IsLn && !IsAsin && !IsAcos && !IsAtan)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    if (FirstValue >= 0)
                    {
                        SecondValue = Math.Sqrt(FirstValue);
                        textBox1.Text = Convert.ToString(SecondValue);
                    }
                    else
                    {
                        IsError = true;
                        error.Show();
                        textBox1.Text = "";
                    }
                }
            }
            else if (textBox1.Text != String.Empty)
                textBox1.Text += " ^ 0,5";
        }

        private void fact_Click(object sender, EventArgs e)
        {
            if (!IsSin && !IsCos && !IsTan && !IsLn)
            {
                HideErrors();
                if (textBox1.Text != String.Empty && textBox1.Text != "-")
                {
                    FirstValue = Convert.ToDouble(textBox1.Text);
                    if (FirstValue % 1 == 0 && FirstValue >= 0)
                    {
                        SecondValue = Module1.Factorial(FirstValue);
                        textBox1.Text = Convert.ToString(SecondValue);
                    }
                    else
                    {
                        IsError = true;
                        error.Show();
                        textBox1.Text = "";
                    }
                }
            }
            else if (textBox1.Text != String.Empty)
                textBox1.Text += "!";
        }

       

        private void ln_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelln.Show();
            IsLn = true;
            symbol = 'l';
        }

        private void brackets_Click_1(object sender, EventArgs e)
        {
            HideErrors();
            if (IsSin || IsCos || IsTan || IsLn || IsAsin || IsAcos || IsAtan)
            {
                if (!BraketClose)
                {
                    textBox1.Text += "(";
                    BraketClose = true;
                }
                else
                {
                    textBox1.Text += ")";
                    BraketClose = false;
                }
            }
        }

        private void tg_Click(object sender, EventArgs e)
        {
            HideErrors();
            labeltan.Show();
            IsTan = true;
            symbol = 't';
        }

        private void error_Click(object sender, EventArgs e)
        {

        }

        private void cos_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelcos.Show();
            IsCos = true;
            symbol = 'c';
        }

        private void labelln_Click(object sender, EventArgs e)
        {

        }

        private void DEG_Click(object sender, EventArgs e)
        {
            DEG.Hide();
            RAD.Show();
            IsRad = false;
        }

        private void RAD_Click(object sender, EventArgs e)
        {
            RAD.Hide();
            DEG.Show();
            IsRad = true;
        }

        private void INV_Click(object sender, EventArgs e)
        {
            OpenINV++;
            if (OpenINV % 2 == 1)
            {
                asin.Show();
                acos.Show();
                atg.Show();
                sin.Hide();
                cos.Hide();
                tg.Hide();
            }
            else
            {
                asin.Hide();
                acos.Hide();
                atg.Hide();
                sin.Show();
                cos.Show();
                tg.Show();
                OpenINV = 0;
            }
 
        }

        private void asin_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelasin.Show();
            IsAsin = true;
            symbol = 'S';
        }

        private void acos_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelacos.Show();
            IsAcos = true;
            symbol = 'C';
        }

        private void atg_Click(object sender, EventArgs e)
        {
            HideErrors();
            labelatan.Show();
            IsAtan = true;
            symbol = 'T';
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void labelasin_Click(object sender, EventArgs e)
        {
        
        }

        private void labelatg_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            HideErrors();
            button2.Hide();
            button1.Show();
            panel1.Hide();
            OpenPanel = 0;
            ToNormalSize();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Show();
            button4.Show();
            button3.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Hide();
            button3.Show();
            button4.Hide();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void labeltan_Click(object sender, EventArgs e)
        {

        }
    }
}

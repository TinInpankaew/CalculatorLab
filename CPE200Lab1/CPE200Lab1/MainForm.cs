﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string secondOperand;
        private string operate;
        private bool Is_Secontime;
        CalculatorEngine Engine;
        private string Memories_Number;

        private void ResetAll()
        {
            lblDisplay.Text = "0";
            secondOperand = "0";
            firstOperand = "0";
            operate = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            Is_Secontime = false;
        }

        public MainForm()
        {
            Engine = new CalculatorEngine();
            InitializeComponent();

            ResetAll();
        }

        private void BtnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                //             resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void BtnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }

            if (Is_Secontime && ((Button)sender).Text == "√")
            {
                secondOperand = lblDisplay.Text;
                lblDisplay.Text = Engine.Calculate("√", secondOperand, "0");
                if (lblDisplay.Text.Length > 8)
                {
                    lblDisplay.Text = lblDisplay.Text.Substring(0, 8);
                }
            }
            else
            if (Is_Secontime)
            {
                secondOperand = lblDisplay.Text;
                lblDisplay.Text = Engine.Calculate(operate, firstOperand, secondOperand);
            }
            else
            {
                firstOperand = lblDisplay.Text;
                switch (((Button)sender).Text)
                {
                    case "+":
                    case "-":
                    case "x":
                    case "÷":
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        break;
                    case "%":
                        lblDisplay.Text = Engine.Calculate(((Button)sender).Text, firstOperand, "0");
                        break;
                    case "√":
                        lblDisplay.Text = Engine.Calculate(((Button)sender).Text, firstOperand, "0");
                        if (lblDisplay.Text.Length > 8)
                        {
                            lblDisplay.Text = lblDisplay.Text.Substring(0, 8);
                        }
                        break;
                    case "1/x":
                        lblDisplay.Text = Engine.Calculate(((Button)sender).Text, firstOperand, "0");
                        break;
                }
            }
            if (((Button)sender).Text != "%" && ((Button)sender).Text != "√" && ((Button)sender).Text != "1/x")
            {
                operate = ((Button)sender).Text;
                firstOperand = lblDisplay.Text;
                isAfterOperater = true;
                Is_Secontime = true;
            }

            isAllowBack = false;
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            secondOperand = lblDisplay.Text;
            string result = Engine.Calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            Is_Secontime = false;
            isAfterEqual = true;
        }

        private void BtnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                ResetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void BtnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                ResetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void CE_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0";
        }

        private void Btn_memories_Click(object sender, EventArgs e)
        {
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "MC":
                    Memories_Number = "0";
                    break;
                case "MR":
                    lblDisplay.Text = Memories_Number;
                    break;
                case "M+":
                    Memories_Number = (Convert.ToDouble(Memories_Number) + Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;
                case "M-":
                    Memories_Number = (Convert.ToDouble(Memories_Number) - Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;
                case "MS":
                    Memories_Number = lblDisplay.Text;
                    break;

            }



        }
    }
}
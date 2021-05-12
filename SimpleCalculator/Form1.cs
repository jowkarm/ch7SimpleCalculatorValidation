//Behrooz Kazemi, and Mohammad Jokar-Konavi
//May 12, 2021
//Exercise 7-2


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())  //The IsValidData method called here.
                {
                    decimal operand1 = Convert.ToDecimal(txtOperand1.Text);
                    string operator1 = txtOperator.Text;
                    decimal operand2 = Convert.ToDecimal(txtOperand2.Text);
                    decimal result = Calculate(operand1, operator1, operand2);

                    result = Math.Round(result, 4);
                    this.txtResult.Text = result.ToString();
                    txtOperand1.Focus();
                }
            }
            //catch (FormatException)
            //{
            //    MessageBox.Show(
            //        "Invalid numeric format. Please check all entries.",
            //        "Entry Error");
            //}
            //catch (OverflowException)
            //{
            //    MessageBox.Show(
            //        "Overflow error. Please enter smaller values.",
            //        "Entry Error");
            //}
            //catch (DivideByZeroException)
            //{
            //    MessageBox.Show(
            //        "Divide-by-zero error. Please enter a non-zero value for operand 2.",
            //        "Entry Error");
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                ex.GetType().ToString() + "\n" +
                ex.StackTrace, "Exception");
            }

        }

        private bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            //Validate the Operand One taxt box
            errorMessage += IsPresent(txtOperand1.Text, txtOperand1.Tag.ToString());
            errorMessage += IsDecimal(txtOperand1.Text, txtOperand1.Tag.ToString());
            errorMessage += IsWithinRange(txtOperand1.Text, txtOperand1.Tag.ToString(), 0, 1000000);

            //Validate the Operator taxt box
            errorMessage += IsPresent(txtOperator.Text, txtOperator.Tag.ToString());
            errorMessage += IsOperator(txtOperator.Text, txtOperator.Tag.ToString());


            //Validate the Operand Two taxt box
            errorMessage += IsPresent(txtOperand2.Text, txtOperand2.Tag.ToString());
            errorMessage += IsDecimal(txtOperand2.Text, txtOperand2.Tag.ToString());
            errorMessage += IsWithinRange(txtOperand2.Text, txtOperand2.Tag.ToString(), 0, 1000000);
            errorMessage += IsValidOperation(txtOperand2.Text, txtOperand2.Tag.ToString(), txtOperator.Text);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }


        //A method that checks for required fields.
        private string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg += name + " is a required field.\n";
            }
            return msg;
        }

        //A method that checks for decimal values.
        private string IsDecimal(string value, string name)
        {
            string msg = "";
            if (!Decimal.TryParse(value, out _))
            {
                msg += name + " must be a valid decimal value.\n";
            }
            return msg;
        }


        //A method that checks for the range of all numeric values.
        private string IsWithinRange(string value, string name, decimal min,
            decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg += name + " must be between " + min + " and " + max + ".\n";
                }
            }
            return msg;
        }

        //A method that checks the operator.
        private string IsOperator(string value, string name)
        {
            string msg = "";
            if (value != "+" && value != "-" & value != "/" & value != "*")
            {
                msg += name + " must be + , - , / , or *.\n";  
            }
            return msg;
        }

        //A method that checks the validity of operation.
        private string IsValidOperation(string value, string name, string operatorOne)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal operandTwo))
            { 
                if (operatorOne == "/" && operandTwo == 0)
                {
               
                    msg += name + " must be a non-zero decimal value.\n";
             
                }
            }
            return msg;
        }
        private decimal Calculate(decimal operand1, string operator1,
            decimal operand2)
        {
            decimal result = 0;
            if (operator1 == "+")
                result = operand1 + operand2;
            else if (operator1 == "-")
                result = operand1 - operand2;
            else if (operator1 == "*")
                result = operand1 * operand2;
            else if (operator1 == "/")
                result = operand1 / operand2;
            return result;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearResult(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
        }
    }
}

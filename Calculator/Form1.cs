using System.Text.RegularExpressions;

namespace Calculator;

public static class Extensions
{
    /// <summary>
    /// Boolean check if string is an operator
    /// </summary>
    /// <param name="s">string to check</param>
    /// <returns>true if +, -, *, /</returns>
    public static bool IsOperator(this string s)
    {
        return s is "+" or "-" or "*" or "/";
    }
}

public partial class Form1 : Form
{
    /// <summary>
    /// Window form constructor, set window form name to the application name
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        Text = Application.ProductName;
        Ans.Text = "0";
    }

    /// <summary>
    /// Adds a string value to the text window
    /// </summary>
    /// <param name="value"> Value to add </param>
    private void InputValue(string value)
    {
        if (value.IsOperator() && string.IsNullOrEmpty(Ans.Text)) return;
        
        if (value.Equals("=")) DoCalculation(Ans.Text);
        
        // If text box shows '0' replace
        if (Ans.Text is "0")
        {
            Ans.Text = $"{value}";
        }
        else if (!value.Equals("="))
        {
            Ans.Text += $"{value}";
        }
    }

    /// <summary>
    /// Clear text boxes
    /// </summary>
    private void Clear()
    {
        Input.Text = string.Empty;
        Ans.Text = string.Empty;
    }

    private void DoCalculation(string text)
    {
        // regex for checking matches of numbers, negative numbers, +, /, -, *
        var regex = new Regex(@"(-?\d+|[+\-*/])");
        var matches = regex.Matches(text); // regex matches in text
        
        if (matches.Count < 1)
        {
            Clear();
            return;
        }

        var currentOp = "+";

        if (!int.TryParse(matches[0].Value, out var result))
        {
            Ans.Text = $"{int.MaxValue}";
            return;
        }

        for (var i = 1; i < matches.Count; i++)
        {
            var match = matches[i].Value; // store current match value

            if (match.IsOperator())
            {
                currentOp = match; // update current operator if it is one
            }
            else // otherwise parse the match to an integer
            {
                int num = int.Parse(match);
                
                try
                {
                    result = DoMathOperation(result, num, currentOp); // set result to: result (operator) num
                }
                catch (OverflowException) // overflow exception, if it is set to max value of integer
                {
                    Ans.Text = $"{int.MaxValue}";
                    return;
                }
            }
        }

        Ans.Text = result.ToString(); // set text to result
    }

    // +, -, *, /, math calculation
    private int DoMathOperation(int a, int b, string operation)
    {
        // checked prevents overflow
        return operation switch
        {
            "+" => checked(a + b),
            "-" => checked(a - b),
            "*" => checked(a * b),
            "/" => b == 0 ? 1 : a / b,
            _ => throw new ArgumentException($"Invalid operator: {operation}")
        };
    }

    /// <summary>
    /// Input values when clicking on buttons
    /// </summary>

    #region Button Clicks

    private void Button1_Click(object sender, EventArgs e) => InputValue("1");
    private void button2_Click(object sender, EventArgs e) => InputValue("2");
    private void button3_Click(object sender, EventArgs e) => InputValue("3");
    private void button4_Click(object sender, EventArgs e) => InputValue("4");
    private void button5_Click(object sender, EventArgs e) => InputValue("5");
    private void Button6_Click(object sender, EventArgs e) => InputValue("6");
    private void button7_Click(object sender, EventArgs e) => InputValue("7");
    private void button8_Click(object sender, EventArgs e) => InputValue("8");
    private void button9_Click(object sender, EventArgs e) => InputValue("9");
    private void button0_Click(object sender, EventArgs e) => InputValue("0");
    private void buttonClear_Click(object sender, EventArgs e) => Clear();
    private void buttonAdd_Click(object sender, EventArgs e) => InputValue("+");
    private void buttonMinus_Click(object sender, EventArgs e) => InputValue("-");
    private void buttonMultiply_Click(object sender, EventArgs e) => InputValue("*");
    private void buttonDivide_Click(object sender, EventArgs e) => InputValue("/");
    private void buttonAns_Click(object sender, EventArgs e) => InputValue("=");

    #endregion Button Clicks
}
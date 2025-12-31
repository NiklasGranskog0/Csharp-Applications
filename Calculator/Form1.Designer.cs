namespace Calculator;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Input = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        buttonAdd = new System.Windows.Forms.Button();
        buttonMinus = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        buttonMultiply = new System.Windows.Forms.Button();
        button9 = new System.Windows.Forms.Button();
        button8 = new System.Windows.Forms.Button();
        button7 = new System.Windows.Forms.Button();
        buttonDivide = new System.Windows.Forms.Button();
        buttonAns = new System.Windows.Forms.Button();
        button0 = new System.Windows.Forms.Button();
        buttonClear = new System.Windows.Forms.Button();
        Ans = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // Input
        // 
        Input.Location = new System.Drawing.Point(12, 12);
        Input.Name = "Input";
        Input.Size = new System.Drawing.Size(178, 23);
        Input.TabIndex = 1;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(12, 67);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(40, 40);
        button1.TabIndex = 2;
        button1.Text = "1";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Button1_Click;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(58, 67);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(40, 40);
        button2.TabIndex = 3;
        button2.Text = "2";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(104, 67);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(40, 40);
        button3.TabIndex = 4;
        button3.Text = "3";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // buttonAdd
        // 
        buttonAdd.Location = new System.Drawing.Point(150, 67);
        buttonAdd.Name = "buttonAdd";
        buttonAdd.Size = new System.Drawing.Size(40, 40);
        buttonAdd.TabIndex = 5;
        buttonAdd.Text = "+";
        buttonAdd.UseVisualStyleBackColor = true;
        buttonAdd.Click += buttonAdd_Click;
        // 
        // buttonMinus
        // 
        buttonMinus.Location = new System.Drawing.Point(150, 113);
        buttonMinus.Name = "buttonMinus";
        buttonMinus.Size = new System.Drawing.Size(40, 40);
        buttonMinus.TabIndex = 9;
        buttonMinus.Text = "-";
        buttonMinus.UseVisualStyleBackColor = true;
        buttonMinus.Click += buttonMinus_Click;
        // 
        // button6
        // 
        button6.Location = new System.Drawing.Point(104, 113);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(40, 40);
        button6.TabIndex = 8;
        button6.Text = "6";
        button6.UseVisualStyleBackColor = true;
        button6.Click += Button6_Click;
        // 
        // button5
        // 
        button5.Location = new System.Drawing.Point(58, 113);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(40, 40);
        button5.TabIndex = 7;
        button5.Text = "5";
        button5.UseVisualStyleBackColor = true;
        button5.Click += button5_Click;
        // 
        // button4
        // 
        button4.Location = new System.Drawing.Point(12, 113);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(40, 40);
        button4.TabIndex = 6;
        button4.Text = "4";
        button4.UseVisualStyleBackColor = true;
        button4.Click += button4_Click;
        // 
        // buttonMultiply
        // 
        buttonMultiply.Location = new System.Drawing.Point(150, 159);
        buttonMultiply.Name = "buttonMultiply";
        buttonMultiply.Size = new System.Drawing.Size(40, 40);
        buttonMultiply.TabIndex = 13;
        buttonMultiply.Text = "X";
        buttonMultiply.UseVisualStyleBackColor = true;
        buttonMultiply.Click += buttonMultiply_Click;
        // 
        // button9
        // 
        button9.Location = new System.Drawing.Point(104, 159);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(40, 40);
        button9.TabIndex = 12;
        button9.Text = "9";
        button9.UseVisualStyleBackColor = true;
        button9.Click += button9_Click;
        // 
        // button8
        // 
        button8.Location = new System.Drawing.Point(58, 159);
        button8.Name = "button8";
        button8.Size = new System.Drawing.Size(40, 40);
        button8.TabIndex = 11;
        button8.Text = "8";
        button8.UseVisualStyleBackColor = true;
        button8.Click += button8_Click;
        // 
        // button7
        // 
        button7.Location = new System.Drawing.Point(12, 159);
        button7.Name = "button7";
        button7.RightToLeft = System.Windows.Forms.RightToLeft.No;
        button7.Size = new System.Drawing.Size(40, 40);
        button7.TabIndex = 10;
        button7.Text = "7";
        button7.UseVisualStyleBackColor = true;
        button7.Click += button7_Click;
        // 
        // buttonDivide
        // 
        buttonDivide.Location = new System.Drawing.Point(150, 205);
        buttonDivide.Name = "buttonDivide";
        buttonDivide.Size = new System.Drawing.Size(40, 40);
        buttonDivide.TabIndex = 17;
        buttonDivide.Text = "/";
        buttonDivide.UseVisualStyleBackColor = true;
        buttonDivide.Click += buttonDivide_Click;
        // 
        // buttonAns
        // 
        buttonAns.Location = new System.Drawing.Point(104, 205);
        buttonAns.Name = "buttonAns";
        buttonAns.Size = new System.Drawing.Size(40, 40);
        buttonAns.TabIndex = 16;
        buttonAns.Text = "=";
        buttonAns.UseVisualStyleBackColor = true;
        buttonAns.Click += buttonAns_Click;
        // 
        // button0
        // 
        button0.Location = new System.Drawing.Point(58, 205);
        button0.Name = "button0";
        button0.Size = new System.Drawing.Size(40, 40);
        button0.TabIndex = 15;
        button0.Text = "0";
        button0.UseVisualStyleBackColor = true;
        button0.Click += button0_Click;
        // 
        // buttonClear
        // 
        buttonClear.Location = new System.Drawing.Point(12, 205);
        buttonClear.Name = "buttonClear";
        buttonClear.Size = new System.Drawing.Size(40, 40);
        buttonClear.TabIndex = 14;
        buttonClear.Text = "C";
        buttonClear.UseVisualStyleBackColor = true;
        buttonClear.Click += buttonClear_Click;
        // 
        // Ans
        // 
        Ans.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Ans.Location = new System.Drawing.Point(12, 41);
        Ans.Name = "Ans";
        Ans.Size = new System.Drawing.Size(178, 23);
        Ans.TabIndex = 18;
        Ans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(233, 256);
        Controls.Add(Ans);
        Controls.Add(buttonDivide);
        Controls.Add(buttonAns);
        Controls.Add(button0);
        Controls.Add(buttonClear);
        Controls.Add(buttonMultiply);
        Controls.Add(button9);
        Controls.Add(button8);
        Controls.Add(button7);
        Controls.Add(buttonMinus);
        Controls.Add(button6);
        Controls.Add(button5);
        Controls.Add(button4);
        Controls.Add(buttonAdd);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(Input);
        Text = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label Ans;

    private System.Windows.Forms.Button button6;

    private System.Windows.Forms.Button button4;

    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button buttonAdd;
    private System.Windows.Forms.Button buttonMinus;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button buttonMultiply;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button buttonDivide;
    private System.Windows.Forms.Button buttonAns;
    private System.Windows.Forms.Button button0;
    private System.Windows.Forms.Button buttonClear;

    private System.Windows.Forms.Label Input;

    #endregion
}
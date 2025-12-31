namespace LottoProgram;

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
        label1 = new System.Windows.Forms.Label();
        num1 = new System.Windows.Forms.TextBox();
        num2 = new System.Windows.Forms.TextBox();
        num3 = new System.Windows.Forms.TextBox();
        num4 = new System.Windows.Forms.TextBox();
        num5 = new System.Windows.Forms.TextBox();
        num6 = new System.Windows.Forms.TextBox();
        num7 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        dragNum = new System.Windows.Forms.TextBox();
        StartLotto = new System.Windows.Forms.Button();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        FiveLabel = new System.Windows.Forms.Label();
        SixLabel = new System.Windows.Forms.Label();
        SevenLabel = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(12, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(100, 23);
        label1.TabIndex = 0;
        label1.Text = "Din Lottorad: ";
        // 
        // num1
        // 
        num1.Location = new System.Drawing.Point(92, 6);
        num1.Name = "num1";
        num1.Size = new System.Drawing.Size(56, 23);
        num1.TabIndex = 1;
        // 
        // num2
        // 
        num2.Location = new System.Drawing.Point(154, 6);
        num2.Name = "num2";
        num2.Size = new System.Drawing.Size(56, 23);
        num2.TabIndex = 2;
        // 
        // num3
        // 
        num3.Location = new System.Drawing.Point(216, 6);
        num3.Name = "num3";
        num3.Size = new System.Drawing.Size(56, 23);
        num3.TabIndex = 3;
        // 
        // num4
        // 
        num4.Location = new System.Drawing.Point(278, 6);
        num4.Name = "num4";
        num4.Size = new System.Drawing.Size(56, 23);
        num4.TabIndex = 4;
        // 
        // num5
        // 
        num5.Location = new System.Drawing.Point(340, 6);
        num5.Name = "num5";
        num5.Size = new System.Drawing.Size(56, 23);
        num5.TabIndex = 5;
        // 
        // num6
        // 
        num6.Location = new System.Drawing.Point(402, 6);
        num6.Name = "num6";
        num6.Size = new System.Drawing.Size(56, 23);
        num6.TabIndex = 6;
        // 
        // num7
        // 
        num7.Location = new System.Drawing.Point(464, 6);
        num7.Name = "num7";
        num7.Size = new System.Drawing.Size(56, 23);
        num7.TabIndex = 7;
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(92, 45);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(100, 23);
        label2.TabIndex = 8;
        label2.Text = "Antal dragningar:";
        // 
        // dragNum
        // 
        dragNum.Location = new System.Drawing.Point(198, 42);
        dragNum.Name = "dragNum";
        dragNum.Size = new System.Drawing.Size(136, 23);
        dragNum.TabIndex = 9;
        // 
        // StartLotto
        // 
        StartLotto.Location = new System.Drawing.Point(340, 41);
        StartLotto.Name = "StartLotto";
        StartLotto.Size = new System.Drawing.Size(180, 23);
        StartLotto.TabIndex = 10;
        StartLotto.Text = "Starta Lotto";
        StartLotto.UseVisualStyleBackColor = true;
        StartLotto.Click += StartLotto_Click;
        // 
        // label3
        // 
        label3.Location = new System.Drawing.Point(92, 110);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(43, 23);
        label3.TabIndex = 11;
        label3.Text = "5 rätt: ";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(205, 111);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(43, 23);
        label4.TabIndex = 13;
        label4.Text = "6 rätt: ";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label5
        // 
        label5.Location = new System.Drawing.Point(319, 111);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(43, 23);
        label5.TabIndex = 15;
        label5.Text = "7 rätt: ";
        label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // FiveLabel
        // 
        FiveLabel.Location = new System.Drawing.Point(131, 110);
        FiveLabel.Name = "FiveLabel";
        FiveLabel.Size = new System.Drawing.Size(43, 23);
        FiveLabel.TabIndex = 17;
        FiveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // SixLabel
        // 
        SixLabel.Location = new System.Drawing.Point(243, 110);
        SixLabel.Name = "SixLabel";
        SixLabel.Size = new System.Drawing.Size(43, 23);
        SixLabel.TabIndex = 18;
        SixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // SevenLabel
        // 
        SevenLabel.Location = new System.Drawing.Point(353, 111);
        SevenLabel.Name = "SevenLabel";
        SevenLabel.Size = new System.Drawing.Size(43, 23);
        SevenLabel.TabIndex = 19;
        SevenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(557, 147);
        Controls.Add(SevenLabel);
        Controls.Add(SixLabel);
        Controls.Add(FiveLabel);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(StartLotto);
        Controls.Add(dragNum);
        Controls.Add(label2);
        Controls.Add(num7);
        Controls.Add(num6);
        Controls.Add(num5);
        Controls.Add(num4);
        Controls.Add(num3);
        Controls.Add(num2);
        Controls.Add(num1);
        Controls.Add(label1);
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label FiveLabel;
    private System.Windows.Forms.Label SixLabel;
    private System.Windows.Forms.Label SevenLabel;

    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox num1;
    private System.Windows.Forms.TextBox num2;
    private System.Windows.Forms.TextBox num3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox num4;
    private System.Windows.Forms.TextBox num5;
    private System.Windows.Forms.TextBox num6;
    private System.Windows.Forms.TextBox num7;
    private System.Windows.Forms.TextBox dragNum;
    private System.Windows.Forms.Button StartLotto;

    #endregion
}
namespace LottoProgram;

public partial class Form1 : Form
{
    private List<string>? m_Numbers;
    private HashSet<int> m_UniqueNumbers;

    /// <summary>
    /// Main Form constructor, sets drawing number
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        dragNum.Text = "999999";
    }

    /// <summary>
    /// Checks if the lotto numbers entered are valid
    /// </summary>
    /// <returns> True if all 7 numbers are valid, otherwise false </returns>
    private bool CheckValidNumbers()
    {
        m_Numbers = [num1.Text, num2.Text, num3.Text, num4.Text, num5.Text, num6.Text, num7.Text];
        m_UniqueNumbers = [];

        foreach (var value in m_Numbers)
        {
            if (!int.TryParse(value, out var number) || number < 1 || number > 35)
            {
                MessageBox.Show($"Please enter a valid number in box {m_Numbers.IndexOf(value) + 1} (1 - 35)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_UniqueNumbers.Add(number)) continue;

            MessageBox.Show($"You multiple {number}'s, you can only have one.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Start lotto drawing when clicking on the button
    /// </summary>
    private void StartLotto_Click(object sender, EventArgs e)
    {
        StartLotto.Enabled = false; // When clicking the button disable it
        
        // Check if the number of lotto draws are valid
        if (!int.TryParse(dragNum.Text, out var dragAntal) || dragAntal < 1)
        {
            MessageBox.Show("Not a valid drag number", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            StartLotto.Enabled = true;
            return;
        }

        if (!CheckValidNumbers())
        {
            StartLotto.Enabled = true;
            return;
        }

        var lottoNumber = new HashSet<int>();
        var rnd = new Random();

        var seven = 0;
        var six = 0;
        var five = 0;

        for (int i = 0; i < dragAntal; i++)
        {
            while (lottoNumber.Count < 7) // Add 7 unique numbers to lotto number between 1 & 35
            {
                lottoNumber.Add(rnd.Next(1, 35));
            }

            // Check how many numbers of our entered numbers exists in lotto number
            var matches = m_UniqueNumbers.Count(num => lottoNumber.Contains(num));

            switch (matches)
            {
                case 7:
                    seven++;
                    break;
                case 6:
                    six++;
                    break;
                case 5:
                    five++;
                    break;
            }

            lottoNumber.Clear();
        }

        SevenLabel.Text = seven.ToString();
        SixLabel.Text = six.ToString();
        FiveLabel.Text = five.ToString();
        StartLotto.Enabled = true;
    }
}
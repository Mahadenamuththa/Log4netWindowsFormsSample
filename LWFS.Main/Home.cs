using System;
using System.Windows.Forms;

namespace LWFS.Main
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OperationLogger.LogMessage(textBox1.Text);
                throw new Exception(textBox1.Text);
            }
            catch (Exception ex)
            {
                ErrorLogger.AddError(ex);
                ErrorLoggerType2.AddError(ex);
            }
        }
    }
}

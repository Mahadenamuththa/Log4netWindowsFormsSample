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
                throw new Exception(textBox1.Text);
            }
            catch (Exception ex)
            {
                ErrorLogger.AddError(ex); 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
	public partial class Form2 : Form
	{
		public static string[] Fields { get; set; }

		public Form2()
		{
			InitializeComponent();
			Fields = new string[]{
				textBox1.Text.ToUpper(),
				textBox2.Text.ToUpper(),
				textBox3.Text.ToUpper(),
				textBox4.Text.ToUpper()
			};
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Hide();
			Fields = new string[]{
				textBox1.Text.ToUpper(),
				textBox2.Text.ToUpper(),
				textBox3.Text.ToUpper(),
				textBox4.Text.ToUpper()
			};
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Fields = new string[]{
				"","","",""
			};
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (Fields != null)
			{
				textBox1.Text = Fields[0];
				textBox2.Text = Fields[1];
				textBox3.Text = Fields[2];
				textBox4.Text = Fields[3];
			}
			this.Hide();
		}
	}
}

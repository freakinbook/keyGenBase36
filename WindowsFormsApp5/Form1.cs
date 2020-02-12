using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp5
{

	public partial class Form1 : Form
	{
		private const string BASE_36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private Form configureForm;
		private static bool resumeGenerating;
		private static Random rand;
		private static Stopwatch stopwatch;
		public Form1()
		{
			InitializeComponent();
			rand = new Random();
			stopwatch = new Stopwatch();
			configureForm = new Form2();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (Form2.Fields != null)
			{
				textBox1.Text = Generate(Form2.Fields);
			}
			else
			{
				textBox1.Text = Generate();
			}
		}

		private static string Generate()
		{
			StringBuilder sb = new StringBuilder();
			char nextChar;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					nextChar = BASE_36[rand.Next(36)];
					sb.Append(nextChar);
				}
				if (i != 3)
					sb.Append("-");
			}
			return sb.ToString();
		}

		private static string Generate(params string[] quarters)
		{
			StringBuilder sb = new StringBuilder();
			char nextChar;
			for (int i = 0; i < 4; i++)
			{
				if (quarters[i] == "")
				{
					for (int j = 0; j < 4; j++)
					{
						nextChar = BASE_36[rand.Next(36)];
						sb.Append(nextChar);
					}
				}
				else
				{
					sb.Append(quarters[i]);
				}
				if (i != 3)
					sb.Append("-");
			}
			return sb.ToString();

		}

		/*private static void GenerateForExport(StreamWriter sw)
		{
			StringBuilder sb = new StringBuilder();
			for (int k = 0; k < 1; k++)
			{
				for (int i = 0; i < 1679616; i++)
				{
					int j = i;
					if (i > 46655)
					{
						sb.Append(BASE_36[i / 46656]);
						j = j % 46656;
					} else
					{
						sb.Append("0");
					}
					if (i > 1295)
					{
						sb.Append(BASE_36[j / 1296]);
						j = i % 1296;
					} else
					{
						sb.Append("0");
					}
					if (i > 35)
					{
						sb.Append(BASE_36[j / 36]);
						j = i % 36;
					} else
					{
						sb.Append("0");
					}
					sb.Append(BASE_36[j]);
					sw.WriteLine(sb.ToString());
					sb.Clear();
				}
			}
			MessageBox.Show("Done");
		}*/

		private static void GenerateForExport()
		{
			resumeGenerating = true;
			new Thread(() =>
			{
				long i = 0L;
				stopwatch.Start();

				using (StreamWriter newFile = new StreamWriter("gen_export.txt"))
				{
					while (resumeGenerating)
					{
						newFile.WriteLine(Generate());
						i++;
					}
					newFile.WriteLine($"Generated {i} keys in {stopwatch.Elapsed}");
				}
				MessageBox.Show("Stopped generating");
				stopwatch.Reset();
			}).Start();

		}

		private static void GenerateForExport(params string[] quarters)
		{
			resumeGenerating = true;
			new Thread(() =>
			{
				long i = 0L;
				stopwatch.Start();
				using (StreamWriter newFile = new StreamWriter("gen_export.txt"))
				{
					while (resumeGenerating)
					{
						newFile.WriteLine(Generate(quarters));
						i++;
					}
					newFile.WriteLine($"Generated {i} keys in {stopwatch.Elapsed}");
					stopwatch.Reset();
				}
				MessageBox.Show("Stopped generating");
			}).Start();

		}


		private void button3_Click(object sender, EventArgs e)
		{
			configureForm.ShowDialog();
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;
			button2.Enabled = false;
			button3.Enabled = false;
			if (Form2.Fields != null)
			{
				GenerateForExport(Form2.Fields);
			}
			else
			{
				GenerateForExport();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			resumeGenerating = false;
			button1.Enabled = true;
			button2.Enabled = true;
			button3.Enabled = true;
		}
	}
}


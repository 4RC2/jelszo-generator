using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JelszóGenerátor
{
    public partial class Form1 : Form
    {
        Int32 karakterekSzáma;

        String kisbetűk;
        String nagybetűk;
        String számok;
        String speciálisKarakterek;
        String ékezetesBetűk;

        String karakterek;

        Random véletlen;
        String jelszó = "";

        FolderBrowserDialog mappaBöngésző;
        String fájlnév = "";
        String fájltípus = "";
        String fájlútvonal = "";

        String jelszóNév;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            karakterek = "";
            jelszó = "";

            kisbetűk = "abcdefghijklmnopqrstuvwxyz";
            nagybetűk = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            számok = "0123456789";
            speciálisKarakterek = @"!#$%'()*+,-./:;<=>?@[\]_";
            ékezetesBetűk = "áÁéÉíÍóÓöÖőŐúÚüÜűŰ";

            if (textBox1.Text != "")
            {
                try
                {
                    Int32.Parse(textBox1.Text);

                    if (Convert.ToInt32(textBox1.Text) != 0)
                    {
                        karakterekSzáma = Convert.ToInt32(textBox1.Text);

                        if (textBox1.Text.StartsWith("+"))
                        {
                            textBox1.Text = textBox1.Text.Substring(1);
                        }
                        else if (textBox1.Text.StartsWith("0") && !textBox1.Text.EndsWith("0"))
                        {
                            if (Regex.Matches(textBox1.Text, "0").Count == 1)
                            {
                                textBox1.Text = textBox1.Text.Substring(1);
                            }
                            else if (Regex.Matches(textBox1.Text, "0").Count == 2)
                            {
                                textBox1.Text = textBox1.Text.Substring(2);
                            }
                        }
                        else if (textBox1.Text.StartsWith("0") && textBox1.Text.EndsWith("0"))
                        {
                            textBox1.Text = textBox1.Text.Substring(1);
                        }
                        else if (karakterekSzáma < 0)
                        {
                            karakterekSzáma = Math.Abs(karakterekSzáma);
                            textBox1.Text = karakterekSzáma.ToString();
                        }

                        if (checkBox1.Checked)
                        {
                            karakterek += kisbetűk;
                        }
                        if (checkBox2.Checked)
                        {
                            karakterek += nagybetűk;
                        }
                        if (checkBox3.Checked)
                        {
                            karakterek += számok;
                        }
                        if (checkBox4.Checked)
                        {
                            karakterek += speciálisKarakterek;
                        }
                        if (checkBox5.Checked)
                        {
                            karakterek += ékezetesBetűk;
                        }

                        if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)
                        {
                            véletlen = new Random();

                            for (int i = 1; i <= karakterekSzáma; i++)
                            {
                                jelszó += karakterek[véletlen.Next(0, karakterek.Length)].ToString();
                                label2.Text = jelszó;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Adja meg, hogy milyen karaktereket tartalmazhasson a generálandó jelszó!",
                                "Jelszó generátor program", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A generálandó jelszó nem állhat 0 karakterből.", "Jelszó generátor program",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("A generálandó jelszó karaktereinek száma csak egész szám lehet.", "Jelszó generátor program",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Adja meg, hogy hány karaktert tartalmazzon a generálandó jelszó!", "Jelszó generátor program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (jelszó != "")
            {
                Clipboard.SetText(jelszó);
                MessageBox.Show("A generált jelszó vágólapra lett másolva."+"\n\n"+"Jelszó:   "+jelszó, "Jelszó generátor program",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Generáljon egy új jelszót!", "Jelszó generátor program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mappaBöngésző = new FolderBrowserDialog();

            if (textBox2.Text != "fájlnév" && textBox2.Text != "" && !textBox2.Text.StartsWith(" ") && !textBox2.Text.StartsWith(" "))
            {
                fájlnév = textBox2.Text.Trim();

                if (comboBox1.SelectedItem != null)
                {
                    fájltípus = comboBox1.Text;

                    try
                    {
                        mappaBöngésző.ShowDialog();

                        if (mappaBöngésző.SelectedPath != "")
                        {
                            fájlútvonal = mappaBöngésző.SelectedPath + "\\" + fájlnév + fájltípus;
                            linkLabel1.Text = fájlútvonal;
                        }
                        else
                        {
                            MessageBox.Show("Adja meg a fájl útvonalát!", "Jelszó generátor program",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception kivétel)
                    {
                        MessageBox.Show(kivétel.Message, "Jelszó generátor program",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Adja meg a fájl típusát!", "Jelszó generátor program",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Adja meg a fájl nevét!", "Jelszó generátor program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBox2.Text = "fájlnév";
                textBox2.ForeColor = Color.DimGray;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (fájlútvonal != "" )
                {
                    if (fájlnév != "")
                    {
                        if (fájltípus != "")
                        {
                            if (textBox3.Text != "pl.: Twitter" && textBox3.Text != "")
                            {
                                jelszóNév = textBox3.Text.Trim() + ": ";
                            }
                            else
                            {
                                jelszóNév = "";
                            }

                            if (jelszó != "")
                            {
                                using (TextWriter szövegÍró = new StreamWriter(fájlútvonal, true))
                                {
                                    szövegÍró.WriteLine(jelszóNév + jelszó);
                                    szövegÍró.WriteLine("");

                                    textBox3.Text = "pl.: Twitter";
                                    textBox3.ForeColor = Color.DimGray;

                                    MessageBox.Show("A fájl mentése sikeres volt." + "\n\n" + "A fájl útvonala: " + fájlútvonal, "Jelszó generátor program",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Generáljon egy új jelszót!", "Jelszó generátor program",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Adja meg a fájl típusát!", "Jelszó generátor program",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Adja meg a fájl nevét!", "Jelszó generátor program",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Adja meg a fájl útvonalát!", "Jelszó generátor program",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception kivétel)
            {
                MessageBox.Show(Convert.ToString(kivétel), "Jelszó generátor program",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "fájlnév")
            {
                textBox2.Text = "";
            }
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            fájlnév = textBox2.Text;

            if (fájlútvonal != "")
            {
                if (mappaBöngésző.SelectedPath != "")
                {
                    fájlútvonal = mappaBöngésző.SelectedPath + "\\" + fájlnév + fájltípus;
                    linkLabel1.Text = fájlútvonal;
                }
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.Black;
            textBox3.Text = "";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fájltípus = comboBox1.Text;

            if (fájlútvonal != "" && textBox2.Text != "")
            {
                if (mappaBöngésző.SelectedPath != "")
                {
                    fájlútvonal = mappaBöngésző.SelectedPath + "\\" + fájlnév + fájltípus;
                    linkLabel1.Text = fájlútvonal;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", mappaBöngésző.SelectedPath);

            if (File.Exists(fájlútvonal))
            {
                Process.Start("explorer.exe", fájlútvonal);
            }
        }
    }
}

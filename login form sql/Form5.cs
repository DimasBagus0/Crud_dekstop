using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_form_sql
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DXY\SQLEXPRESS;Initial Catalog=dimas;Integrated Security=True");

        string imglocation = "";
        SqlCommand cmd;

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin keluar ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Form1 form2 = new Form1();
                form2.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.* ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = openFileDialog.FileName.ToString();
                pictureBox1.ImageLocation = imglocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [table_dataSiswa] (nis,namasiswa,kelas,jurusan,alamatrumah,foto) values ('" + txt_nis.Text + "','" + txt_nama.Text + "','" + txt_kelas.Text + "','" + txt_jurusan.Text + "','" + txt_alamat.Text + "',@images)";
            cmd.Parameters.Add(new SqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data Berhasil Disimpan");
            txt_nis.Text = "";
            txt_nama.Text = "";
            txt_kelas.Text = "";
            txt_jurusan.Text = "";
            txt_alamat.Text = "";
            pictureBox1.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_nis.Text = "";
            txt_nama.Text = "";
            txt_kelas.Text = "";
            txt_jurusan.Text = "";
            txt_alamat.Text = "";

            pictureBox1.Image = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from table_dataSiswa";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from table_dataSiswa";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update [table_dataSiswa] set nis='" + this.txt_nis.Text + "',namasiswa='" + this.txt_nama.Text + "',kelas='" + this.txt_kelas.Text + "',jurusan='" + this.txt_jurusan.Text + "',alamatrumah='" + this.txt_alamat.Text + "',foto=@images where nis='" + this.txt_nis.Text + "'";
            cmd.Parameters.Add(new SqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            conn.Close();
            display_data();
            MessageBox.Show("Data Berhasil Diupdate");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from [table_dataSiswa] where nis = '" + txt_nis.Text + "'";
            cmd.ExecuteNonQuery();
            conn.Close();
            txt_nis.Text = "";
            display_data();
            MessageBox.Show("Data Berhasil Dihapus");
        }

        private void textcari_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Data Expert";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Dimas Bagus Adityas";
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [table_dataSiswa] where namasiswa='" + textcari.Text + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            textcari.Text = "";
        }
    }
}

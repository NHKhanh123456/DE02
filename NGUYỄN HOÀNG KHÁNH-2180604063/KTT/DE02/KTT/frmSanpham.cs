using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KTT
{
    public partial class Form_QuanLy : Form
    {
        public Form_QuanLy()
        {
            InitializeComponent();
        }

        private void Form_QuanLy_Load(object sender, EventArgs e)
        {
            Model_Data model_Data = new Model_Data();
            List<LoaiSP> ListLoai = model_Data.LoaiSP.ToList();
            List<Sanpham> ListSP = model_Data.Sanpham.ToList();

            comboBox_LoaiSP.DataSource = ListLoai;
            comboBox_LoaiSP.DisplayMember = "TenLoai";
            comboBox_LoaiSP.ValueMember = "MaLoai";

            dataGridView1.Rows.Clear();
            foreach (var item in ListSP)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.MaSP;
                dataGridView1.Rows[index].Cells[1].Value = item.TenSP;
                dataGridView1.Rows[index].Cells[2].Value = item.Ngaynhap;
                dataGridView1.Rows[index].Cells[3].Value = item.LoaiSP.TenLoai;
            }
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = textBox_MaSP.Text;
            dataGridView1.Rows[index].Cells[1].Value = textBox_TenSP.Text;
            dataGridView1.Rows[index].Cells[2].Value = dateTimePicker_NgayNhap.Value;
            dataGridView1.Rows[index].Cells[3].Value = comboBox_LoaiSP.Text;

            Model_Data model_Data = new Model_Data();
            List<LoaiSP> ListLoai = model_Data.LoaiSP.ToList();
            Sanpham SP = new Sanpham();
            SP.MaSP = textBox_MaSP.Text;
            SP.TenSP = textBox_TenSP.Text;
            SP.Ngaynhap = (DateTime)dateTimePicker_NgayNhap.Value;
            SP.LoaiSP = ListLoai[comboBox_LoaiSP.SelectedIndex];
            model_Data.Sanpham.Add(SP);
            model_Data.SaveChanges();
        }

        private void comboBox_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button_Sua_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox_MaSP.Text)
                {
                    dataGridView1.Rows[i].Cells[1].Value = textBox_TenSP.Text;
                    dataGridView1.Rows[i].Cells[2].Value = dateTimePicker_NgayNhap.Value;
                }
            }
            Model_Data model_Data = new Model_Data();
            var sanpham = model_Data.Sanpham.FirstOrDefault(sp => sp.MaSP == textBox_MaSP.Text);
            if (sanpham != null)
            {
                // Update properties
                sanpham.TenSP = textBox_TenSP.Text;
                sanpham.Ngaynhap = (DateTime)dateTimePicker_NgayNhap.Value;

                // Save changes to the database
                model_Data.SaveChanges();
            }
            else
            {
                Console.WriteLine("Sanpham not found.");
            }
        }

        int stt = -1;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) 
            {
                stt = e.RowIndex;
            }
        }

        private void button_Xoa_Click(object sender, EventArgs e)
        {
            Model_Data model_Data = new Model_Data();
            var sanpham = model_Data.Sanpham.FirstOrDefault(sp => sp.MaSP == textBox_MaSP.Text);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox_MaSP.Text)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }    
            }
            if (sanpham != null)
            {
                // Remove the entity from the context
                model_Data.Sanpham.Remove(sanpham);

                // Save changes to the database
                model_Data.SaveChanges();

                Console.WriteLine("Sanpham deleted successfully.");
            }
            else
            {
                Console.WriteLine("Sanpham not found.");
            }
        }

        private void button_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Tim_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

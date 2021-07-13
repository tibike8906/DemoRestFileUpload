using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileDataCommunication;
using RestUI.Common;

namespace RestUI
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFileName.Text))
            {
                CommonVoids.ErrorMsg("Nem adott meg fájl nevet!");
            }

            RestControl rest = new RestControl();
            FileResponse respone =  rest.GetData(tbFileName.Text);
            if (!string.IsNullOrWhiteSpace(respone.Message))
            {
                CommonVoids.ErrorMsg(respone.Message);
                return;
            }
            SaveFileDialog sfDialod = new SaveFileDialog();
            sfDialod.FileName = respone.File.FirstOrDefault().FileName;
            sfDialod.Title = "Fájl mentése";
            sfDialod.ShowDialog();

            if (string.IsNullOrWhiteSpace(sfDialod.FileName)) return;

            using (FileStream fs = File.Create(sfDialod.FileName))
            {
                byte[] data = new UTF8Encoding(true).GetBytes(respone.File.FirstOrDefault().FileData);
                fs.Write(data, 0, data.Length);
            }            
        }
    }
}

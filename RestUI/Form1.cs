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

            FileControl fileControl = new FileControl();
            fileControl.SaveFiles(respone);
            tbFileName.Text = string.Empty;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RestControl rest = new RestControl();
            FileResponse respone = rest.GetData();
            if (!string.IsNullOrWhiteSpace(respone.Message))
            {
                CommonVoids.ErrorMsg(respone.Message);
                return;
            }
            FileControl fileControl = new FileControl();
            fileControl.SaveFiles(respone);
         
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FileControl fileControl = new FileControl();

            FileDescription fileDescription = fileControl.OpenFile();
            if (fileDescription == null) return;
            RestControl rest = new RestControl();
            string response = rest.SendFile(fileDescription);
            if(response != null)
                MessageBox.Show(response, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}

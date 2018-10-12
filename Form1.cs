using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPClases_ABC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          //  By Arturo Zambrano Vera - TIC
          
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            
            Double RangoIp = Convert.ToDouble(txtIp1.Text);
            //************************  PARA LA RED CLASE A **********************************
            if (RangoIp >= 0 && 128 > RangoIp)
            {
                txtMascara.Text = "255.0.0.0";
                txtClase.Text = "A";
                CargandoSubred();
            }
            //************************  PARA LA RED CLASE B **********************************
            else if (RangoIp >= 128 && 192 > RangoIp)
            {
                txtMascara.Text = "255.255.0.0";
                txtClase.Text = "B";
                CargandoSubred();
            }
            //************************  PARA LA RED CLASE C **********************************
            else if (RangoIp >= 192 && 224 > RangoIp)
            {
                txtMascara.Text = "255.255.255.0";
                txtClase.Text = "C";
                CargandoSubred();
            }
            else if (RangoIp >= 224)
            {
                MessageBox.Show("Numero Fuera de Rango de Ip"," Ip de red",MessageBoxButtons.OKCancel);
                txtIp1.Text = "";
                txtIp2.Text = "";
                txtIp3.Text = "";
                txtIp4.Text = "";
                txtNumeroSubred.Text = "";
            }
        }

        //************************** CARGANDO LAS SUBREDES EN LA TABLA ****************************
        public void CargandoSubred()
        {
            //***** numero de subred total: 0,2,6,14,30,62 --> (2^i )- 2 = resultado
            Double NumSubredTotal = 0;
            Double CantSubred = 0;
            Double NumSubred = 0;
            int SubredIngresada = Convert.ToInt32(txtNumeroSubred.Text);
            for (int i = 1; i < 9; i++)
            {
                //******************TOTAL DE SUBRED *****************
                NumSubredTotal = (Math.Pow(2, i) - 2);
                if (SubredIngresada <= NumSubredTotal)
                {
                    //************* cantidad de bits utilizados *******************
                    CantSubred = Math.Pow(2, 8 - i);
                    NumSubred = Math.Pow(2, i);

                    listarId_Red(CantSubred, NumSubred);
                    listarId_Broadcast(NumSubred, CantSubred);
                    listarIp_Configurable(NumSubred, CantSubred);
                    pintarSubred(SubredIngresada);
                    break;
                }
            }
        }

        //************************** LISTANDO LOS DATOS DE LA ID_RED ****************************

        Double conthost1;
        Double conthost2;
        Double conthost3;
        String ipRed = "";
        public void listarId_Red(Double CatSubred, Double NumSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;
            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA RED CLASE A *************************
                if (txtClase.Text.Equals("A"))
                {
                    ipRed = txtIp1.Text + "." + Convert.ToString(conthost1) + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3);
                    conthost1 = conthost1 + CatSubred;
                }
                //************************PARA LA RED CLASE B *************************
                else if (txtClase.Text.Equals("B"))
                {
                    ipRed = txtIp1.Text + "." + txtIp2.Text + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3);
                    conthost2 = conthost2 + CatSubred;
                }
                //************************PARA LA RED CLASE C *************************
                else if (txtClase.Text.Equals("C"))
                {
                    ipRed = txtIp1.Text + "." + txtIp2.Text + "." + txtIp3.Text + "." + Convert.ToString(conthost3);
                    conthost3 = conthost3 + CatSubred;
                }
                
                dgvtablaSubred.Rows.Add();
                dgvtablaSubred.Rows[i].Cells[0].Value = i;
                dgvtablaSubred.Rows[i].Cells[1].Value = ipRed;
                
            }
        }

        ////************************** LISTANDO LOS DATOS DE LA RANGO_IP_CONFIGURABLE ****************************

        String ipRed1 = "";
        String ipRed2 = "";
        public void listarIp_Configurable(Double NumSubred, Double CatSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;

            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA RED CLASE A *************************
                if (txtClase.Text.Equals("A"))
                {
                    ipRed1 = txtIp1.Text + "." + Convert.ToString(conthost1) + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3 + 1);
                    conthost1 = conthost1 + CatSubred;
                    ipRed2 = txtIp1.Text + "." + Convert.ToString(conthost1 - 1) + "." + Convert.ToString(conthost2 + 255) + "." + Convert.ToString((conthost3 + 255) - 1);
                }
                //************************PARA LA RED CLASE B *************************
                else if (txtClase.Text.Equals("B"))
                {
                    ipRed1 = txtIp1.Text + "." + txtIp2.Text + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3 + 1);
                    conthost2 = conthost2 + CatSubred;
                    //contultimo = contultimo + CatSubred;
                    ipRed2 = txtIp1.Text + "." + txtIp2.Text + "." + Convert.ToString(conthost2 - 2) + "." + Convert.ToString((conthost3 + 255) - 1);
                }
                //************************PARA LA RED CLASE C *************************
                else if (txtClase.Text.Equals("C"))
                {
                    ipRed1 = txtIp1.Text + "." + txtIp2.Text + "." + txtIp3.Text + "." + Convert.ToString(conthost3 + 1);
                    conthost3 = conthost3 + CatSubred;
                    ipRed2 = txtIp1.Text + "." + txtIp2.Text + "." + txtIp3.Text + "." + Convert.ToString(conthost3 - 2);
                }
                
                String ipRangoRed = ipRed1 + " -- " + ipRed2;
                dgvtablaSubred.Rows.Add();
                dgvtablaSubred.Rows[i].Cells[2].Value = ipRangoRed;

            }

        }
            
        //************************** LISTANDO LOS DATOS DE LA ID_BROADCAST ****************************

        public void listarId_Broadcast(Double NumSubred,Double CatSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;
            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA RED CLASE A *************************
                if (txtClase.Text.Equals("A"))
                {
                    conthost1 = conthost1 + CatSubred;
                    ipRed = txtIp1.Text + "." + Convert.ToString(conthost1 - 1) + "." + Convert.ToString(conthost3 + 255) + "." + Convert.ToString(conthost3 + 255);
                }
                //************************PARA LA RED CLASE B *************************
                else if (txtClase.Text.Equals("B"))
                {
                    conthost2 = conthost2 + CatSubred;
                    ipRed = txtIp1.Text + "." + txtIp2.Text + "." + Convert.ToString(conthost2 - 1) + "." + Convert.ToString(conthost3 + 255);
                }
                //************************PARA LA RED CLASE C *************************
                else if (txtClase.Text.Equals("C"))
                {
                    conthost3 = conthost3 + CatSubred;
                    ipRed = txtIp1.Text + "." + txtIp2.Text + "." + txtIp3.Text + "." + Convert.ToString(conthost3 - 1);
                }
                dgvtablaSubred.Rows.Add();
                dgvtablaSubred.Rows[i].Cells[3].Value = ipRed;
                
            }
        }

        //************************** PINTANDO LA CANTIDAD DE SUBREDES DE LA TABLA ****************************

        public void pintarSubred(double subred)
        {
            for (int i = 0; i < subred; i++)
            {
               // dgvtablaSubred.Rows[i + 1].DefaultCellStyle.Equals(BackColor = Color.Red);
                dgvtablaSubred.Rows[i + 1].DefaultCellStyle.BackColor = Color.Orange;
                //dgvtablaSubred.RowsDefaultCellStyle.BackColor = Color.Blue;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgvtablaSubred_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

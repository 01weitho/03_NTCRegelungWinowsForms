using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_NTCRegelungWinowsForms
{
    public partial class Form1 : Form
    {
        public delegate void addDelegate(string mystring);
        public delegate void addDelegate2();
        addDelegate Delegate_aktTemperatur;
        addDelegate2 Delegate_Hysterese;
        addDelegate2 Delegate_Sollwert;
        addDelegate Delegate_AnfSollwert;
        addDelegate Delegate_AnfHysterese;
        bool anfangswertEingelesen = false, anfHysterese = false, anfSollwert = false;

        public Form1()
        {
            InitializeComponent();

            Delegate_aktTemperatur = new addDelegate(_aktTemperatur);
            Delegate_Hysterese = new addDelegate2(_hystereseAendern);
            Delegate_Sollwert = new addDelegate2(_sollwertAendern);
            Delegate_AnfHysterese = new addDelegate(_anfHysterese);
            Delegate_AnfSollwert = new addDelegate(_anfSollwert);
            cb_aendernItems();
        }


        #region Ereignisprozeduren

        #region COM auswaehlen
        /// <summary>
        /// Wenn die Combobox cb_Ports zum aktiv wird werden alle aktiven 
        /// Comschnitstellen in die Combobox geschrieben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Ports_Enter(object sender, EventArgs e)
        {
            cb_Ports.Items.Clear();
            string[] PortNames = SerialPort.GetPortNames();
            cb_Ports.Items.AddRange(PortNames);
        }

        /// <summary>
        /// Wenn der btn gklickt wird, wird der SerialPort mit der ausgewähleten Schnitstelle
        /// aus der Combobox geöffnet/geschlossen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Ports_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                btn_Ports.BackColor = Color.Red;
                btn_Ports.Text = "OPEN";
            }
            else
            {
                serialPort1.PortName = cb_Ports.SelectedItem.ToString();
                serialPort1.Open();
                btn_Ports.BackColor = Color.Green;
                btn_Ports.Text = "CLOSE";

                _werteAbfragen();
            }
        }

        #endregion

        #region Data Recieved
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incommingData = string.Empty;
            incommingData = serialPort1.ReadExisting();
            if (incommingData == "Sollwert")
            {
                lbl_aktSollwert.Invoke(Delegate_Sollwert);
            }
            else
            {
                if (incommingData == "Hysterese")
                {
                    lbl_aktHysterese.Invoke(Delegate_Hysterese);
                }
                else
                {
                    if (incommingData[0] == 's')
                    {
                        incommingData = incommingData.Substring(1);
                        lbl_aktSollwert.Invoke(Delegate_AnfSollwert, new Object[] { incommingData });
                        _werteAbfragen();
                    }
                    else
                    {
                        if (incommingData[0] == 'h')
                        {
                            incommingData = incommingData.Substring(1);
                            lbl_aktHysterese.Invoke(Delegate_AnfHysterese, new Object[] { incommingData });
                            _werteAbfragen();
                        }
                       else
                        {
                            lbl_aktTemp.Invoke(this.Delegate_aktTemperatur, new Object[] { incommingData });
                        }
                    }
                }
            }

        }

        #endregion

        #region Btn Werte aendern
        private void btn_aendernWerte_Click(object sender, EventArgs e)
        {
            string selected = cb_aendernWerte.SelectedItem.ToString();
            switch (selected)
            {
                case "Hysterese":
                    serialPort1.Write("HystereseC");
                    break;

                case "Sollwert":
                    serialPort1.Write("SollwertC");
                    break;
            }
        }
        #endregion

        #endregion

        #region Tools

        #region aktuelle Temperatur
        /// <summary>
        /// Methode wird durch den Delegaten aufgerufen und schreibt die aktuelle Tempertur
        /// in das Label.
        /// </summary>
        /// <param name="mystring"></param>
        private void _aktTemperatur(string mystring)
        {
            lbl_aktTemp.Text = mystring;
        }
        #endregion

        #region Sollwert aendern
        /// <summary>
        /// Der neue Sollwert wird dem Arduino mitgeteil und auf der Form1
        /// im entsprechenden Label angezeigt.
        /// </summary>
        private void _sollwertAendern()
        {
            string neuerSollwert = txb_aendern.Text;
            lbl_aktSollwert.Text = neuerSollwert;

            serialPort1.Write(neuerSollwert + "C");
        }
        #endregion

        #region Hysterese aendern
        /// <summary>
        /// Die neue Hysterese wird dem Arduino mitgeteil und auf der Form1
        /// im entsprechenden Label angezeigt.
        /// </summary>
        private void _hystereseAendern()
        {
            string neueHysterese = txb_aendern.Text;
            lbl_aktHysterese.Text = neueHysterese;

            serialPort1.Write(neueHysterese + "C");
        }
        #endregion

        #region Anfangswerte
        private void _werteAbfragen()
        {
            if (!anfangswertEingelesen)
            {
                if (!anfHysterese)
                {
                    serialPort1.Write("hystereseC");
                    anfHysterese = true;
                }
                else
                {
                    if (!anfSollwert)
                    {
                        serialPort1.Write("sollwertC");
                        anfSollwert = true;
                        anfangswertEingelesen = true;
                    }
                    else
                    {
                        anfangswertEingelesen = anfHysterese = anfSollwert = false;
                    }
                }
            }

        }

        private void _anfHysterese(string Wert)
        {
            lbl_aktHysterese.Text = Wert;
        }

        private void _anfSollwert(string Wert)
        {
            lbl_aktSollwert.Text = Wert;
        }
        #endregion

        #region cb_Ändern
        private void cb_aendernItems()
        {
            cb_aendernWerte.Items.Clear();
            string[] items = new string[2];
            items[0] = "Hysterese";
            items[1] = "Sollwert";
            cb_aendernWerte.Items.AddRange(items);

        }
        #endregion

        #endregion
    }
}

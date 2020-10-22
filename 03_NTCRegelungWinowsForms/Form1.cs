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
/* Thomas Weithaler
 * Gufler Jonas
 * 5BEL
 * 22.10.2020
 */

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
        addDelegate Delegate_Recieved;
        addDelegate Delegate_Sent;
        bool anfangswertEingelesen = false, anfHysterese = false, anfSollwert = false;
        //Variablen für das Koordinatensystem:
        Bitmap bmp;
        Graphics g;
        Pen Raster = new Pen(Color.Gray, 1);
        Pen Achsen = new Pen(Color.Black, 2);
        Pen Funktion = new Pen(Color.Red, 3);
        public Int64 Koordinate_x = 0;
        public int Abstand;
        public float lastVoltIn = 0;
        public const int AnfangWerteX = 10;
        public bool firstSizeChanged = false;
        public const int MaxWert = 200; //Diese Konstante beeinflusst wie sehr dei Kennlinie gestreckt wird wenn sie gleich 100 ist so
                                        //entschpricht das odere Ende der Pikturebox in der Y-Achse dem Wert 100.


        public Form1()
        {
            InitializeComponent();

            Delegate_aktTemperatur = new addDelegate(_aktTemperatur);
            Delegate_Hysterese = new addDelegate2(_hystereseAendern);
            Delegate_Sollwert = new addDelegate2(_sollwertAendern);
            Delegate_AnfHysterese = new addDelegate(_anfHysterese);
            Delegate_AnfSollwert = new addDelegate(_anfSollwert);
            Delegate_Recieved = new addDelegate(_recievedData);
            Delegate_Sent = new addDelegate(_lblSentData);
            cb_aendernItems();

            bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            g = Graphics.FromImage(bmp);

            //Zeichnet das Raster mit den gewünschten Eigenschaften
            Abstand = pb.ClientSize.Width / 100;
            Koordinate_x = AnfangWerteX - Abstand;
            Raster_zeichen(Abstand,Achsen,Raster);
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
            lbl_Recieved.Invoke(Delegate_Recieved, new Object[] { incommingData });
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

        #region SizeChanged
        private void pb_SizeChanged(object sender, EventArgs e)
        {
            if (firstSizeChanged)
            {
                Koordinate_x = AnfangWerteX;
                g.Clear(pb.BackColor);
                pb.Image = bmp;
                pb.Invalidate();
                Raster_zeichen(Abstand, Achsen, Raster);
            }
            else
            {
                firstSizeChanged = true;
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
            //Kennlinie zeichnen
            Funktion_zeichnen(mystring, Funktion, Abstand);
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
            neuerSollwert = neuerSollwert + "C";
            serialPort1.Write(neuerSollwert);
            lbl_sent.Invoke(Delegate_Sent, new Object[] { neuerSollwert });
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
            neueHysterese = neueHysterese + "C";
            serialPort1.Write(neueHysterese);
            lbl_sent.Invoke(Delegate_Sent, new Object[] { neueHysterese });
        }
        #endregion

        #region RecievedData
        private void _recievedData(string mystring)
        {
            lbl_Recieved.Text = mystring;
        }
        #endregion

        #region Anfangswerte
        private void _werteAbfragen()
        {
            if (!anfangswertEingelesen)
            {
                if (!anfHysterese)
                {
                    string temp = "hystereseC";
                    serialPort1.Write(temp);
                    lbl_sent.Invoke(Delegate_Sent, new Object[] { temp });
                    anfHysterese = true;

                }
                else
                {
                    if (!anfSollwert)
                    {
                        string temp = "sollwertC";
                        serialPort1.Write(temp);
                        lbl_sent.Invoke(Delegate_Sent, new Object[] { temp }); anfSollwert = true;
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

        #region RasterZeichnen
        /// <summary>
        /// Zeichnet ein Raster in die Picturbox 
        /// </summary>
        /// <param name="AbstandRaster">Der Abstand zwischen den einzelnen Linien</param>
        /// <param name="Stift1">Der Stift mit  dem das Raster gezeichnet werden soll</param>
        private void Raster_zeichen(int AbstandRaster, Pen StiftBreit,Pen Stift1)
        {
            int hoehe = pb.ClientSize.Height;
            int breite = pb.ClientSize.Width;

            g.DrawString("0", this.Font, Brushes.Black, 0, hoehe - (AnfangWerteX + 1));
            g.DrawLine(StiftBreit, AnfangWerteX, 0, AnfangWerteX, hoehe);
            g.DrawLine(StiftBreit, 0, hoehe - AnfangWerteX, breite, hoehe - AnfangWerteX);

            for (int i = AnfangWerteX + AbstandRaster; i < breite; i = i + AbstandRaster)
            {
                g.DrawLine(Stift1, i, 0, i, hoehe-AnfangWerteX);
            }

            for (int i = hoehe - AnfangWerteX - AbstandRaster; i > 0; i = i - AbstandRaster)
            {
                g.DrawLine(Stift1, AnfangWerteX, i, breite, i);
            }

            pb.Image = bmp;
            pb.Invalidate();
        }
        #endregion

        #region KennlinieZeichnen
        /// <summary>
        /// Zeichnet den uebergebenen Wert in die Picturebox als Kennlinie ein
        /// </summary>
        /// <param name="string_in">Wert für die Kennlinie</param>
        /// <param name="Stift1">Stift mit welchenm die Linie gezeichnet werden soll</param>
        /// <param name="AbstandRaster">Der Abstand der zwischen Den einzelnen Punkten liegen soll.</param>
        private void Funktion_zeichnen(string string_in, Pen Stift1, int AbstandRaster)
        {
            int hoehe = pb.ClientSize.Height;
            int breite = pb.ClientSize.Width;

            float Volt_in = Convert.ToSingle(string_in);
            Volt_in = Volt_in * (hoehe / MaxWert);

            if (Koordinate_x == AnfangWerteX-Abstand)
            {
                lastVoltIn = Volt_in;
                Koordinate_x = Koordinate_x + AbstandRaster;
            }
            else
            {
                g.DrawLine(Stift1, Koordinate_x, hoehe - lastVoltIn, Koordinate_x + AbstandRaster, hoehe - Volt_in);
                lastVoltIn = Volt_in;

                if (Koordinate_x < breite - AbstandRaster)
                {
                    Koordinate_x = Koordinate_x + Abstand;
                }
                else
                {
                    Koordinate_x = AnfangWerteX;
                    g.Clear(pb.BackColor);
                    Raster_zeichen(Abstand, Achsen, Raster); ;
                }
            }
            pb.Image = bmp;
            pb.Invalidate();

        }
        #endregion

        #region Label SentData beschreiben
        private void _lblSentData(string mystring)
        {
            lbl_sent.Text = mystring;
        }

        #endregion

        #endregion
    }
}

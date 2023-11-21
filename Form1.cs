using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using GeneralUtility;
using MySql.Data.MySqlClient;
using System.Web;
using System.IO;


namespace View
{
    public partial class Form1 : Form
    {
        //設定資料庫
        private MySqlConnection _connection;
        private string _dbHost = "127.0.0.1";
        private string _dbPort = "3306";
        private string _dbUserName = "root";
        private string _dbPassword = "";
        private string _dbName = "invent";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //開啟資料庫
            string connStr = string.Format("server={0}; port={1}; uid={2}; pwd={3}; database={4}; charset=utf8;", _dbHost, _dbPort, _dbUserName, _dbPassword, _dbName);
            _connection = new MySqlConnection(connStr);
            _connection.Open();
            LoadPalletName(null,null);
            Plabel1.Text = Plabel2.Text = Plabel3.Text = Plabel4.Text = Plabel5.Text = Plabel6.Text = Plabel7.Text = Plabel8.Text 
                = Plabel9.Text = Plabel10.Text = Plabel11.Text = Plabel12.Text = Plabel13.Text = Plabel14.Text = Plabel15.Text = Plabel16.Text = "";
            Button1_Click(null,null);
        }

        private void Interbutton_Click(object sender, EventArgs e)
        {
            string strpallet = palletsettextBox.Text;
            string strkey = "0";

            if (PalletradioButton1.Checked == true)
            {
                PalletradioButton1.Text = palletsettextBox.Text;
                labelp01.Text = palletsettextBox.Text;
                strkey = "1";
            }
            else if (PalletradioButton2.Checked == true)
            {
                PalletradioButton2.Text = palletsettextBox.Text;
                labelp02.Text = palletsettextBox.Text;
                strkey = "2";
            }
            else if (PalletradioButton3.Checked == true)
            {
                PalletradioButton3.Text = palletsettextBox.Text;
                labelp03.Text = palletsettextBox.Text;
                strkey = "3";
            }
            else if (PalletradioButton4.Checked == true)
            {
                PalletradioButton4.Text = palletsettextBox.Text;
                labelp04.Text = palletsettextBox.Text;
                strkey = "4";
            }
            else if (PalletradioButton5.Checked == true)
            {
                PalletradioButton5.Text = palletsettextBox.Text;
                labelp05.Text = palletsettextBox.Text;
                strkey = "5";
            }
            else if (PalletradioButton6.Checked == true)
            {
                PalletradioButton6.Text = palletsettextBox.Text;
                labelp06.Text = palletsettextBox.Text;
                strkey = "6";
            }
            else if (PalletradioButton7.Checked == true)
            {
                PalletradioButton7.Text = palletsettextBox.Text;
                labelp07.Text = palletsettextBox.Text;
                strkey = "7";
            }
            else if (PalletradioButton8.Checked == true)
            {
                PalletradioButton8.Text = palletsettextBox.Text;
                labelp08.Text = palletsettextBox.Text;
                strkey = "8";
            }
            else if (PalletradioButton9.Checked == true)
            {
                PalletradioButton9.Text = palletsettextBox.Text;
                labelp09.Text = palletsettextBox.Text;
                strkey = "9";
            }
            else if (PalletradioButton10.Checked == true)
            {
                PalletradioButton10.Text = palletsettextBox.Text;
                labelp10.Text = palletsettextBox.Text;
                strkey = "10";
            }
            else if (PalletradioButton11.Checked == true)
            {
                PalletradioButton11.Text = palletsettextBox.Text;
                labelp11.Text = palletsettextBox.Text;
                strkey = "11";
            }
            else if (PalletradioButton12.Checked == true)
            {
                PalletradioButton12.Text = palletsettextBox.Text;
                labelp12.Text = palletsettextBox.Text;
                strkey = "12";
            }
            else if (PalletradioButton13.Checked == true)
            {
                PalletradioButton13.Text = palletsettextBox.Text;
                labelp13.Text = palletsettextBox.Text;
                strkey = "13";
            }
            else if (PalletradioButton14.Checked == true)
            {
                PalletradioButton14.Text = palletsettextBox.Text;
                labelp14.Text = palletsettextBox.Text;
                strkey = "14";
            }
            else if (PalletradioButton15.Checked == true)
            {
                PalletradioButton15.Text = palletsettextBox.Text;
                labelp15.Text = palletsettextBox.Text;
                strkey = "15";
            }
            else if (PalletradioButton16.Checked == true)
            {
                PalletradioButton16.Text = palletsettextBox.Text;
                labelp16.Text = palletsettextBox.Text;
                strkey = "16";
            }

            if (_connection.Ping())
            {
                MySqlCommand cmd = _connection.CreateCommand();

                int icount = GetSQLResultPallet(_connection, strkey);

                if (icount > 0)
                {
                    cmd.CommandText = string.Format("UPDATE pallets SET palletname = '{0}' WHERE palletno = {1};", strpallet,strkey);

                }
                else
                {
                    cmd.CommandText = string.Format("INSERT INTO pallets ( palletname, palletno) VALUES('{0}',{1});", strpallet, strkey);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("資料庫連線異常，請重新嘗試。", "資料庫異常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            palletsettextBox.Text = "";
        }

        private void PalletsettextBox_KeyDown(object sender, KeyEventArgs e)
        {   
            if (e.KeyCode == Keys.Enter)
                Interbutton_Click(null, null);
        }

        private void LoadPalletName(object sender, KeyEventArgs e)
        {
            string[] strpalletoutput = new string[16];
            for (int i = 0; i < 16; i++)
            {
                if (_connection.Ping())
                {
                    MySqlCommand cmd = _connection.CreateCommand();
                    int icount = GetSQLResultPallet(_connection, Convert.ToString(i+1));
                    if (icount <= 0)
                        continue;
                    else if (icount > 0)
                    {
                        cmd.CommandText = string.Format("SELECT palletname FROM pallets WHERE palletno = {0};", Convert.ToString(i+1));
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            strpalletoutput[i] = reader["palletname"].ToString();
                        }

                        // 關閉讀取資料庫資料的元件
                       reader.Close();
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("資料庫連線異常，請重新嘗試。", "資料庫異常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            labelp01.Text = strpalletoutput[0];
            labelp02.Text = strpalletoutput[1];
            labelp03.Text = strpalletoutput[2];
            labelp04.Text = strpalletoutput[3];
            labelp05.Text = strpalletoutput[4];
            labelp06.Text = strpalletoutput[5];
            labelp07.Text = strpalletoutput[6];
            labelp08.Text = strpalletoutput[7];
            labelp09.Text = strpalletoutput[8];
            labelp10.Text = strpalletoutput[9];
            labelp11.Text = strpalletoutput[10];
            labelp12.Text = strpalletoutput[11];
            labelp13.Text = strpalletoutput[12];
            labelp14.Text = strpalletoutput[13];
            labelp15.Text = strpalletoutput[14];
            labelp16.Text = strpalletoutput[15];

            PalletradioButton1.Text = strpalletoutput[0];
            PalletradioButton2.Text = strpalletoutput[1];
            PalletradioButton3.Text = strpalletoutput[2];
            PalletradioButton4.Text = strpalletoutput[3];
            PalletradioButton5.Text = strpalletoutput[4];
            PalletradioButton6.Text = strpalletoutput[5];
            PalletradioButton7.Text = strpalletoutput[6];
            PalletradioButton8.Text = strpalletoutput[7];
            PalletradioButton9.Text = strpalletoutput[8];
            PalletradioButton10.Text = strpalletoutput[9];
            PalletradioButton11.Text = strpalletoutput[10];
            PalletradioButton12.Text = strpalletoutput[11];
            PalletradioButton13.Text = strpalletoutput[12];
            PalletradioButton14.Text = strpalletoutput[13];
            PalletradioButton15.Text = strpalletoutput[14];
            PalletradioButton16.Text = strpalletoutput[15];
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            string[] strpalletnumber = new string[16];
            strpalletnumber[0] = PalletradioButton1.Text;
            strpalletnumber[1] = PalletradioButton2.Text;
            strpalletnumber[2] = PalletradioButton3.Text;
            strpalletnumber[3] = PalletradioButton4.Text;
            strpalletnumber[4] = PalletradioButton5.Text;
            strpalletnumber[5] = PalletradioButton6.Text;
            strpalletnumber[6] = PalletradioButton7.Text;
            strpalletnumber[7] = PalletradioButton8.Text;
            strpalletnumber[8] = PalletradioButton9.Text;
            strpalletnumber[9] = PalletradioButton10.Text;
            strpalletnumber[10] = PalletradioButton11.Text;
            strpalletnumber[11] = PalletradioButton12.Text;
            strpalletnumber[12] = PalletradioButton13.Text;
            strpalletnumber[13] = PalletradioButton14.Text;
            strpalletnumber[14] = PalletradioButton15.Text;
            strpalletnumber[15] = PalletradioButton16.Text;

            string[] strpalletoutput = new string[16];
            strpalletoutput[0] = Plabel1.Text;
            strpalletoutput[1] = Plabel2.Text;
            strpalletoutput[2] = Plabel3.Text;
            strpalletoutput[3] = Plabel4.Text;
            strpalletoutput[4] = Plabel5.Text;
            strpalletoutput[5] = Plabel6.Text;
            strpalletoutput[6] = Plabel7.Text;
            strpalletoutput[7] = Plabel8.Text;
            strpalletoutput[8] = Plabel9.Text;
            strpalletoutput[9] = Plabel10.Text;
            strpalletoutput[10] = Plabel11.Text;
            strpalletoutput[11] = Plabel12.Text;
            strpalletoutput[12] = Plabel13.Text;
            strpalletoutput[13] = Plabel14.Text;
            strpalletoutput[14] = Plabel15.Text;
            strpalletoutput[15] = Plabel16.Text;

            DataTable dt_buffer = new DataTable();

            DataGridView[] PalletdataGrid = new DataGridView[16];
            PalletdataGrid[0] = PalletdataGridView1;
            PalletdataGrid[1] = PalletdataGridView2;
            PalletdataGrid[2] = PalletdataGridView3;
            PalletdataGrid[3] = PalletdataGridView4;
            PalletdataGrid[4] = PalletdataGridView5;
            PalletdataGrid[5] = PalletdataGridView6;
            PalletdataGrid[6] = PalletdataGridView7;
            PalletdataGrid[7] = PalletdataGridView8;
            PalletdataGrid[8] = PalletdataGridView9;
            PalletdataGrid[9] = PalletdataGridView10;
            PalletdataGrid[10] = PalletdataGridView11;
            PalletdataGrid[11] = PalletdataGridView12;
            PalletdataGrid[12] = PalletdataGridView13;
            PalletdataGrid[13] = PalletdataGridView14;
            PalletdataGrid[14] = PalletdataGridView15;
            PalletdataGrid[15] = PalletdataGridView16;

            for (int j = 0; j < 16; j++)
            {
                PalletdataGrid[j].Rows.Clear();
                strpalletoutput[j] = "";
                if (!_connection.Ping()) _connection.Open();
                if (_connection.Ping())
                {
                    string strCommand = string.Format("select distinct productid from items where palletid ='{0}' and date_out is null", strpalletnumber[j]);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(strCommand, _connection);
                    adapter.Fill(dt_buffer);
                }

                if (dt_buffer.Rows.Count == 0)
                    continue;
                else if (dt_buffer.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_buffer.Rows.Count; i++)
                    {
                        strpalletoutput[j] += (i + 1) + ": " + GetSQLResultCountReason(_connection, strpalletnumber[j], dt_buffer.Rows[i][0].ToString()) + "\n";
                    }
                }
                LoadDataTableToProduct(dt_buffer, PalletdataGrid[j]);

                Plabel1.Text = strpalletoutput[0];
                Plabel2.Text = strpalletoutput[1];
                Plabel3.Text = strpalletoutput[2];
                Plabel4.Text = strpalletoutput[3];
                Plabel5.Text = strpalletoutput[4];
                Plabel6.Text = strpalletoutput[5];
                Plabel7.Text = strpalletoutput[6];
                Plabel8.Text = strpalletoutput[7];
                Plabel9.Text = strpalletoutput[8];
                Plabel10.Text = strpalletoutput[9];
                Plabel11.Text = strpalletoutput[10];
                Plabel12.Text = strpalletoutput[11];
                Plabel13.Text = strpalletoutput[12];
                Plabel14.Text = strpalletoutput[13];
                Plabel15.Text = strpalletoutput[14];
                Plabel16.Text = strpalletoutput[15];
                dt_buffer.Clear();
            }
        }

        private int GetSQLResultCountReason(MySqlConnection conn, string strKey, string strVal)
        {
            int ireturn = 0;
            if (!conn.Ping()) conn.Open();
            if (conn.Ping())
            {
                string strcommand = string.Format("select count(*) from items where palletid = '{0}' and productid = '{1}' and date_out is null", strKey, strVal);
                MySqlCommand cmd = new MySqlCommand(strcommand, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        if (reader.IsDBNull(i)) continue;
                        switch (reader.GetName(i))
                        {
                            case "count(*)":
                                ireturn = reader.GetInt32(i);
                                break;
                        }
                    }
                }
                reader.Close();
            }
            return ireturn;
        }

        private int GetSQLResultPallet(MySqlConnection conn, string strVal)
        {
            int ireturn = 0;
            if (!conn.Ping()) conn.Open();
            if (conn.Ping())
            {
                string strcommand = string.Format("select count(*) from pallets where palletno = {0} ", strVal);
                MySqlCommand cmd = new MySqlCommand(strcommand, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        if (reader.IsDBNull(i)) continue;
                        switch (reader.GetName(i))
                        {
                            case "count(*)":
                                ireturn = reader.GetInt32(i);
                                break;
                        }
                    }
                }
                reader.Close();
            }
            return ireturn;
        }

        private void LoadDataTableToProduct(DataTable dt, DataGridView dataGridViewA)
        {
            dataGridViewA.Rows.Clear();
            if (dt == null || dt.Rows.Count == 0) return;
            foreach (DataRow dr in dt.Rows)
            {
                int irowindex = dataGridViewA.Rows.Add(dr);
                foreach (DataGridViewColumn col in dataGridViewA.Columns)
                {
                    switch (col.Name)
                    {
                        case "product1":
                            dataGridViewA.Rows[irowindex].Cells["product1"].Value = dr["productid"].ToString();
                            break;
                        case "product2":
                            dataGridViewA.Rows[irowindex].Cells["product2"].Value = dr["productid"].ToString();
                            break;
                        case "product3":
                            dataGridViewA.Rows[irowindex].Cells["product3"].Value = dr["productid"].ToString();
                            break;
                        case "product4":
                            dataGridViewA.Rows[irowindex].Cells["product4"].Value = dr["productid"].ToString();
                            break;
                        case "product5":
                            dataGridViewA.Rows[irowindex].Cells["product5"].Value = dr["productid"].ToString();
                            break;
                        case "product6":
                            dataGridViewA.Rows[irowindex].Cells["product6"].Value = dr["productid"].ToString();
                            break;
                        case "product7":
                            dataGridViewA.Rows[irowindex].Cells["product7"].Value = dr["productid"].ToString();
                            break;
                        case "product8":
                            dataGridViewA.Rows[irowindex].Cells["product8"].Value = dr["productid"].ToString();
                            break;
                        case "product9":
                            dataGridViewA.Rows[irowindex].Cells["product9"].Value = dr["productid"].ToString();
                            break;
                        case "product10":
                            dataGridViewA.Rows[irowindex].Cells["product10"].Value = dr["productid"].ToString();
                            break;
                        case "product11":
                            dataGridViewA.Rows[irowindex].Cells["product11"].Value = dr["productid"].ToString();
                            break;
                        case "product12":
                            dataGridViewA.Rows[irowindex].Cells["product12"].Value = dr["productid"].ToString();
                            break;
                        case "product13":
                            dataGridViewA.Rows[irowindex].Cells["product13"].Value = dr["productid"].ToString();
                            break;
                        case "product14":
                            dataGridViewA.Rows[irowindex].Cells["product14"].Value = dr["productid"].ToString();
                            break;
                        case "product15":
                            dataGridViewA.Rows[irowindex].Cells["product15"].Value = dr["productid"].ToString();
                            break;
                        case "product16":
                            dataGridViewA.Rows[irowindex].Cells["product16"].Value = dr["productid"].ToString();
                            break;
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //System.Timers.Timer timer1 = new System.Timers.Timer();
            int value = Convert.ToInt32(StextBox.Text.ToString());
            timer1.Interval = 1000 * value;    //間隔1分執行
            timer1.Enabled = true;
            timer1.Start();
            //Console.ReadKey();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Button11_Click(null, null);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            //timer1.Enabled = false;
        }

        private void PalletradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(PalletradioButton1.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton2.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton3.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton4.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton5.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton6.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton7.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton8.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton9.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton10.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton11.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton12.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton13.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton14.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton15.Checked == true)
                palletsettextBox.Focus();
            else if (PalletradioButton16.Checked == true)
                palletsettextBox.Focus();
        }
    }
}
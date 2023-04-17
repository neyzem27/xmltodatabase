using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.IO;

namespace XMLtoSQLserver
{
    public partial class Form1 : Form
    {

        string strbarkod;
        string strbarkod2;
        string fiyat_ham;
        string resim_url;
        int countData=0;
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();

        public Form1()
        {
            InitializeComponent();
            //C:\Users\ken\Documents\Visual Studio 2010\Projects\XMLtoSQLserver\XMLtoSQLserver\books.xml
            // Create an XmlReader  C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver
            //using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))

        }
        public DataTable CreateDataTableXML(string XmlFile,string Firma,XmlDocument formatdoc)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(XmlFile);

            DataTable Dt = new DataTable();

            try
            {   //xml yapısı degisiyor dosya içinde bu yüzden format diye xml oluşturuldu.
                Dt.TableName = GetTableName(XmlFile,Firma);
                XmlNode NodoEstructura = formatdoc.DocumentElement.ChildNodes.Cast<XmlNode>().ToList()[0];
                progressBar1.Maximum = NodoEstructura.ChildNodes.Count;
                progressBar1.Value = 0;
                XmlNodeList lstVideos = NodoEstructura.ChildNodes;
                // Visit each node
               for (int i = 0; i < lstVideos.Count; i++)
                {
                    // Look for a node named CastMembers
                    if (lstVideos[i].ChildNodes.Count>1)
                    {
                        // Once/if you find it,
                        // 1. Access its first child
                        // 2. Create a list of its child nodes
                        XmlNodeList lstActors = lstVideos[i].ChildNodes;
                        // Display the values of the nodes
                        for (int j = 0; j < lstActors.Count; j++)
                            Dt.Columns.Add(lstActors[j].Name, typeof(String));
                    }
                    else
                    {
                        Dt.Columns.Add(lstVideos[i].Name, typeof(String));
                    }
                    Progress();
                }
                /* foreach (XmlNode columna in NodoEstructura.ChildNodes)
                  {

                      Dt.Columns.Add(columna.Name, typeof(String));
                      Progress();
                  }*/
                XmlNode Filas = doc.DocumentElement;
                progressBar1.Maximum = Filas.ChildNodes.Count;
                progressBar1.Value = 0;
                XmlNodeList lstVideoss = Filas.ChildNodes;
                // Visit each node
                for (int i = 0; i < lstVideoss.Count; i++)
                {
                    DataRow row2 = Dt.NewRow();
                    int k = 0;
                    // Look for a node named CastMembers
                    XmlNodeList lstActorss = lstVideoss[i].ChildNodes;
                    for (int o = 0; o < lstActorss.Count; o++)
                    {
                       
                        if (lstActorss[o].ChildNodes.Count > 1)
                        {
                            // Once/if you find it,
                            // 1. Access its first child
                            // 2. Create a list of its child nodes
                            XmlNodeList lstActorsss = lstActorss[o].ChildNodes;
                            // Display the values of the nodes
                            for (int j = 0; j < lstActorsss.Count; j++)
                            {
                                Console.WriteLine(lstActorsss[j].InnerText);
                                row2[k] = lstActorsss[j].InnerText;
                                k++;
                            }
                        }
                        else
                        {
                            Console.WriteLine(lstActorss[o].InnerText);
                            row2[k] = lstActorss[o].InnerText;
                            k++;
                        }
                    }
                   // Console.WriteLine(row2);
                    countData++;
                    Dt.Rows.Add(row2);
                    Progress();
                }
                /* XmlNode Filas = doc.DocumentElement;
                  progressBar1.Maximum = Filas.ChildNodes.Count;
                  progressBar1.Value = 0;
                  foreach (XmlNode Fila in Filas.ChildNodes)
                  {
                      List<string> Valores = Fila.ChildNodes.Cast<XmlNode>().ToList().Select(x => x.InnerText).ToList();
                      Dt.Rows.Add(Valores.ToArray());//Dt.Rows.Add nasıl kullanılıyor.incele
                     Progress();
                  }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablosuya ekleme işlemi başarısız  " + ex);
            }
            return Dt;
        }

        public string CreateTableQuery(DataTable table)
        {
            string sqlsc = "CREATE TABLE " + table.TableName + "(";
            progressBar1.Maximum = table.Columns.Count;
            progressBar1.Value = 0;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "[" + table.Columns[i].ColumnName + "]";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";

                Progress();
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }

        public string GetTableName(string file,string Firma)
        {
            FileInfo fi = new FileInfo(file);
            string TableName = "";
            if (Firma == "GUCLU")
                TableName = "xml_fiyat_guclu";//fi.Name.Replace(fi.Extension, "");
            else if(Firma == "KADIOGLU")
                TableName = "xml_fiyat_kadi";
            else if (Firma == "BIGPOINT")
                TableName = "xml_fiyat_bigpoint";

            return TableName;
        }

        public void Progress()
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
               
                Application.DoEvents();
            }
        }
        //GÜÇLÜ XML AKTARMA
        private void btnWriteToXML_Click(object sender, EventArgs e)
        {
            string XMlFile = txtFilePath.Text;
            System.Xml.XmlDocument myXmlDocument = new System.Xml.XmlDocument();
            myXmlDocument.Load(@"C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver\guclu_format.xml");
            if (File.Exists(XMlFile))
            {
                // Conversion Xml file to DataTable
                DataTable dt = CreateDataTableXML(XMlFile,"GUCLU", myXmlDocument);
                if (dt.Columns.Count == 0)
                    dt.ReadXml(XMlFile);

                // Creating Query for Table Creation
                string Query = CreateTableQuery(dt);
                SqlConnection con = new SqlConnection(@"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_001;User ID =sa;Password =Sa1234;Integrated Security=True");
                con.Open();

                // Deletion of Table if already Exist
                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('dbo." + dt.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dt.TableName + ";", con);
                
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("delete from [dbo].[xml_fiyat_guclu_hiz]", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("INSERT INTO [dbo].[xml_fiyat_guclu_hiz]  ([G_stokkodu],[barkod],[barkod2],[tarih],[fiyat],[resim_url]) SELECT STOKKODU, BIRIM1BARKOD1, BIRIM1BARKOD2, GETDATE(), CONVERT(float, IsNull(REPLACE(REPLACE(REPLACE(REPLACE(LISTEFIYAT, '(', '-'), ')', ''), ',', ''), '$', ''), 0)), RESIM1 FROM xml_fiyat_guclu", con);
                cmd3.ExecuteNonQuery();

                // Table Creation
                cmd = new SqlCommand(Query, con);
                int check = cmd.ExecuteNonQuery();
                if (check != 0)
                {
                    // Copy Data from DataTable to Sql Table
                    using (var bulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        foreach (DataColumn col in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = dt.TableName;
                        bulkCopy.WriteToServer(dt);
                    }

                    MessageBox.Show("Tablo oluşturuldu.Güçlü Aktarım yapıldı.Urun sayısı: "+ countData + " Table adı:" + dt.TableName);
                }
                con.Close();
            }

            /* İLK DENEME 
             int count = -1;
             conn.ConnectionString = @"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_001;User ID =sa;Password =Sa1234;Integrated Security=True";
             conn.Open();
             string queryStmt = "DELETE FROM xml_fiyat_guclu";
             SqlCommand _cmd1 = new SqlCommand(queryStmt, conn);
             _cmd1.ExecuteNonQuery();

             //C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver\xml_kadi.xml
             using (XmlReader reader = XmlReader.Create(txtFilePath.Text))
             {
                 while (reader.Read())
                 {
                     reader.ReadToFollowing("URL");
                     resim_url =reader.ReadInnerXml();
                     reader.ReadToFollowing("BIRIM1BARKOD2");
                     strbarkod2 = reader.ReadInnerXml();
                     reader.ReadToFollowing("BIRIM1BARKOD1");
                     strbarkod = reader.ReadInnerXml();  
                     reader.ReadToFollowing("LISTEFIYAT");
                     fiyat_ham = reader.ReadInnerXml().Replace('.',',');

                     try
                     {
                         //Output to database

                         queryStmt = "INSERT INTO xml_fiyat_guclu(barkod,barkod2,tarih,fiyat,resim_url)" +
                                             "VALUES(@barkod,@barkod2,@tarih,@fiyat,@resim_url)";

                         using (SqlCommand _cmd = new SqlCommand(queryStmt, conn))
                         {
                             _cmd.Parameters.Add("@barkod", SqlDbType.NVarChar, 20);
                             _cmd.Parameters.Add("@barkod2", SqlDbType.NVarChar, 20);
                             _cmd.Parameters.Add("@tarih", SqlDbType.DateTime, 50);
                             _cmd.Parameters.Add("@fiyat", SqlDbType.Float);
                             _cmd.Parameters.Add("@resim_url", SqlDbType.NVarChar, 127);
                             _cmd.Parameters["@barkod"].Value = strbarkod;
                             _cmd.Parameters["@barkod2"].Value = strbarkod2;
                             _cmd.Parameters["@tarih"].Value = DateTime.Now;
                             _cmd.Parameters["@fiyat"].Value = Convert.ToDouble(fiyat_ham);
                             _cmd.Parameters["@resim_url"].Value = resim_url;

                             _cmd.ExecuteNonQuery();
                         }

                     }// end try

                     catch (Exception ex)
                     {
                         //MessageBox.Show("xml_fiyat_kadi tablosuna ekleme işlemi başarısız"+ex);
                     }


                     count++;
                 }
             }

             conn.Close();
             lblDataWrittenToXML.Text = "Data yazma işlemi yapıldı.Kontrol ediniz.Girilen kayıt sayısı " + count;
             lblDataWrittenToXML.Visible = true;*/

        }
        //C:\Users\ken\Documents\Visual Studio 2010\Projects\XMLtoSQLserver\XMLtoSQLserver\books.xml
        // Create an XmlReader  C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver
        //using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
        // KADIOGLU Aktarım
        private void btnWriteXMLtoDatabase_Click(object sender, EventArgs e)
        {

            string XMlFile = txtFilePath.Text;
            System.Xml.XmlDocument myXmlDocument = new System.Xml.XmlDocument();
            myXmlDocument.Load(@"C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver\kadi_format.xml");
            if (File.Exists(XMlFile))
            {
                // Conversion Xml file to DataTable
                DataTable dt = CreateDataTableXML(XMlFile,"KADIOGLU", myXmlDocument);
                if (dt.Columns.Count == 0)
                    dt.ReadXml(XMlFile);

                // Creating Query for Table Creation
                string Query = CreateTableQuery(dt);
                SqlConnection con = new SqlConnection(@"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_001;User ID =sa;Password =Sa1234;Integrated Security=True");
                con.Open();

                // Deletion of Table if already Exist
                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('dbo." + dt.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dt.TableName + ";", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("delete from [dbo].[xml_fiyat_kadi_hiz]", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("INSERT INTO [dbo].[xml_fiyat_kadi_hiz] ([K_stokkodu],[barkod],[barkod2],[tarih],[fiyat],[resim_url]) SELECT ID, RTRIM(LTRIM(BIRIM_1_BARCODE)), BIRIM_2_BARCODE, GETDATE(), CONVERT(float, IsNull(REPLACE(REPLACE(REPLACE(REPLACE(FIYAT, '(', '-'), ')', ''), ',', '.'), '$', ''), 0)), RESIM1 FROM xml_fiyat_kadi where LEN(RTRIM(LTRIM(BIRIM_1_BARCODE))) < 30 and LEN(RTRIM(LTRIM(BIRIM_2_BARCODE))) < 30", con);
                cmd3.ExecuteNonQuery();


                // Table Creation
                cmd = new SqlCommand(Query, con);
                int check = cmd.ExecuteNonQuery();
                if (check != 0)
                {
                    // Copy Data from DataTable to Sql Table
                    using (var bulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        foreach (DataColumn col in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = dt.TableName;
                        bulkCopy.WriteToServer(dt);
                    }

                    MessageBox.Show("Tablo oluşturuldu.Kadıoğlu Aktarım yapıldı.Urun sayısı: "+countData+" Table adı:" + dt.TableName);
                }
                con.Close();
            }
            /*  
               ilk deneme
               int count = -1;
              conn.ConnectionString = @"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_001;User ID =sa;Password =Sa1234;Integrated Security=True";
              conn.Open();
              string queryStmt = "DELETE FROM xml_fiyat_kadi";
              SqlCommand _cmd1 = new SqlCommand(queryStmt, conn);
              _cmd1.ExecuteNonQuery();

              //C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver\xml_kadi.xml
              using (XmlReader reader = XmlReader.Create(txtFilePath.Text))
              {
                  while (reader.Read())
                  {
                      reader.ReadToFollowing("RESIM1");
                      resim_url = reader.ReadInnerXml();
                      reader.ReadToFollowing("FIYAT_HAM");
                      fiyat_ham = reader.ReadInnerXml();
                      reader.ReadToFollowing("BIRIM_1_BARCODE");
                      strbarkod = reader.ReadInnerXml();
                      reader.ReadToFollowing("BIRIM_2_BARCODE");
                      strbarkod2 = reader.ReadInnerXml();



                      try
                      {
                          //Output to database

                          queryStmt = "INSERT INTO xml_fiyat_kadi(barkod,barkod2,tarih,fiyat,resim_url)" +
                                              "VALUES(@barkod,@barkod2,@tarih,@fiyat,@resim_url)";

                          using (SqlCommand _cmd = new SqlCommand(queryStmt, conn))
                          {
                              _cmd.Parameters.Add("@barkod", SqlDbType.NVarChar, 20);
                              _cmd.Parameters.Add("@barkod2", SqlDbType.NVarChar, 20);
                              _cmd.Parameters.Add("@tarih", SqlDbType.DateTime, 50);
                              _cmd.Parameters.Add("@fiyat", SqlDbType.Float);
                              _cmd.Parameters.Add("@resim_url", SqlDbType.NVarChar,127);
                              // _cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                              // _cmd.Parameters.Add("@AuthorFirstName", SqlDbType.NVarChar, 50);
                              //_cmd.Parameters.Add("@AuthorLastName", SqlDbType.NVarChar, 50);
                              _cmd.Parameters["@barkod"].Value = strbarkod;
                              _cmd.Parameters["@barkod2"].Value = strbarkod2;
                              _cmd.Parameters["@tarih"].Value = DateTime.Today;
                              _cmd.Parameters["@fiyat"].Value = Convert.ToDouble(fiyat_ham);
                              _cmd.Parameters["@resim_url"].Value = resim_url;
                              //_cmd.Parameters["@AuthorFirstName"].Value = strAuthorFirstName;
                              //_cmd.Parameters["@AuthorLastName"].Value = strAuthorLastName;

                              _cmd.ExecuteNonQuery();
                          }

                      }// end try

                      catch (Exception ex)
                      {
                          //MessageBox.Show("xml_fiyat_kadi tablosuna ekleme işlemi başarısız"+ex);
                      }


                      count++;
                  }
              }



              conn.Close();
              lblXMLtoDatabase.Text = "Data yazma işlemi yapıldı.Kontrol ediniz.Girilen kayıt sayısı " + count;
              lblXMLtoDatabase.Visible = true;*/

        }// end btnWriteXMLtoDatabase_Click

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtFilePath.Text = OFD.FileName;
        }

        private void BtnBigpoint_Click(object sender, EventArgs e)
        {
            string XMlFile = txtFilePath.Text;
            System.Xml.XmlDocument myXmlDocument = new System.Xml.XmlDocument();
            myXmlDocument.Load(@"C:\github\ExchangingDataBetweenXMLandSQLserver-master\XMLtoSQLserver\XMLtoSQLserver\bigpoint_format.xml");
            if (File.Exists(XMlFile))
            {
                // Conversion Xml file to DataTable
                DataTable dt = CreateDataTableXML(XMlFile, "BIGPOINT", myXmlDocument);
                if (dt.Columns.Count == 0)
                    dt.ReadXml(XMlFile);

                // Creating Query for Table Creation
                string Query = CreateTableQuery(dt);
                SqlConnection con = new SqlConnection(@"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_001;User ID =sa;Password =Sa1234;Integrated Security=True");
                con.Open();

                // Deletion of Table if already Exist
                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('dbo." + dt.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dt.TableName + ";", con);
                cmd.ExecuteNonQuery();

                // Table Creation
                cmd = new SqlCommand(Query, con);
                int check = cmd.ExecuteNonQuery();
                if (check != 0)
                {
                    // Copy Data from DataTable to Sql Table
                    using (var bulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        foreach (DataColumn col in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = dt.TableName;
                        bulkCopy.WriteToServer(dt);
                    }

                    MessageBox.Show("Tablo oluşturuldu.Kadıoğlu Aktarım yapıldı.Urun sayısı: " + countData + " Table adı:" + dt.TableName);
                }
                con.Close();
            }

        }
    }// end partial class Form1 : Form
}// end namespace XMLtoSQLserver

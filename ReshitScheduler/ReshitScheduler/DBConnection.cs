﻿using MySql.Data.MySqlClient;
using ReshitScheduler;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Data
{
    public class DBConnection
    {
        private string databaseName = "reshit";
        private MySqlConnection connection = null;
        private static DBConnection _instance = null;
        public static DBConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBConnection();
                return _instance;
            }
        }

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }

        private DBConnection()
        {
        }

        public bool IsConnected
        {
            get
            {
                if (connection == null || connection.State != System.Data.ConnectionState.Open)
                {
                    return false;
                }
                return true;
            }
        }

        public void Connect()
        {
            if (!this.IsConnected)
            {
                try
                {
                    if (String.IsNullOrEmpty(databaseName))
                    {
                        return;
                    }
                    string strConnectionString = string.Format("Server=den1.mysql2.gear.host; database={0}; UID=reshit; password=Aa5407582@", databaseName);
                    
                    if (HttpContext.Current.Request.IsLocal && Environment.MachineName == "IDAN-PC")
                    {
                        strConnectionString = string.Format("Server=localhost; database={0}; UID=root; password=1111", databaseName);
                    }

                    connection = new MySqlConnection(strConnectionString);
                    connection.Open();
                }
                catch (Exception e)
                {
                    e.GetType();
                }
            }
        }

        public void Close()
        {
            connection.Close();
        }

        public DataTable GetAllDataFromTable(string strTableName)
        {
            DataTable dtTable = null;
            this.Connect();
            if (this.IsConnected)
            {
                string query = "SELECT * FROM " + strTableName + " order by " + strTableName + ".id";

                MySqlDataAdapter daAdapter = new MySqlDataAdapter(query, connection);
                daAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dtTable = new DataTable();
                daAdapter.Fill(dtTable);

            }
            this.Close();
            return dtTable;
        }

        public DataTable GetAllDataFromTable(string strTableName, string strWhereClause)
        {
            DataTable dtTable = null;
            this.Connect();
            if (this.IsConnected)
            {
                string query = "SELECT * FROM " + strTableName + strWhereClause + " order by " + strTableName + ".id";

                MySqlDataAdapter daAdapter = new MySqlDataAdapter(query, connection);
                daAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dtTable = new DataTable();
                daAdapter.Fill(dtTable);

            }
            this.Close();
            return dtTable;
        }

        public bool UpdateTableRow(string strTableName, int nId, GridViewRow gvrRowData)
        {
            this.Connect();

            if (this.IsConnected)
            {
                string strQuery = "update " + strTableName + " set ";

                foreach (DataControlFieldCell CurrentField in gvrRowData.Cells)
                {
                    if (CurrentField.ContainingField is AutoGeneratedField &&
                        CurrentField.Controls.Count > 0)
                    {

                        strQuery += (CurrentField.ContainingField as AutoGeneratedField).DataField;
                        strQuery += " = '";
                        if (CurrentField.Controls[0] is TextBox)
                        {
                            strQuery += (CurrentField.Controls[0] as TextBox).Text.Replace("'", "''");
                        }
                        else if (CurrentField.Controls[0] is DropDownList)
                        {
                            strQuery += (CurrentField.Controls[0] as DropDownList).SelectedValue.Replace("'", "''");
                        }
                        strQuery += "',";
                    }
                }
                strQuery = strQuery.Remove(strQuery.Length - 1);
                strQuery += " where id = " + nId;
                try
                {
                    MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }

            }

            this.Close();
            return true;

        }

        public bool InsertTableRow(DataTable dtTable, GridViewRow gvrRowData)
        {
            this.Connect();

            if (this.IsConnected)
            {
                string strFields = string.Empty;
                string strValues = string.Empty;
                foreach (DataControlFieldCell CurrentField in gvrRowData.Cells)
                {
                    if (CurrentField.ContainingField is AutoGeneratedField &&
                        CurrentField.Controls.Count > 0 &&
                        CurrentField.Controls[0] is TextBox)
                    {
                        strFields += (CurrentField.ContainingField as AutoGeneratedField).DataField + ",";
                        if (dtTable.Columns[(CurrentField.ContainingField as AutoGeneratedField).DataField].DataType == typeof(string) ||
                            dtTable.Columns[(CurrentField.ContainingField as AutoGeneratedField).DataField].DataType == typeof(TimeSpan))
                        {
                            strValues += "'" + (CurrentField.Controls[0] as TextBox).Text + "',";
                        }
                        else
                        {
                            strValues += (CurrentField.Controls[0] as TextBox).Text == string.Empty ? "-1," :
                                (CurrentField.Controls[0] as TextBox).Text + ",";
                        }
                    }

                }
                strFields = strFields.Remove(strFields.Length - 1);
                strValues = strValues.Remove(strValues.Length - 1);
                string strQuery = "insert into " + dtTable.TableName + "(" + strFields + ") values (" + strValues + ")";
                try
                {
                    MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }
            }
            this.Close();

            return true;
        }

        public bool InsertTableRow(string tableName, string strFields, string strValues)
        {
            this.Connect();

            if (this.IsConnected)
            {
                string strQuery = "insert into " + tableName + " (" + strFields + ") values (" + strValues + ")";
                try
                {
                    MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }
            }
            return true;
        }

        public DataTable GetDataTableByQuery(string strQuery)
        {
            this.Connect();
            DataTable dtTable = null;

            if (this.IsConnected)
            {
                try
                {
                    MySqlDataAdapter daAdapter = new MySqlDataAdapter(strQuery, connection);
                    daAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dtTable = new DataTable();
                    daAdapter.Fill(dtTable);

                    MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                finally
                {
                    this.Close();

                }
            }


            return dtTable;
        }

        public string GetStringByQuery(string strQuery)
        {
            return GetDataTableByQuery(strQuery).Rows[0][0].ToString();
        }

        public DataTable GetForeignKeys(string strTableName)
        {
            string strQuery = "SELECT CONSTRAINT_NAME,REFERENCED_TABLE_NAME " +
                              "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS " +
                               "where table_name = '" + strTableName + "'";
            DataTable dtForeignKeys = GetDataTableByQuery(strQuery);

            foreach (DataRow drCurrentRow in dtForeignKeys.Rows)
            {
                drCurrentRow[0] = drCurrentRow[0].ToString().Remove(0, strTableName.Length + 1);
            }
            return dtForeignKeys;

        }

        public DataTable GetConstraintDataTable(string strTableName)
        {
            string strDisplayQuery = GetDisplayQuery(strTableName);

            return GetDataTableByQuery(strDisplayQuery);
        }
        public DataTable GetConstraintData(string strTableName, int nID)
        {
            string strColumnName = GetDisplayQuery(strTableName);

            string strQuery = "SELECT id," + strColumnName + " as name FROM " + strTableName + " where id = " + nID;
            return GetDataTableByQuery(strQuery);
        }

        public string GetDisplayQuery(string strTableName)
        {
            string strQuery = "SELECT display_column_query " +
                              "FROM tables_information " +
                               "where table_name = '" + strTableName + "'";
            return GetStringByQuery(strQuery);
        }
        public DataSet GetAllTables()
        {
            DataSet dsAllTables = new DataSet();
            DataTable dtTables = DBConnection.Instance.GetDataTableByQuery("select table_name from INFORMATION_SCHEMA.tables where table_schema = 'reshit'");
            foreach (DataRow CurrentTable in dtTables.Rows)
            {
                dsAllTables.Tables.Add(this.GetAllDataFromTable(CurrentTable["table_name"].ToString(), string.Empty));
            }
            return dsAllTables;
        }

        //PopulateEmpty data for GridView 
        public DataTable GetEmptyDataTable(DataTable dtOriginalTable)
        {
            DataTable dtEmpty = new DataTable();

            foreach (DataColumn dcCurrentColumn in dtOriginalTable.Columns)
            {
                dtEmpty.Columns.Add(dcCurrentColumn.ColumnName, dcCurrentColumn.DataType);
            }
            DataRow dataRow = dtEmpty.NewRow();

            //Inserting a new row,datatable .newrow creates a blank row
            dtEmpty.Rows.Add(dataRow);//adding row to the datatable
            dtEmpty.TableName = dtOriginalTable.TableName;
            return dtEmpty;
        }

        public string GetCurrentYearID()
        {
            return this.GetStringByQuery("select value from preferences where name = 'current_year_id'");
        }

        public void ExecuteNonQuery(string strCommand)
        {
            this.Connect();
            if (this.IsConnected)
            {
                MySqlCommand msqlCommand = new MySqlCommand(strCommand, this.connection);
                msqlCommand.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}
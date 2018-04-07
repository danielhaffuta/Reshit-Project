﻿using MySql.Data.MySqlClient;
using ReshitScheduler;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Data
{
    public class DBConnection
    {
        private string databaseName = "reshit";
        private MySqlConnection connection = null;
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
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
                        return; ;
                    }
                    string strConnectionString = string.Format("Server=localhost; database={0}; UID=root; password=1111", databaseName);
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
                string query = "SELECT * FROM " + strTableName;

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
                        CurrentField.Controls.Count > 0 )
                    {
                        
                        strQuery += (CurrentField.ContainingField as AutoGeneratedField).DataField;
                        strQuery += " = '";
                        if (CurrentField.Controls[0] is TextBox)
                        {
                            strQuery += (CurrentField.Controls[0] as TextBox).Text;
                        }
                        else if(CurrentField.Controls[0] is DropDownList)
                        {
                            strQuery += (CurrentField.Controls[0] as DropDownList).SelectedValue;
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

        public bool InsertTableRow(string strTableName, GridViewRow gvrRowData)
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
                        strValues += "'" + (CurrentField.Controls[0] as TextBox).Text + "',";
                    }

                }
                strFields = strFields.Remove(strFields.Length - 1);
                strValues = strValues.Remove(strValues.Length - 1);
                string strQuery = "insert into " + strTableName + "(" + strFields + ") values (" + strValues + ")";
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
            }
            this.Close();

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
            string strQuery = "SELECT display_column_name " + 
                              "FROM tables_display_columns " +
                               "where table_name = '" + strTableName + "'";
            string strColumnName = GetStringByQuery(strQuery);

            strQuery = "SELECT id," + strColumnName + " as name FROM " + strTableName;
            return GetDataTableByQuery(strQuery);
        }
        public DataTable GetConstraintData(string strTableName,int nID)
        {
            string strQuery = "SELECT display_column_name " +
                              "FROM tables_display_columns " +
                               "where table_name = '" + strTableName + "'";
            string strColumnName = GetStringByQuery(strQuery);

            strQuery = "SELECT id," + strColumnName + " as name FROM " + strTableName + " where id = " + nID;
            return GetDataTableByQuery(strQuery);
        }
    }
}
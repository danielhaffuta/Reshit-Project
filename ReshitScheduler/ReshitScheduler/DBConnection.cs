﻿using MySql.Data.MySqlClient;
using ReshitScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace ReshitScheduler
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
                string query = "SELECT * FROM " + strTableName + " " + strWhereClause + " order by " + strTableName + ".id";

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

        public bool UpdateTableRow(string strTableName, int nID, string strFields, string strValues)
        {
            this.Connect();

            if (this.IsConnected)
            {
                string strQuery = "update " + strTableName + " set ";
                string[] straFields = strFields.Split(':');
                string[] straValues = strValues.Split(':');
                if (straFields.Length != straValues.Length)
                {
                    return false;
                }
                for (int nCurrentFieldIndex = 0; nCurrentFieldIndex < straFields.Length; nCurrentFieldIndex++)
                {
                    strQuery += straFields[nCurrentFieldIndex] + " = " + straValues[nCurrentFieldIndex] + ",";

                }
                strQuery = strQuery.Remove(strQuery.Length - 1);
                strQuery += " where id = " + nID;
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

        public bool InsertTableRow(string tableName, string strFields, string strValues,out int nInsertID)
        {
            this.Connect();

            nInsertID = -1;
            if (this.IsConnected)
            {
                string strQuery = "insert into " + tableName + " (" + strFields + ") values (" + strValues + ")";
                try
                {
                    MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    command.ExecuteNonQuery();
                    nInsertID = Convert.ToInt32(command.LastInsertedId);
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

                    //MySqlCommand command = new MySqlCommand(strQuery, this.connection);
                    //command.ExecuteNonQuery();
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

        public DataTable GetDataTableForDisplay(string strTableName, string strWhereClause = "", string strOrderByClause = "")
        {
            string strDisplayQuery = GetDisplayQuery(strTableName);

            return GetDataTableByQuery(strDisplayQuery + " " + strWhereClause + " " + strOrderByClause);
        }
        public string GetConstraintData(string strTableName, int nID)
        {
            string strColumnName = GetDisplayQuery(strTableName);

            string strQuery = "select name from (" + strColumnName + " where " + strTableName + ".id = " + nID + ") table_name";
            return GetStringByQuery(strQuery);
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

        public int GetCurrentYearID()
        {
            return Convert.ToInt32(GetStringByQuery("select value from preferences where name = 'current_year_id'"));
        }

        public void IncreaseCurrentYearID()
        {
                int nCurrentYearID = Convert.ToInt32(GetCurrentYearID());
            this.Connect();

            if (this.IsConnected)
            {

                MySqlCommand command = new MySqlCommand("update preferences set value = (value + 1) where name = 'current_year_id'", this.connection);

                command.ExecuteNonQuery();

                command = new MySqlCommand("insert into teachers(first_name,last_name,teacher_type_id,user_name,password,year_id)" +
                                           " select first_name, last_name, teacher_type_id, user_name, password, " + (nCurrentYearID + 1) + 
                                           " from teachers where year_id = " + nCurrentYearID, this.connection);
                command.ExecuteNonQuery();

                command = new MySqlCommand("insert into classes(grade_id,class_number,teacher_id)" +
                                           " select (grade_id+1), class_number,new_teacher.id from classes " +
                                           " inner join teachers old_teacher on old_teacher.id = classes.teacher_id" +
                                           " inner join teachers new_teacher on new_teacher.user_name = old_teacher.user_name "+
                                           " and new_teacher.year_id = " + (nCurrentYearID + 1) +
                                           " where grade_id < 8", this.connection);
                command.ExecuteNonQuery();

                command = new MySqlCommand("insert into teacher_class_access(teacher_id,class_id)" +
                                           " select new_teacher.id,new_class.id from teacher_class_access " +
                                           " inner join teachers old_teacher on old_teacher.id = teacher_class_access.teacher_id " +
                                           " inner join teachers new_teacher on new_teacher.user_name = old_teacher.user_name " +
                                                                           " and new_teacher.year_id = " + (nCurrentYearID + 1) +
                                           " inner join classes old_class on old_class.id = teacher_class_access.class_id " +
                                           " inner join teachers old_class_teacher on old_class_teacher.id = old_class.teacher_id " +
                                           " inner join teachers new_class_teacher on new_class_teacher.user_name = old_class_teacher.user_name " +
                                                                                 " and new_class_teacher.year_id = "+ (nCurrentYearID + 1)+
                                           " inner join classes new_class on new_class.teacher_id = new_class_teacher.id  " +
                                                                        " and new_class.class_number = old_class.class_number " +
                                                                        " and new_class.grade_id = (old_class.grade_id +1) ", this.connection);
                command.ExecuteNonQuery();



                command = new MySqlCommand("insert into groups(group_name,teacher_id)" +
                                           " select group_name, new_teacher.id from groups " +
                                           " inner join teachers old_teacher on old_teacher.id = groups.teacher_id" +
                                           " inner join teachers new_teacher on new_teacher.user_name = old_teacher.user_name " +
                                                                           " and new_teacher.year_id = " + (nCurrentYearID + 1)  , this.connection);

                command.ExecuteNonQuery();

                command = new MySqlCommand("insert into courses(course_name,teacher_id)" +
                                          " select course_name, new_teacher.id from courses " +
                                          " inner join teachers old_teacher on old_teacher.id = courses.teacher_id" +
                                          " inner join teachers new_teacher on new_teacher.user_name = old_teacher.user_name " +
                                                                          " and new_teacher.year_id = " + (nCurrentYearID + 1), this.connection);

                command.ExecuteNonQuery();


                command = new MySqlCommand("insert into students_classes(student_id,class_id)" +
                                          " select students_classes.student_id, new_class.id from students_classes " +
                                           " inner join classes old_class on old_class.id = students_classes.class_id " +
                                           " inner join teachers old_class_teacher on old_class_teacher.id = old_class.teacher_id " +
                                           " inner join teachers new_class_teacher on new_class_teacher.user_name = old_class_teacher.user_name " +
                                                                                 " and new_class_teacher.year_id = " + (nCurrentYearID + 1) +
                                           " inner join classes new_class on new_class.teacher_id = new_class_teacher.id  " +
                                                                        " and new_class.class_number = old_class.class_number " +
                                                                        " and new_class.grade_id = (old_class.grade_id +1) ", this.connection);
                                    

                command.ExecuteNonQuery();





                command = new MySqlCommand("insert into hours_in_day(hour_of_school_day,start_time,finish_time,is_break,year_id)" +
                                          " select hour_of_school_day,start_time,finish_time,is_break," + (nCurrentYearID + 1) +
                                          " from hours_in_day where year_id = " + nCurrentYearID, this.connection);

                command.ExecuteNonQuery();
            }

            this.Close();
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

        public DataRow GetStudentDetails(int nStudentID)
        {

            return DBConnection.Instance.GetDataTableByQuery(" select concat(students.first_name,' ' ,students.last_name) as name," +
                                                                            " picture_path,concat(grades.grade_name,classes.class_number) as class," +
                                                                            " classes.id as class_id, students.id as student_id," +
                                                                            " students.mother_cellphone, students.mother_full_name," +
                                                                            " students.father_cellphone, students.father_full_name," +
                                                                            " students.home_phone, students.parents_email," +
                                                                            " students.settlement" +
                                                                            " from students " +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " inner join classes on classes.id = students_classes.class_id" +
                                                                            " inner join teachers on teachers.id = classes.teacher_id " +
                                                                            " and teachers.year_id = (select value from preferences where name = 'current_year_id')" +
                                                                            " inner join grades on grades.id = classes.grade_id" +
                                                                            " where students.id = " + nStudentID).Rows[0];
        }

        public DataTable GetStudentsDetails(List<int> lstStudentIDs)
        {

            return DBConnection.Instance.GetDataTableByQuery(" select concat(students.first_name,' ' ,students.last_name) as name," +
                                                                            " picture_path,concat(grades.grade_name,classes.class_number) as class," +
                                                                            " classes.id as class_id, students.id as student_id," +
                                                                            " students.mother_cellphone, students.mother_full_name," +
                                                                            " students.father_cellphone, students.father_full_name," +
                                                                            " students.home_phone, students.parents_email," +
                                                                            " students.settlement" +
                                                                            " from students " +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " inner join classes on classes.id = students_classes.class_id" +
                                                                            " inner join teachers on teachers.id = classes.teacher_id " +
                                                                            " and teachers.year_id = (select value from preferences where name = 'current_year_id')" +
                                                                            " inner join grades on grades.id = classes.grade_id" +
                                                                            " where students.id in (" + string.Join(",", lstStudentIDs.Select(number => number.ToString()).ToArray()) + ")");
        }

        public DataTable GetHours()
        {
            return DBConnection.Instance.GetDataTableForDisplay("hours_in_day", "where hours_in_day.year_id = (select value from preferences where name = 'current_year_id')", "order by hour_of_school_day");
        }

        public DataTable GetStudentEvaluations(int nStudentID)
        {
            return GetDataTableByQuery(
                " select group_name as lesson_name,ifnull(evaluation,\"\" ) as evaluation from students_schedule" +
                " left join groups_evaluations on groups_evaluations.student_id = students_schedule.student_id" +
                " and groups_evaluations.group_id = students_schedule.group_id" +
                " inner join groups on groups.id = students_schedule.group_id" +
                " where students_schedule.student_id = " + nStudentID +
                " union" +
                " select distinct course_name as lesson_name, ifnull(evaluation,\"\" ) as evaluation from classes_schedule" +
                " inner join students_classes on students_classes.class_id = classes_schedule.class_id" +
                                            " and students_classes.student_id = " + nStudentID +
                " inner join courses on courses.id = classes_schedule.course_id" +
                " left join courses_evaluations on courses_evaluations.course_id = classes_schedule.course_id" +
                                              " and courses_evaluations.student_id = " + nStudentID +
                " where " + nStudentID + " not in (select student_id from students_schedule where students_schedule.day_id = classes_schedule.day_id" +
                                                                                            " and students_schedule.hour_id = classes_schedule.hour_id)");
        }

        public DataTable GetClassStudents(int nClassID)
        {
            return GetDataTableByQuery(" select concat(students.first_name,' ' ,students.last_name) as name," +
                                        " picture_path,concat(grades.grade_name,classes.class_number) as class," +
                                        " classes.id as class_id, students.id as student_id," +
                                        " students.mother_cellphone, students.mother_full_name," +
                                        " students.father_cellphone, students.father_full_name," +
                                        " students.home_phone, students.parents_email," +
                                        " students.settlement" +
                                        " from students " +
                                        " inner join students_classes on students_classes.student_id = students.id" +
                                        " inner join classes on classes.id = students_classes.class_id" +
                                        " inner join teachers on teachers.id = classes.teacher_id " +
                                        " and teachers.year_id = (select value from preferences where name = 'current_year_id')" +
                                        " inner join grades on grades.id = classes.grade_id" +
                                        " where classes.id = " + nClassID);
        }

        public DataTable GetCourseEvaluations(int nCourse)
        {
            return GetDataTableByQuery(" select  distinct (students.id) as student_id," +
                        " students_classes.class_id as class_id," +
                        " concat(grades.grade_name, classes.class_number) as class," +
                        " courses.course_name as lesson_name," +
                        " concat(students.first_name, ' ', students.last_name) as student_name," +
                        " students.picture_path,ifnull(evaluation,\"\" ) as evaluation ,courses_evaluations.id as evaluation_id " +
                    " from classes_schedule" +
                    " inner join courses on courses.id = classes_schedule.course_id" +
                    " inner join classes on classes.id = classes_schedule.class_id" +
                    " inner join grades on grades.id = classes.grade_id" +
                    " inner join students_classes on students_classes.class_id = classes_schedule.class_id " +
                    " inner join students on students.id = students_classes.student_id" +
                    " left join courses_evaluations on courses_evaluations.course_id = courses.id" +
                                                   " and courses_evaluations.student_id = students.id" +
                    " where classes_schedule.course_id = " + nCourse +
                    " and students.id not in (select student_id from students_schedule where students_schedule.day_id = classes_schedule.day_id" +
                                                                                       " and students_schedule.hour_id = classes_schedule.hour_id)");
        }

        public DataTable GetGroupEvaluations(int nGroupID)
        {
            return GetDataTableByQuery(" select  distinct (students.id) as student_id," +
                        " students_classes.class_id as class_id," +
                        " concat(grades.grade_name, classes.class_number) as class," +
                        " groups.group_name as lesson_name," +
                        " concat(students.first_name, ' ', students.last_name) as student_name," +
                        " students.picture_path,ifnull(evaluation,\"\" ) as evaluation ,groups_evaluations.id as evaluation_id " +
                    " from students_schedule" +
                    " inner join groups on groups.id = students_schedule.group_id" +
                    " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
                    " inner join students on students.id = students_classes.student_id" +
                    " inner join classes on classes.id = students_classes.class_id" +
                    " inner join grades on grades.id = classes.grade_id" +
                    " left join groups_evaluations on groups_evaluations.group_id = groups.id " +
                                                  " and groups_evaluations.student_id = students.id" +
                    " where groups.id = " + nGroupID);
        }

        public DataTable GetTeacherClasses(int nTeacherID)
        {

            return GetDataTableByQuery(GetDisplayQuery("classes") +
                " inner join teacher_class_access on teacher_class_access.class_id = classes.id " +
                " where teacher_class_access.teacher_id = " + nTeacherID);
        }

        public DataTable GetThisYearTeachers()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("teachers") +
                                            " where year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetThisYearClasses()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("classes") +
                                            " where teachers.year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetThisYearClassesDetails()
        {
            return this.GetDataTableByQuery("select classes.id as class_id, grades.id as grade_id,grades.grade_name,classes.class_number,"+                                            " teachers.id as teacher_id, concat(teachers.first_name, ' ', teachers.last_name) as teacher_name"+
                                            " from classes"+
                                            " inner join grades on grades.id = classes.grade_id"+
                                            " inner join teachers on teachers.id = classes.teacher_id"+
                                            " inner join years on years.id = teachers.year_id"+
                                            " where teachers.year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetThisYearCourses()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("courses") +
                                            " where teachers.year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetThisYearGroups()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("groups") +
                                            " where teachers.year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetThisYearTeachersAccesses()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("teacher_class_access") +
                                            " where teachers.year_id=(select value from preferences where name='current_year_id')");
        }

        public DataTable GetGrades()
        {
            return this.GetDataTableByQuery(GetDisplayQuery("grades"));
        }
    }
}
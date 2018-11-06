using RoinCableTester.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace RoinCableTester.Communication {
    public class Database {

        public SQLiteConnection OpenConn(string database) {
            database = Path.Combine(Util.GetAppPath(), database + ".db");
            string cnstr = string.Format("Data Source=" + database + ";Version=3;New=False;Compress=True;");
            SQLiteConnection icn = new SQLiteConnection();
            icn.ConnectionString = cnstr;
            if (icn.State == ConnectionState.Open) {
                icn.Close();
            }
            icn.Open();
            return icn;
        }

        public void CreateSQLiteDatabase(string database) {
            database = Path.Combine(Util.GetAppPath(), database + ".db");
            string cnstr = string.Format("Data Source=" + database + ";Version=3;New=True;Compress=True;");
            SQLiteConnection icn = new SQLiteConnection();
            icn.ConnectionString = cnstr;
            icn.Open();
            icn.Close();
        }

        public void CreateSQLiteTable(string database, string createTableString) {
            using (SQLiteConnection icn = OpenConn(database)) {
                SQLiteCommand cmd = new SQLiteCommand(createTableString, icn);
                SQLiteTransaction mySqlTransaction = icn.BeginTransaction();
                try {
                    cmd.Transaction = mySqlTransaction;
                    cmd.ExecuteNonQuery();
                    mySqlTransaction.Commit();
                } catch (Exception ex) {
                    mySqlTransaction.Rollback();
                    throw (ex);
                }
            }
        }

        public void SQLiteExecuteNonQuery(string database, string sqlSelectString) {
            using (SQLiteConnection icn = OpenConn(database)) {
                SQLiteCommand cmd = new SQLiteCommand(sqlSelectString, icn);
                SQLiteTransaction mySqlTransaction = icn.BeginTransaction();
                try {
                    cmd.Transaction = mySqlTransaction;
                    cmd.ExecuteNonQuery();
                    mySqlTransaction.Commit();
                } catch (Exception ex) {
                    mySqlTransaction.Rollback();
                    throw (ex);
                }
            }
        }

        public DataTable GetDataTable(string database, string sqliteString) {
            DataTable myDataTable = new DataTable();
            using (SQLiteConnection icn = OpenConn(database)) {
                SQLiteDataAdapter da = new SQLiteDataAdapter(sqliteString, icn);
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                myDataTable = ds.Tables[0];
            }
            return myDataTable;
        }

        public string SQLiteExecuteScalar(string database, string sqliteString) {
            object obj = null;
            using (SQLiteConnection icn = OpenConn(database)) {
                SQLiteCommand cmd = new SQLiteCommand(sqliteString, icn);
                obj = cmd.ExecuteScalar();
            }
            return obj.ToString();
        }
    }
}

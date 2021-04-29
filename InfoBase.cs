using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ED_Systems_v2
{
    class InfoBase
    {
        private DataTable dt;
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter adapter;

        public string exeption;

        public InfoBase()
        {
            exeption = "";
            try
            {
                conn = new SQLiteConnection(@"Data Source=dbfiles\info.sqlite;Version=3;");
                conn.Open();
                cmd = new SQLiteCommand
                {
                    Connection = conn
                };
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message;
            }

        }
        ~InfoBase()
        {
            try
            {
                conn.Close();
            }
            catch { };
            conn.Dispose();
        }
        public void InsertEvent(DateTime timestamp, string ev)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO events (timestamp, event) VALUES (@timestamp, @event)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = timestamp;
            cmd.Parameters.Add("@event", DbType.String).Value = ev;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public DataTable SelectFederal(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM federal WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectEmpire(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM empire WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectAlliance(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM alliance WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectCombat(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM combat WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectTrade(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM trade WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectExplore(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM explore WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectCQC(int id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM cqc WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectRaw()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM raw";
            cmd.Parameters.Clear();
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectEncoded()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM encoded";
            cmd.Parameters.Clear();
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectManuf()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM manufactured";
            cmd.Parameters.Clear();
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectProgramSettings()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM program_settings";
            cmd.Parameters.Clear();
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectUserSettings()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM user_settings";
            cmd.Parameters.Clear();
            adapter = new SQLiteDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public void UpdateLogPath(string path)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET LogPath = @LogPath
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@LogPath", DbType.String).Value = path;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateLastLog(string path)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET LastLog = @LastLog
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@LastLog", DbType.String).Value = path;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateFromImgPath(string path)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET FromImgPath = @FromImgPath
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@FromImgPath", DbType.String).Value = path;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateToImgPath(string path)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET ToImgPath = @ToImgPath
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@ToImgPath", DbType.String).Value = path;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateBackupPath(string path)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET BackupPath = @BackupPath
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@BackupPath", DbType.String).Value = path;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateMaxRadius(int value)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET MaxRadius = @MaxRadius
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MaxRadius", DbType.Int32).Value = value;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateBkgLastLine(int value)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET BkgLastLine = @BkgLastLine
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@BkgLastLine", DbType.Int32).Value = value;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateLastTimestamp(ulong value)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET LastTimestamp = @LastTimestamp
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@LastTimestamp", DbType.UInt64).Value = value;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateBkgRead(bool value)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE user_settings
                                SET BkgRead = @BkgRead
                                WHERE rowid = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@BkgRead", DbType.Boolean).Value = value;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
    }
}

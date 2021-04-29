using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;

namespace ED_Systems_v2
{
    class CMDRBase
    {
        private DataTable dt;
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter adapter;

        public string exeption;

        public CMDRBase()
        {
            exeption = "";
            try
            {
                conn = new SQLiteConnection(@"Data Source=dbfiles\cmdr.sqlite;Version=3;");
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
        ~CMDRBase()
        {
            try
            {
                conn.Close();
            }
            catch { };
            conn.Dispose();
        }
        //cmdr
        public DataTable SelectCMDRs()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM cmdrs";
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
        public DataTable SelectCMDRByName(string name)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM cmdrs WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
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
        public void UpdateCMDR(string name, UInt64 syskey, string sysname)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE cmdrs
                                SET syskey = @syskey,
                                    sysname = @sysname
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@syskey", DbType.UInt64).Value = syskey;
            cmd.Parameters.Add("@sysname", DbType.String).Value = sysname;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertCMDR(string name)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO cmdrs (name) VALUES (@name)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO status (name) VALUES (@name)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        //status
        public DataTable SelectStatus(string name)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM status WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
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
        public void UpdateCredits(string name, LoadGame info)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET credits = @credits
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@credits", DbType.Int64).Value = info.Credits;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateCredits(string name, Int64 credits)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET credits = @credits
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@credits", DbType.Int64).Value = credits;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateAddCredits(string name, Int64 credits)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET credits = credits + @credits
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@credits", DbType.Int64).Value = credits;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateRank(string name, Rank info)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET combat = @combat,
                                    cqc = @cqc,
                                    explore = @explore,
                                    trade = @trade,
                                    empire = @empire,
                                    federal = @federal,
                                    alliance = @alliance
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@combat", DbType.UInt32).Value = info.Combat;
            cmd.Parameters.Add("@cqc", DbType.UInt32).Value = info.CQC;
            cmd.Parameters.Add("@explore", DbType.UInt32).Value = info.Explore;
            cmd.Parameters.Add("@trade", DbType.UInt32).Value = info.Trade;
            cmd.Parameters.Add("@empire", DbType.UInt32).Value = info.Empire;
            cmd.Parameters.Add("@federal", DbType.UInt32).Value = info.Federation;
            cmd.Parameters.Add("@alliance", DbType.UInt32).Value = info.Alliance;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateReputation(string name, Reputation info)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET repempire = @repempire,
                                    repfederal = @repfederal,
                                    repalliance = @repalliance
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@repempire", DbType.UInt32).Value = info.Empire;
            cmd.Parameters.Add("@repfederal", DbType.UInt32).Value = info.Federation;
            cmd.Parameters.Add("@repalliance", DbType.UInt32).Value = info.Alliance;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateProgress(string name, Progress info)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE status
                                SET prgcombat = @prgcombat,
                                    prgcqc = @prgcqc,
                                    prgexplore = @prgexplore,
                                    prgtrade = @prgtrade,
                                    prgempire = @prgempire,
                                    prgfederal = @prgfederal,
                                    prgalliance = @prgalliance
                                WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@prgcombat", DbType.UInt32).Value = info.Combat;
            cmd.Parameters.Add("@prgcqc", DbType.UInt32).Value = info.CQC;
            cmd.Parameters.Add("@prgexplore", DbType.UInt32).Value = info.Explore;
            cmd.Parameters.Add("@prgtrade", DbType.UInt32).Value = info.Trade;
            cmd.Parameters.Add("@prgempire", DbType.UInt32).Value = info.Empire;
            cmd.Parameters.Add("@prgfederal", DbType.UInt32).Value = info.Federation;
            cmd.Parameters.Add("@prgalliance", DbType.UInt32).Value = info.Alliance;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        
        //materials
        public DataTable SelectMaterials(string name)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM materials WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
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
        public DataTable SelectMaterialByKey(string name, string key)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM materials WHERE name = @name AND key = @key";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@key", DbType.String).Value = key;
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
        public void InsertMaterial(string name, string key, int count)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO materials (name, key, count)
                                       VALUES (@name, @key, @count)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@key", DbType.String).Value = key;
            cmd.Parameters.Add("@count", DbType.Int32).Value = count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateMaterial(string name, string key, int count)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE materials
                                SET count = @count
                                WHERE name = @name AND key = @key";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@key", DbType.String).Value = key;
            cmd.Parameters.Add("@count", DbType.Int32).Value = count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateAddMaterial(string name, string key, int count)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE materials
                                SET count = count + @count
                                WHERE name = @name AND key = @key";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@key", DbType.String).Value = key;
            cmd.Parameters.Add("@count", DbType.Int32).Value = count;
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

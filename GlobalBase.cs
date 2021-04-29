using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems_v2
{
    class GlobalBase
    {
        public bool connected = false;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private MySqlTransaction tr;
        private DataTable dt;

        public string exeption = "";
        public GlobalBase(string host, string db, string port, string user, string pwd)
        {
            // Connection String.
            string connString = "Server=" + host +
                                ";Database=" + db +
                                ";port=" + port+
                                ";User Id=" + user +
                                ";password=" + pwd;
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                connected = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("MySQL Error:\r\n" + ex.Message);
                connected = false;
            }
        }
        ~GlobalBase()
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch { };
        }

        private string BodyClass(string s)
        {
            switch (s)
            {
                case "Earthlike body":
                    s = "Earth-like world";
                    break;
                case "Gas giant with ammonia based life":
                    s = "Gas giant with ammonia-based life";
                    break;
                case "Gas giant with water based life":
                    s = "Gas giant with water-based life";
                    break;
                case "Helium rich gas giant":
                    s = "Helium-rich gas giant";
                    break;
                case "High metal content body":
                    s = "High metal content world";
                    break;
                case "Metal rich body":
                    s = "Metal-rich body";
                    break;
                case "Rocky ice body":
                    s = "Rocky Ice world";
                    break;

                default:
                    break;
            }
            return s;
        }
        //systems
        public int SelectSystemsCount(UInt64 SKey)
        {
            exeption = "";
            int count = 0;
            cmd.CommandText = @"SELECT COUNT(*)
                                FROM Systems
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            try
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return count;
        }
        public DataTable SelectSystems()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Systems";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public void InsertSystem(FSDJump row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z)
                                VALUES (@SKey, @Name, @X, @Y, @Z)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertSystem(Location row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z)
                                VALUES (@SKey, @Name, @X, @Y, @Z)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystem(Location row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystem(FSDJump row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertSystem(CarrierJump row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z)
                                VALUES (@SKey, @Name, @X, @Y, @Z)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystem(CarrierJump row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", MySqlDbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", MySqlDbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", MySqlDbType.Double).Value = row.StarPos[2];
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        //bodies
        public DataTable SelectBodyByName(UInt64 SKey, string name)
        {
            dt = new DataTable();
            exeption = "";
            cmd.CommandText = @"SELECT *
                                FROM Bodies
                                WHERE SKey = @SKey AND
                                      FullName = @FullName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = name;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectBodies(UInt64 SKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Bodies
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectAllBodies()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Bodies";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public UInt64 InsertBody(UInt64 SKey, Scan row)
        {
            exeption = "";
            UInt64 BKey = 0;
            string bodyClass = "";
            string type = "";
            int landable = row.Landable ? 1 : 0;
            row.PlanetClass = row.PlanetClass.Replace("Sudarsky", "").Trim();
            if (row.PlanetClass != "")
            {
                type = "Planet";
                bodyClass = row.PlanetClass.Substring(0, 1).ToUpper() + row.PlanetClass.Substring(1);
                bodyClass = BodyClass(bodyClass);
            }
            else if (row.StarType != "")
            {
                type = "Star";
                bodyClass = row.StarType + row.Subclass.ToString() + " " + row.Luminosity;
            }
            else if (row.BodyName.IndexOf("Belt Cluster", StringComparison.OrdinalIgnoreCase) > 0)
            {
                type = "Belt";
                bodyClass = "";
            }
            else
            {
                //это кольцо
                return BKey;
            }
            if (row.ReserveLevel != "")
            {
                row.ReserveLevel = row.ReserveLevel.Substring(0, 1).ToUpper() + row.ReserveLevel.Substring(1);
            }
            if (row.Volcanism.Length > 1)
                row.Volcanism = row.Volcanism.Substring(0, 1).ToUpper() + row.Volcanism.Substring(1);
            string planetName = row.BodyName.Replace(row.StarSystem, "").Trim();
            if (planetName == "") planetName = row.StarSystem;


            cmd.CommandText = @"INSERT INTO Bodies 
                                    (SKey, FullName, Name, Type, Class, Distance, Volcanism, Landable, Reserve)
                                VALUES 
                                    (@SKey, @FullName, @Name, @Type, @Class, @Distance, @Volcanism, @Landable, @Reserve)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = row.BodyName;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = planetName;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = type;
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = bodyClass;
            cmd.Parameters.Add("@Distance", MySqlDbType.Double).Value = Math.Round(row.DistanceFromArrivalLS, 2);
            cmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = row.Volcanism;
            cmd.Parameters.Add("@Landable", MySqlDbType.Int32).Value = Convert.ToByte(row.Landable);
            cmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = row.ReserveLevel;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                cmd.CommandText = @"SELECT LAST_INSERT_ID() FROM Bodies";
                cmd.Parameters.Clear();

                try
                {
                    BKey = Convert.ToUInt64(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    exeption = ex.Message + Environment.NewLine + cmd.CommandText;
                }
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            
            return BKey;
        }
        public UInt64 InsertEDSMBody(UInt64 SKey, EDSMBody row, string sysName)
        {
            exeption = "";
            UInt64 BKey = 0;

            int landable = (row.isLandable ?? false) ? 1 : 0;

            row.volcanismType = (row.volcanismType == "No volcanism" ? "" : row.volcanismType);
            if (row.volcanismType.Length > 1)
                row.volcanismType = row.volcanismType.Substring(0, 1).ToUpper() + row.volcanismType.Substring(1);
            string planetName = row.name.Replace(sysName, "").Trim();
            if (planetName == "") planetName = sysName;
            row.subType = BodyClass(row.subType);

            cmd.CommandText = @"INSERT INTO Bodies 
                                    (SKey, FullName, Name, Type, Class, Distance, Volcanism, Landable, Reserve)
                                VALUES 
                                    (@SKey, @FullName, @Name, @Type, @Class, @Distance, @Volcanism, @Landable, @Reserve)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = row.name;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = planetName;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = row.type;
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = row.subType;
            cmd.Parameters.Add("@Distance", MySqlDbType.Double).Value = row.distanceToArrival;
            cmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = row.volcanismType;
            cmd.Parameters.Add("@Landable", MySqlDbType.Int32).Value = Convert.ToByte(landable);
            cmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = "";
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                cmd.CommandText = @"SELECT LAST_INSERT_ID() FROM Bodies";
                cmd.Parameters.Clear();

                try
                {
                    BKey = Convert.ToUInt64(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    exeption = ex.Message + Environment.NewLine + cmd.CommandText;
                }
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            
            return BKey;
        }
        public void UpdateBody(UInt64 SKey, UInt64 BKey, Scan row)
        {
            exeption = "";
            string bodyClass = "";
            string type = "";
            int landable = row.Landable ? 1 : 0;
            row.PlanetClass = row.PlanetClass.Replace("Sudarsky", "").Trim();
            if (row.PlanetClass != "")
            {
                type = "Planet";
                bodyClass = row.PlanetClass.Substring(0, 1).ToUpper() + row.PlanetClass.Substring(1);
                bodyClass = BodyClass(bodyClass);
            }
            if (row.ReserveLevel != "")
            {
                row.ReserveLevel = row.ReserveLevel.Substring(0, 1).ToUpper() + row.ReserveLevel.Substring(1);
            }
            if (row.Volcanism.Length > 1)
                row.Volcanism = row.Volcanism.Substring(0, 1).ToUpper() + row.Volcanism.Substring(1);

            if (row.StarType != "")
            {
                type = "Star";
                bodyClass = row.StarType + row.Subclass.ToString() + " " + row.Luminosity;
            }
            if (row.BodyName.IndexOf("Belt Cluster", StringComparison.OrdinalIgnoreCase) > 0)
            {
                type = "Belt";
                bodyClass = "";
            }

            cmd.CommandText = @"UPDATE Bodies
                                SET Type = @Type,
                                    Distance = @Distance,
                                    Class = @Class,
                                    Volcanism = @Volcanism,
                                    Landable = @Landable,
                                    Reserve = @Reserve
                                WHERE SKey = @SKey AND
                                      BKey = @BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.String).Value = BKey;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = type;
            cmd.Parameters.Add("@Distance", MySqlDbType.Double).Value = Math.Round(row.DistanceFromArrivalLS, 2);
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = bodyClass;
            cmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = row.Volcanism;
            cmd.Parameters.Add("@Landable", MySqlDbType.Int32).Value = Convert.ToByte(row.Landable);
            cmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = row.ReserveLevel;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        
        //signals
        public DataTable SelectSignalByName(UInt64 SKey, UInt64 BKey, Signal elem)
        {
            exeption = "";
            string name;
            name = elem.Type.Replace("$SAA_SignalType_", "");
            name = name.Replace(";", "");

            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Signals
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = name;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectAllSignals()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Signals";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public void InsertSignal(UInt64 SKey, UInt64 BKey, Signal elem)
        {
            exeption = "";
            string name;
            name = elem.Type.Replace("$SAA_SignalType_", "");
            name = name.Replace(";", "");

            cmd.CommandText = @"INSERT INTO Signals (SKey, BKey, Name, Count)
                                         VALUES (@SKey, @BKey, @Name, @Count)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.Int64).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = name;
            cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSignal(UInt64 SKey, UInt64 BKey, Signal elem)
        {
            exeption = "";
            string name;
            name = elem.Type.Replace("$SAA_SignalType_", "");
            name = name.Replace(";", "");

            cmd.CommandText = @"UPDATE Signals
                                SET Count = @Count
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.String).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = name;
            cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }

        //raw
        public int SelectRawCount(UInt64 SKey, UInt64 BKey, string name)
        {
            exeption = "";
            int count = 0;
            cmd.CommandText = @"SELECT Count(*)
                                FROM Raw
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = name;
            try
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return count;
        }
        public DataTable SelectAllRaw()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Raw";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public void InsertRaw(UInt64 SKey, UInt64 BKey, Material elem)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Raw (SKey, BKey, Name, Percent)
                                                            VALUES (@SKey, @BKey, @Name, @Percent)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Percent", MySqlDbType.Double).Value = elem.Percent;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateRaw(UInt64 SKey, UInt64 BKey, Material elem)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Raw
                                SET Percent = @Percent
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Percent", MySqlDbType.Double).Value = elem.Percent;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        //rings
        public DataTable SelectRingByName(UInt64 SKey, UInt64 BKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Rings
                                WHERE SKey = @SKey  AND 
                                      BKey = @BKey AND
                                      FullName = @FullName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = name;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectRingByName(UInt64 SKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Rings
                                WHERE SKey = @SKey  AND 
                                      FullName = @FullName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = name;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectAllRings()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Rings";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public UInt64 InsertRing(UInt64 SKey, UInt64 BKey, Ring elem, string ringName)
        {
            if (elem.RingClass == "Metalic") elem.RingClass = "Metallic";
            exeption = "";
            UInt64 RKey = 0;
            cmd.CommandText = @"INSERT INTO Rings (SKey, BKey, FullName, Name, Class)
                                       VALUES (@SKey, @BKey, @FullName, @Name, @Class)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = ringName;
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = elem.RingClass;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                cmd.CommandText = @"SELECT LAST_INSERT_ID() FROM Rings";
                cmd.Parameters.Clear();

                try
                {
                    RKey = Convert.ToUInt64(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    exeption = ex.Message + Environment.NewLine + cmd.CommandText;
                }
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            
            return RKey;
        }
        public void InsertEDSMRing(UInt64 SKey, UInt64 BKey, EDSMRing elem, string ringName)
        {
            if (elem.type == "Metalic") elem.type = "Metallic";
            exeption = "";
            cmd.CommandText = @"INSERT INTO Rings (SKey, BKey, FullName, Name, Class)
                                       VALUES (@SKey, @BKey, @FullName, @Name, @Class)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", MySqlDbType.String).Value = elem.name;
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = ringName;
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = elem.type.Replace(" ", "");
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }

        }
        public void UpdateRing(UInt64 SKey, UInt64 BKey, UInt64 RKey, Ring elem)
        {
            if (elem.RingClass == "Metalic") elem.RingClass = "Metallic";
            exeption = "";
            cmd.CommandText = @"UPDATE Rings
                                SET Class = @Class
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      RKey = @RKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@RKey", MySqlDbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Class", MySqlDbType.String).Value = elem.RingClass;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        //minerals
        public DataTable SelectMineralByName(UInt64 SKey, UInt64 BKey, UInt64 RKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Minerals
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      RKey = @RKey AND
                                      Type = @Type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@RKey", MySqlDbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = name;
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public DataTable SelectAllMinerals()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Minerals";
            cmd.Parameters.Clear();
            adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return dt;
        }
        public void InsertMineral(UInt64 SKey, UInt64 BKey, UInt64 RKey, Signal elem)
        {
            elem.Type = elem.Type.Substring(0, 1).ToUpper() + elem.Type.Substring(1);
            exeption = "";
            cmd.CommandText = @"INSERT INTO Minerals (SKey, BKey, RKey, Type, Count)
                                         VALUES (@SKey, @BKey, @RKey, @Type, @Count)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.Int64).Value = BKey;
            cmd.Parameters.Add("@RKey", MySqlDbType.Int64).Value = RKey;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = elem.Type;
            cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateMineral(UInt64 SKey, UInt64 BKey, UInt64 RKey, Signal elem)
        {
            elem.Type = elem.Type.Substring(0, 1).ToUpper() + elem.Type.Substring(1);
            exeption = "";
            cmd.CommandText = @"UPDATE Minerals
                                SET Count = @Count
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      RKey = @RKey AND
                                      Type = @Type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", MySqlDbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", MySqlDbType.Int64).Value = BKey;
            cmd.Parameters.Add("@RKey", MySqlDbType.String).Value = RKey;
            cmd.Parameters.Add("@Type", MySqlDbType.String).Value = elem.Type;
            cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;
            //transaction
            tr = conn.BeginTransaction();
            cmd.Transaction = tr;
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch (MySqlException ex)
            {
                tr.Rollback();
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
    }
}

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
    public class LocalBase
    {
        private DataTable dt;
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter adapter;

        public string exeption;

        public LocalBase()
        {
            exeption = "";
            try
            {
                conn = new SQLiteConnection(@"Data Source=dbfiles\edsys.sqlite;Version=3;");
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
        ~LocalBase()
        {
            try
            {
                conn.Close();
            }
            catch { };
            conn.Dispose();
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

        //filters
        public DataTable SelectDistinctRaw()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT Name FROM Raw";
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
        public DataTable SelectDistinctMinerals()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT Type FROM Minerals";
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
        public DataTable SelectDistinctRings()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT Class FROM Rings";
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
        public DataTable SelectDistinctBodies()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT Class FROM Bodies
                                WHERE Type = @Type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Type", DbType.String).Value = "Planet";
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

        //systems
        public DataTable SelectSystemByKey(UInt64 SKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Systems
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
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
        public DataTable SelectSystems(double cx, double cy, double cz, double radius)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Systems
                                WHERE (X - @cx) * (X - @cx) + 
                                      (Y - @cy) * (Y - @cy) +
		                              (Z - @cz) * (Z - @cz) < @radius * @radius";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@cx", DbType.Double).Value = cx;
            cmd.Parameters.Add("@cy", DbType.Double).Value = cy;
            cmd.Parameters.Add("@cz", DbType.Double).Value = cz;
            cmd.Parameters.Add("@radius", DbType.Double).Value = radius;
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
        public DataTable SelectSystemsForMap(double minX, double maxX, double minZ, double maxZ)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT t1.Name, t1.X, t1.Y, t1.Z, t2.Class
                                FROM Systems t1, Bodies t2
                                WHERE t1.X > @minX AND t1.X < @maxX AND
                                      t1.Z > @minZ AND t1.Z < @maxZ AND
                                      t1.SKey = t2.SKey AND
                                      t2.Distance = @Dist";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@minX", DbType.Double).Value = minX;
            cmd.Parameters.Add("@maxX", DbType.Double).Value = maxX;
            cmd.Parameters.Add("@minZ", DbType.Double).Value = minZ;
            cmd.Parameters.Add("@maxZ", DbType.Double).Value = maxZ;
            cmd.Parameters.Add("@Dist", DbType.Double).Value = 0;
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

        public DataTable SelectAllSystemsForMap()
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT t1.Name, t1.X, t1.Y, t1.Z, t2.Class
                                FROM Systems t1, Bodies t2
                                WHERE t1.SKey = t2.SKey AND
                                      t2.Distance = @Dist";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Dist", DbType.Double).Value = 0;
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
        public DataTable SelectSystemsByRaw(double cx, double cy, double cz, double radius, string rawName)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT t1.*
                                FROM Systems t1, Raw t2
                                WHERE t1.SKey=t2.SKey AND t2.Name = @Name AND
                                          (t1.X - @X) * (t1.X - @X) + 
                                          (t1.Y - @Y) * (t1.Y - @Y) +
		                                  (t1.Z - @Z) * (t1.Z - @Z) < @radius * @radius";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@X", DbType.Double).Value = cx;
            cmd.Parameters.Add("@Y", DbType.Double).Value = cy;
            cmd.Parameters.Add("@Z", DbType.Double).Value = cz;
            cmd.Parameters.Add("@radius", DbType.Double).Value = radius;
            cmd.Parameters.Add("@Name", DbType.String).Value = rawName;
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
        public DataTable SelectSystemsByMineral(double cx, double cy, double cz, double radius, string mineralName)
        {
            exeption = "";
            dt = new DataTable();
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT t1.*
                                FROM Systems t1, Minerals t2
                                WHERE t1.SKey=t2.SKey AND t2.Type = @Type AND
                                          (t1.X - @X) * (t1.X - @X) + 
                                          (t1.Y - @Y) * (t1.Y - @Y) +
		                                  (t1.Z - @Z) * (t1.Z - @Z) < @radius * @radius";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@X", DbType.Double).Value = cx;
            cmd.Parameters.Add("@Y", DbType.Double).Value = cy;
            cmd.Parameters.Add("@Z", DbType.Double).Value = cz;
            cmd.Parameters.Add("@radius", DbType.Double).Value = radius;
            cmd.Parameters.Add("@Type", DbType.String).Value = mineralName;
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
        public DataTable SelectSystemsByRing(double cx, double cy, double cz, double radius, string ringName)
        {
            exeption = "";
            dt = new DataTable();
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT t1.*
                                FROM Systems t1, Rings t2
                                WHERE t1.SKey=t2.SKey AND t2.Class = @Class AND
                                          (t1.X - @X) * (t1.X - @X) + 
                                          (t1.Y - @Y) * (t1.Y - @Y) +
		                                  (t1.Z - @Z) * (t1.Z - @Z) < @radius * @radius";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@X", DbType.Double).Value = cx;
            cmd.Parameters.Add("@Y", DbType.Double).Value = cy;
            cmd.Parameters.Add("@Z", DbType.Double).Value = cz;
            cmd.Parameters.Add("@radius", DbType.Double).Value = radius;
            cmd.Parameters.Add("@Class", DbType.String).Value = ringName;
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
        public DataTable SelectSystemsByBody(double cx, double cy, double cz, double radius, string bodyClass)
        {
            exeption = "";
            dt = new DataTable();
            dt = new DataTable();
            cmd.CommandText = @"SELECT DISTINCT t1.*
                                FROM Systems t1, Bodies t2
                                WHERE t1.SKey=t2.SKey AND t2.Class = @Class AND
                                          (t1.X - @X) * (t1.X - @X) + 
                                          (t1.Y - @Y) * (t1.Y - @Y) +
		                                  (t1.Z - @Z) * (t1.Z - @Z) < @radius * @radius";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@X", DbType.Double).Value = cx;
            cmd.Parameters.Add("@Y", DbType.Double).Value = cy;
            cmd.Parameters.Add("@Z", DbType.Double).Value = cz;
            cmd.Parameters.Add("@radius", DbType.Double).Value = radius;
            cmd.Parameters.Add("@Class", DbType.String).Value = bodyClass;
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

        public void InsertSystem(FSDJump row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z, timestamp)
                                VALUES (@SKey, @Name, @X, @Y, @Z, @timestamp)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertSystem(CarrierJump row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z, timestamp)
                                VALUES (@SKey, @Name, @X, @Y, @Z, @timestamp)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertSystem(Location row)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Systems (SKey, Name, X, Y, Z, timestamp)
                                VALUES (@SKey, @Name, @X, @Y, @Z, @timestamp)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }


        public void UpdateSystem(FSDJump row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z, timestamp = @timestamp
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystem(CarrierJump row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z, timestamp = @timestamp
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystem(Location row)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Name = @Name, X = @X, Y = @Y, Z = @Z, timestamp = @timestamp
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = row.SystemAddress;
            cmd.Parameters.Add("@Name", DbType.String).Value = row.StarSystem;
            cmd.Parameters.Add("@X", DbType.Double).Value = row.StarPos[0];
            cmd.Parameters.Add("@Y", DbType.Double).Value = row.StarPos[1];
            cmd.Parameters.Add("@Z", DbType.Double).Value = row.StarPos[2];
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = row.timestamp;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateSystemComment(UInt64 SKey, string comment)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Systems
                                SET Comment = @Comment
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@Comment", DbType.String).Value = comment;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }

        //bodies
        public DataTable SelectBodyByName(UInt64 SKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Bodies
                                WHERE SKey = @SKey AND
                                      FullName = @FullName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = name;
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
        public DataTable SelectBodies(UInt64 SKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Bodies
                                WHERE SKey = @SKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
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
        public DataTable SelectBodiesByRaw(UInt64 SKey, string rawName)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT t1.*
                                FROM Bodies t1, Raw t2
                                WHERE t1.SKey = @SKey AND
                                      t1.SKey = t2.SKey AND
                                      t1.BKey = t2.BKey AND
                                      t2.Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = rawName;
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
        public DataTable SelectBodiesByMineral(UInt64 SKey, string mineralName)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT t1.*
                                FROM Bodies t1, Minerals t2
                                WHERE t1.SKey = @SKey AND
                                      t1.SKey = t2.SKey AND
                                      t1.BKey = t2.BKey AND
                                      t2.Type = @Type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@Type", DbType.String).Value = mineralName;
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
        public DataTable SelectBodiesByRing(UInt64 SKey, string ringlName)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT t1.*
                                FROM Bodies t1, Rings t2
                                WHERE t1.SKey = @SKey AND
                                      t1.SKey = t2.SKey AND
                                      t1.BKey = t2.BKey AND
                                      t2.Class = @Class
                                GROUP BY t1.BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@Class", DbType.String).Value = ringlName;
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
        public DataTable SelectBodiesByClass(UInt64 SKey, string bodyClass)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Bodies
                                WHERE SKey = @SKey AND
                                      Class = @Class";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@Class", DbType.String).Value = bodyClass;
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
                bodyClass = row.StarType + row.Subclass.ToString() + " (" + row.Luminosity + ")";
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = row.BodyName;
            cmd.Parameters.Add("@Name", DbType.String).Value = planetName;
            cmd.Parameters.Add("@Type", DbType.String).Value = type;
            cmd.Parameters.Add("@Class", DbType.String).Value = bodyClass;
            cmd.Parameters.Add("@Distance", DbType.Double).Value = Math.Round(row.DistanceFromArrivalLS, 2);
            cmd.Parameters.Add("@Volcanism", DbType.String).Value = row.Volcanism;
            cmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(row.Landable);
            cmd.Parameters.Add("@Reserve", DbType.String).Value = row.ReserveLevel;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            //cmd.CommandText = @"SELECT LAST_INSERT_ID()";
            cmd.CommandText = @"SELECT seq FROM sqlite_sequence WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = "Bodies";
            try
            {
                BKey = Convert.ToUInt64(cmd.ExecuteScalar());
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return BKey;
        }
        public UInt64 InsertEDSMBody(UInt64 SKey, EDSMBody row, string sysName)
        {
            exeption = "";
            UInt64 BKey = 0;
            
            int landable = (row.isLandable ?? false) ? 1 : 0;

            if (row.volcanismType == null) row.volcanismType = "";
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = row.name;
            cmd.Parameters.Add("@Name", DbType.String).Value = planetName;
            cmd.Parameters.Add("@Type", DbType.String).Value = row.type;
            cmd.Parameters.Add("@Class", DbType.String).Value = row.subType;
            cmd.Parameters.Add("@Distance", DbType.Double).Value = row.distanceToArrival;
            cmd.Parameters.Add("@Volcanism", DbType.String).Value = row.volcanismType;
            cmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(landable);
            cmd.Parameters.Add("@Reserve", DbType.String).Value = "";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            //cmd.CommandText = @"SELECT LAST_INSERT_ID()";
            cmd.CommandText = @"SELECT seq FROM sqlite_sequence WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = "Bodies";
            try
            {
                BKey = Convert.ToUInt64(cmd.ExecuteScalar());
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
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
                bodyClass = row.StarType + row.Subclass.ToString() + " (" + row.Luminosity + ")";
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.String).Value = BKey;
            cmd.Parameters.Add("@Type", DbType.String).Value = type;
            cmd.Parameters.Add("@Distance", DbType.Double).Value = Math.Round(row.DistanceFromArrivalLS, 2);
            cmd.Parameters.Add("@Class", DbType.String).Value = bodyClass;
            cmd.Parameters.Add("@Volcanism", DbType.String).Value = row.Volcanism;
            cmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(row.Landable);
            cmd.Parameters.Add("@Reserve", DbType.String).Value = row.ReserveLevel;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateBodiesComment(UInt64 SKey, UInt64 BKey,  string comment)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Bodies
                                SET Comment = @Comment
                                WHERE SKey = @SKey AND
                                      BKey = @BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.String).Value = BKey;
            cmd.Parameters.Add("@Comment", DbType.String).Value = comment;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = name;
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
        public DataTable SelectSignals(UInt64 SKey, UInt64 BKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Signals
                                WHERE SKey = @SKey AND
                                      BKey = @BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
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
        public void InsertSignal(UInt64 SKey, UInt64 BKey, Signal elem)
        {
            exeption = "";
            string name;
            name = elem.Type.Replace("$SAA_SignalType_", "");
            name = name.Replace(";", "");

            cmd.CommandText = @"INSERT INTO Signals (SKey, BKey, Name, Count)
                                         VALUES (@SKey, @BKey, @Name, @Count)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.Int64).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = name;
            cmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.String).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = name;
            cmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }

        //raw
        public DataTable SelectRawByName(UInt64 SKey, UInt64 BKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Raw
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      Name = @Name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = name;
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
        public DataTable SelectRaw(UInt64 SKey, UInt64 BKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Raw
                                WHERE SKey = @SKey AND
                                      BKey = @BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
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
        public void InsertRaw(UInt64 SKey, UInt64 BKey, Material elem)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO Raw (SKey, BKey, Name, Percent)
                                                            VALUES (@SKey, @BKey, @Name, @Percent)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Percent", DbType.Double).Value = elem.Percent;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Percent", DbType.Double).Value = elem.Percent;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = name;
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
        public DataTable SelectRingByName(UInt64 SKey, string name)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Rings
                                WHERE SKey = @SKey  AND 
                                      FullName = @FullName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = name;
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
        public DataTable SelectRings(UInt64 SKey, UInt64 BKey)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Rings
                                WHERE SKey = @SKey  AND 
                                      BKey = @BKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
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
        public UInt64 InsertRing(UInt64 SKey, UInt64 BKey, Ring elem, string ringName)
        {
            if (elem.RingClass == "Metalic") elem.RingClass = "Metallic";
            exeption = "";
            UInt64 RKey = 0;
            cmd.CommandText = @"INSERT INTO Rings (SKey, BKey, FullName, Name, Class)
                                       VALUES (@SKey, @BKey, @FullName, @Name, @Class)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = elem.Name;
            cmd.Parameters.Add("@Name", DbType.String).Value = ringName;
            cmd.Parameters.Add("@Class", DbType.String).Value = elem.RingClass;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            cmd.CommandText = @"SELECT seq FROM sqlite_sequence WHERE name = @name";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@name", DbType.String).Value = "Rings";
            try
            {
                RKey = Convert.ToUInt64(cmd.ExecuteScalar());
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            return RKey;
        }
        public void InsertEDSMRing(UInt64 SKey, UInt64 BKey, EDSMRing elem, string ringName)
        {
            if (elem.type == "Metalic") elem.type = "Metallic";
            exeption = "";
            cmd.CommandText = @"INSERT INTO Rings (SKey, BKey, FullName, Name, Class)
                                       VALUES (@SKey, @BKey, @FullName, @Name, @Class)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@FullName", DbType.String).Value = elem.name;
            cmd.Parameters.Add("@Name", DbType.String).Value = ringName;
            cmd.Parameters.Add("@Class", DbType.String).Value = elem.type.Replace(" ", "");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Class", DbType.String).Value = elem.RingClass;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateRingsComment(UInt64 SKey, UInt64 BKey, UInt64 RKey, string comment)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Rings
                                SET Comment = @Comment
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      RKey = @RKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.String).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Comment", DbType.String).Value = comment;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Type", DbType.String).Value = name;
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
        public DataTable SelectMinerals(UInt64 SKey, UInt64 BKey, UInt64 RKeу)
        {
            exeption = "";
            dt = new DataTable();
            cmd.CommandText = @"SELECT *
                                FROM Minerals
                                WHERE SKey = @SKey  AND 
                                      BKey = @BKey  AND
                                      RKey = @RKeу";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.UInt64).Value = BKey;
            cmd.Parameters.Add("@RKeу", DbType.UInt64).Value = RKeу;
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
        public void InsertMineral(UInt64 SKey, UInt64 BKey, UInt64 RKey, Signal elem)
        {
            elem.Type = elem.Type.Substring(0, 1).ToUpper() + elem.Type.Substring(1);
            exeption = "";
            cmd.CommandText = @"INSERT INTO Minerals (SKey, BKey, RKey, Type, Count)
                                         VALUES (@SKey, @BKey, @RKey, @Type, @Count)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.Int64).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.Int64).Value = RKey;
            cmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;
            cmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
            cmd.Parameters.Add("@SKey", DbType.Int64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.Int64).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.String).Value = RKey;
            cmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;
            cmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateMineralsComment(UInt64 SKey, UInt64 BKey, UInt64 RKey, string comment)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE Minerals
                                SET Comment = @Comment
                                WHERE SKey = @SKey AND
                                      BKey = @BKey AND
                                      RKey = @RKey";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@SKey", DbType.UInt64).Value = SKey;
            cmd.Parameters.Add("@BKey", DbType.String).Value = BKey;
            cmd.Parameters.Add("@RKey", DbType.UInt64).Value = RKey;
            cmd.Parameters.Add("@Comment", DbType.String).Value = comment;
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

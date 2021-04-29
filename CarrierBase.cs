using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems_v2
{
    class CarrierBase
    {
        private DataTable dt;
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter adapter;

        public string exeption;

        public CarrierBase()
        {
            exeption = "";
            try
            {
                conn = new SQLiteConnection(@"Data Source=dbfiles\carrier.sqlite;Version=3;");
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
        ~CarrierBase()
        {
            try
            {
                conn.Close();
            }
            catch { };
            conn.Dispose();
        }

        public DataTable SelectCarrier(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM carriers WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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
        public DataTable SelectCarriers()
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM carriers";
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
        public void InsertCarrier(CarrierBuy info)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO carriers (id, callsign, currentsystem)
                                VALUES (@id, @callsign, @currentsystem)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@callsign", DbType.String).Value = info.Callsign;
            cmd.Parameters.Add("@currentsystem", DbType.String).Value = info.Location;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO finance (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO space (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO stats (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void InsertCarrier(CarrierStats info)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO carriers (id, callsign, name)
                                VALUES (@id, @callsign, @name)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@callsign", DbType.String).Value = info.Callsign;
            cmd.Parameters.Add("@name", DbType.String).Value = info.Name;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO finance (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO space (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            exeption = "";
            cmd.CommandText = @"INSERT INTO stats (id) VALUES (@id)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateCarreerStats(CarrierStats info)
        {
            dt = new DataTable();
            exeption = "";
            cmd.CommandText = @"UPDATE carriers
                                SET callsign = @callsign,
                                    name = @name
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@callsign", DbType.String).Value = info.Callsign;
            cmd.Parameters.Add("@name", DbType.String).Value = info.Name;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            exeption = "";
            cmd.CommandText = @"UPDATE finance
                                SET balance = @balance,
                                    reserve = @reserve,
                                    available = @available
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@balance", DbType.Int64).Value = info.Finance.CarrierBalance;
            cmd.Parameters.Add("@reserve", DbType.Int64).Value = info.Finance.ReserveBalance;
            cmd.Parameters.Add("@available", DbType.Int64).Value = info.Finance.AvailableBalance;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            exeption = "";
            cmd.CommandText = @"UPDATE space
                                SET total = @total,
                                    crew = @crew,
                                    cargo = @cargo,
                                    cargoreserve = @cargoreserve,
                                    shippaks = @shippaks,
                                    modulepaks = @modulepaks,
                                    free = @free
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@total", DbType.Int32).Value = info.SpaceUsage.TotalCapacity;
            cmd.Parameters.Add("@crew", DbType.Int32).Value = info.SpaceUsage.Crew;
            cmd.Parameters.Add("@cargo", DbType.Int32).Value = info.SpaceUsage.Cargo;
            cmd.Parameters.Add("@cargoreserve", DbType.Int32).Value = info.SpaceUsage.CargoSpaceReserved;
            cmd.Parameters.Add("@shippaks", DbType.Int32).Value = info.SpaceUsage.ShipPacks;
            cmd.Parameters.Add("@modulepaks", DbType.Int32).Value = info.SpaceUsage.ModulePacks;
            cmd.Parameters.Add("@free", DbType.Int32).Value = info.SpaceUsage.FreeSpace;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            //
            exeption = "";
            cmd.CommandText = @"UPDATE stats
                                SET dockingaccess = @dockingaccess,
                                    allownotorious = @allownotorious,
                                    fuel = @fuel,
                                    range = @range,
                                    rangemax = @rangemax,
                                    pendingdecommission = @pendingdecommission
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
            cmd.Parameters.Add("@dockingaccess", DbType.String).Value = info.DockingAccess;
            cmd.Parameters.Add("@allownotorious", DbType.Boolean).Value = info.AllowNotorious;
            cmd.Parameters.Add("@fuel", DbType.Int32).Value = info.FuelLevel;
            cmd.Parameters.Add("@range", DbType.Int32).Value = info.JumpRangeCurr;
            cmd.Parameters.Add("@rangemax", DbType.Int32).Value = info.JumpRangeMax;
            cmd.Parameters.Add("@pendingdecommission", DbType.Int32).Value = info.PendingDecommission;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
            foreach(Crew elem in info.Crew)
            {
                exeption = "";

                cmd.CommandText = @"SELECT * FROM crew WHERE id = @id AND role = @role";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
                cmd.Parameters.Add("@role", DbType.String).Value = elem.CrewRole;
                adapter = new SQLiteDataAdapter(cmd);
                try
                {
                    adapter.Fill(dt);
                }
                catch (SQLiteException ex)
                {
                    exeption = ex.Message + Environment.NewLine + cmd.CommandText;
                }
                if(dt.Rows.Count == 0)
                {
                    exeption = "";
                    cmd.CommandText = @"INSERT INTO crew (id, role, activated, enabled, name)
                                VALUES (@id, @role, @activated, @enabled, @name)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
                    cmd.Parameters.Add("@role", DbType.String).Value = elem.CrewRole;
                    cmd.Parameters.Add("@activated", DbType.Boolean).Value = elem.Activated;
                    cmd.Parameters.Add("@enabled", DbType.Boolean).Value = elem.Enabled;
                    cmd.Parameters.Add("@name", DbType.String).Value = elem.CrewName;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        exeption = ex.Message + Environment.NewLine + cmd.CommandText;
                    }
                }
                else
                {
                    exeption = "";
                    cmd.CommandText = @"UPDATE crew
                                        SET activated = @activated,
                                            enabled = @enabled,
                                            name = @name
                                        WHERE id = @id AND role = @role";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@id", DbType.Int64).Value = info.CarrierID;
                    cmd.Parameters.Add("@role", DbType.String).Value = elem.CrewRole;
                    cmd.Parameters.Add("@activated", DbType.Boolean).Value = elem.Activated;
                    cmd.Parameters.Add("@enabled", DbType.Boolean).Value = elem.Enabled;
                    cmd.Parameters.Add("@name", DbType.String).Value = elem.CrewName;
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
        public void UpdateCurrent(Int64 id, string name, string body, UInt64 syskey, bool visited)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE carriers
                                SET system = @system,
                                    body = @body,
                                    syskey = @syskey,
                                    visited = @visited
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@system", DbType.String).Value = name;
            cmd.Parameters.Add("@body", DbType.String).Value = body;
            cmd.Parameters.Add("@syskey", DbType.Int64).Value = syskey;
            cmd.Parameters.Add("@visited", DbType.Boolean).Value = visited;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateVisited(long id, bool visited)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE carriers
                                SET visited = @visited
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@visited", DbType.Boolean).Value = visited;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateCarreerCredits(Int64 id, Int64 credits)
        {
            exeption = "";
            cmd.CommandText = @"UPDATE finance
                                SET balance = @balance
                                WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@balance", DbType.Int64).Value = credits;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public DataTable SelectFinance(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM finance WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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
        public DataTable SelectSpace(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM space WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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
        public DataTable SelectStats(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM stats WHERE id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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
        public DataTable SelectCrew(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM crew WHERE id = @id ORDER BY role ASC";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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

        public DataTable SelectCargo(Int64 id)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM cargo WHERE id = @id AND count > 0";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
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
        public DataTable SelectCargoByType(Int64 id, string type)
        {
            exeption = "";
            dt = new DataTable();

            cmd.CommandText = @"SELECT * FROM cargo WHERE id = @id AND type = @type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@type", DbType.String).Value = type;
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
        public void InsertCargo(Int64 id, Transfer info, DateTime timestamp)
        {
            exeption = "";
            cmd.CommandText = @"INSERT INTO cargo (id, timestamp, type, count, localise)
                                VALUES (@id, @timestamp, @type, @count, @localise)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = timestamp;
            cmd.Parameters.Add("@type", DbType.String).Value = info.Type;
            cmd.Parameters.Add("@count", DbType.Int32).Value = info.Count;
            cmd.Parameters.Add("@localise", DbType.String).Value = info.Type_Localised;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateCargo(Int64 id, Transfer info, DateTime timestamp)
        {
            exeption = "";
            int count = 0;
            if (info.Direction == "tocarrier")
                count = info.Count;
            else
                count = -info.Count;

            cmd.CommandText = @"UPDATE cargo
                                SET count = CASE
                                            WHEN (count + @count) > 0
                                            THEN count + @count
                                            ELSE 0
                                            END,
                                    timestamp = @timestamp
                                WHERE id = @id AND type = @type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@count", DbType.Int32).Value = count;
            cmd.Parameters.Add("@timestamp", DbType.DateTime).Value = timestamp;
            cmd.Parameters.Add("@type", DbType.String).Value = info.Type;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                exeption = ex.Message + Environment.NewLine + cmd.CommandText;
            }
        }
        public void UpdateCargo(Int64 id, string type, int count)
        {
            exeption = "";
           
            cmd.CommandText = @"UPDATE cargo
                                SET count = @count
                                WHERE id = @id AND type = @type";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", DbType.Int64).Value = id;
            cmd.Parameters.Add("@count", DbType.Int32).Value = count;
            cmd.Parameters.Add("@type", DbType.String).Value = type;

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

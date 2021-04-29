using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED_Systems_v2
{
    public partial class Main : Form
    {
        LocalBase lb;
        GlobalBase gb;
        CMDRBase cb;
        InfoBase ib;
        CarrierBase crb;
        double cx = 0; //current x
        double cy = 0; //current y
        double cz = 0; //current z
        double fcx = 0; //fc current x
        double fcy = 0; //fc current y
        double fcz = 0; //fc current z
        ulong oldFCSysAddr = 0;
        string oldFCSysName = "";
        string oldFCSysBody = "";
        bool notVisited = true;

        private bool loadingData;
        DataTable filterRaw;
        DataTable filterMinerals;
        DataTable filterRings;
        DataTable filterBodies;
        DBTables dbt = new DBTables();
        private int systemsRowIndex = 0;
        private int bodiesRowIndex = 0;
        private int ringsRowIndex = 0;
        //reputation
        int federationReputation;
        int empireReputation;
        int allianceReputation;
        //progress
        int federationProgress;
        int empireProgress;
        int allianceProgress;
        int combatProgress;
        int tradeProgress;
        int exploreProgress;
        int cqcProgress;

        long carrierID = 0;

        string logCMDR;
        long logFC = 0;
        //List<string> screens = new List<string>(); 
        //settings
        string donateAddr = "";
        string host = "";
        string port = "";
        string db = "";
        string user = "";
        string pwd = "";
        //-------------------
        string logPath = "";
        string lastLog = "";
        string bakPath = "";
        string toPath = "";
        string fromPath = "";
        int maxRadius = 500;
        ulong lastTime = 0;
        bool bkgRead = false;
        int bkgLastLine = -1;
        //-------------------
        string cmdrName = "";
        ulong cmdrLastKey = 0;
        string cmdrLastName = "";
        //-------------------
        ulong fcLastKey = 0;
        bool visited = true;

        //events
        FSDJump fsdJump = new FSDJump();
        CarrierJump carrierJump = new CarrierJump();
        Scan scan = new Scan();
        SAASignalsFound saaSignals = new SAASignalsFound();
        Event ev = new Event();
        Commander cmdr = new Commander();
        LoadGame load = new LoadGame();
        Rank rank = new Rank();
        Reputation reputation = new Reputation();
        Progress progress = new Progress();
        CarrierStats carrierStats = new CarrierStats();
        CmdrMaterials materials = new CmdrMaterials();
        MaterialCollected materialCollected = new MaterialCollected();
        EngineerCraft engineerCraft = new EngineerCraft();
        Synthesis synthesis = new Synthesis();
        MaterialTrade materialTrade = new MaterialTrade();
        BuyAmmo buyAmmo = new BuyAmmo();
        RefuelAll refuelAll = new RefuelAll();
        Repair repair = new Repair();
        BuyDrones buyDrones = new BuyDrones();
        Resurrect resurrect = new Resurrect();
        FetchRemoteModule fetchRemoteModule = new FetchRemoteModule();
        MarketBuy marketBuy = new MarketBuy();
        MarketSell marketSell = new MarketSell();
        MissionCompleted missionCompleted = new MissionCompleted();
        ModuleBuy moduleBuy = new ModuleBuy();
        ModuleSell moduleSell = new ModuleSell();
        ModuleSellRemote moduleSellRemote = new ModuleSellRemote();
        MultiSellExplorationData multiSellExplorationData = new MultiSellExplorationData();
        PayBounties payBounties = new PayBounties();
        PayFines payFines = new PayFines();
        PowerplayFastTrack powerplayFastTrack = new PowerplayFastTrack();
        PowerplaySalary powerplaySalary = new PowerplaySalary();
        RedeemVoucher redeemVoucher = new RedeemVoucher();
        RepairAll repairAll = new RepairAll();
        SellDrones sellDrones = new SellDrones();
        ShipyardBuy shipyardBuy = new ShipyardBuy();
        ShipyardSell shipyardSell = new ShipyardSell();
        ShipyardTransfer shipyardTransfer = new ShipyardTransfer();
        CarrierBuy carrierBuy = new CarrierBuy();
        CarrierBankTransfer carrierBankTransfer = new CarrierBankTransfer();
        CarrierJumpRequest carrierJumpRequest = new CarrierJumpRequest();
        CargoTransfer cargoTransfer = new CargoTransfer();
        CarrierJumpCancelled carrierJumpCancelled = new CarrierJumpCancelled();
        Screenshot screenshot = new Screenshot();
        Location location = new Location();
        public Main()
        {
            InitializeComponent();

            ib = new InfoBase();
            if (InfoBaseExeption()) return;
            cb = new CMDRBase();
            if (CMDRBaseExeption()) return;
            lb = new LocalBase();
            if (LocalBaseExeption()) return;
            crb = new CarrierBase();
            if (CarrierBaseExeption()) return;

            dgvSystems.DataSource = dbt.systems;
            dgvSystems.AutoGenerateColumns = false;

            dgvBodies.DataSource = dbt.bodies;
            dgvBodies.AutoGenerateColumns = false;

            dgvSignals.DataSource = dbt.signals;
            dgvSignals.AutoGenerateColumns = false;

            dgvRaw.DataSource = dbt.raw;
            dgvRaw.AutoGenerateColumns = false;

            dgvRings.DataSource = dbt.rings;
            dgvRings.AutoGenerateColumns = false;

            dgvMinerals.DataSource = dbt.minerals;
            dgvMinerals.AutoGenerateColumns = false;

            dgvCargo.AutoGenerateColumns = false;

            dbt.craw = ib.SelectRaw();
            if (InfoBaseExeption()) return;
            dgvCmdrRaw.DataSource = dbt.craw;
            dgvCmdrRaw.AutoGenerateColumns = false;
            dgvCmdrRaw.Sort(dgvCmdrRaw.Columns["dgvcCmdrRawRu"], ListSortDirection.Ascending);

            dbt.cencoded = ib.SelectEncoded();
            if (InfoBaseExeption()) return;
            dgvCmdrEncoded.DataSource = dbt.cencoded;
            dgvCmdrEncoded.AutoGenerateColumns = false;
            dgvCmdrEncoded.Sort(dgvCmdrEncoded.Columns["dgvcCmdrEncodedRu"], ListSortDirection.Ascending);

            dbt.cmanuf = ib.SelectManuf();
            if (InfoBaseExeption()) return;
            dgvCmdrManuf.DataSource = dbt.cmanuf;
            dgvCmdrManuf.AutoGenerateColumns = false;
            dgvCmdrManuf.Sort(dgvCmdrManuf.Columns["dgvcManufRu"], ListSortDirection.Ascending);

            Uri url = new Uri(@"http://edspace.ga/starsinfo/index.html");
            webBrowserStars.Url = url;

            //settings
            DataTable dt = new DataTable();
            dt = ib.SelectProgramSettings();
            if(ib.exeption == "")
            {
                donateAddr = dt.Rows[0]["DonateAddr"].ToString();
                host = dt.Rows[0]["MySqlHost"].ToString();
                port = dt.Rows[0]["MySqlPort"].ToString();
                db = dt.Rows[0]["MySqlDb"].ToString();
                user = dt.Rows[0]["MySqlUser"].ToString();
                pwd = dt.Rows[0]["MySqlPwd"].ToString();
            }
            else
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + ib.exeption);
            }
            dt = ib.SelectUserSettings();
            if (ib.exeption == "")
            {
                logPath = dt.Rows[0]["LogPath"].ToString();
                lastLog = dt.Rows[0]["LastLog"].ToString();
                bakPath = dt.Rows[0]["BackupPath"].ToString();
                toPath = dt.Rows[0]["ToImgPath"].ToString();
                fromPath = dt.Rows[0]["FromImgPath"].ToString();
                maxRadius = Convert.ToInt32(dt.Rows[0]["MaxRadius"]);
                lastTime = Convert.ToUInt64(dt.Rows[0]["LastTimestamp"]);
                bkgRead = Convert.ToBoolean(dt.Rows[0]["BkgRead"]);
                bkgLastLine = Convert.ToInt32(dt.Rows[0]["BkgLastLine"]);
            }
            else
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + ib.exeption);
            }
        }
        private bool LocalBaseExeption()
        {
            if (lb.exeption != "")
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + lb.exeption);
                return true;
            }
            return false;
        }
        private bool InfoBaseExeption()
        {
            if (ib.exeption != "")
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + ib.exeption);
                return true;
            }
            return false;
        }
        private bool CMDRBaseExeption()
        {
            if (cb.exeption != "")
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + cb.exeption);
                return true;
            }
            return false;
        }
        private bool CarrierBaseExeption()
        {
            if (crb.exeption != "")
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + crb.exeption);
                return true;
            }
            return false;
        }
        private bool GlobalBaseExeption()
        {
            if (gb.exeption != "")
            {
                MessageBox.Show("Ошибка MySQL:\r\n" + gb.exeption);
                //return true;
            }
            return false;
        }
        private ulong ToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Convert.ToUInt64(Math.Floor(diff.TotalSeconds));
        }
        private void Main_Load(object sender, EventArgs e)
        {
            DataTable dt;
            LoadCMDRs();
            LoadCarriers();

            toolStripStatusLastLog.Text = lastLog;
            toolStripStatusLogPath.Text = logPath;
            lblSystems.Text = "Систем 0 Радиус поиска " +
                maxRadius.ToString() + " св.л.";

            tbxScreenFrom.Text = fromPath;
            tbxScreenTo.Text = toPath;
            tbxBackup.Text = bakPath;

            filterRaw = lb.SelectDistinctRaw();
            if (LocalBaseExeption()) return;
            filterMinerals = lb.SelectDistinctMinerals();
            if (LocalBaseExeption()) return;
            filterRings = lb.SelectDistinctRings();
            if (LocalBaseExeption()) return;
            filterBodies = lb.SelectDistinctBodies();
            if (LocalBaseExeption()) return;

            dt = lb.SelectSystemByKey(cmdrLastKey);
            if(lb.exeption == "" && dt.Rows.Count > 0)
            {
                toolStripStatusCurrent.Text = dt.Rows[0]["Name"].ToString();
                cx = Convert.ToDouble(dt.Rows[0]["X"]);
                cy = Convert.ToDouble(dt.Rows[0]["Y"]);
                cz = Convert.ToDouble(dt.Rows[0]["Z"]);
            }

            if(fcLastKey != 0)
            {
                dt = lb.SelectSystemByKey(fcLastKey);
                if (LocalBaseExeption()) return;
                if (dt.Rows.Count > 0)
                {
                    fcx = Convert.ToDouble(dt.Rows[0]["X"]);
                    fcy = Convert.ToDouble(dt.Rows[0]["Y"]);
                    fcz = Convert.ToDouble(dt.Rows[0]["Z"]);
                }
   
            }

            FillMiningFilter(filterRaw);
            LoadSystems();

            //watches
            if (bkgRead) readLogTimer.Start();
            if (logPath == "")
            {
                fswDir.EnableRaisingEvents = false;
                readLogTimer.Stop();
            }
            try
            {
                fswDir.Path = logPath;
            }
            catch(Exception ex)
            {
                fswDir.EnableRaisingEvents = false;
            }
            chkEnableBkgRead.Checked = bkgRead;
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private void LoadCMDRs()
        {
            DataTable dt;
       
            dt = cb.SelectCMDRs();
            if (cb.exeption == "")
            {
                foreach (DataRow row in dt.Rows)
                {
                    cbxCMDR.Items.Add(row["name"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + cb.exeption);
            }
            if (cbxCMDR.Items.Count > 0)
            {
                cbxCMDR.SelectedItem = cbxCMDR.Items[0];
            }
        }
        private void LoadCarriers()
        {
            DataTable dt;
            dt = crb.SelectCarriers();
            if(crb.exeption == "")
            {
                foreach (DataRow row in dt.Rows)
                {
                    cbxCarrier.Items.Add(row["name"].ToString() +
                        " " + row["callsign"].ToString() +
                        " (" + row["id"].ToString() + ")");
                }
            }
            else
            {
                MessageBox.Show("Ошибка локальной базы:\r\n" + crb.exeption);
            }
            if(cbxCarrier.Items.Count > 0)
            {
                cbxCarrier.SelectedItem = cbxCarrier.Items[0];
            }
        }
        private void FillMiningFilter(DataTable dt)
        {
            cbxMiningFilter.Items.Clear();
            cbxMiningFilter.Items.Add("");
            foreach (DataRow row in dt.Rows)
            {
                if (rbRaw.Checked) cbxMiningFilter.Items.Add(row["Name"].ToString());
                if (rbMinerals.Checked) cbxMiningFilter.Items.Add(row["Type"].ToString());
                if (rbRings.Checked) cbxMiningFilter.Items.Add(row["Class"].ToString());
                if (rbBodies.Checked) cbxMiningFilter.Items.Add(row["Class"].ToString());
            }
            cbxMiningFilter.Text = "";
        }
        private void ApplyFilter()
        {
            string selection = "";
            if (cbxMiningFilter.SelectedItem != null)
            {
                selection = cbxMiningFilter.SelectedItem.ToString();
            }
            LoadSystems(selection);
        }
        private void GetLogData(string path, string file)
        {
            if (!File.Exists(path)) return;
            readLogTimer.Stop();
            ulong timestamp = 0;
            gb = new GlobalBase(host, db, port, user, pwd);

            List<string> fileLines = new List<string>();
            
            FileInfo log = new FileInfo(path);
            StreamReader stream = new StreamReader(log.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            while (!stream.EndOfStream)
            {
                fileLines.Add(stream.ReadLine());
            }
            stream.Close();
            log = null;
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Maximum = fileLines.Count;
            foreach (string line in fileLines)
            {
                timestamp = SaveEvent(line);
                if (gb.connected && GlobalBaseExeption())
                {
                    gb.connected = false;
                }
                if (LocalBaseExeption()) return;
                if (CMDRBaseExeption()) return;
                if (CarrierBaseExeption()) return;
                toolStripProgressBar.PerformStep();
            }
            toolStripProgressBar.Value = 0;
            if(timestamp > lastTime)
            {
                lastLog = file;
                ib.UpdateLastLog(lastLog);
                lastTime = timestamp;
                ib.UpdateLastTimestamp(lastTime);
                toolStripStatusLastLog.Text = lastLog;
                bkgLastLine = fileLines.Count - 1;
                ib.UpdateBkgLastLine(bkgLastLine);

            }
            filterRaw = lb.SelectDistinctRaw();
            if (LocalBaseExeption()) return;
            filterMinerals = lb.SelectDistinctMinerals();
            if (LocalBaseExeption()) return;
            filterRings = lb.SelectDistinctRings();
            if (LocalBaseExeption()) return;
            filterBodies = lb.SelectDistinctBodies();
            if (LocalBaseExeption()) return;
            if (rbRaw.Checked)
            {
                FillMiningFilter(filterRaw);
            }
            if (rbMinerals.Checked)
            {
                FillMiningFilter(filterMinerals);
            }
            if (rbRings.Checked)
            {
                FillMiningFilter(filterRings);
            }
            if (rbBodies.Checked)
            {
                FillMiningFilter(filterBodies);
            }
            
            LoadCMDRData();
            LoadCarrierData();
            ApplyFilter();
            if (bkgRead) readLogTimer.Start();
        }
        private ulong SaveEvent(string line)
        {
            
            DataTable dt = new DataTable();
            UInt64 SKey = 0;
            UInt64 BKey = 0;
            UInt64 RKey = 0;
            UInt64 glBKey = 0;
            UInt64 glRKey = 0;
            ulong timestamp = 0;

            int count = 0;

            if (line.Length == 0) return 0;

            ev = JsonConvert.DeserializeObject<Event>(line);
            ib.InsertEvent(ev.timestamp, ev.@event);
            timestamp = ToTimestamp(ev.timestamp);
            //"event":"Location"
            if (line.IndexOf("\"event\":\"Location\"") >= 0)
            {
                location = JsonConvert.DeserializeObject<Location>(line);
                dt = lb.SelectSystemByKey(location.SystemAddress);
                if (dt.Rows.Count == 0) lb.InsertSystem(location); //add
                else lb.UpdateSystem(location);    //update
                //global
                if (gb.connected)
                {
                    count = gb.SelectSystemsCount(location.SystemAddress);
                    if (count == 0) gb.InsertSystem(location); //add
                    else gb.UpdateSystem(location);//update
                }
                //current
                cx = Convert.ToDouble(location.StarPos[0]);
                cy = Convert.ToDouble(location.StarPos[1]);
                cz = Convert.ToDouble(location.StarPos[2]);
                toolStripStatusCurrent.Text = location.StarSystem;
                //SKey
                cmdrLastKey = location.SystemAddress;
                cmdrLastName = location.StarSystem;
                cb.UpdateCMDR(logCMDR, cmdrLastKey, cmdrLastName);

                if (location.SystemAddress == fcLastKey)
                {
                    visited = true;
                    fcx = cx;
                    fcy = cy;
                    fcz = cz;
                    crb.UpdateVisited(logFC, visited);
                }
            }
            //"event":"FSDJump"
            if (line.IndexOf("\"event\":\"FSDJump\"") >= 0)
            {
                fsdJump = JsonConvert.DeserializeObject<FSDJump>(line);
                //local
                dt = lb.SelectSystemByKey(fsdJump.SystemAddress);
                if (dt.Rows.Count == 0) lb.InsertSystem(fsdJump); //add
                else lb.UpdateSystem(fsdJump);    //update
                //global
                if (gb.connected)
                {
                    count = gb.SelectSystemsCount(fsdJump.SystemAddress);
                    if (count == 0) gb.InsertSystem(fsdJump); //add
                    else gb.UpdateSystem(fsdJump);//update
                }
                //current
                cx = Convert.ToDouble(fsdJump.StarPos[0]);
                cy = Convert.ToDouble(fsdJump.StarPos[1]);
                cz = Convert.ToDouble(fsdJump.StarPos[2]);
                toolStripStatusCurrent.Text = fsdJump.StarSystem;
                //SKey
                cmdrLastKey = fsdJump.SystemAddress;
                cmdrLastName = fsdJump.StarSystem;
                cb.UpdateCMDR(logCMDR, cmdrLastKey, cmdrLastName);

                if(fsdJump.SystemAddress == fcLastKey)
                {
                    visited = true;
                    fcx = cx;
                    fcy = cy;
                    fcz = cz;
                    crb.UpdateVisited(logFC, visited);
                }
            }
            //"event":"CarrierJump"
            if (line.IndexOf("\"event\":\"CarrierJump\"") >= 0)
            {
                carrierJump = JsonConvert.DeserializeObject<CarrierJump>(line);
                visited = true;
                
                //local
                dt = lb.SelectSystemByKey(carrierJump.SystemAddress);
                if (dt.Rows.Count == 0) lb.InsertSystem(carrierJump); //add
                else lb.UpdateSystem(carrierJump);    //update
                if (logFC != 0)
                    crb.UpdateCurrent(logFC, carrierJump.StarSystem, carrierJump.Body, carrierJump.SystemAddress, visited);
                //global
                if (gb.connected)
                {
                    count = gb.SelectSystemsCount(carrierJump.SystemAddress);
                    if (count == 0) gb.InsertSystem(carrierJump); //add
                    else gb.UpdateSystem(carrierJump);//update
                }
                if (carrierJump.Docked) //корабль в карриере
                {
                    //current
                    cx = Convert.ToDouble(carrierJump.StarPos[0]);
                    cy = Convert.ToDouble(carrierJump.StarPos[1]);
                    cz = Convert.ToDouble(carrierJump.StarPos[2]);
                    toolStripStatusCurrent.Text = carrierJump.StarSystem;
                    cmdrLastKey= carrierJump.SystemAddress;
                    cmdrLastName = carrierJump.StarSystem;
                    cb.UpdateCMDR(logCMDR, cmdrLastKey, cmdrLastName);
                }
                //fc current
                fcx = Convert.ToDouble(carrierJump.StarPos[0]);
                fcy = Convert.ToDouble(carrierJump.StarPos[1]);
                fcz = Convert.ToDouble(carrierJump.StarPos[2]);
                //SKeyFC
                fcLastKey = carrierJump.SystemAddress;
                
            }
            //"event":"Scan"
            if (line.IndexOf("\"event\":\"Scan\"") >= 0)
            {
                scan = JsonConvert.DeserializeObject<Scan>(line);

                if (scan.SystemAddress == 0 || scan.StarSystem == "")
                {
                    //если нет адреса или названия системы то берем из последнего прыжка
                    scan.SystemAddress = cmdrLastKey;
                    scan.StarSystem = cmdrLastName;
                }
                SKey = scan.SystemAddress;
                dt = lb.SelectSystemByKey(SKey);
                if (dt.Rows.Count == 0) return timestamp; //нет в базе
                dt = lb.SelectBodyByName(SKey, scan.BodyName);
                if (dt.Rows.Count == 0)
                {
                    //add
                    BKey = lb.InsertBody(SKey, scan);
                    if (BKey == 0) return timestamp;
                }
                else
                {
                    //update
                    BKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                    lb.UpdateBody(SKey, BKey, scan);
                }
                //global
                if (gb.connected)
                {
                    dt = gb.SelectBodyByName(SKey, scan.BodyName);
                    if (dt.Rows.Count == 0)
                    {
                        //add
                        glBKey = gb.InsertBody(SKey, scan);
                    }
                    else
                    {
                        //update
                        glBKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                        gb.UpdateBody(SKey, glBKey, scan);
                    }
                }
                //RAW
                if (scan.Materials != null)
                {
                    foreach (Material elem in scan.Materials)
                    {
                        dt = lb.SelectRawByName(SKey, BKey, elem.Name);
                        if (dt.Rows.Count == 0)
                        {
                            //add
                            lb.InsertRaw(SKey, BKey, elem);
                        }
                        else
                        {
                            //update
                            lb.UpdateRaw(SKey, BKey, elem);
                        }
                        //global
                        if (gb.connected && glBKey != 0)
                        {
                            count = gb.SelectRawCount(SKey, glBKey, elem.Name);
                            if (count == 0)
                            {
                                //add
                                gb.InsertRaw(SKey, glBKey, elem);
                            }
                            else
                            {
                                //update
                                gb.UpdateRaw(SKey, glBKey, elem);
                            }
                        }
                    }
                }
                //Rings
                if (scan.Rings != null)
                {
                    foreach (Ring elem in scan.Rings)
                    {
                        elem.RingClass = elem.RingClass.Replace("eRingClass_", "");
                        dt = lb.SelectRingByName(SKey, BKey, elem.Name);
                        if (dt.Rows.Count == 0)
                        {
                            //add
                            RKey = lb.InsertRing(SKey, BKey, elem, elem.Name.Replace(scan.BodyName, ""));
                        }
                        else
                        {
                            //update
                            RKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                            lb.UpdateRing(SKey, BKey, RKey, elem);
                        }
                        //global
                        if (gb.connected && glBKey != 0)
                        {
                            dt = gb.SelectRingByName(SKey, glBKey, elem.Name);
                            if (dt.Rows.Count == 0)
                            {
                                //add
                                glRKey = gb.InsertRing(SKey, BKey, elem, elem.Name.Replace(scan.BodyName, ""));
                            }
                            else
                            {
                                //update
                                glRKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                                gb.UpdateRing(SKey, glBKey, glRKey, elem);
                            }
                        }
                    }
                }
            }
            //"event":"SAASignalsFound"
            if (line.IndexOf("\"event\":\"SAASignalsFound\"") >= 0)
            {
                saaSignals = JsonConvert.DeserializeObject<SAASignalsFound>(line);
                SKey = saaSignals.SystemAddress;
                BKey = 0;
                RKey = 0;
                glBKey = 0;
                glRKey = 0;

                //поиск нужной планеты
                dt = lb.SelectBodyByName(SKey, saaSignals.BodyName);
                if (dt.Rows.Count > 0)
                {
                    BKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                }
                //поиск нужной планеты
                if (gb.connected)
                {
                    dt = gb.SelectBodyByName(SKey, saaSignals.BodyName);
                    if (dt.Rows.Count > 0)
                    {
                        glBKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                    }
                }
                //поиск кольца
                dt = lb.SelectRingByName(SKey, saaSignals.BodyName);
                if (dt.Rows.Count > 0)
                {
                    BKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                    RKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                }

                if (gb.connected)
                {
                    dt = gb.SelectRingByName(SKey, saaSignals.BodyName);
                    if (dt.Rows.Count > 0)
                    {
                        glBKey = Convert.ToUInt64(dt.Rows[0]["BKey"]);
                        glRKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                    }
                }

                foreach (Signal elem in saaSignals.Signals)
                {
                    if (elem.Type.IndexOf("$") == 0)
                    {
                        dt = lb.SelectSignalByName(SKey, BKey, elem);
                        if (dt.Rows.Count == 0)
                        {
                            lb.InsertSignal(SKey, BKey, elem);
                        }
                        else
                        {
                            lb.UpdateSignal(SKey, BKey, elem);
                        }
                        if (gb.connected)
                        {
                            dt = gb.SelectSignalByName(SKey, BKey, elem);
                            if (dt.Rows.Count == 0)
                            {
                                gb.InsertSignal(SKey, BKey, elem);
                            }
                            else
                            {
                                gb.UpdateSignal(SKey, BKey, elem);
                            }
                        }
                    }
                    else
                    {
                        if (RKey != 0)
                        {
                            dt = lb.SelectMineralByName(SKey, BKey, RKey, elem.Type);
                            if (dt.Rows.Count == 0)
                            {
                                //add
                                lb.InsertMineral(SKey, BKey, RKey, elem);
                            }
                            else
                            {
                                //update
                                RKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                                lb.UpdateMineral(SKey, BKey, RKey, elem);
                            }
                        }
                        if (gb.connected && glBKey != 0 && glRKey != 0)
                        {
                            dt = gb.SelectMineralByName(SKey, glBKey, glRKey, elem.Type);
                            if (dt.Rows.Count == 0)
                            {
                                //add
                                gb.InsertMineral(SKey, glBKey, glRKey, elem);
                            }
                            else
                            {
                                //update
                                glRKey = Convert.ToUInt64(dt.Rows[0]["RKey"]);
                                gb.UpdateMineral(SKey, glBKey, glRKey, elem);
                            }
                        }
                    }
                }
            }
            //"event":"Commander"
            if (line.IndexOf("\"event\":\"Commander\"") >= 0)
            {
                cmdr = JsonConvert.DeserializeObject<Commander>(line);
                dt = cb.SelectCMDRByName(cmdr.Name);
                if (dt.Rows.Count == 0)
                {
                    cb.InsertCMDR(cmdr.Name);
                    logCMDR = cmdr.Name;
                    cbxCMDR.Items.Add(cmdr.Name);
                }
            }
            //"event":"LoadGame"
            if (line.IndexOf("\"event\":\"LoadGame\"") >= 0)
            {
                load = JsonConvert.DeserializeObject<LoadGame>(line);
                cb.UpdateCredits(cmdr.Name, load);
            }
            //"event":"Rank"
            if (line.IndexOf("\"event\":\"Rank\"") >= 0)
            {
                rank = JsonConvert.DeserializeObject<Rank>(line);
                cb.UpdateRank(cmdr.Name, rank);
            }
            //"event":"Reputation"
            if (line.IndexOf("\"event\":\"Reputation\"") >= 0)
            {
                reputation = JsonConvert.DeserializeObject<Reputation>(line);
                cb.UpdateReputation(cmdr.Name, reputation);
            }
            //"event":"Progress"
            if (line.IndexOf("\"event\":\"Progress\"") >= 0)
            {
                progress = JsonConvert.DeserializeObject<Progress>(line);
                cb.UpdateProgress(cmdr.Name, progress);
            }
            //"event":"Materials"
            if (line.IndexOf("\"event\":\"Materials\"") >= 0)
            {
                materials = JsonConvert.DeserializeObject<CmdrMaterials>(line);
                foreach (Raw elem in materials.Raw)
                {
                    dt = cb.SelectMaterialByKey(cmdr.Name, elem.Name.ToLower());
                    if (dt.Rows.Count == 0)
                    {
                        cb.InsertMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                    else
                    {
                        cb.UpdateMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                }
                foreach (Encoded elem in materials.Encoded)
                {
                    dt = cb.SelectMaterialByKey(cmdr.Name, elem.Name.ToLower());
                    if (dt.Rows.Count == 0)
                    {
                        cb.InsertMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                    else
                    {
                        cb.UpdateMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                }
                foreach (Manufactured elem in materials.Manufactured)
                {
                    dt = cb.SelectMaterialByKey(cmdr.Name, elem.Name.ToLower());
                    if (dt.Rows.Count == 0)
                    {
                        cb.InsertMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                    else
                    {
                        cb.UpdateMaterial(cmdr.Name, elem.Name, elem.Count);
                    }
                }
            }
            //"event":"MaterialCollected"
            if (line.IndexOf("\"event\":\"MaterialCollected\"") >= 0)
            {
                materialCollected = JsonConvert.DeserializeObject<MaterialCollected>(line);
                dt = cb.SelectMaterialByKey(cmdr.Name, materialCollected.Name.ToLower());
                if (dt.Rows.Count == 0)
                {
                    cb.InsertMaterial(cmdr.Name, materialCollected.Name.ToLower(), materialCollected.Count);
                }
                else
                {
                    cb.UpdateAddMaterial(cmdr.Name, materialCollected.Name.ToLower(), materialCollected.Count);
                }
            }
            //"event":"EngineerCraft"
            if (line.IndexOf("\"event\":\"EngineerCraft\"") >= 0)
            {
                engineerCraft = JsonConvert.DeserializeObject<EngineerCraft>(line);
                foreach (Ingredient elem in engineerCraft.Ingredients)
                {
                    cb.UpdateAddMaterial(cmdr.Name, elem.Name.ToLower(), -elem.Count);
                }
            }
            //"event":"Synthesis"
            if (line.IndexOf("\"event\":\"Synthesis\"") >= 0)
            {
                synthesis = JsonConvert.DeserializeObject<Synthesis>(line);
                foreach (SyntesisMaterial elem in synthesis.Materials)
                {
                    cb.UpdateAddMaterial(cmdr.Name, elem.Name.ToLower(), -elem.Count);
                }
            }
            //"event":"MaterialTrade"
            if (line.IndexOf("\"event\":\"MaterialTrade\"") >= 0)
            {
                materialTrade = JsonConvert.DeserializeObject<MaterialTrade>(line);
                dt = cb.SelectMaterialByKey(cmdr.Name, materialTrade.Received.Material.ToLower());
                if (dt.Rows.Count == 0)
                {
                    cb.InsertMaterial(cmdr.Name, materialTrade.Received.Material.ToLower(), materialTrade.Received.Quantity);
                }
                else
                {
                    cb.UpdateAddMaterial(cmdr.Name, materialTrade.Received.Material.ToLower(), materialTrade.Received.Quantity);
                }
                cb.UpdateAddMaterial(cmdr.Name, materialTrade.Paid.Material.ToLower(), -materialTrade.Paid.Quantity);
            }
            //"event":"BuyAmmo"
            if (line.IndexOf("\"event\":\"BuyAmmo\"") >= 0)
            {
                buyAmmo = JsonConvert.DeserializeObject<BuyAmmo>(line);
                cb.UpdateAddCredits(cmdr.Name, -buyAmmo.Cost);
            }
            //"event":"RefuelAll"
            if (line.IndexOf("\"event\":\"RefuelAll\"") >= 0)
            {
                refuelAll = JsonConvert.DeserializeObject<RefuelAll>(line);
                cb.UpdateAddCredits(cmdr.Name, -refuelAll.Cost);
            }
            //"event":"Repair"
            if (line.IndexOf("\"event\":\"Repair\"") >= 0)
            {
                repair = JsonConvert.DeserializeObject<Repair>(line);
                cb.UpdateAddCredits(cmdr.Name, -repair.Cost);
            }
            //"event":"BuyDrones"
            if (line.IndexOf("\"event\":\"BuyDrones\"") >= 0)
            {
                buyDrones = JsonConvert.DeserializeObject<BuyDrones>(line);
                cb.UpdateAddCredits(cmdr.Name, -buyDrones.TotalCost);
            }
            //"event":"Resurrect"
            if (line.IndexOf("\"event\":\"Resurrect\"") >= 0)
            {
                resurrect = JsonConvert.DeserializeObject<Resurrect>(line);
                cb.UpdateAddCredits(cmdr.Name, -resurrect.Cost);
            }
            //"event":"FetchRemoteModule"
            if (line.IndexOf("\"event\":\"FetchRemoteModule\"") >= 0)
            {
                fetchRemoteModule = JsonConvert.DeserializeObject<FetchRemoteModule>(line);
                cb.UpdateAddCredits(cmdr.Name, -fetchRemoteModule.TransferCost);
            }
            //"event":"MarketBuy"
            if (line.IndexOf("\"event\":\"MarketBuy\"") >= 0)
            {
                marketBuy = JsonConvert.DeserializeObject<MarketBuy>(line);
                cb.UpdateAddCredits(cmdr.Name, -marketBuy.TotalCost);
            }
            //"event":"MarketSell"
            if (line.IndexOf("\"event\":\"MarketSell\"") >= 0)
            {
                marketSell = JsonConvert.DeserializeObject<MarketSell>(line);
                cb.UpdateAddCredits(cmdr.Name, marketSell.TotalSale);
            }
            //"event":"MissionCompleted"
            if (line.IndexOf("\"event\":\"MissionCompleted\"") >= 0)
            {
                missionCompleted = JsonConvert.DeserializeObject<MissionCompleted>(line);
                cb.UpdateAddCredits(cmdr.Name, missionCompleted.Reward);
                if (missionCompleted.CommodityReward != null)
                {
                    foreach (CommodityReward elem in missionCompleted.CommodityReward)
                    {
                        dt = cb.SelectMaterialByKey(cmdr.Name, elem.Name.ToLower());
                        if (dt.Rows.Count == 0)
                        {
                            cb.InsertMaterial(cmdr.Name, elem.Name.ToLower(), elem.Count);
                        }
                        else
                        {
                            cb.UpdateAddMaterial(cmdr.Name, elem.Name.ToLower(), elem.Count);
                        }
                    }
                }
                if (missionCompleted.MaterialsReward != null)
                {
                    foreach (MaterialsReward elem in missionCompleted.MaterialsReward)
                    {
                        dt = cb.SelectMaterialByKey(cmdr.Name, elem.Name.ToLower());
                        if (dt.Rows.Count == 0)
                        {
                            cb.InsertMaterial(cmdr.Name, elem.Name.ToLower(), elem.Count);
                        }
                        else
                        {
                            cb.UpdateAddMaterial(cmdr.Name, elem.Name.ToLower(), elem.Count);
                        }
                    }
                }
            }
            //"event":"ModuleBuy"
            if (line.IndexOf("\"event\":\"ModuleBuy\"") >= 0)
            {
                moduleBuy = JsonConvert.DeserializeObject<ModuleBuy>(line);
                cb.UpdateAddCredits(cmdr.Name, moduleBuy.SellPrice - moduleBuy.BuyPrice);
            }
            //"event":"ModuleSell"
            if (line.IndexOf("\"event\":\"ModuleSell\"") >= 0)
            {
                moduleSell = JsonConvert.DeserializeObject<ModuleSell>(line);
                cb.UpdateAddCredits(cmdr.Name, moduleSell.SellPrice);
            }
            //"event":"ModuleSellRemote"
            if (line.IndexOf("\"event\":\"ModuleSellRemote\"") >= 0)
            {
                moduleSellRemote = JsonConvert.DeserializeObject<ModuleSellRemote>(line);
                cb.UpdateAddCredits(cmdr.Name, moduleSellRemote.SellPrice);
            }
            //"event":"MultiSellExplorationData"
            if (line.IndexOf("\"event\":\"MultiSellExplorationData\"") >= 0)
            {
                multiSellExplorationData = JsonConvert.DeserializeObject<MultiSellExplorationData>(line);
                cb.UpdateAddCredits(cmdr.Name, multiSellExplorationData.TotalEarnings);
            }
            //"event":"PayBounties"
            if (line.IndexOf("\"event\":\"PayBounties\"") >= 0)
            {
                payBounties = JsonConvert.DeserializeObject<PayBounties>(line);
                cb.UpdateAddCredits(cmdr.Name, payBounties.Amount);
            }
            //"event":"PayFines"
            if (line.IndexOf("\"event\":\"PayFines\"") >= 0)
            {
                payFines = JsonConvert.DeserializeObject<PayFines>(line);
                cb.UpdateAddCredits(cmdr.Name, -payFines.Amount);
            }
            //"event":"PowerplayFastTrack"
            if (line.IndexOf("\"event\":\"PowerplayFastTrack\"") >= 0)
            {
                powerplayFastTrack = JsonConvert.DeserializeObject<PowerplayFastTrack>(line);
                cb.UpdateAddCredits(cmdr.Name, -powerplayFastTrack.Cost);
            }
            //"event":"PowerplaySalary"
            if (line.IndexOf("\"event\":\"PowerplaySalary\"") >= 0)
            {
                powerplaySalary = JsonConvert.DeserializeObject<PowerplaySalary>(line);
                cb.UpdateAddCredits(cmdr.Name, powerplaySalary.Amount);
            }
            //"event":"RedeemVoucher"
            if (line.IndexOf("\"event\":\"RedeemVoucher\"") >= 0)
            {
                redeemVoucher = JsonConvert.DeserializeObject<RedeemVoucher>(line);
                cb.UpdateAddCredits(cmdr.Name, redeemVoucher.Amount);
            }
            //"event":"RepairAll"
            if (line.IndexOf("\"event\":\"RepairAll\"") >= 0)
            {
                repairAll = JsonConvert.DeserializeObject<RepairAll>(line);
                cb.UpdateAddCredits(cmdr.Name, -repairAll.Cost);
            }
            //"event":"SellDrones"
            if (line.IndexOf("\"event\":\"SellDrones\"") >= 0)
            {
                sellDrones = JsonConvert.DeserializeObject<SellDrones>(line);
                cb.UpdateAddCredits(cmdr.Name, sellDrones.TotalSale);
            }
            //"event":"ShipyardBuy"
            if (line.IndexOf("\"event\":\"ShipyardBuy\"") >= 0)
            {
                shipyardBuy = JsonConvert.DeserializeObject<ShipyardBuy>(line);
                cb.UpdateAddCredits(cmdr.Name, -shipyardBuy.ShipPrice);
            }
            //"event":"ShipyardSell"
            if (line.IndexOf("\"event\":\"ShipyardSell\"") >= 0)
            {
                shipyardSell = JsonConvert.DeserializeObject<ShipyardSell>(line);
                cb.UpdateAddCredits(cmdr.Name, shipyardSell.ShipPrice);
            }
            //"event":"ShipyardTransfer"
            if (line.IndexOf("\"event\":\"ShipyardTransfer\"") >= 0)
            {
                shipyardTransfer = JsonConvert.DeserializeObject<ShipyardTransfer>(line);
                cb.UpdateAddCredits(cmdr.Name, -shipyardTransfer.TransferPrice);
            }
            //"event":"CarrierBuy"
            if (line.IndexOf("\"event\":\"CarrierBuy\"") >= 0)
            {
                carrierBuy = JsonConvert.DeserializeObject<CarrierBuy>(line);
                crb.InsertCarrier(carrierBuy);
                cb.UpdateAddCredits(cmdr.Name, -carrierBuy.Price);
                logFC = carrierBuy.CarrierID;
                cbxCarrier.Items.Add(carrierBuy.Callsign +
                " (" + carrierBuy.CarrierID.ToString() + ")");
            }
            //"event":"CarrierStats"
            if (line.IndexOf("\"event\":\"CarrierStats\"") >= 0)
            {
                carrierStats = JsonConvert.DeserializeObject<CarrierStats>(line);
                logFC = carrierStats.CarrierID;
                dt = crb.SelectCarrier(logFC);
                if (dt.Rows.Count == 0)
                {
                    crb.InsertCarrier(carrierStats);//add
                    crb.UpdateCarreerStats(carrierStats);//update
                    cbxCarrier.Items.Add(carrierStats.Name + " " + carrierStats.Callsign +
                        " (" + carrierStats.CarrierID.ToString() + ")");
                }
                else crb.UpdateCarreerStats(carrierStats);    //update
            }
            //"event":"CarrierBankTransfer"
            if (line.IndexOf("\"event\":\"CarrierBankTransfer\"") >= 0)
            {
                carrierBankTransfer = JsonConvert.DeserializeObject<CarrierBankTransfer>(line);
                crb.UpdateCarreerCredits(carrierBankTransfer.CarrierID, carrierBankTransfer.CarrierBalance);
                cb.UpdateCredits(cmdr.Name, carrierBankTransfer.PlayerBalance);
            }
            //"event":"CarrierJumpRequest"
            if (line.IndexOf("\"event\":\"CarrierJumpRequest\"") >= 0)
            {
                carrierJumpRequest = JsonConvert.DeserializeObject<CarrierJumpRequest>(line);
                oldFCSysAddr = fcLastKey;
                dt = lb.SelectSystemByKey(carrierJumpRequest.SystemAddress);
                if (dt.Rows.Count != 0)
                {
                    visited = true;
                    fcx = Convert.ToDouble(dt.Rows[0]["X"]);
                    fcy = Convert.ToDouble(dt.Rows[0]["Y"]);
                    fcz = Convert.ToDouble(dt.Rows[0]["Z"]);
                }
                else
                {
                    visited = false;
                }
                fcLastKey = carrierJumpRequest.SystemAddress;
                dt = crb.SelectCarrier(carrierJumpRequest.CarrierID);
                oldFCSysName = dt.Rows[0]["system"].ToString();
                oldFCSysBody = dt.Rows[0]["body"].ToString();
                crb.UpdateCurrent(carrierJumpRequest.CarrierID, carrierJumpRequest.SystemName, carrierJumpRequest.Body, fcLastKey, visited);
    
            }
            //"event":"CarrierJumpCancelled"
            if (line.IndexOf("\"event\":\"CarrierJumpCancelled\"") >= 0)
            {
                carrierJumpCancelled = JsonConvert.DeserializeObject<CarrierJumpCancelled>(line);
                dt = lb.SelectSystemByKey(oldFCSysAddr);
                if (dt.Rows.Count != 0)
                {
                    visited = true;
                    fcx = Convert.ToDouble(dt.Rows[0]["X"]);
                    fcy = Convert.ToDouble(dt.Rows[0]["Y"]);
                    fcz = Convert.ToDouble(dt.Rows[0]["Z"]);
                }
                else
                {
                    visited = false;
                }
                fcLastKey = oldFCSysAddr;
                crb.UpdateCurrent(carrierJumpCancelled.CarrierID, oldFCSysName, oldFCSysBody, fcLastKey, visited);
            }
            //"event":"CargoTransfer"
            if (line.IndexOf("\"event\":\"CargoTransfer\"") >= 0)
            {
                cargoTransfer = JsonConvert.DeserializeObject<CargoTransfer>(line);
                foreach (Transfer elem in cargoTransfer.Transfers)
                {
                    dt = crb.SelectCargoByType(carrierID, elem.Type);
                    if (dt.Rows.Count == 0)
                        crb.InsertCargo(carrierID, elem, cargoTransfer.timestamp);
                    else
                    {
                        if (cargoTransfer.timestamp > Convert.ToDateTime(dt.Rows[0]["timestamp"]).ToUniversalTime())
                            crb.UpdateCargo(carrierID, elem, cargoTransfer.timestamp);
                    }
                }
            }
            //"event":"Screenshot"
            if (line.IndexOf("\"event\":\"Screenshot\"") >= 0 && 
                tbxScreenFrom.Text != "" &&
                tbxScreenTo.Text != "")
            {
                screenshot = JsonConvert.DeserializeObject<Screenshot>(line);
                int ind = screenshot.Filename.LastIndexOf(@"\");
                string file = screenshot.Filename.Substring(ind + 1);
                if(File.Exists(tbxScreenFrom.Text + @"\" + file))
                {
                    Bitmap bmp = new Bitmap(tbxScreenFrom.Text + @"\" + file);
                    string newFile = screenshot.Body + " " + screenshot.timestamp.ToString("(dd-MM-yy HH-mm-ss)") + ".png";
                    bmp.Save(tbxScreenTo.Text + @"\" + newFile);
                    lblScreenName.Text = newFile;
                    pbxScreenshot.Image = bmp;
                    //screens.Add(tbxScreenFrom.Text + @"\" + file);
                }
            }

            Application.DoEvents();
            
            return timestamp;
        }
        private void LoadSystems(string filter = "")
        {
            double dx;
            double dy;
            double dz;

            loadingData = true;
            if (filter == "") //no filter
            {
                dbt.systems = lb.SelectSystems(cx, cy, cz, maxRadius);
                if (LocalBaseExeption()) return;
            }
            else if(rbRaw.Checked) //filter on raw
            {
                dbt.systems = lb.SelectSystemsByRaw(cx, cy, cz, maxRadius, filter);
                if (LocalBaseExeption()) return;
            }
            else if(rbMinerals.Checked)//filter on minerals
            {
                dbt.systems = lb.SelectSystemsByMineral(cx, cy, cz, maxRadius, filter);
                if (LocalBaseExeption()) return;
            }
            else if(rbRings.Checked)//filter on rings
            {
                dbt.systems = lb.SelectSystemsByRing(cx, cy, cz, maxRadius, filter);
                if (LocalBaseExeption()) return;
            }
            else
            {
                dbt.systems = lb.SelectSystemsByBody(cx, cy, cz, maxRadius, filter);
                if (LocalBaseExeption()) return;
            }
            if(dbt.systems.Rows.Count == 0)
            {
                dbt.systems.Rows.Clear();
                dgvSystems.DataSource = dbt.systems;
                dbt.bodies.Rows.Clear();
                dgvBodies.DataSource = dbt.bodies;
                dbt.raw.Rows.Clear();
                dgvRaw.DataSource = dbt.raw;
                dbt.rings.Rows.Clear();
                dgvRings.DataSource = dbt.rings;
                dbt.minerals.Rows.Clear();
                dgvMinerals.DataSource = dbt.minerals;
                return;
            }
            foreach (DataRow row in dbt.systems.Rows)
            {
                dx = Convert.ToDouble(row["X"]) - cx;
                dy = Convert.ToDouble(row["Y"]) - cy;
                dz = Convert.ToDouble(row["Z"]) - cz;
                row["Distance"] = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
                if (!visited)
                {
                    row["FCDistance"] = 0;
                }
                else
                {
                    if (fcLastKey != 0)
                    {
                        dx = Convert.ToDouble(row["X"]) - fcx;
                        dy = Convert.ToDouble(row["Y"]) - fcy;
                        dz = Convert.ToDouble(row["Z"]) - fcz;
                        row["FCDistance"] = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
                    }
                }
            }
            dgvSystems.DataSource = dbt.systems;
            dgvSystems.Sort(dgvSystems.Columns["dgvsDistance"], ListSortDirection.Ascending);
            dgvSystems.CurrentCell = dgvSystems.Rows[0].Cells["dgvsName"];
            dgvSystems.Update();
            systemsRowIndex = dgvSystems.CurrentRow.Index;
            lblSystems.Text = "Систем " + dbt.systems.Rows.Count.ToString() + ". Радиус поиска " +
                maxRadius.ToString() + " св.л.";

            //distance from Sol
            double dfs = Math.Round(Math.Sqrt(cx * cx + cy * cy + cz * cz), 2);
            
            lblDistance.Text = "Текущие расстояния: от Sol " + dfs.ToString() + " св.л.; ";
            if (!visited)
            {
                lblDistance.Text = lblDistance.Text + "от носителя (неизвестно); ";
            }
            else
            {
                if (fcLastKey != 0)
                {
                    //distance from carrier
                    dx = cx - fcx;
                    dy = cy - fcy;
                    dz = cz - fcz;
                    double dfc = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
                    lblDistance.Text = lblDistance.Text + "от носителя " + dfc.ToString() + " св.л.; ";
                }
            }
            //distance from galaxy center
            dx = cx - 25.21875;
            dy = cy - -20.90625;
            dz = cz - 25899.96875;
            double dfgc = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
            lblDistance.Text = lblDistance.Text + "от центра галактики " + dfgc.ToString() + " св.л.";

            LoadBodies(Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value), filter);
            loadingData = false;

        }
        private void LoadBodies(UInt64 SKey, string filter = "")
        {
            if (filter == "") //no filter
            {
                dbt.bodies = lb.SelectBodies(SKey);
                if (LocalBaseExeption()) return;
            }
            else if (rbRaw.Checked) //filter on raw
            {
                dbt.bodies = lb.SelectBodiesByRaw(SKey, filter);
                if (LocalBaseExeption()) return;
            }
            else if (rbMinerals.Checked)//filter on minerals
            {
                dbt.bodies = lb.SelectBodiesByMineral(SKey, filter);
                if (LocalBaseExeption()) return;
            }
            else if(rbRings.Checked)//filter on rings
            {
                dbt.bodies = lb.SelectBodiesByRing(SKey, filter);
                if (LocalBaseExeption()) return;
            }
            else
            {
                dbt.bodies = lb.SelectBodiesByClass(SKey, filter);
                if (LocalBaseExeption()) return;
            }

            if (dbt.bodies.Rows.Count == 0)
            {
                dbt.bodies.Rows.Clear();
                dgvBodies.DataSource = dbt.bodies;
                dbt.signals.Rows.Clear();
                dgvSignals.DataSource = dbt.signals;
                dbt.raw.Rows.Clear();
                dgvRaw.DataSource = dbt.raw;
                dbt.rings.Rows.Clear();
                dgvRings.DataSource = dbt.rings;
                dbt.minerals.Rows.Clear();
                dgvMinerals.DataSource = dbt.minerals;
                if (dgvSystems.CurrentRow == null)
                    lblPlanets.Text = "";
                else
                    lblPlanets.Text = "В системе " + dgvSystems.CurrentRow.Cells["dgvsName"].Value.ToString() + " — 0 тел";
                return;
            }
            dgvBodies.DataSource = dbt.bodies;

            dgvBodies.Sort(dgvBodies.Columns["dgvbDistance"], ListSortDirection.Ascending);
            dgvBodies.CurrentCell = dgvBodies.Rows[0].Cells["dgvbName"];
            dgvBodies.Update();
            bodiesRowIndex = dgvBodies.CurrentRow.Index;

            int bCount = dbt.bodies.Rows.Count;
            string s = bCount.ToString();
            s = s.Substring(s.Length - 1);
            string bText = " тел. ";
            if(s == "1" && (bCount < 10 || bCount > 20))
            {
                bText = " тело. ";
            }
            if ((s == "2" || s == "3" || s == "4") && (bCount < 10 || bCount > 20))
            {
                bText = " тела. ";
            }

            lblPlanets.Text = "В системе " + dgvSystems.CurrentRow.Cells["dgvsName"].Value.ToString() + " — " +
                bCount.ToString() + bText;

            if (dbt.bodies.Rows.Count > 0)
            {
                Loadsignals(Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value),
                    Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value));
                LoadRaw(Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value),
                    Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value));
                LoadRings(Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value),
                    Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value));
            }
        }
        private void Loadsignals(UInt64 SKey, UInt64 BKey)
        {
            dbt.signals = lb.SelectSignals(SKey, BKey);
            if (LocalBaseExeption()) return;
            if (dbt.signals.Rows.Count == 0)
            {
                dbt.signals.Rows.Clear();
                dgvSignals.DataSource = dbt.signals;
                return;
            }
            dgvSignals.DataSource = dbt.signals;
            dgvSignals.Sort(dgvSignals.Columns["dgvsiName"], ListSortDirection.Ascending);
            dgvSignals.CurrentCell = dgvSignals.Rows[0].Cells["dgvsiName"];
            dgvSignals.Update();
        }
        private void LoadRaw(UInt64 SKey, UInt64 BKey)
        {
            dbt.raw = lb.SelectRaw(SKey, BKey);
            if (LocalBaseExeption()) return;
            if (dbt.raw.Rows.Count == 0)
            {
                dbt.raw.Rows.Clear();
                dgvRaw.DataSource = dbt.raw;
                return;
            }
            dgvRaw.DataSource = dbt.raw;
            dgvRaw.Sort(dgvRaw.Columns["dgvrName"], ListSortDirection.Ascending);
            dgvRaw.CurrentCell = dgvRaw.Rows[0].Cells["dgvrName"];
            dgvRaw.Update();
        }
        private void LoadRings(UInt64 SKey, UInt64 BKey)
        {
            dbt.rings = lb.SelectRings(SKey, BKey);
            if (LocalBaseExeption()) return;
            if (dbt.rings.Rows.Count == 0)
            {
                dbt.rings.Rows.Clear();
                dgvRings.DataSource = dbt.rings;
                return;
            }
            dgvRings.DataSource = dbt.rings;
            dgvRings.Sort(dgvRings.Columns["dgvriName"], ListSortDirection.Ascending);
            dgvRings.CurrentCell = dgvRings.Rows[0].Cells["dgvriName"];
            dgvRings.Update();
            ringsRowIndex = dgvBodies.CurrentRow.Index;
            if (dbt.rings.Rows.Count > 0)
            {
                LoadMinerals(Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriSKey"].Value),
                    Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriBKey"].Value),
                    Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriRKey"].Value));
            }
        }
        private void LoadMinerals(UInt64 SKey, UInt64 BKey, UInt64 RKey)
        {
            dbt.minerals = lb.SelectMinerals(SKey, BKey, RKey);
            if (LocalBaseExeption()) return;
            if (dbt.minerals.Rows.Count == 0)
            {
                dbt.minerals.Rows.Clear();
                dgvMinerals.DataSource = dbt.minerals;
                return;
            }
            dgvMinerals.DataSource = dbt.minerals;
            dgvMinerals.Sort(dgvMinerals.Columns["dgvmType"], ListSortDirection.Ascending);
            dgvMinerals.CurrentCell = dgvMinerals.Rows[0].Cells["dgvmType"];
            dgvMinerals.Update();
        }
        private void LoadCMDRData()
        {
            if (cbxCMDR.SelectedItem == null)
            {
                return;
            }
            cmdrName = cbxCMDR.SelectedItem.ToString();
            DataTable dt;
            DataTable info;
            dt = cb.SelectCMDRByName(cmdrName);
            cmdrLastKey = Convert.ToUInt64(dt.Rows[0]["syskey"]);
            cmdrLastName = dt.Rows[0]["sysname"].ToString();

            dt = cb.SelectStatus(cmdrName);
            if (dt.Rows.Count > 0)
            {
                lblCredits.Text = String.Format("Баланс: {0:### ### ### ### ##0} Cr",
                    dt.Rows[0]["credits"]);
                //rank
                info = ib.SelectFederal(Convert.ToInt32(dt.Rows[0]["federal"]));
                if (InfoBaseExeption()) return;
                lblFederationI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectEmpire(Convert.ToInt32(dt.Rows[0]["empire"]));
                if (InfoBaseExeption()) return;
                lblEmpireI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectAlliance(Convert.ToInt32(dt.Rows[0]["alliance"]));
                if (InfoBaseExeption()) return;
                lblAllianceI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectCombat(Convert.ToInt32(dt.Rows[0]["combat"]));
                if (InfoBaseExeption()) return;
                lblCombatI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectTrade(Convert.ToInt32(dt.Rows[0]["trade"]));
                if (InfoBaseExeption()) return;
                lblTradeI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectExplore(Convert.ToInt32(dt.Rows[0]["explore"]));
                if (InfoBaseExeption()) return;
                lblExploreI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                info = ib.SelectCQC(Convert.ToInt32(dt.Rows[0]["cqc"]));
                if (InfoBaseExeption()) return;
                lblCQCI.Text = info.Rows[0]["en"].ToString() + " (" +
                    info.Rows[0]["ru"].ToString() + ")";

                //reputation
                federationReputation = Convert.ToInt32(dt.Rows[0]["repfederal"]);
                empireReputation = Convert.ToInt32(dt.Rows[0]["repempire"]);
                allianceReputation = Convert.ToInt32(dt.Rows[0]["repalliance"]);

                Reputation(federationReputation, pbxFederationReputation);
                Reputation(empireReputation, pbxEmpireReputation);
                Reputation(allianceReputation, pbxAllianceReputation);

                //progress
                federationProgress = Convert.ToInt32(dt.Rows[0]["prgfederal"]);
                empireProgress = Convert.ToInt32(dt.Rows[0]["prgempire"]);
                allianceProgress = Convert.ToInt32(dt.Rows[0]["prgalliance"]);
                tradeProgress = Convert.ToInt32(dt.Rows[0]["prgtrade"]);
                combatProgress = Convert.ToInt32(dt.Rows[0]["prgcombat"]);
                exploreProgress = Convert.ToInt32(dt.Rows[0]["prgexplore"]);
                cqcProgress = Convert.ToInt32(dt.Rows[0]["prgcqc"]);

                Progress(federationProgress, pbxFederationProgress);
                Progress(empireProgress, pbxEmpireProgress);
                Progress(allianceProgress, pbxAllianceProgress);
                Progress(combatProgress, pbxCombatProgress);
                Progress(tradeProgress, pbxTradeProgress);
                Progress(exploreProgress, pbxExploreProgress);
                Progress(cqcProgress, pbxCQCProgress);
            }
            //materials
            DataRow foundRow;
            dt = cb.SelectMaterials(cmdrName);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dbt.craw.Rows)
                {
                    foundRow = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("key") == row["key"].ToString());
                    if (foundRow == null) continue;
                    row["count"] = foundRow["count"];
                }
                dgvCmdrRaw.Refresh();
                foreach (DataRow row in dbt.cencoded.Rows)
                {
                    foundRow = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("key") == row["key"].ToString());
                    if (foundRow == null) continue;
                    row["count"] = foundRow["count"];
                }
                dgvCmdrEncoded.Refresh();
                foreach (DataRow row in dbt.cmanuf.Rows)
                {
                    foundRow = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("key") == row["key"].ToString());
                    if (foundRow == null) continue;
                    row["count"] = foundRow["count"];
                }
                dgvCmdrManuf.Refresh();
            }
        }
        //reputation
        private void Reputation(int value, PictureBox pbx)
        {
            Bitmap bmp = new Bitmap(pbx.Size.Width,
                pbx.Size.Height);
            pbx.Image = bmp;
            Graphics g = Graphics.FromImage(pbx.Image);

            SolidBrush brush;

            g.Clear(Color.LightGray);
            if (value >= 0)
                brush = new SolidBrush(Color.LimeGreen);
            else
                brush = new SolidBrush(Color.Red);

            float percent = pbx.Size.Width / 200f; // от -100 до +100
            float barWidth = (value + 100) * percent;
            g.FillRectangle(brush, 0f, 0f, barWidth, pbx.Size.Height);

            float tx = pbx.Size.Width / 2f - 15;
            float ty = pbx.Size.Height / 2f - 12;
            g.DrawString(value.ToString() + "%",
                new Font("Segoe UI", 10),
                new SolidBrush(Color.Black),
                tx, ty);
        }
        //progress
        private void Progress(int value, PictureBox pbx)
        {
            Bitmap bmp = new Bitmap(pbx.Size.Width,
                pbx.Size.Height);
            pbx.Image = bmp;
            Graphics g = Graphics.FromImage(pbx.Image);

            SolidBrush brush;

            g.Clear(Color.LightGray);
            brush = new SolidBrush(Color.LimeGreen);

            float percent = pbx.Size.Width / 100f;
            float barWidth = value * percent;
            g.FillRectangle(brush, 0f, 0f, barWidth, pbx.Size.Height);

            float tx = pbx.Size.Width / 2f - 15;
            float ty = pbx.Size.Height / 2f - 12;
            g.DrawString(value.ToString() + "%",
                new Font("Segoe UI", 10),
                new SolidBrush(Color.Black),
                tx, ty);
        }
        //carrier
        private void LoadCarrierData()
        {
            DataTable dt;
            if (cbxCarrier.SelectedItem == null)
            {
                return;
            }
            string s = cbxCarrier.SelectedItem.ToString();
            int index = s.LastIndexOf("(");
            s = s.Substring(index + 1, s.Length - index - 2);
            carrierID = Convert.ToInt64(s);

            dt = crb.SelectCarrier(carrierID);
            if (CarrierBaseExeption()) return;
            if (dt.Rows.Count == 0) return;
            fcLastKey = Convert.ToUInt64(dt.Rows[0]["syskey"]);
            visited = Convert.ToBoolean(dt.Rows[0]["visited"]);
            lblCurrentCurrierSystem.Text = "Местонахождение: " + dt.Rows[0]["system"].ToString();
            string body = dt.Rows[0]["body"].ToString();
            if(body != "")
            {
                body = body.Replace(dt.Rows[0]["system"].ToString(), "").Trim();
                lblCurrentCurrierSystem.Text = lblCurrentCurrierSystem.Text + " (Планета: " + body + ")";
            }
            if (!visited)
            {
                lblCurrentCurrierSystem.Text = lblCurrentCurrierSystem.Text + " не посещалась";
            }

            dt = crb.SelectFinance(carrierID);
            if (CarrierBaseExeption()) return;
            if (dt.Rows.Count == 0) return;
            lblBalanceI.Text =   String.Format("{0:### ### ### ### ##0} Cr", dt.Rows[0]["balance"]);
            lblReserveI.Text =   String.Format("{0:### ### ### ### ##0} Cr", dt.Rows[0]["reserve"]);
            lblAvaikableI.Text = String.Format("{0:### ### ### ### ##0} Cr", dt.Rows[0]["available"]);

            dt = crb.SelectSpace(carrierID);
            if (CarrierBaseExeption()) return;
            if (dt.Rows.Count == 0) return;
            lblTotalI.Text = dt.Rows[0]["total"].ToString() + " т.";
            lblCrewI.Text = dt.Rows[0]["crew"].ToString() + " т.";
            lblCargoI.Text = dt.Rows[0]["cargo"].ToString() + " т.";
            lblCargoReserveI.Text = dt.Rows[0]["cargoreserve"].ToString() + " т.";
            lblShipPaksI.Text = dt.Rows[0]["shippaks"].ToString() + " т.";
            lblModulePaksI.Text = dt.Rows[0]["modulepaks"].ToString() + " т.";
            lblFreeI.Text = dt.Rows[0]["free"].ToString() + " т.";

            dt = crb.SelectStats(carrierID);
            if (CarrierBaseExeption()) return;
            if (dt.Rows.Count == 0) return;
            lblFuelI.Text = dt.Rows[0]["fuel"].ToString() + " т.";
            LblJumpI.Text = dt.Rows[0]["range"].ToString() + " св.л.";
            lblMaxJumpI.Text = dt.Rows[0]["rangemax"].ToString() + " св.л.";

            dt = crb.SelectCrew(carrierID);
            if (CarrierBaseExeption()) return;
            if (dt.Rows.Count == 0) return;
            try
            {
                //0-BlackMarket
                if (Convert.ToBoolean(dt.Rows[0]["activated"]))
                    lblBlackMarketA.Text = "Активировано";
                else
                    lblBlackMarketA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[0]["enabled"]))
                    lblBlackMarketE.Text = "Включено";
                else
                    lblBlackMarketE.Text = "Выключено";
                //1-Captain
                if (Convert.ToBoolean(dt.Rows[1]["activated"]))
                    lblCaptainA.Text = "Активировано";
                else
                    lblCaptainA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[1]["enabled"]))
                    lblCaptainE.Text = "Включено";
                else
                    lblCaptainE.Text = "Выключено";
                //2-CarrierFuel
                if (Convert.ToBoolean(dt.Rows[2]["activated"]))
                    lblCarrierFuelA.Text = "Активировано";
                else
                    lblCarrierFuelA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[2]["enabled"]))
                    lblCarrierFuelE.Text = "Включено";
                else
                    lblCarrierFuelE.Text = "Выключено";
                //3-Commodities
                if (Convert.ToBoolean(dt.Rows[3]["activated"]))
                    lblMarketA.Text = "Активировано";
                else
                    lblMarketA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[3]["enabled"]))
                    lblMarketE.Text = "Включено";
                else
                    lblMarketE.Text = "Выключено";
                //4-Exploration
                if (Convert.ToBoolean(dt.Rows[4]["activated"]))
                    lblExplorationA.Text = "Активировано";
                else
                    lblExplorationA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[4]["enabled"]))
                    lblExplorationE.Text = "Включено";
                else
                    lblExplorationE.Text = "Выключено";
                //5-Outfitting
                if (Convert.ToBoolean(dt.Rows[5]["activated"]))
                    lblOutfitingA.Text = "Активировано";
                else
                    lblOutfitingA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[5]["enabled"]))
                    lblOutfitingE.Text = "Включено";
                else
                    lblOutfitingE.Text = "Выключено";
                //6-Rearm
                if (Convert.ToBoolean(dt.Rows[6]["activated"]))
                    lblRearmA.Text = "Активировано";
                else
                    lblRearmA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[6]["enabled"]))
                    lblRearmE.Text = "Включено";
                else
                    lblRearmE.Text = "Выключено";
                //7-Refuel
                if (Convert.ToBoolean(dt.Rows[7]["activated"]))
                    lblRefuelA.Text = "Активировано";
                else
                    lblRefuelA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[7]["enabled"]))
                    lblRefuelE.Text = "Включено";
                else
                    lblRefuelE.Text = "Выключено";
                //8-Repair
                if (Convert.ToBoolean(dt.Rows[8]["activated"]))
                    lblRepairA.Text = "Активировано";
                else
                    lblRepairA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[8]["enabled"]))
                    lblRepairE.Text = "Включено";
                else
                    lblRepairE.Text = "Выключено";
                //9-Shipyard
                if (Convert.ToBoolean(dt.Rows[9]["activated"]))
                    lblShipyardA.Text = "Активировано";
                else
                    lblShipyardA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[9]["enabled"]))
                    lblShipyardE.Text = "Включено";
                else
                    lblShipyardE.Text = "Выключено";
                //10-VoucherRedemption
                if (Convert.ToBoolean(dt.Rows[10]["activated"]))
                    lblVoucherA.Text = "Активировано";
                else
                    lblVoucherA.Text = "Не активировано";
                if (Convert.ToBoolean(dt.Rows[10]["enabled"]))
                    lblVoucherE.Text = "Включено";
                else
                    lblVoucherE.Text = "Выключено";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //cargo
            dt = crb.SelectCargo(carrierID);
            if (CarrierBaseExeption()) return;
            dgvCargo.DataSource = dt;

        }
        private void BkgReadLog(string path)
        {
            bool newData = false;
            readLogTimer.Stop();
            toolStripStatusLastLog.BackColor = Color.Red;
            toolStripStatusLastLog.ForeColor = Color.White;
            
            //fswFile.EnableRaisingEvents = false;
            gb = new GlobalBase(host, db, port, user, pwd);
            List<string> fileLines = new List<string>();
            StreamReader stream;

            
            try
            {
                FileInfo log = new FileInfo(path);
                stream = new StreamReader(log.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }
            catch(Exception ex)
            {
                chkEnableBkgRead.Checked = false;
                toolStripStatusLastLog.BackColor = Color.Green;
                toolStripStatusLastLog.ForeColor = Color.White;
                MessageBox.Show("Ошибка чтения лога:\r\n" + ex.Message);
                return;
            }
            
            while (!stream.EndOfStream)
            {
                fileLines.Add(stream.ReadLine());
            }
            stream.Close();
            if(fileLines.Count == 0) return;
            int lastFileLine = fileLines.Count - 1;
            for (int i = bkgLastLine + 1; i <= lastFileLine; i++)
            {
                newData = true;
                lastTime = SaveEvent(fileLines[i]);
                if (gb.connected && GlobalBaseExeption())
                {
                    gb.connected = false;
                }
                if (LocalBaseExeption() || CMDRBaseExeption() || CarrierBaseExeption())
                {
                    chkEnableBkgRead.Checked = false;
                    readLogTimer.Start();
                    return;
                }
                tbxBkgReadLog.Text = tbxBkgReadLog.Text + i.ToString() + 
                    " " + fileLines[i] + System.Environment.NewLine;
                tbxBkgReadLog.SelectionStart = tbxBkgReadLog.Text.Length;
                tbxBkgReadLog.ScrollToCaret();
                tbxBkgReadLog.Refresh();
                bkgLastLine++;
                ib.UpdateBkgLastLine(bkgLastLine);
                ib.UpdateLastTimestamp(lastTime);
            }
            //lastReadLine = lastFileLine;
            if (newData)
            {
                LoadCMDRData();
                LoadCarrierData();
                ApplyFilter();
            }
            
            //fswFile.EnableRaisingEvents = true;
            toolStripStatusLastLog.BackColor = Color.Green;
            toolStripStatusLastLog.ForeColor = Color.White;
            readLogTimer.Start();
        }


        //events
        private void LogFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                logPath = fbd.SelectedPath;
                bkgLastLine = -1;
                ib.UpdateLogPath(logPath);
                ib.UpdateBkgLastLine(bkgLastLine);

                toolStripStatusLogPath.Text = fbd.SelectedPath;
                //watches
                fswDir.Path = logPath;
                //fswFile.Path = Properties.Settings.Default.LogPath;
                fswDir.EnableRaisingEvents = true;
                //fswFile.EnableRaisingEvents = true;
                if(bkgRead) readLogTimer.Start();
            }
        }

        private void LoadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Journal.xxxxxxxxxxxx.xx.log|*.log";
            opf.Title = "Select log file";
            opf.InitialDirectory = logPath;
            if (opf.ShowDialog() == DialogResult.OK)
            {
                GetLogData(opf.FileName, opf.SafeFileName);
            }
        }

        private void cbxMiningFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void rbRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRaw.Checked)
            {
                FillMiningFilter(filterRaw);
                ApplyFilter();
            }
        }

        private void rbMinerals_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMinerals.Checked)
            {
                FillMiningFilter(filterMinerals);
                ApplyFilter();
            }
        }

        private void rbRings_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRings.Checked)
            {
                FillMiningFilter(filterRings);
                ApplyFilter();
            }
        }

        private void rbBodies_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBodies.Checked)
            {
                FillMiningFilter(filterBodies);
                ApplyFilter();
            }
        }

        private void GetEDSMDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gb = new GlobalBase(host, db, port, user, pwd);
            DataTable dt = new DataTable();
            EDSMInfo edsm = new EDSMInfo();
            string edsmRequest;
            string sysName;
            UInt64 SKey;
            UInt64 BKey;
            UInt64 glBKey;
            string ringName;

            if (dgvSystems.CurrentRow == null)
            {
                MessageBox.Show("Не выбрана система");
                return;
            }
            sysName = dgvSystems.CurrentRow.Cells["dgvsName"].Value.ToString();
            edsmRequest = "https://www.edsm.net/api-system-v1/bodies?systemName=" + WebUtility.UrlEncode(sysName);
            WebRequest request = WebRequest.Create(edsmRequest);
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Ошибка: " + response.StatusDescription);
                    response.Close();
                    return;
                }
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                edsm = JsonConvert.DeserializeObject<EDSMInfo>(responseFromServer);
            }
            catch(WebException ex)
            {
                MessageBox.Show("Ошибка EDSM: " + Environment.NewLine + ex.Message);
                return;
            }
 
            SKey = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value);
            if (edsm.bodies == null) return;
            foreach (EDSMBody body in edsm.bodies)
            {
                dt = lb.SelectBodyByName(SKey, body.name);
                if(dt.Rows.Count == 0)
                {
                    BKey = lb.InsertEDSMBody(SKey, body, sysName);
                    if (LocalBaseExeption()) return;
                    if (body.rings != null)
                    {
                        foreach (EDSMRing ring in body.rings)
                        {
                            ringName = ring.name.Replace(body.name, "").Trim().ToString();
                            lb.InsertEDSMRing(SKey, BKey, ring, ringName);
                            if (LocalBaseExeption()) return;
                        }
                    }
                }
            }
            if (gb.connected)
            {
                foreach (EDSMBody body in edsm.bodies)
                {
                    dt = gb.SelectBodyByName(SKey, body.name);
                    if (dt.Rows.Count == 0)
                    {
                        glBKey = gb.InsertEDSMBody(SKey, body, sysName);
                        if (GlobalBaseExeption()) return;
                        if (body.rings != null)
                        {
                            foreach (EDSMRing ring in body.rings)
                            {
                                ringName = ring.name.Replace(body.name, "").Trim().ToString();
                                gb.InsertEDSMRing(SKey, glBKey, ring, ringName);
                                if (GlobalBaseExeption()) return;
                            }
                        }
                    }
                }
            }
            LoadBodies(SKey, "");
        }

        private void dgvSystems_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (systemsRowIndex == dgvSystems.CurrentRow.Index) return;

            systemsRowIndex = dgvSystems.CurrentRow.Index;

            UInt64 SKey = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value);

            string selection = "";
            if (cbxMiningFilter.SelectedItem != null)
            {
                selection = cbxMiningFilter.SelectedItem.ToString();
            }
            LoadBodies(SKey, selection);
        }

        private void dgvSystems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvSystems.CurrentRow.Cells["dgvsComment"].Value.ToString();
            UInt64 SKey = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value);
            lb.UpdateSystemComment(SKey, comment);
            LocalBaseExeption();
        }

        private void dgvBodies_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (bodiesRowIndex == dgvBodies.CurrentRow.Index) return;
            bodiesRowIndex = dgvBodies.CurrentRow.Index;

            UInt64 SKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value);
            Loadsignals(SKey, BKey);
            LoadRaw(SKey, BKey);
            LoadRings(SKey, BKey);
        }

        private void dgvBodies_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvBodies.CurrentRow.Cells["dgvbComment"].Value.ToString();
            UInt64 SKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value);
            lb.UpdateBodiesComment(SKey, BKey, comment);
            LocalBaseExeption();
        }

        private void dgvRings_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (ringsRowIndex == dgvRings.CurrentRow.Index) return;
            ringsRowIndex = dgvRings.CurrentRow.Index;

            UInt64 SKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriBKey"].Value);
            UInt64 RKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriRKey"].Value);
            LoadMinerals(SKey, BKey, RKey);
        }

        private void dgvRings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvRings.CurrentRow.Cells["dgvriComment"].Value.ToString();
            UInt64 SKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriBKey"].Value);
            UInt64 RKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriRKey"].Value);
            lb.UpdateRingsComment(SKey, BKey, RKey, comment);
            LocalBaseExeption();
        }

        private void dgvMinerals_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvMinerals.CurrentRow.Cells["dgvmComment"].Value.ToString();
            UInt64 SKey = Convert.ToUInt64(dgvMinerals.CurrentRow.Cells["dgvmSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvMinerals.CurrentRow.Cells["dgvmBKey"].Value);
            UInt64 RKey = Convert.ToUInt64(dgvMinerals.CurrentRow.Cells["dgvmRKey"].Value);
            lb.UpdateMineralsComment(SKey, BKey, RKey, comment);
            LocalBaseExeption();
        }

        private void MaxDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMaxDistanse setDist = new FormMaxDistanse();
            setDist.maxDist = maxRadius;
            setDist.ShowDialog();
            maxRadius = Convert.ToInt32(setDist.maxDist);
            ib.UpdateMaxRadius(maxRadius);
            ApplyFilter();
        }

        private void SetCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvSystems.CurrentRow == null) return;
            cmdrLastKey = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value);
            cmdrLastName = dgvSystems.CurrentRow.Cells["dgvsName"].Value.ToString();
            cb.UpdateCMDR(cmdrName, cmdrLastKey, cmdrLastName);
            toolStripStatusCurrent.Text = dgvSystems.CurrentRow.Cells["dgvsName"].Value.ToString();
            cx = Convert.ToDouble(dgvSystems.CurrentRow.Cells["dgvsX"].Value);
            cy = Convert.ToDouble(dgvSystems.CurrentRow.Cells["dgvsY"].Value);
            cz = Convert.ToDouble(dgvSystems.CurrentRow.Cells["dgvsZ"].Value);
            ApplyFilter();
        }

        private void tbxSystemsFilter_TextChanged(object sender, EventArgs e)
        {
            (dgvSystems.DataSource as DataTable).DefaultView.RowFilter =
                    String.Format("Name like '%{0}%'", tbxSystemsFilter.Text);
            string selection = "";
            if ((dgvSystems.DataSource as DataTable).DefaultView.Count == 0)
            {
                LoadBodies(0, selection);
            }
            else
            {
                UInt64 SKey = Convert.ToUInt64((dgvSystems.DataSource as DataTable).DefaultView[0][0]);
                if (cbxMiningFilter.SelectedItem != null)
                {
                    selection = cbxMiningFilter.SelectedItem.ToString();
                }
                LoadBodies(SKey, selection);
            }
        }

        private void btnSystemsFilterClear_Click(object sender, EventArgs e)
        {
            tbxSystemsFilter.Text = "";
        }

        private void cbxCMDR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;

            LoadCMDRData();
        }

        private void pbxFederationProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(federationProgress, pbxFederationProgress);
        }

        private void pbxEmpireProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(empireProgress, pbxEmpireProgress);
        }

        private void pbxAllianceProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(allianceProgress, pbxAllianceProgress);
        }

        private void pbxCombatProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(combatProgress, pbxCombatProgress);
        }

        private void pbxTradeProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(tradeProgress, pbxTradeProgress);
        }

        private void pbxExploreProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(exploreProgress, pbxExploreProgress);
        }

        private void pbxCQCProgress_SizeChanged(object sender, EventArgs e)
        {
            Progress(cqcProgress, pbxCQCProgress);
        }

        private void dgvSystems_Sorted(object sender, EventArgs e)
        {
            if (loadingData) return;

            UInt64 SKey = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["dgvsSKey"].Value);

            string selection = "";
            if (cbxMiningFilter.SelectedItem != null)
            {
                selection = cbxMiningFilter.SelectedItem.ToString();
            }
            LoadBodies(SKey, selection);
        }

        private void dgvBodies_Sorted(object sender, EventArgs e)
        {
            if (loadingData) return;

            UInt64 SKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvBodies.CurrentRow.Cells["dgvbBKey"].Value);
            Loadsignals(SKey, BKey);
            LoadRaw(SKey, BKey);
            LoadRings(SKey, BKey);
        }

        private void dgvRings_Sorted(object sender, EventArgs e)
        {
            if (loadingData) return;

            UInt64 SKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriSKey"].Value);
            UInt64 BKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriBKey"].Value);
            UInt64 RKey = Convert.ToUInt64(dgvRings.CurrentRow.Cells["dgvriRKey"].Value);
            LoadMinerals(SKey, BKey, RKey);
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMap Map = new FormMap();
            Map.lb = lb;
            Map.currx = cx;
            Map.currz = cz;
            Map.Show();
        }

        private void cbxCarrier_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadCarrierData();
        }

        private void toolStripStatusDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(donateAddr);
        }

        private void dgvCargo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Int64 id = Convert.ToInt64(dgvCargo.CurrentRow.Cells["dgvCargoId"].Value);
            string type = dgvCargo.CurrentRow.Cells["dgvCargoType"].Value.ToString();
            if (Int32.TryParse(dgvCargo.CurrentRow.Cells["dgvCargoCount"].Value.ToString(),
                out int count))
            {
                crb.UpdateCargo(id, type, count);
                CarrierBaseExeption();
            }
        }

        private void chkEnableBkgRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableBkgRead.Checked)
            {
                if (logPath == "")
                {
                    MessageBox.Show("Не задан каталог логов");
                    return;
                }
                readLogTimer.Start();
                toolStripStatusLastLog.BackColor = Color.Green;
                toolStripStatusLastLog.ForeColor = Color.White;
            }
            else
            {
                readLogTimer.Stop();
                toolStripStatusLastLog.BackColor = SystemColors.Control;
                toolStripStatusLastLog.ForeColor = SystemColors.ControlText;
            }
            ib.UpdateBkgRead(chkEnableBkgRead.Checked);
        }

        private void fswDir_Created(object sender, FileSystemEventArgs e)
        {
            if (chkEnableBkgRead.Checked)
            {
                tbxBkgReadLog.Text = "Найден лог " + e.Name + System.Environment.NewLine;
                toolStripStatusLastLog.Text = e.Name;
                lastLog = e.Name;
                bkgLastLine = -1;
                ib.UpdateLastLog(lastLog);
                ib.UpdateBkgLastLine(bkgLastLine);
            }
        }

        private void dgvCmdrRaw_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            float prc = 0;
            float cou = Convert.ToSingle(dgvCmdrRaw.Rows[e.RowIndex].Cells["dgvcCmdrRawCount"].Value);
            float cap = Convert.ToSingle(dgvCmdrRaw.Rows[e.RowIndex].Cells["dgvcCmdrRawCapacity"].Value);

            prc = cou / cap;
            if (prc < 0.25f)
            {
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Tomato;
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (prc < 0.5f)
            {
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 0.75f)
            {
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 1.0f)
            {
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.YellowGreen;
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dgvCmdrRaw.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvCmdrEncoded_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            float prc = 0;
            float cou = Convert.ToSingle(dgvCmdrEncoded.Rows[e.RowIndex].Cells["dgvcCmdrEncodedCount"].Value);
            float cap = Convert.ToSingle(dgvCmdrEncoded.Rows[e.RowIndex].Cells["dgvcCmdrEncodedCapasity"].Value);

            prc = cou / cap;
            if (prc < 0.25f)
            {
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Tomato;
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (prc < 0.5f)
            {
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 0.75f)
            {
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 1.0f)
            {
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.YellowGreen;
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dgvCmdrEncoded.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvCmdrManuf_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            float prc = 0;
            float cou = Convert.ToSingle(dgvCmdrManuf.Rows[e.RowIndex].Cells["dgvcManufCount"].Value);
            float cap = Convert.ToSingle(dgvCmdrManuf.Rows[e.RowIndex].Cells["dgvcManufCapasity"].Value);

            prc = cou / cap;
            if (prc < 0.25f)
            {
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Tomato;
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (prc < 0.5f)
            {
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 0.75f)
            {
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (prc < 1.0f)
            {
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.YellowGreen;
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dgvCmdrManuf.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void btnScreenFrom_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxScreenFrom.Text = fbd.SelectedPath;
                fromPath = fbd.SelectedPath;
                ib.UpdateFromImgPath(fromPath);
            }
        }

        private void btnScreenTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxScreenTo.Text = fbd.SelectedPath;
                toPath = fbd.SelectedPath;
                ib.UpdateToImgPath(toPath);
            }
        }

        private void btnBackupPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxBackup.Text = fbd.SelectedPath;
                bakPath = fbd.SelectedPath;
                ib.UpdateBackupPath(bakPath);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (tbxBackup.Text == "") return;
            DirCopy(logPath, bakPath, DateTime.Now);
        }

        private void FileCopy(string from, string to, DateTime dt)
        {
            long sizeFrom;
            long sizeTo;
            FileInfo infoFrom;
            FileInfo infoTo;
            int ind;
            string fileName;

            ind = from.LastIndexOf(@"\");
            fileName = from.Substring(ind + 1);
            to += "\\" + fileName;
            if (File.Exists(to))
            {
                infoFrom = new FileInfo(from);
                infoTo = new FileInfo(to);
                sizeFrom = infoFrom.Length;
                sizeTo = infoTo.Length;
                if (sizeFrom != sizeTo)
                {
                    try
                    {
                        File.Copy(from, to, true);
                        tbxBackupLog.Text += "Перезаписан файл " + fileName + " в "
                           + dt.ToString()
                           + Environment.NewLine;
                    }
                    catch (Exception ex)
                    {
                        tbxBackupLog.Text += "Ошибка в "
                           + dt.ToString() + "(" + ex.Message + ")" + Environment.NewLine;
                    }
                }
            }
            else
            {
                try
                {
                    File.Copy(from, to, true);
                    tbxBackupLog.Text += "Создан файл " + fileName + " в "
                       + dt.ToString()
                       + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    tbxBackupLog.Text += "Ошибка в "
                       + dt.ToString() + "(" + ex.Message + ")" + Environment.NewLine;
                }
            }
        }

        private void DirCopy(string from, string to, DateTime dt)
        {
            FileInfo[] files = null;
            DirectoryInfo infoFrom;
            DirectoryInfo[] subDir = null;
            int ind;
            string dir;
            string sub;

            infoFrom = new DirectoryInfo(from);
            subDir = infoFrom.GetDirectories();
            for (int i = 0; i < subDir.Length; i++)
            {
                sub = subDir[i].FullName;
                ind = sub.LastIndexOf(@"\");
                dir = to + "\\" + sub.Substring(ind + 1);
                if (!Directory.Exists(dir))
                {
                    try
                    {
                        Directory.CreateDirectory(dir);
                        tbxBackupLog.Text  += "Создан каталог " + dir + " at "
                        + dt.ToString()
                        + Environment.NewLine;
                    }
                    catch (Exception ex)
                    {
                        tbxBackupLog.Text += "Ошибка в "
                        + dt.ToString() + "(" + ex.Message + ")" + Environment.NewLine;
                    }
                }
                Application.DoEvents();
                this.DirCopy(sub, dir, dt);
            }
            files = infoFrom.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileCopy(files[i].FullName, to, dt);
                Application.DoEvents();
            }
        }

        private void readLogTimer_Tick(object sender, EventArgs e)
        {
            if(logPath != "" && lastLog != "")
                BkgReadLog(logPath + @"\" + lastLog);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems_v2
{
    // "event":"CarrierBuy"
    public class CarrierBuy
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public long BoughtAtMarket { get; set; } = 0;
        public string Location { get; set; } = "";
        public long SystemAddress { get; set; } = 0;
        public Int64 Price { get; set; } = 0;
        public string Variant { get; set; } = "";
        public string Callsign { get; set; } = "";
    }

    //"event":"CarrierStats"
    public class SpaceUsage
    {
        public int TotalCapacity { get; set; } = 0;
        public int Crew { get; set; } = 0;
        public int Cargo { get; set; } = 0;
        public int CargoSpaceReserved { get; set; } = 0;
        public int ShipPacks { get; set; } = 0;
        public int ModulePacks { get; set; } = 0;
        public int FreeSpace { get; set; } = 0;
    }

    public class Finance
    {
        public Int64 CarrierBalance { get; set; } = 0;
        public Int64 ReserveBalance { get; set; } = 0;
        public Int64 AvailableBalance { get; set; } = 0;
        public int ReservePercent { get; set; } = 0;
        public int TaxRate { get; set; } = 0;
    }

    public class Crew
    {
        public string CrewRole { get; set; } = "";
        public bool Activated { get; set; } = false;
        public bool Enabled { get; set; } = false;
        public string CrewName { get; set; } = "";
    }

    public class CarrierStats
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public string Callsign { get; set; } = "";
        public string Name { get; set; } = "";
        public string DockingAccess { get; set; } = "";
        public bool AllowNotorious { get; set; } = false;
        public int FuelLevel { get; set; } = 0;
        public double JumpRangeCurr { get; set; } = 0;
        public double JumpRangeMax { get; set; } = 0;
        public bool PendingDecommission { get; set; } = false;
        public SpaceUsage SpaceUsage { get; set; } = new SpaceUsage();
        public Finance Finance { get; set; } = new Finance();
        public List<Crew> Crew { get; set; } = new List<Crew>();
        public List<object> ShipPacks { get; set; } = new List<object>();
        public List<object> ModulePacks { get; set; } = new List<object>();
    }

    // "event":"CarrierBankTransfer"
    public class CarrierBankTransfer
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public Int64 Deposit { get; set; } = 0;
        public Int64 PlayerBalance { get; set; } = 0;
        public Int64 CarrierBalance { get; set; } = 0;
    }

    // "event":"CarrierCrewServices"
    public class CarrierCrewServices
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public string CrewRole { get; set; } = "";
        public string Operation { get; set; } = "";
        public string CrewName { get; set; } = "";
    }

    // "event":"CarrierFinance"
    public class CarrierFinance
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public int TaxRate { get; set; } = 0;
        public int CarrierBalance { get; set; } = 0;
        public int ReserveBalance { get; set; } = 0;
        public int AvailableBalance { get; set; } = 0;
        public int ReservePercent { get; set; } = 0;
    }

    //"event":"CarrierDepositFuel" 
    public class CarrierDepositFuel
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public int Amount { get; set; } = 0;
        public int Total { get; set; } = 0;
    }

    // "event":"CarrierJump"
    public class StationFaction
    {
        public string Name { get; set; } = "";
    }

    public class StationEconomy
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public double Proportion { get; set; } = 0;
    }

    public class CarrierJump
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public bool Docked { get; set; } = false;
        public string StationName { get; set; } = "";
        public string StationType { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public StationFaction StationFaction { get; set; } = new StationFaction();
        public string StationGovernment { get; set; } = "";
        public string StationGovernment_Localised { get; set; } = "";
        public List<string> StationServices { get; set; } = new List<string>();
        public string StationEconomy { get; set; } = "";
        public string StationEconomy_Localised { get; set; } = "";
        public List<StationEconomy> StationEconomies { get; set; } = new List<StationEconomy>();
        public string StarSystem { get; set; } = "";
        public UInt64 SystemAddress { get; set; } = 0;
        public List<double> StarPos { get; set; } = new List<double>();
        public string SystemAllegiance { get; set; } = "";
        public string SystemEconomy { get; set; } = "";
        public string SystemEconomy_Localised { get; set; } = "";
        public string SystemSecondEconomy { get; set; } = "";
        public string SystemSecondEconomy_Localised { get; set; } = "";
        public string SystemGovernment { get; set; } = "";
        public string SystemGovernment_Localised { get; set; } = "";
        public string SystemSecurity { get; set; } = "";
        public string SystemSecurity_Localised { get; set; } = "";
        public long Population { get; set; } = 0;
        public string Body { get; set; } = "";
        public int BodyID { get; set; } = 0;
        public string BodyType { get; set; } = "";
    }

    //"event":"CarrierJumpRequest",
    public class CarrierJumpRequest
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
        public string SystemName { get; set; } = "";
        public string Body { get; set; } = "";
        public ulong SystemAddress { get; set; } = 0;
        public int BodyID { get; set; } = 0;
    }

    //"event":"CargoTransfer"
    public class Transfer
    {
        public string Type { get; set; } = "";
        public string Type_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
        public string Direction { get; set; } = "";
    }

    public class CargoTransfer
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public List<Transfer> Transfers { get; set; } = new List<Transfer>();
    }

    //"event":"CarrierJumpCancelled"
    public class CarrierJumpCancelled
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long CarrierID { get; set; } = 0;
    }
}


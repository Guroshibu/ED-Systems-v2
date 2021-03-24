using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems_v2
{
    //"event":"Commander"
    public class Commander
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string FID { get; set; } = "";
        public string Name { get; set; } = "";
    }
    //"event":"LoadGame"
    public class LoadGame
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string FID { get; set; } = "";
        public string Commander { get; set; } = "";
        public bool Horizons { get; set; } = false;
        public string Ship { get; set; } = "";
        public string Ship_Localised { get; set; } = "";
        public int ShipID { get; set; } = 0;
        public string ShipName { get; set; } = "";
        public string ShipIdent { get; set; } = "";
        public double FuelLevel { get; set; } = 0;
        public double FuelCapacity { get; set; } = 0;
        public string GameMode { get; set; } = "";
        public Int64 Credits { get; set; } = 0;
        public int Loan { get; set; } = 0;
    }
    //"event":"Rank"
    public class Rank
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public int Combat { get; set; } = 0;
        public int Trade { get; set; } = 0;
        public int Explore { get; set; } = 0;
        public int Empire { get; set; } = 0;
        public int Federation { get; set; } = 0;
        public int Alliance { get; set; } = 0;
        public int CQC { get; set; } = 0;
    }
    //"event":"Reputation"
    public class Reputation
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public double Empire { get; set; } = 0;
        public double Federation { get; set; } = 0;
        public double Alliance { get; set; } = 0;
    }
    //"event":"Progress"
    public class Progress
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public int Combat { get; set; } = 0;
        public int Trade { get; set; } = 0;
        public int Explore { get; set; } = 0;
        public int Empire { get; set; } = 0;
        public int Federation { get; set; } = 0;
        public int Alliance { get; set; } = 0;
        public int CQC { get; set; } = 0;
    }
    //"event":"Statistics"
    public class BankAccount //+
    {
        public int Current_Wealth { get; set; } = 0;
        public int Spent_On_Ships { get; set; } = 0;
        public int Spent_On_Outfitting { get; set; } = 0;
        public int Spent_On_Repairs { get; set; } = 0;
        public int Spent_On_Fuel { get; set; } = 0;
        public int Spent_On_Ammo_Consumables { get; set; } = 0;
        public int Insurance_Claims { get; set; } = 0;
        public int Spent_On_Insurance { get; set; } = 0;
        public int Owned_Ship_Count { get; set; } = 0;
    }

    public class Combat //+
    {
        public int Bounties_Claimed { get; set; } = 0;
        public int Bounty_Hunting_Profit { get; set; } = 0;
        public int Combat_Bonds { get; set; } = 0;
        public int Combat_Bond_Profits { get; set; } = 0;
        public int Assassinations { get; set; } = 0;
        public int Assassination_Profits { get; set; } = 0;
        public int Highest_Single_Reward { get; set; } = 0;
        public int Skimmers_Killed { get; set; } = 0;
    }

    public class Crime //+
    {
        public int Notoriety { get; set; } = 0;
        public int Fines { get; set; } = 0;
        public int Total_Fines { get; set; } = 0;
        public int Bounties_Received { get; set; } = 0;
        public int Total_Bounties { get; set; } = 0;
        public int Highest_Bounty { get; set; } = 0;
    }

    public class Smuggling //+
    {
        public int Black_Markets_Traded_With { get; set; } = 0;
        public int Black_Markets_Profits { get; set; } = 0;
        public int Resources_Smuggled { get; set; } = 0;
        public double Average_Profit { get; set; } = 0;
        public int Highest_Single_Transaction { get; set; } = 0;
    }

    public class Trading //+
    {
        public int Markets_Traded_With { get; set; } = 0;
        public int Market_Profits { get; set; } = 0;
        public int Resources_Traded { get; set; } = 0;
        public double Average_Profit { get; set; } = 0;
        public int Highest_Single_Transaction { get; set; } = 0;
    }

    public class Mining //+
    {
        public int Mining_Profits { get; set; } = 0;
        public int Quantity_Mined { get; set; } = 0;
        public int Materials_Collected { get; set; } = 0;
    }

    public class Exploration //+
    {
        public int Systems_Visited { get; set; } = 0;
        public int Exploration_Profits { get; set; } = 0;
        public int Planets_Scanned_To_Level_2 { get; set; } = 0;
        public int Planets_Scanned_To_Level_3 { get; set; } = 0;
        public int Efficient_Scans { get; set; } = 0;
        public int Highest_Payout { get; set; } = 0;
        public int Total_Hyperspace_Distance { get; set; } = 0;
        public int Total_Hyperspace_Jumps { get; set; } = 0;
        public double Greatest_Distance_From_Start { get; set; } = 0;
        public int Time_Played { get; set; } = 0;
    }

    public class Passengers //+
    {
        public int Passengers_Missions_Bulk { get; set; } = 0;
        public int Passengers_Missions_VIP { get; set; } = 0;
        public int Passengers_Missions_Delivered { get; set; } = 0;
        public int Passengers_Missions_Ejected { get; set; } = 0;
    }

    public class SearchAndRescue //+
    {
        public int SearchRescue_Traded { get; set; } = 0;
        public int SearchRescue_Profit { get; set; } = 0;
        public int SearchRescue_Count { get; set; } = 0;
    }

    public class Crafting
    {
        public int Count_Of_Used_Engineers { get; set; } = 0;
        public int Recipes_Generated { get; set; } = 0;
        public int Recipes_Generated_Rank_1 { get; set; } = 0;
        public int Recipes_Generated_Rank_2 { get; set; } = 0;
        public int Recipes_Generated_Rank_3 { get; set; } = 0;
        public int Recipes_Generated_Rank_4 { get; set; } = 0;
        public int Recipes_Generated_Rank_5 { get; set; } = 0;
    }

    public class Multicrew
    {
        public int Multicrew_Time_Total { get; set; } = 0;
        public int Multicrew_Gunner_Time_Total { get; set; } = 0;
        public int Multicrew_Fighter_Time_Total { get; set; } = 0;
        public int Multicrew_Credits_Total { get; set; } = 0;
        public int Multicrew_Fines_Total { get; set; } = 0;
    }

    public class MaterialTraderStats
    {
        public int Trades_Completed { get; set; } = 0;
        public int Materials_Traded { get; set; } = 0;
    }

    public class Statistics
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public BankAccount Bank_Account { get; set; } = new BankAccount();
        public Combat Combat { get; set; } = new Combat();
        public Crime Crime { get; set; } = new Crime();
        public Smuggling Smuggling { get; set; } = new Smuggling();
        public Trading Trading { get; set; } = new Trading();
        public Mining Mining { get; set; } = new Mining();
        public Exploration Exploration { get; set; } = new Exploration();
        public Passengers Passengers { get; set; } = new Passengers();
        public SearchAndRescue Search_And_Rescue { get; set; } = new SearchAndRescue();
        public Crafting Crafting { get; set; } = new Crafting();
        public Crew Crew { get; set; } = new Crew();
        public Multicrew Multicrew { get; set; } = new Multicrew();
        public MaterialTraderStats Material_Trader_Stats { get; set; } = new MaterialTraderStats();
    }
    
    

    //"event":"Materials"
    public class Raw
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class Manufactured
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class Encoded
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class CmdrMaterials
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public List<Raw> Raw { get; set; } = new List<Raw>();
        public List<Manufactured> Manufactured { get; set; } = new List<Manufactured>();
        public List<Encoded> Encoded { get; set; } = new List<Encoded>();
    }

    //"event":"MaterialCollected"
    public class MaterialCollected
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Category { get; set; } = "";
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    //"event":"EngineerCraft"
    public class Ingredient
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class Modifier
    {
        public string Label { get; set; } = "";
        public double Value { get; set; } = 0;
        public double OriginalValue { get; set; } = 0;
        public int LessIsGood { get; set; } = 0;
    }

    public class EngineerCraft
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Slot { get; set; } = "";
        public string Module { get; set; } = "";
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string Engineer { get; set; } = "";
        public int EngineerID { get; set; } = 0;
        public int BlueprintID { get; set; } = 0;
        public string BlueprintName { get; set; } = "";
        public int Level { get; set; } = 0;
        public double Quality { get; set; } = 0;
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
    }

    //"event":"Synthesis"
    public class SyntesisMaterial
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class Synthesis
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Name { get; set; } = "";
        public List<SyntesisMaterial> Materials { get; set; } = new List<SyntesisMaterial>();
    }

    //"event":"MaterialTrade"
    public class Paid
    {
        public string Material { get; set; } = "";
        public string Material_Localised { get; set; } = "";
        public string Category { get; set; } = "";
        public int Quantity { get; set; } = 0;
    }

    public class Received
    {
        public string Material { get; set; } = "";
        public string Material_Localised { get; set; } = "";
        public string Category { get; set; } = "";
        public int Quantity { get; set; } = 0;
    }

    public class MaterialTrade
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public string TraderType { get; set; } = "";
        public Paid Paid { get; set; } = new Paid();
        public Received Received { get; set; } = new Received();
    }

    //"event":"BuyAmmo" ----
    public class BuyAmmo
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
    }

    //"event":"RefuelAll" ----
    public class RefuelAll
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
        public double Amount { get; set; } = 0;
    }

    //"event":"Repair" ----
    public class Repair
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Item { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
    }

    //"event":"BuyDrones" ----
    public class BuyDrones
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Type { get; set; } = "";
        public int Count { get; set; } = 0;
        public int BuyPrice { get; set; } = 0;
        public Int64 TotalCost { get; set; } = 0;
    }
    //"event":"Resurrect" ----
    public class Resurrect
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Option { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
        public bool Bankrupt { get; set; } = false;
    }
    //"event":"FetchRemoteModule" ----
    public class FetchRemoteModule
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public int StorageSlot { get; set; } = 0;
        public string StoredItem { get; set; } = "";
        public string StoredItem_Localised { get; set; } = "";
        public int ServerId { get; set; } = 0;
        public Int64 TransferCost { get; set; } = 0;
        public int TransferTime { get; set; } = 0;
        public string Ship { get; set; } = "";
        public int ShipID { get; set; } = 0;
    }

    //"event":"MarketBuy" ----
    public class MarketBuy
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public string Type { get; set; } = "";
        public string Type_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
        public int BuyPrice { get; set; } = 0;
        public Int64 TotalCost { get; set; } = 0;
    }

    //"event":"MarketSell" ++++
    public class MarketSell
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public string Type { get; set; } = "";
        public string Type_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
        public int SellPrice { get; set; } = 0;
        public Int64 TotalSale { get; set; } = 0;
        public int AvgPricePaid { get; set; } = 0;
    }
    //"event":"MissionCompleted" ++++
    public class CommodityReward
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public int Count { get; set; } = 0;
    }

    public class Effect
    {
        public string effect { get; set; } = "";
        public string Effect_Localised { get; set; } = "";
        public string Trend { get; set; } = "";
    }

    public class Influence
    {
        public Int64 SystemAddress { get; set; } = 0;
        public string Trend { get; set; } = "";
        public string influence { get; set; } = "";
    }

    public class FactionEffect
    {
        public string Faction { get; set; } = "";
        public List<Effect> Effects { get; set; } = new List<Effect>();
        public List<Influence> Influence { get; set; } = new List<Influence>();
        public string ReputationTrend { get; set; } = "";
        public string Reputation { get; set; } = "";
    }

    public class MissionCompleted
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Faction { get; set; } = "";
        public string Name { get; set; } = "";
        public int MissionID { get; set; } = 0;
        public string TargetFaction { get; set; } = "";
        public string DestinationSystem { get; set; } = "";
        public string DestinationStation { get; set; } = "";
        public Int64 Reward { get; set; } = 0;
        public List<CommodityReward> CommodityReward { get; set; } = new List<CommodityReward>();
        public List<FactionEffect> FactionEffects { get; set; } = new List<FactionEffect>();
    }

    //"event":"ModuleBuy" ---+++
    public class ModuleBuy
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Slot { get; set; } = "";
        public string SellItem { get; set; } = "";
        public string SellItem_Localised { get; set; } = "";
        public Int64 SellPrice { get; set; } = 0;
        public string BuyItem { get; set; } = "";
        public string BuyItem_Localised { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public Int64 BuyPrice { get; set; } = 0;
        public string Ship { get; set; } = "";
        public int ShipID { get; set; } = 0;
    }

    //"event":"ModuleSell" ++++
    public class ModuleSell
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public long MarketID { get; set; } = 0;
        public string Slot { get; set; } = "";
        public string SellItem { get; set; } = "";
        public string SellItem_Localised { get; set; } = "";
        public Int64 SellPrice { get; set; } = 0;
        public string Ship { get; set; } = "";
        public int ShipID { get; set; } = 0;
    }

    //"event":"ModuleSellRemote" ++++
    public class ModuleSellRemote
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public int StorageSlot { get; set; } = 0;
        public string SellItem { get; set; } = "";
        public string SellItem_Localised { get; set; } = "";
        public int ServerId { get; set; } = 0;
        public Int64 SellPrice { get; set; } = 0;
        public string Ship { get; set; } = "";
        public int ShipID { get; set; } = 0;
    }
    //"event":"MultiSellExplorationData" +++
    public class Discovered
    {
        public string SystemName { get; set; } = "";
        public int NumBodies { get; set; } = 0;
    }

    public class MultiSellExplorationData
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public List<Discovered> Discovered { get; set; } = new List<Discovered>();
        public int BaseValue { get; set; } = 0;
        public int Bonus { get; set; } = 0;
        public Int64 TotalEarnings { get; set; } = 0;
    }

    //"event":"PayBounties" +++
    public class PayBounties
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public Int64 Amount { get; set; } = 0;
        public string Faction { get; set; } = "";
        public string Faction_Localised { get; set; } = "";
        public int ShipID { get; set; } = 0;
        public double BrokerPercentage { get; set; } = 0;
    }

    //"event":"PayFines" ---
    public class PayFines
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public Int64 Amount { get; set; } = 0;
        public bool AllFines { get; set; } = false;
        public string Faction { get; set; } = "";
        public int ShipID { get; set; } = 0;
    }

    //"event":"PowerplayFastTrack" ---
    public class PowerplayFastTrack
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Power { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
    }
    //"event":"PowerplaySalary" +++
    public class PowerplaySalary
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Power { get; set; } = "";
        public Int64 Amount { get; set; } = 0;
    }
    //"event":"RedeemVoucher" +++
    public class Faction
    {
        public string faction { get; set; } = "";
        public int Amount { get; set; } = 0;
    }

    public class RedeemVoucher
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Type { get; set; } = "";
        public Int64 Amount { get; set; } = 0;
        public List<Faction> Factions { get; set; } = new List<Faction>();
    }
    //"event":"RepairAll" ---
    public class RepairAll
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public Int64 Cost { get; set; } = 0;
    }
    //"event":"SellDrones" +++
    public class SellDrones
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string Type { get; set; } = "";
        public int Count { get; set; } = 0;
        public int SellPrice { get; set; } = 0;
        public Int64 TotalSale { get; set; } = 0;
    }
    //"event":"ShipyardBuy" ---
    public class ShipyardBuy
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string ShipType { get; set; } = "";
        public string ShipType_Localised { get; set; } = "";
        public Int64 ShipPrice { get; set; } = 0;
        public string StoreOldShip { get; set; } = "";
        public int StoreShipID { get; set; } = 0;
        public long MarketID { get; set; } = 0;
    }
    //"event":"ShipyardSell" +++
    public class ShipyardSell
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string ShipType { get; set; } = "";
        public string ShipType_Localised { get; set; } = "";
        public int SellShipID { get; set; } = 0;
        public Int64 ShipPrice { get; set; } = 0;
        public long MarketID { get; set; } = 0;
    }
    //"event":"ShipyardTransfer" ---
    public class ShipyardTransfer
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string ShipType { get; set; } = "";
        public string ShipType_Localised { get; set; } = "";
        public int ShipID { get; set; } = 0;
        public string System { get; set; } = "";
        public long ShipMarketID { get; set; } = 0;
        public double Distance { get; set; } = 0;
        public Int64 TransferPrice { get; set; } = 0;
        public int TransferTime { get; set; } = 0;
        public long MarketID { get; set; } = 0;
    }


}

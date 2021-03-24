using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems_v2
{
    class DBTables
    {
        public DataTable systems;
        public DataTable bodies;
        public DataTable signals;
        public DataTable raw;
        public DataTable rings;
        public DataTable minerals;
        public DataTable craw;
        public DataTable cencoded;
        public DataTable cmanuf;

        public DBTables()
        {
            DataColumn column;
            //systems
            systems = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Distance",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "FCDistance",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "X",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Y",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Z",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.DateTime"),
                ColumnName = "timestamp",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            systems.Columns.Add(column);

            //bodies
            bodies = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "BKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "FullName",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Type",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Class",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Distance",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Volcanism",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "Landable",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Reserve",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            bodies.Columns.Add(column);

            //signals 
            signals = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            signals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "BKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            signals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            signals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "Count",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            signals.Columns.Add(column);

            //raw
            raw = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "BKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            raw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            raw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            raw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Percent",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            raw.Columns.Add(column);

            //rings
            rings = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "RKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "BKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "FullName",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Class",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            rings.Columns.Add(column);

            //minerals
            minerals = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "RKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "BKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.UInt64"),
                ColumnName = "SKey",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Type",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "Count",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            minerals.Columns.Add(column);

            //craw
            craw = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "key",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "en",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "ru",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "group",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "capasity",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "count",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            craw.Columns.Add(column);

            //сencoded
            cencoded = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "key",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "en",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "ru",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "group",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "capasity",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "count",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cencoded.Columns.Add(column);

            //cmanuf
            cmanuf = new DataTable();
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "key",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "en",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "ru",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "group",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "capasity",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "count",
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            cmanuf.Columns.Add(column);

        }
    }
}

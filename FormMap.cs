using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED_Systems_v2
{
    public partial class FormMap : Form
    {
        public LocalBase lb;
        private DataTable sdt;
        private DataView dv;
        public double currx;
        public double currz;

        private Bitmap map;
        private float ly; //масштаб 1св. г в пикселях
        private bool move = false;
        private int mx = 0;
        private int mz = 0;

        private float rs;
        private float re;

        private bool showNames = true;

        public FormMap()
        {
            InitializeComponent();
        }

        public void MapRefresh()
        {
            float x;
            float z;
            string y;
            string name;
            string sc;
            bool fill = true;
            bool line = false;
            bool border = false;

            float cx = pbxMap.Size.Width / 2;
            float cz = pbxMap.Size.Height / 2;

            float tx;
            float tz;

            double mapMinX = currx - pbxMap.Size.Width / 2 / ly;
            double mapMaxX = currx + pbxMap.Size.Width / 2 / ly;
            double mapMinZ = currz - pbxMap.Size.Height / 2 / ly;
            double mapMaxZ = currz + pbxMap.Size.Height / 2 / ly;

            sdt = lb.SelectSystemsForMap(mapMinX, mapMaxX, mapMinZ, mapMaxZ);
            sdt.DefaultView.Sort = "Y asc";
            sdt = sdt.DefaultView.ToTable();


            map = new Bitmap(pbxMap.Size.Width, pbxMap.Size.Height);
            pbxMap.Image = map;
            Graphics g = Graphics.FromImage(pbxMap.Image);
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen;

            pen = new Pen(Color.Gray, 1);
            g.DrawLine(pen, cx, 0, cx, pbxMap.Size.Height);
            g.DrawString("Z",
                    new Font("Segoe UI", 10),
                    new SolidBrush(Color.Gray),
                    cx + 2, 2);

            g.DrawLine(pen, 0, cz, pbxMap.Size.Width, cz);
            g.DrawString("X",
                    new Font("Segoe UI", 10),
                    new SolidBrush(Color.Gray),
                    pbxMap.Size.Width - 12, cz - 17);

            foreach (DataRow r in sdt.Rows)
            {
                pen = new Pen(Color.White, 1);
                rs = 3.5f;
                re = 7f;

                fill = true;
                line = false;
                border = false;

                x = cx + (Convert.ToSingle(r["X"]) - Convert.ToSingle(currx)) * ly;
                z = cz - (Convert.ToSingle(r["Z"]) - Convert.ToSingle(currz)) * ly;
                y = Math.Round(Convert.ToSingle(r["Y"]), 2).ToString();

                name = r["Name"].ToString();
                sc = r["Class"].ToString();
                //размер и вид
                if (sc.Contains("(0") || sc.Contains("giper giant"))
                {
                    rs = 6.0f;
                    re = 12f;
                }
                if (sc.Contains("(I") || sc.Contains("super giant"))
                {
                    rs = 5.5f;
                    re = 11f;
                }
                if (sc.Contains("(II") || sc.Contains("giant"))
                {
                    rs = 5.5f;
                    re = 11f;
                }
                if (sc.Contains("(III") || sc.Contains("giant"))
                {
                    rs = 5.0f;
                    re = 10f;
                }
                if (sc.Contains("(IV") || sc.Contains("sub giant"))
                {
                    rs = 4.5f;
                    re = 9f;
                }
                if (sc.Contains("(VI"))
                {
                    rs = 3.0f;
                    re = 6f;
                }
                if (sc.Contains("(VII"))
                {
                    rs = 2.5f;
                    re = 5f;
                }

                if (sc.Contains("SupermassiveBlackHole"))
                {
                    rs = 7.5f;
                    re = 15f;
                    fill = false;
                }
                if (sc.Contains("Giant") || sc.Contains("giant"))
                {
                    rs = 5.0f;
                    re = 10f;
                }
                if (sc.Contains("GiperGiant") || sc.Contains("giper giant"))
                {
                    rs = 6.0f;
                    re = 12f;
                }
                if (sc.Contains("SuperGiant") || sc.Contains("super giant"))
                {
                    rs = 5.5f;
                    re = 11f;
                }
                if (sc.Contains("SubGiant") || sc.Contains("sub giant"))
                {
                    rs = 4.5f;
                    re = 9f;
                }
                //цвет
                if(sc.Substring(0, 1) == "O")
                {
                    brush.Color = Color.FromArgb(255, 66, 170, 255);
                }
                if (sc.Substring(0, 1) == "B")
                {
                    brush.Color = Color.FromArgb(255, 168, 216, 255);
                }
                if (sc.Substring(0, 1) == "A")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 255);
                }
                if (sc.Substring(0, 1) == "F")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 153);
                }
                if (sc.Substring(0, 1) == "G")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 0);
                }
                if (sc.Substring(0, 1) == "K")
                {
                    brush.Color = Color.FromArgb(255, 255, 165, 0);
                }
                if (sc.Substring(0, 1) == "M")
                {
                    brush.Color = Color.FromArgb(255, 255, 0, 0);
                }
                if (sc.Substring(0, 1) == "D")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 255);
                    border = true;
                    pen = new Pen(Color.Azure, 1);
                }
                if (sc.Substring(0, 1) == "H")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 255);
                    fill = false;
                }
                if (sc.Substring(0, 1) == "L")
                {
                    brush.Color = Color.FromArgb(255, 204, 102, 0);
                }
                if (sc.Substring(0, 1) == "T")
                {
                    brush.Color = Color.FromArgb(255, 153, 77, 0);
                    border = true;
                    pen = new Pen(Color.FromArgb(255, 204, 102, 0), 1);
                }
                if (sc.Substring(0, 1) == "Y")
                {
                    brush.Color = Color.FromArgb(255, 102, 51, 0);
                    border = true;
                    pen = new Pen(Color.FromArgb(255, 204, 102, 0), 1);
                }
                if (sc.Substring(0, 3) == "TTS" || sc == "T Tauri Star")
                {
                    brush.Color = Color.FromArgb(255, 255, 255, 0);
                    border = true;
                    pen = new Pen(Color.Red, 1);
                }
                if (sc.Substring(0, 1) == "N")
                {
                    brush.Color = Color.FromArgb(255, 51, 51, 255);
                    line = true;
                    pen = new Pen(Color.FromArgb(255, 51, 51, 255), 1);
                }
                //рисование
                if (fill) g.FillEllipse(brush, x - rs, z - rs, re, re);
                    else g.DrawEllipse(pen, x - rs, z - rs, re, re);

                if(line) g.DrawLine(pen, z - 2 * re, z, z - 2 * re, z);
                if(border) g.DrawEllipse(pen, x - rs, z - rs, re, re);

                if (showNames)
                {
                    tx = x;
                    tz = z + re;

                    g.DrawString("Y:" + y + "\r\n" + name,
                    new Font("Segoe UI", 8),
                    new SolidBrush(Color.White),
                    tx, tz);
                }
            }
            toolStripStatusCoords.Text = "X: " + Math.Round(currx, 2).ToString() + "св.л " +
                " Z: " + Math.Round(currz, 2).ToString() + "св.л ";
        }

        private void FormMap_Load(object sender, EventArgs e)
        {
            ly = Convert.ToSingle(nudScale.Value);
            //sdt = lb.SelectAllSystemsForMap();
            //dv = new DataView(sdt);
            MapRefresh();
        }

        private void FormMap_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void nudScale_ValueChanged(object sender, EventArgs e)
        {
            ly = Convert.ToSingle(nudScale.Value);
            if (ly > 200f)
            {
                nudScale.Value = 200m;
                ly = 200f;
                return;
            }
            if (ly < 0.01f)
            {
                nudScale.Value = 0.01m;
                ly = 0.01f;
                return;
            }
            //---- 10-200
            if (nudScale.Maximum == 200m)
            {
                if (ly < 10f)
                {
                    nudScale.Maximum = 11m;
                    nudScale.Minimum = 0.9m;
                    nudScale.Increment = 1m;
                    nudScale.Value = 9m;
                    ly = 9f;
                    showNames = false;
                }
            }
            //--- 1-10
            if (nudScale.Maximum == 11m)
            {
                if (ly > 10f)
                {
                    nudScale.Maximum = 200m;
                    nudScale.Minimum = 9m;
                    nudScale.Increment = 10m;
                    nudScale.Value = 20m;
                    ly = 9f;
                    showNames = true;
                }
                if (ly < 1f)
                {
                    nudScale.Maximum = 1.1m;
                    nudScale.Minimum = 0.09m;
                    nudScale.Increment = 0.1m;
                    nudScale.Value = 0.9m;
                    ly = 0.9f;
                }
            }
            //--- 0.1-1
            if (nudScale.Maximum == 1.1m)
            {
                if (ly > 1f)
                {
                    nudScale.Maximum = 11m;
                    nudScale.Minimum = 0.9m;
                    nudScale.Increment = 1m;
                    nudScale.Value = 2m;
                    ly = 2f;
                }
                if (ly < 0.1f)
                {
                    nudScale.Maximum = 0.11m;
                    nudScale.Minimum = 0.01m;
                    nudScale.Increment = 0.01m;
                    nudScale.Value = 0.09m;
                    ly = 0.09f;
                }
            }
            if (nudScale.Maximum == 0.11m)
            {
                if (ly > 0.1f)
                {
                    nudScale.Maximum = 1.1m;
                    nudScale.Minimum = 0.09m;
                    nudScale.Increment = 0.1m;
                    nudScale.Value = 0.2m;
                    ly = 0.2f;
                }
            }

            MapRefresh();
        }

        private void pbxMap_SizeChanged(object sender, EventArgs e)
        {
            MapRefresh();
        }

        private void pbxMap_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                move = true;
                mx = e.X;
                mz = e.Y;
            }
            
        }

        private void pbxMap_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void pbxMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                currx += Convert.ToSingle((mx - e.X) / ly);
                currz -= Convert.ToSingle((mz - e.Y) / ly);
                mx = e.X;
                mz = e.Y;
                MapRefresh();
            }
        }

        private void pbxMap_MouseClick(object sender, MouseEventArgs e)
        {
            double clickX;
            double clickZ;
            float cx;
            float cy;

            DataTable dt;
            if(e.Button == MouseButtons.Right)
            {
                cx = pbxMap.Size.Width / 2;
                cy = pbxMap.Size.Height / 2;

                clickX = currx  - Convert.ToSingle((cx - e.X) / ly);
                clickZ = currz + Convert.ToSingle((cy - e.Y) / ly);
                double MinX = clickX - 3.5 / ly;
                double MaxX = clickX + 3.5 / ly;
                double MinZ = clickZ - 3.5 / ly;
                double MaxZ = clickZ + 3.5 / ly;

                dt = lb.SelectSystemsForMap(MinX, MaxX, MinZ, MaxZ);
                tbxInfo.Text = "";
                foreach(DataRow row in dt.Rows)
                {
                    tbxInfo.Text = tbxInfo.Text + row["Name"].ToString() + " [" +
                        row["Class"].ToString() + "] (X: " +
                        Math.Round(Convert.ToSingle(row["X"]), 2).ToString() + ", Y: " +
                        Math.Round(Convert.ToSingle(row["Y"]), 2).ToString() + ", Z: " +
                        Math.Round(Convert.ToSingle(row["Z"]), 2).ToString() + ")" +
                        System.Environment.NewLine;
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            currz += 5 / ly;
            MapRefresh();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            currz -= 5 / ly;
            MapRefresh();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            currx -= 5 / ly;
            MapRefresh();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            currx += 5 / ly;
            MapRefresh();
        }
    }
}

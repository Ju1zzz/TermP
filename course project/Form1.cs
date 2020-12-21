using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course_project
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
        Radar point1;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 5,
                SpeedMax = 15,
                ColorFrom = Color.LightPink,
                ColorTo = Color.FromArgb(0, Color.Purple),
                ParticlesPerTick = 7,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter);

            point1 = new Radar
            {
                X = picDisplay.Width / 2 ,
                Y = picDisplay.Height / 2,
            };
            emitter.impactPoints.Add(point1);
        }

        


        private void timer1_Tick(object sender, EventArgs e)
        {

            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }

            picDisplay.Invalidate();
        } 
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

            // а тут передаем положение мыши, в положение гравитона
            point1.X = e.X;
            point1.Y = e.Y;
            var g = Graphics.FromImage(picDisplay.Image);
            if (e.Button == MouseButtons.Left)
            {
                Point pos = new Point(Cursor.Position.X - e.X, Cursor.Position.Y - e.Y);
                picDisplay.Location = PointToClient(pos);
            }
        }

        private void picDispaly_MouseDown(object sender, MouseEventArgs e)
        {
            point1.X = e.X;
            point1.Y = e.Y;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace example1
{
    public partial class frmMain : Form
    {
        Random mRdm;
        Thread mThreadRed;
        Thread mThreadBlue;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            mThreadRed = new Thread(DrawRedRectangles);
            mThreadRed.Start();

            // Resume thread:  mThreadBlue.Resume()
            // Suspend thread:  mThreadRed.Suspend()

        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            mThreadBlue = new Thread(DrawBlueRectangles);
            mThreadBlue.Start();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mRdm = new Random();
            this.Width = 800;
            this.Height = 500;
        }

        private void DrawRedRectangles()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                this.CreateGraphics().DrawRectangle(new Pen(Brushes.Red, 4), new Rectangle(mRdm.Next(0, this.Width), mRdm.Next(0, this.Height), 20, 20));
            }
        }

        private void DrawBlueRectangles()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                this.CreateGraphics().DrawRectangle(new Pen(Brushes.Blue, 4), new Rectangle(mRdm.Next(0, this.Width), mRdm.Next(0, this.Height), 20, 20));
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mThreadRed != null)
                mThreadRed.Join();      // .Join() - wait for thread to finish, or .Abort() - terminate thread
            if (mThreadBlue != null)
                mThreadBlue.Join();
            MessageBox.Show("threads done");
            if ((mThreadRed != null && mThreadRed.IsAlive) || (mThreadBlue != null && mThreadBlue.IsAlive))
                e.Cancel = true;
        }
    }
}

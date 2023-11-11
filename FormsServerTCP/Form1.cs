using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlayingCardLib;
using System.IO;

namespace FormsServerTCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox cardDisplay;
        Button CardButton;
        TCPProgram connection;
        ImageList imageList;
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new TCPProgram("127.0.0.1", 2049);
            cardDisplay = CreatePircturebox(10, 10, 71, 104, Temp_Paint);
            CardButton = CreateButton(350, 10, 60, 20, "GetCard", buttonClicked);
            imageList = new ImageList();
            imageList.ImageSize = cardDisplay.Size;
            GetImages();
        }

        private Image[] GetImages()
        {
            List<Image> temp = new List<Image>();
            Deck deck = new Deck();
            try
            {
                foreach( PlayingCard card in deck.cards)
                {
                    string filepath = $"./cardimages/front/{card.getAbbrev()}.png";
                    if (!File.Exists(filepath)) continue;
                    Image img = Image.FromFile(filepath);
                    temp.Add(img);
                    imageList.Images.Add(card.getAbbrev(), img);
                }
                return temp.ToArray();
            }
            catch
            {
                return temp.ToArray();
            }
        }

        private Button CreateButton(int x, int y, int w, int h, string text, EventHandler test)
        {
            Button temp = new Button();
            temp.Text = text;
            temp.Location = new Point(x, y);
            temp.Width = w;
            temp.Height = h;
            temp.Click += test;
            Controls.Add(temp);
            return temp;
        }

        private void buttonClicked(object sender, EventArgs e)
        { 
            PlayingCard temp = connection.requestCard();
            temp.FrontImage = imageList.Images[temp.getAbbrev()];

            cardDisplay.Tag = temp;

            cardDisplay.Refresh();
        }

        private PictureBox CreatePircturebox(int x, int y, int w, int h, PaintEventHandler paintEventHandler )
        {
            PictureBox temp = new PictureBox();
            temp.Location = new Point(x, y);
            temp.Size = new Size(w, h);
            temp.Paint += paintEventHandler;
            Controls.Add(temp);
            return temp;
        }
        private void Temp_Paint(object sender, PaintEventArgs e)
        {
            PictureBox box = sender as PictureBox;
            Graphics g = e.Graphics;
            PlayingCard p = box.Tag as PlayingCard;
            if (p == null) return;
            if (p.FrontImage == null)
            {
                g.DrawString(p.getAbbrev(), new Font("comic sans", 21f), new SolidBrush(Color.Black), 0,0);
                return;
            }
            g.DrawImage(p.FrontImage, 0,0, box.Size.Width, box.Size.Height);
            
        }
    }
}

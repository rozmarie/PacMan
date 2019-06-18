using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        Maze pole;
        PacMan pacman;
        Ghost Blinky;
        Ghost Pinky;
        Ghost Inky;
        Ghost Clyde;
        static TextBox Tbox;
        Timer timer;
        static int meritko = 25;
        static int points = 0;
        public Form1()
        {
            InitializeComponent();
        }
        class PacMan
        {
            public int x = 14, y = 23;
            char state = '<';
            PictureBox PicPac;
            public PacMan(Form form)
            {
                PicPac = new PictureBox();
                PicPac.Parent = form;
                PicPac.Width = 20;
                PicPac.Height = 20;
                PicPac.BackgroundImage = Image.FromFile("pacman_right.png");
                PicPac.Top = y*meritko;
                PicPac.Left = x*meritko;
            }
            public void MakeStep(Maze pole)
            {
                if(state == 'V')
                {
                    if ((pole.field[x, y - 1] != 'X') && (pole.field[x, y - 1] != '_'))
                    {
                        if (pole.field[x, y - 1] == '.')
                        {
                            points++;
                            Tbox.Text = "Points: " + points;
                            pole.pics[x, y - 1].Visible = false;
                        }
                        pole.field[x, y] = ' ';
                        y--;
                        PicPac.Top = y * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else if(state == '>')
                {
                    if ((x == 0) && (y == 14))
                    {
                        pole.field[x,y] = ' ';
                        x = 27;
                        PicPac.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                    else if ((pole.field[x - 1, y] != 'X') && (pole.field[x - 1, y] != '_'))
                    {
                        if (pole.field[x - 1, y] == '.')
                        {
                            points++;
                            Tbox.Text = "Points: " + points;
                            pole.pics[x - 1, y].Visible = false;
                        }
                        pole.field[x, y] = ' ';
                        x--;
                        PicPac.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else if(state == 'Λ')
                {
                    if ((pole.field[x, y + 1] != 'X') && (pole.field[x, y + 1] != '_'))
                    {
                        if (pole.field[x, y + 1] == '.')
                        {
                            points++;
                            Tbox.Text = "Points: " + points;
                            pole.pics[x, y + 1].Visible = false;
                        }
                        pole.field[x, y] = ' ';
                        y++;
                        PicPac.Top = y * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else
                {
                    if ((x == 27) && (y == 14))
                    {
                        pole.field[x, y] = ' ';
                        x = 0;
                        PicPac.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                    else if ((pole.field[x + 1, y] != 'X') && (pole.field[x + 1, y] != '_'))
                    {
                        if (pole.field[x + 1, y] == '.')
                        {
                            points++;
                            Tbox.Text = "Points: " + points;
                            pole.pics[x + 1, y].Visible = false;
                        }
                        pole.field[x, y] = ' ';
                        x++;
                        PicPac.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                }
            }
            public void TurnUp(Maze pole)
            {
                state = 'V';
                PicPac.BackgroundImage = Image.FromFile("pacman_up.png");
                pole.field[x, y] = state;
            }
            public void TurnRight(Maze pole)
            {
                state = '<';
                PicPac.BackgroundImage = Image.FromFile("pacman_right.png");
                pole.field[x, y] = state;
            }
            public void TurnDown(Maze pole)
            {
                state = 'Λ';
                PicPac.BackgroundImage = Image.FromFile("pacman_down.png");
                pole.field[x, y] = state;
            }
            public void TurnLeft(Maze pole)
            {
                state = '>';
                PicPac.BackgroundImage = Image.FromFile("pacman_left.png");
                pole.field[x, y] = state;
            }
        }
        class Ghost
        {
            public int x, y;
            char state  = 'o', step = ' ';
            PictureBox Ghost_pic;
            Image[] GhostPics;
            public Ghost(Form form, int x, int y, Image picR, Image picD, Image picL, Image picU)
            {
                this.x = x;
                this.y = y;
                GhostPics = new Image[] { picR, picD, picL, picU };
                Ghost_pic = new PictureBox();
                Ghost_pic.Parent = form;
                Ghost_pic.Width = 20;
                Ghost_pic.Height = 20;
                Ghost_pic.BackgroundImage = GhostPics[0];
                Ghost_pic.Top = y * meritko;
                Ghost_pic.Left = x * meritko;
            }
            public void RandomMove(Maze pole)
            {
                if ((x == 27) && (y == 14))
                {
                    if (state == 'r')
                    {
                        pole.field[x, y] = ' ';
                        x = 0;
                        Ghost_pic.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                    else MoveLeft(pole);
                }
                else if ((x == 0) && (y == 14))
                {
                    if (state == 'l')
                    {
                        pole.field[x, y] = ' ';
                        x = 27;
                        Ghost_pic.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                    else MoveRight(pole);
                }
                else
                {
                    int spaces = FreeSpaces(pole);
                    if (spaces == 4)
                    {
                        Random rnd = new Random();
                        int movement = rnd.Next(4);
                        switch (movement)
                        {
                            case 0:
                                MoveLeft(pole);
                                break;
                            case 1:
                                MoveRight(pole);
                                break;
                            case 2:
                                MoveUp(pole);
                                break;
                            case 3:
                                MoveDown(pole);
                                break;
                        }
                    }
                    else
                    {
                        int up = 0, left = 0, down = 0, right = 0;
                        if ((pole.field[x - 1, y] != 'X') && (pole.field[x - 1, y] != 'u') && (pole.field[x - 1, y] != 'd') && (pole.field[x - 1, y] != 'r') && (pole.field[x - 1, y] != 'l')) left = 1;
                        if ((pole.field[x + 1, y] != 'X') && (pole.field[x + 1, y] != 'u') && (pole.field[x + 1, y] != 'd') && (pole.field[x + 1, y] != 'r') && (pole.field[x + 1, y] != 'l')) right = 1;
                        if ((pole.field[x, y - 1] != 'X') && (pole.field[x, y - 1] != 'u') && (pole.field[x, y - 1] != 'd') && (pole.field[x, y - 1] != 'r') && (pole.field[x, y - 1] != 'l')) up = 1;
                        if ((pole.field[x, y + 1] != 'X') && (pole.field[x, y + 1] != 'u') && (pole.field[x, y + 1] != 'd') && (pole.field[x, y + 1] != 'r') && (pole.field[x, y + 1] != 'l')) down = 1;
                        //go straight
                        if ((left == 1) && (right == 1) && (down == 0) && (up == 0)) MakeStep(pole);
                        if ((left == 0) && (right == 0) && (down == 1) && (up == 1)) MakeStep(pole);
                        //combinations of three
                        if ((left == 1) && (right == 0) && (down == 1) && (up == 1))
                        {
                            if(state == 'r')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveDown(pole);
                                        break;
                                }
                            }
                            else if(state == 'd')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveLeft(pole);
                                        break;
                                    case 1:
                                        MoveDown(pole);
                                        break;
                                }
                            }
                            else
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveLeft(pole);
                                        break;
                                }
                            }
                        }
                        if ((left == 1) && (right == 1) && (down == 0) && (up == 1))
                        {
                            if (state == 'r')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveRight(pole);
                                        break;
                                }
                            }
                            else if (state == 'd')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveLeft(pole);
                                        break;
                                    case 1:
                                        MoveRight(pole);
                                        break;
                                }
                            }
                            else
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveLeft(pole);
                                        break;
                                }
                            }
                        }
                        if ((left == 0) && (right == 1) && (down == 1) && (up == 1))
                        {
                            if (state == 'l')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveDown(pole);
                                        break;
                                }
                            }
                            else if (state == 'd')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveRight(pole);
                                        break;
                                    case 1:
                                        MoveDown(pole);
                                        break;
                                }
                            }
                            else
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveUp(pole);
                                        break;
                                    case 1:
                                        MoveRight(pole);
                                        break;
                                }
                            }
                        }
                        if ((left == 1) && (right == 1) && (down == 1) && (up == 0))
                        {
                            if (state == 'r')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveRight(pole);
                                        break;
                                    case 1:
                                        MoveDown(pole);
                                        break;
                                }
                            }
                            else if (state == 'u')
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveLeft(pole);
                                        break;
                                    case 1:
                                        MoveRight(pole);
                                        break;
                                }
                            }
                            else
                            {
                                Random rnd = new Random();
                                int movement = rnd.Next(2);
                                switch (movement)
                                {
                                    case 0:
                                        MoveDown(pole);
                                        break;
                                    case 1:
                                        MoveLeft(pole);
                                        break;
                                }
                            }
                        }
                        //corners
                        if ((left == 1) && (right == 0) && (down == 0) && (up == 1))
                        {
                            if (state == 'r') MoveUp(pole);
                            else MoveLeft(pole);
                        }
                        if ((left == 1) && (right == 0) && (down == 1) && (up == 0))
                        {
                            if (state == 'r') MoveDown(pole);
                            else MoveLeft(pole);
                        }
                        if ((left == 0) && (right == 1) && (down == 1) && (up == 0))
                        {
                            if (state == 'l') MoveDown(pole);
                            else MoveRight(pole);
                        }
                        if ((left == 0) && (right == 1) && (down == 0) && (up == 1))
                        {
                            if (state == 'l') MoveUp(pole);
                            else MoveRight(pole);
                        }
                        //one way
                        if ((left == 1) && (right == 0) && (down == 0) && (up == 0)) MoveLeft(pole);
                        if ((left == 0) && (right == 0) && (down == 0) && (up == 1)) MoveUp(pole);
                        if ((left == 0) && (right == 0) && (down == 1) && (up == 0)) MoveDown(pole);
                        if ((left == 0) && (right == 1) && (down == 0) && (up == 0)) MoveRight(pole);
                    }
                }
            }
            int FreeSpaces(Maze pole)
            {
                int spaces = 0;
                if ((pole.field[x, y - 1] != 'X') && (pole.field[x, y - 1] != 'u') && (pole.field[x, y - 1] != 'd') && (pole.field[x, y - 1] != 'r') && (pole.field[x, y - 1] != 'l')) spaces++;
                if ((pole.field[x, y + 1] != 'X') && (pole.field[x, y + 1] != 'u') && (pole.field[x, y + 1] != 'd') && (pole.field[x, y + 1] != 'r') && (pole.field[x, y + 1] != 'l')) spaces++;
                if ((pole.field[x - 1, y] != 'X') && (pole.field[x - 1, y] != 'u') && (pole.field[x - 1, y] != 'd') && (pole.field[x - 1, y] != 'r') && (pole.field[x - 1, y] != 'l')) spaces++;
                if ((pole.field[x + 1, y] != 'X') && (pole.field[x + 1, y] != 'u') && (pole.field[x + 1, y] != 'd') && (pole.field[x + 1, y] != 'r') && (pole.field[x + 1, y] != 'l')) spaces++;
                return spaces;
            }
            void MoveLeft(Maze pole)
            {
                Ghost_pic.BackgroundImage = GhostPics[2];
                state = 'l';
                pole.field[x, y] = step;
                step = pole.field[x - 1, y];
                x--;
                pole.field[x, y] = state;
                Ghost_pic.Left = x * meritko;
            }
            void MoveRight(Maze pole)
            {
                Ghost_pic.BackgroundImage = GhostPics[0];
                state = 'r';
                pole.field[x, y] = step;
                step = pole.field[x + 1, y];
                x++;
                pole.field[x, y] = state;
                Ghost_pic.Left = x * meritko;
            }
            void MoveUp(Maze pole)
            {
                Ghost_pic.BackgroundImage = GhostPics[3];
                state = 'u';
                pole.field[x, y] = step;
                step = pole.field[x, y - 1];
                y--;
                pole.field[x, y] = state;
                Ghost_pic.Top = y * meritko;
            }
            void MoveDown(Maze pole)
            {
                Ghost_pic.BackgroundImage = GhostPics[1];
                state = 'd';
                pole.field[x, y] = step;
                step = pole.field[x, y + 1];
                y++;
                pole.field[x, y] = state;
                Ghost_pic.Top = y * meritko;
            }
            public void MakeStep(Maze pole)
            {
                if (state == 'u')
                {
                    if ((pole.field[x, y - 1] != 'X') && (pole.field[x, y - 1] != 'u') && (pole.field[x, y - 1] != 'd') && (pole.field[x, y - 1] != 'r') && (pole.field[x, y - 1] != 'l'))
                    {
                        pole.field[x, y] = step;
                        step = pole.field[x, y - 1];
                        y--;
                        Ghost_pic.Top = y * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else if (state == 'l')
                {
                    if ((pole.field[x - 1, y] != 'X') && (pole.field[x - 1, y] != 'u') && (pole.field[x - 1, y] != 'd') && (pole.field[x - 1, y] != 'r') && (pole.field[x - 1, y] != 'l'))
                    {
                        pole.field[x, y] = step;
                        step = pole.field[x - 1, y];
                        x--;
                        Ghost_pic.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else if (state == 'd')
                {
                    if ((pole.field[x, y + 1] != 'X') && (pole.field[x, y + 1] != 'u') && (pole.field[x, y + 1] != 'd') && (pole.field[x, y + 1] != 'r') && (pole.field[x, y + 1] != 'l'))
                    {
                        pole.field[x, y] = step;
                        step = pole.field[x, y + 1];
                        y++;
                        Ghost_pic.Top = y * meritko;
                        pole.field[x, y] = state;
                    }
                }
                else
                {
                    if ((pole.field[x + 1, y] != 'X') && (pole.field[x + 1, y] != 'u') && (pole.field[x + 1, y] != 'd') && (pole.field[x + 1, y] != 'r') && (pole.field[x + 1, y] != 'l'))
                    {
                        pole.field[x, y] = step;
                        step = pole.field[x + 1, y];
                        x++;
                        Ghost_pic.Left = x * meritko;
                        pole.field[x, y] = state;
                    }
                }
            }
        }
        class Maze
        {
            int width, height;
            public char[,] field;
            public PictureBox[,] pics;
            public Maze()
            {
                width = 28;
                height = 31;
                field = new char[width, height];
                pics = new PictureBox[width, height];
            }
            public void GetBored(System.IO.StreamReader pacfile, Form form)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        field[x, y] = Convert.ToChar(pacfile.Read());
                        if (field[x, y] == '.')
                        {
                            pics[x, y] = new PictureBox
                            {
                                Parent = form
                            };
                            pics[x, y].Width = 5;
                            pics[x, y].Height = 5;
                            pics[x, y].BackgroundImage = Image.FromFile("food5.png");
                            pics[x, y].Left = x * meritko + 7;
                            pics[x, y].Top = y * meritko + 7;
                        }
                        else if (field[x, y] == 'X')
                        {
                            pics[x, y] = new PictureBox
                            {
                                Parent = form
                            };
                            pics[x, y].Width = 20;
                            pics[x, y].Height = 20;
                            pics[x, y].BackgroundImage = Image.FromFile("wall20.png");
                            pics[x, y].Left = x * meritko;
                            pics[x, y].Top = y * meritko;
                        }
                    }
                    pacfile.ReadLine();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Tbox = new TextBox();
            Tbox.Text = "Points: " + points;
            Tbox.Parent = this;
            Tbox.Top = 200;
            Tbox.Left = 750;
            Tbox.Enabled = false;
            Tbox.BackColor = Color.Black;
            Tbox.BorderStyle = BorderStyle.None;
            Tbox.Width = 150;
            Tbox.Font = new Font(Tbox.Font.FontFamily, 16);
            pole = new Maze();
            pacman = new PacMan(this);
            Blinky = new Ghost(this, 1, 1, Image.FromFile("red_right.png"), Image.FromFile("red_down.png"), Image.FromFile("red_left.png"), Image.FromFile("red_up.png"));
            Pinky = new Ghost(this, 26, 1, Image.FromFile("pink_right.png"), Image.FromFile("pink_down.png"), Image.FromFile("pink_left.png"), Image.FromFile("pink_up.png"));
            Inky = new Ghost(this, 1, 29, Image.FromFile("blue_right.png"), Image.FromFile("blue_down.png"), Image.FromFile("blue_left.png"), Image.FromFile("blue_up.png"));
            Clyde = new Ghost(this, 26, 29, Image.FromFile("orange_right.png"), Image.FromFile("orange_down.png"), Image.FromFile("orange_left.png"), Image.FromFile("orange_up.png"));
            System.IO.StreamReader pacfile = new System.IO.StreamReader("pac.txt");
            pole.GetBored(pacfile, this);
            timer = new Timer();
            timer.Interval = 450;
            timer.Tick += Movement;
            timer.Enabled = true;
        }
        public void Movement(object sender, EventArgs e)
        {
            pacman.MakeStep(pole);
            Inky.RandomMove(pole);
            Blinky.RandomMove(pole);
            Clyde.RandomMove(pole);
            Pinky.RandomMove(pole);
            if((pacman.x == Inky.x) && (pacman.y == Inky.y))
            {
                timer.Enabled = false;
                string msg = "You were eaten by Inky! Better luck next time.";
                string capt = "Loss!";
                MessageBoxButtons butts = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, capt, butts);
                if (res == DialogResult.OK) Close();
            }
            else if ((pacman.x == Blinky.x) && (pacman.y == Blinky.y))
            {
                timer.Enabled = false;
                string msg = "You were eaten by Blinky! Better luck next time.";
                string capt = "Loss!";
                MessageBoxButtons butts = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, capt, butts);
                if (res == DialogResult.OK) Close();
            } 
            else if ((pacman.x == Clyde.x) && (pacman.y == Clyde.y))
            {
                timer.Enabled = false;
                string msg = "You were eaten by Clyde! Better luck next time.";
                string capt = "Loss!";
                MessageBoxButtons butts = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, capt, butts);
                if (res == DialogResult.OK) Close();
            }
            else if ((pacman.x == Pinky.x) && (pacman.y == Pinky.y))
            {
                timer.Enabled = false;
                string msg = "You were eaten by Pinky! Better luck next time.";
                string capt = "Loss!";
                MessageBoxButtons butts = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, capt, butts);
                if (res == DialogResult.OK) Close();
            }
            if (points == 279)
            {
                timer.Enabled = false;
                string msg = "You ate all the food and dodged the ghosts!";
                string capt = "Victory!";
                MessageBoxButtons butts = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, capt, butts);
                if (res == DialogResult.OK) Close();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    pacman.TurnUp(pole);
                    break;
                case Keys.Left:
                case Keys.A:
                    pacman.TurnLeft(pole);
                    break;
                case Keys.Down:
                case Keys.S:
                    pacman.TurnDown(pole);
                    break;
                case Keys.Right:
                case Keys.D:
                    pacman.TurnRight(pole);
                    break;
            }
        }
    
    }
}

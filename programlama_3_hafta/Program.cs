using System;
using System.Threading;
using System.Xml.Linq;

namespace programlama_3_hafta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Creater creater = new Creater(false ,0 ,"right");
            Game_Bord_Border game_Bord_Border = new Game_Bord_Border();
            Physical physical = new Physical();
            int x = 1;
            int y = 1;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow) physical.Jump();
                    //else if (key == ConsoleKey.DownArrow)   game_Bord_Border.y_Border("down", ref y);
                    else if (key == ConsoleKey.LeftArrow)  game_Bord_Border.x_Border("left", ref x);
                    else if (key == ConsoleKey.RightArrow) game_Bord_Border.x_Border("right", ref x);
                }

                int milliseconds = 10;
                physical.physical_update(ref y);
                creater.create_Update(ref x, ref y);
                Thread.Sleep(milliseconds);
                Console.Clear();
            }
        }
    }

    public class Creater
    {
        private bool game_Over_Toggle;
        private int score;
        private string score_Toggle;
        public Creater(bool game_Over_Taggle, int score, string score_Toggle)
        {
            this.game_Over_Toggle = game_Over_Taggle;
            this.score = score;
            this.score_Toggle = score_Toggle;
        }
        public bool Game_Over_Toggle   // property
        {
            get { return game_Over_Toggle; }   // get method
            set { game_Over_Toggle = value; }  // set method
        }

        public void create_Update( ref int x, ref int y)
        {
            

            if (game_Over_Toggle == true)
            {
                game_Over_Page();
                return;
            }
            else
            {
                Cube_Creater(ref x, ref y);
                ground_Creaater(ref y);
                score_Creater(score);
                game_Name_Creater();
                if ((x < 58 && x > 50|| x < 33 && x > 25) && y == 28)
                    game_Over_Toggle = true;

                if (x == 15 && (y == 28 || y == 27) && score_Toggle == "left")
                {
                    score_Toggle = "right";
                    score++;
                }
                if (x == 75 && (y == 28 || y == 27) && score_Toggle == "right")
                {
                    score_Toggle = "left";
                    score++;
                }

            }

        }
        public void Cube_Creater(ref int x ,ref int y )
        {
            for (int i = 0; i < y; i++) Console.WriteLine(""); // satır
            for (int j = 0; j < x; j++) Console.Write("  ");
            Console.Write("+---+\n");
            for (int j = 0; j < x; j++) Console.Write("  ");
            Console.WriteLine("|   |");
            for (int j = 0; j < x; j++) Console.Write("  ");
            Console.WriteLine("+---+");
        }

        public void ground_Creaater(ref int cude_Position)
        {
            int ground_Base_Position = 28;
            int ground_Position = ground_Base_Position  - cude_Position; 
            for (int i = 0; i < ground_Position; i++) Console.WriteLine("");
            for (int i = 0; i < 170; i++)
            {
                if(i<120 && i>100) Console.Write(" ");
                else if (i < 70 && i > 50) Console.Write(" ");
                else
                Console.Write("-");
            }
        }
        public void game_Over_Page()
        {
            for (int i = 0; i < 7; i++) Console.WriteLine("");
            Console.WriteLine("         ******  ******* **     ** ******      ****** *         * ****** ******\r\n");
            Console.WriteLine("         *       *     * * *   * * *           *    *  *       *  *      *    *\r\n");
            Console.WriteLine("         *  ***  * *** * *  ***  * *****       *    *   *     *   *****  ******\r\n");
            Console.WriteLine("         *    *  *     * *       * *           *    *    *   *    *      *  *  \r\n");
            Console.WriteLine("         ******  *     * *       * ******      ******     * *     ****** *    *\r\n");
            Console.WriteLine("                                       GAME OVER");
        }

        public void score_Creater(int score)
        {
            Console.WriteLine(" \n\n         Score: {0}", score);
        }

        void game_Name_Creater()
        {
            Console.WriteLine(" \n CUBE GAME \n\n verison 0.2");
        }
        #region flag
        //public void flag_Creater(int num, ref int cude_Position)
        //{
        //    int flag_Base_Position = 28;            
        //    int flag_Position = flag_Base_Position - cude_Position;
        //    for (int i = 0; i < flag_Position; i++) Console.WriteLine("");
        //    if (num == 1)
        //    {
        //        flag();
        //    }
        //}
        ////   
        ///*    +---+
        // *    |   |
        // *    +---+
        // *    *
        // *    *
        // */
        //public void flag()
        //{
        //    Console.Write("+---+\n");
        //    Console.Write("|   |\n");
        //    Console.Write("+---+\n");
        //    Console.Write("*\n");
        //    Console.Write("*");
        //}
        #endregion
    }

    public class Game_Bord_Border
    {
        public void x_Border(string Arrow, ref int x )
        {
            int top_Border = 0;
            int lower_Border = 82;
            // false up and true down
            if (Arrow == "left") { 
                x--;
                if(x < top_Border) x = top_Border;
            }
            else
            {
                x++;
                if(x > lower_Border) x = lower_Border;
            }

        }
        public void y_Border(string Arrow , ref int y)
        {
            // false up and true down
            int top_Border = 0;
            int lower_Border = 28;
            if (Arrow == "up")
            {
                y--;
                if (y < top_Border) y = top_Border;
            }
            else
            {
                y++;
                if (y > lower_Border) y = lower_Border;
            }

        }
    }

    public class Physical
    {
        Game_Bord_Border game_Bord_Border = new Game_Bord_Border();
        private int jump = 10;
        public void Gravity(ref int y, bool gravity = true)
        {
            // default gravity 6/s hzi
            if (gravity == true) {
                game_Bord_Border.y_Border("down", ref y);
            }
        }
        public void Jump()
        {
            jump = 0;
        }

        public void physical_update(ref int y)
        {
            if (jump != 10)
            {
                game_Bord_Border.y_Border("up", ref y);
                jump++;
            }
            else
                Gravity(ref y);
        }
    }

}


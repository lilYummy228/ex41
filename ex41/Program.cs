using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex41
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(1123, "elleeee", 10, true);
            DataBase players = new DataBase(player);

            players.ShowInfo();
        }
    }

    class DataBase
    {
        public Player Player;

        public DataBase(Player player)
        {
            Player = player;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID:{Player.ID}\nNickName: {Player.NickName}\nУровень: {Player.Level}\nФлаг: {Player.IsBannedFlag}");
        }
    }

    class Player
    {
        public int ID;
        public string NickName;
        public int Level;
        public bool IsBannedFlag;

        public Player(int id, string nickName, int level, bool isBannedFlag)
        {
            ID = id;
            NickName = nickName;
            Level = level;
            IsBannedFlag = isBannedFlag;
        }
    }
}

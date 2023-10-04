using System;
using System.Collections.Generic;

namespace ex41
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandBanPlayer = "2";
            const string CommandUnBanPlayer = "3";
            const string CommandDeletePlayer = "4";
            const string CommandShowAllPlayers = "5";
            const string CommandExit = "6";

            bool isOpen = true;
            DataBase dataBase = new DataBase();

            while (isOpen)
            {
                Console.WriteLine("База данных игроков\n");
                Console.Write($"{CommandAddPlayer} - Добавить игрока\n{CommandBanPlayer} - Забанить игрока\n{CommandUnBanPlayer} - Разбанить игрока\n" +
                    $"{CommandDeletePlayer} - Удалить игрока\n{CommandShowAllPlayers} - Показать список всех игроков\n" +
                    $"{CommandExit} - Выход из программы\n\nКакую операцию хотите выполнить? ");
                string chosenOperation = Console.ReadLine();

                switch (chosenOperation)
                {
                    case CommandAddPlayer:
                        dataBase.AddPlayer();
                        break;

                    case CommandBanPlayer:
                        dataBase.BanPlayer();
                        break;

                    case CommandUnBanPlayer:
                        dataBase.UnBanPlayer();
                        break;

                    case CommandDeletePlayer:
                        dataBase.DeletePlayer();
                        break;

                    case CommandShowAllPlayers:
                        dataBase.ShowDataPlayers();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class DataBase
    {
        private Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _playerID;

        public DataBase()
        {
            _playerID = 0;
        }
        public void AddPlayer()
        {
            string answerYes = "1";
            string answerNo = "2";
            bool isBanned;

            Console.Write("Введите имя игрока: ");
            string name = Console.ReadLine();
            Console.Write("Введите уровень игрока: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int level);

            if (isNumber == false)
            {
                Console.WriteLine("Неккоректный ввод...");
                return;
            }

            if (level <= 0)
            {
                level = 1;
            }

            Console.Write($"{answerYes} - да\n{answerNo} - нет\nИгрок находится в бане? ");
            string input = Console.ReadLine();

            if (input == answerYes)
            {
                isBanned = true;
            }
            else if (input == answerNo)
            {
                isBanned = false;
            }
            else
            {
                Console.WriteLine("Неккоректный ввод...");
                return;
            }

            _players.Add(_playerID, new Player(name, level, isBanned));
            _playerID++;
            Console.WriteLine($"Игрок с ником {name} успешно добавлен...");
        }

        public bool ShowDataPlayers()
        {
            if (_players.Count != 0)
            {
                foreach (var player in _players)
                {
                    Console.Write($"{player.Key + 1}. ");
                    _players[player.Key].ShowInfo();
                    Console.WriteLine();
                }

                return true;
            }
            else
            {
                Console.WriteLine("Список игроков пуст...");
                return false;
            }
        }

        public void BanPlayer()
        {
            bool isFilled = ShowDataPlayers();

            if (isFilled)
            {
                Console.Write("\nИгрока под каким номером вы хотите забанить? ");
                bool isNumber = int.TryParse(Console.ReadLine(), out int number);

                if (isNumber = CheckInput(isNumber, number))
                {
                    if (_players[number - 1].IsBanned == false)
                    {
                        _players[number - 1].Ban();

                        Console.WriteLine($"Игрок под номером {number} успешно забанен...");
                    }
                    else
                    {
                        Console.WriteLine("Игрок уже в бане...");
                    }
                }
            }
        }

        public void UnBanPlayer()
        {
            bool isFilled = ShowDataPlayers();

            if (isFilled)
            {
                Console.Write("Игрока под каким номером вы хотите разбанить? ");
                bool isNumber = int.TryParse(Console.ReadLine(), out int number);

                if (isNumber = CheckInput(isNumber, number))
                {
                    if (_players[number - 1].IsBanned)
                    {
                        _players[number - 1].UnBan();

                        Console.WriteLine($"Игрок под номером {number} успешно разбанен");
                    }
                    else
                    {
                        Console.WriteLine("Игрок итак не в бане");
                    }
                }
            }
        }

        public void DeletePlayer()
        {
            bool isFilled = ShowDataPlayers();

            if (isFilled)
            {
                Console.WriteLine("Игрока под каким номером вы хотите удалить? ");
                bool isNumber = int.TryParse(Console.ReadLine(), out int number);

                if (isNumber = CheckInput(isNumber, number))
                {
                    _players.Remove(number - 1);

                    Console.WriteLine($"Игрок под номером {number} успешно удален...");
                }
            }
        }

        public bool CheckInput(bool isNumber, int number)
        {
            if (isNumber && _players.ContainsKey(number - 1))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Неккоректный ввод...");
                return false;
            }
        }
    }

    class Player
    {
        public Player(string nickName, int level, bool isBanned)
        {
            NickName = nickName;
            Level = level;
            IsBanned = isBanned;
        }

        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя игрока: {NickName}\nУровень игрока: {Level}");

            if (IsBanned == true)
            {
                Console.WriteLine("Игрок забанен");
            }
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void UnBan()
        {
            IsBanned = false;
        }
    }
}


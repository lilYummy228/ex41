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
            Database database = new Database();

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
                        database.AddPlayer();
                        break;

                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandUnBanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandDeletePlayer:
                        database.DeletePlayer();
                        break;

                    case CommandShowAllPlayers:
                        database.ShowDataPlayers();
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

    class Database
    {
        private Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _playerID;

        public Database()
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

            if (int.TryParse(Console.ReadLine(), out int level) == false)
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
            Console.WriteLine($"Игрок с ником {name} успешно добавлен...");
            _playerID++;
        }

        public void ShowDataPlayers()
        {
            Console.Clear();
            Console.WriteLine("Список всех игроков:");

            if (_players.Count != 0)
            {
                foreach (var player in _players)
                {
                    Console.Write($"ID: {player.Key}\n");
                    _players[player.Key].ShowInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Список игроков пуст...");
            }
        }

        public void BanPlayer()
        {
            ShowDataPlayers();

            if (IsFilled())
            {
                Console.Write("\nИгрока под каким ID вы хотите забанить? ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (_players.ContainsKey(id))
                    {
                        if (_players[id].IsBanned == false)
                        {
                            _players[id].Ban();

                            Console.WriteLine($"Игрок под ID {id} успешно забанен...");
                        }
                        else
                        {
                            Console.WriteLine("Игрок уже в бане...");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Игрока под ID {id} не существует...");
                    }
                }
                else
                {
                    Console.WriteLine("Неккоректный ввод...");
                }
            }
        }

        public void UnbanPlayer()
        {
            ShowDataPlayers();

            if (IsFilled())
            {
                Console.Write("Игрока под каким ID вы хотите разбанить? ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (_players.ContainsKey(id))
                    {
                        if (_players[id].IsBanned)
                        {
                            _players[id].UnBan();

                            Console.WriteLine($"Игрок под ID {id} успешно разбанен");
                        }
                        else
                        {
                            Console.WriteLine("Игрок итак не в бане");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Игрока под ID {id} не существует...");
                    }
                }
                else
                {
                    Console.WriteLine("Неккоректный ввод...");
                }
            }
        }

        public void DeletePlayer()
        {
            ShowDataPlayers();

            if (IsFilled())
            {
                Console.WriteLine("Игрока под каким ID вы хотите удалить? ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (_players.ContainsKey(id))
                    {
                        _players.Remove(id);

                        Console.WriteLine($"Игрок под ID {id} успешно удален...");
                    }
                    else
                    {
                        Console.WriteLine($"Игрока под ID {id} не существует...");
                    }
                }
                else
                {
                    Console.WriteLine("Неккоректный ввод...");
                }
            }
        }

        private bool IsFilled()
        {
            if (_players.Count > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Список игроков пуст...");
                return false;
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
}


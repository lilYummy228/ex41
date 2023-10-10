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
                Console.Write($"{CommandAddPlayer} - Добавить игрока\n" +
                    $"{CommandBanPlayer} - Забанить игрока\n" +
                    $"{CommandUnBanPlayer} - Разбанить игрока\n" +
                    $"{CommandDeletePlayer} - Удалить игрока\n" +
                    $"{CommandShowAllPlayers} - Показать список всех игроков\n" +
                    $"{CommandExit} - Выход из программы\n" +
                    $"\nКакую операцию хотите выполнить? ");

                switch (Console.ReadLine())
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
        private int _playerId;

        public Database()
        {
            _playerId = 0;
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

            _players.Add(_playerId, new Player(_playerId, name, level, isBanned));
            Console.WriteLine($"Игрок с ником {name} успешно добавлен...");
            _playerId++;
        }

        public void ShowDataPlayers()
        {
            Console.Clear();
            Console.WriteLine("Список всех игроков:");

            if (IsFilled())
            {
                foreach (var player in _players)
                {
                    Console.Write($"ID: {player.Key}\n");
                    _players[player.Key].ShowInfo();
                    Console.WriteLine();
                }
            }
        }

        public void BanPlayer()
        {
            ShowDataPlayers();

            if (IsFilled())
            {
                if (IsPlayerFoundById(out Player player))
                {
                    if (player.IsBanned == false)
                    {
                        _players[player.Id].Ban();

                        Console.WriteLine($"Игрок под ID {player.Id} успешно забанен");
                    }
                    else
                    {
                        Console.WriteLine("Игрок итак в бане");
                    }
                }
            }
        }

        public void UnbanPlayer()
        {
            ShowDataPlayers();

            if (IsFilled())
            {
                if (IsPlayerFoundById(out Player player))
                {
                    if (player.IsBanned)
                    {
                        _players[player.Id].UnBan();

                        Console.WriteLine($"Игрок под ID {player.Id} успешно разбанен");
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
            ShowDataPlayers();

            if (IsFilled())
            {
                if (IsPlayerFoundById(out Player player))
                {
                    _players.Remove(player.Id);
                    Console.WriteLine($"Игрок под ID {player.Id} успешно удален...");
                }
            }
        }

        private bool IsPlayerFoundById(out Player player)
        {
            Console.Write("Введите ID игрока: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_players.ContainsKey(id))
                {
                    player = _players[id];
                    return true;
                }
                else
                {
                    Console.WriteLine($"Игрока под ID {id} не существует...");
                    player = null;
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Неккоректный ввод...");
                player = null;
                return false;
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
    }

    class Player
    {
        public Player(int id, string nickName, int level, bool isBanned)
        {
            Id = id;
            NickName = nickName;
            Level = level;
            IsBanned = isBanned;
        }

        public int Id { get; private set; }
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


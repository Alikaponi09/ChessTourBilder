using MauiApp3.Data.Controler;
using MauiApp3.Data.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MauiApp3.Data
{
    internal class Helper
    {
        static Regex regex = new Regex("[^а-яА-Яa-zA-Z]");

        public static Hashtable StringToInt = new Hashtable(new Dictionary<char, int>()
            {
                {'A', 1 },
                {'B', 2 },
                {'C', 3 },
                {'D', 4 },
                {'E', 5 },
                {'F', 6 },
                {'G', 7 },
                {'H', 8 }
            }
        );

        public static string[] IntToString = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };

        private static string Text() => $"Поле не должно быть пустым";
        private static string Text(string str) => $"Поле {str} не должно быть пустым";
        public static string FI() => OrganizerControler.nowOrganizer.FirstName + " " + OrganizerControler.nowOrganizer.MiddleName;
        public static bool CheckDeleteButton() => OrganizerControler.nowOrganizer.OrganizerID == EventControler.nowEvent.OrganizerID || OrganizerControler.nowOrganizer.Administrator != -1;

        public static bool CheckOrganizer(Organizer organizer, ref string[] bools)
        {
            if (string.IsNullOrWhiteSpace(organizer.FirstName))
                bools[0] = Text();
            else if (regex.IsMatch(organizer.FirstName))
                bools[0] = "Поле может содержать только буквы";

            if (string.IsNullOrWhiteSpace(organizer.MiddleName))
                bools[1] = Text();
            else if (regex.IsMatch(organizer.MiddleName))
                bools[1] = "Поле может содержать только буквы";

            if (!string.IsNullOrWhiteSpace(organizer.LastName))
                if (regex.IsMatch(organizer.LastName))
                    bools[2] = "Поле может содержать только буквы";

            if (string.IsNullOrWhiteSpace(organizer.Login))
                bools[3] = Text();
            else if (OrganizerControler.Get().Where(p => p.Login == organizer.Login).FirstOrDefault() != default(Organizer))
                bools[3] = "Пользователь уже существует";

            if (string.IsNullOrWhiteSpace(organizer.Password))
                bools[4] = Text();


            return bools.All(p => p == null);
        }

        public static bool CheckPlayer(Player player, ref string[] bools)
        {
            if (player.FIDEID.ToString().Length != 7)
                bools[0] = "FIDEID должен состоять из 7 цифр";

            if (PlayerControler.Get().Where(p => p.FIDEID == player.FIDEID).FirstOrDefault() != default(Player))
                bools[0] = "Игрок уже существует";

            if (string.IsNullOrWhiteSpace(player.FirstName))
                bools[1] = Text();

            if (string.IsNullOrWhiteSpace(player.MiddleName))
                bools[2] = Text();

            if (player.Birthday == default(DateTime))
                bools[4] = Text();

            if (player.ELORating < 0)
                bools[3] = "ЕLO не может быть меньше 0";

            if (string.IsNullOrWhiteSpace(player.Contry))
                bools[6] = Text();

            return bools.All(p => p == null);
        }

        public static bool CheckConsignment(Consignment consignment, ref string[] bools)
        {
            if (consignment.DateStart == default(DateTime))
                bools[0] = Text();
            else if (consignment.DateStart < DateTime.Now)
                bools[0] = "Не может быть меньше сегоднящней";

            if (consignment.blackPlayer.PlayerID == 0)
                bools[1] = "Игрок не выбран";

            if (consignment.whitePlayer.PlayerID == 0)
                bools[2] = "Игрок не выбран";
            else if (consignment.blackPlayer.PlayerID == consignment.whitePlayer.PlayerID)
                bools[2] = "Человек не может играть сам с собой";

            return bools.All(p => p == null);
        }

        public static bool CheckEvent(Event @event, ref string[] bools)
        {
            if (string.IsNullOrWhiteSpace(@event.Name))
                bools[0] = Text();

            if (@event.PrizeFund <= 0)
                bools[1] = "Призовой фонд должен быть больше 0";

            if (string.IsNullOrWhiteSpace(@event.LocationEvent))
                bools[2] = Text();

            if (@event.DataStart == default(DateTime))
                bools[3] = Text();
            else if (@event.DataStart < DateTime.Now)
                bools[3] = "не может быть меньше сегоднящней";

            if (@event.DataFinish == default(DateTime))
                bools[4] = Text();
            else if (@event.DataFinish < @event.DataStart)
                bools[4] = "не может быть меньше чем Дата начала";

            return bools.All(p => p == null);
        }
    }
}

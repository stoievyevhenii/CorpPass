using CorpPass.Models;
using System.Collections.Generic;

namespace CorpPass.Services
{
    public static class ItemConverter
    {
        private static readonly char NewLineSymbol = '#';

        public static string ConvertItemToCSV(Item item)
        {
            return $"{item.Created},{item.Description.Replace('\n', NewLineSymbol)},{item.Folder},{item.Group},{item.Icon},{item.IsLeaked},{item.LastModified},{item.Login},{item.Name},{item.Password},{item.PasswordScore},{item.Id}";
        }
        public static List<Item> ConvertCSVToItemModel(string csvText)
        {
            var itemsList = new List<Item>();
            var csvLines = csvText.Trim().Split('\n');

            foreach (var item in csvLines)
            {
                var itemLine = item.Split(',');

                var itemModel = new Item()
                {
                    Created = itemLine[0],
                    Description = itemLine[1],
                    Folder = itemLine[2],
                    Group = itemLine[3],
                    Icon = itemLine[4],
                    IsLeaked = bool.Parse(itemLine[5]),
                    LastModified = itemLine[6],
                    Login = itemLine[7],
                    Name = itemLine[8],
                    Password = itemLine[9],
                    PasswordScore = double.Parse(itemLine[10]),
                    Id = itemLine[11],
                };

                itemsList.Add(itemModel);
            }

            return itemsList;
        }
    }
}

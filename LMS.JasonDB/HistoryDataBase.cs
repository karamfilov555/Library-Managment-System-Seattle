using LMS.Contracts.DataBaseContracts;
using LMS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LMS.JsonDB
{
    public class HistoryDataBase : IHistoryDataBase
    {
        private const string historyPath = @"../../../CheckOutHistory.json";
        public HistoryDataBase()
        {
        }
        public IList<HistoryRegistry> ReadCheckOutHistory()
        {
            string jsonIn = File.ReadAllText(historyPath);
            var existingHistory = JsonConvert.DeserializeObject<List<HistoryRegistry>>(jsonIn);
            return existingHistory;
        }
        public void WriteCheckOutHistory(string jsonToOutput)
        {
            File.WriteAllText(historyPath, jsonToOutput);
        }
        public void AddToCheckOutHistoryJson(string title, string isbn, string username, string returnDate)
        {
            var initialFile = File.ReadAllText(historyPath);
            var array = JArray.Parse(initialFile);
            var bookToAdd = new JObject();

            bookToAdd["Title"] = title;
            bookToAdd["ISBN"] = isbn;
            bookToAdd["Username"] = username;
            bookToAdd["ReturnDate"] = returnDate;
            array.Add(bookToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            WriteCheckOutHistory(jsonToOutput);
        }
    }
}

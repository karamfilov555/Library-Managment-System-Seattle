using LMS.Contracts.DataBaseContracts;
using LMS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void RemoveRegistryFromJsonDb(string isbn)
        {
            var existingHistory = ReadCheckOutHistory();
            var registy = existingHistory.FirstOrDefault(x => x.ISBN == isbn);
            existingHistory.Remove(registy);
            var jsonToOutput = JsonConvert.SerializeObject(existingHistory, Formatting.Indented);
            WriteCheckOutHistory(jsonToOutput);
        }
        public void AddToCheckOutHistoryJson(string title, string author,int pages, int year, string country,string language,string subject, string isbn,string username, string returnDate)
        {
            var initialFile = File.ReadAllText(historyPath);
            var array = JArray.Parse(initialFile);
            var bookToAdd = new JObject();
            bookToAdd["Title"] = title;
            bookToAdd["Author"] = author;
            bookToAdd["Pages"] = pages;
            bookToAdd["Year"] = year;
            bookToAdd["Country"] = country;
            bookToAdd["Language"] = language;
            bookToAdd["Subject"] = subject;
            bookToAdd["ISBN"] = isbn;
            bookToAdd["Username"] = username;
            bookToAdd["ReturnDate"] = returnDate;
            array.Add(bookToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            WriteCheckOutHistory(jsonToOutput);
        }
    }
}

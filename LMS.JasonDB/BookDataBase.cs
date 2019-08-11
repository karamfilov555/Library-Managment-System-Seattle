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
    public class BookDataBase : IBookDataBase
    {
        private const string bookPath = @"../../../Books.json";
        public BookDataBase()
        {
        }
        public IList<Book> ReadBooks()
        {
            string jsonIn = File.ReadAllText(bookPath);
            var existingBooks = JsonConvert.DeserializeObject<IList<Book>>(jsonIn);
            return existingBooks;
        }
        public void WriteBooks(string jsonToOutput)
        {
            File.WriteAllText(bookPath, jsonToOutput);
        }
        public void AddBookToJsonDB(string title, string author, int pages, int year, string country, string language, string subject, string isbn)
        {
            var initialFile = File.ReadAllText(bookPath);
            var array = JArray.Parse(initialFile);
            var bookToAdd = new JObject();
            bookToAdd["Author"] = author;
            bookToAdd["Country"] = country;
            bookToAdd["Language"] = language;
            bookToAdd["Subject"] = subject;
            bookToAdd["Pages"] = pages;
            bookToAdd["Title"] = title;
            bookToAdd["Year"] = year;
            bookToAdd["Rack"] = title[0].ToString().ToUpper();
            bookToAdd["Reservation"] = "Available";
            bookToAdd["ISBN"] = isbn;
            array.Add(bookToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(bookPath, jsonToOutput);
        }
        public void RemoveBookFromJsonDb(string title)
        {
            var existingBooks = ReadBooks();
            var book = existingBooks.FirstOrDefault(x => x.Title == title);
            existingBooks.Remove(book);
            var jsonToOutput = JsonConvert.SerializeObject(existingBooks, Formatting.Indented);
            WriteBooks(jsonToOutput);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Models;
using LMS.Services.Contracts;

namespace LMS.Core.Commands
{
    public class SearchBooksCommand : ICommand
    {
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        private readonly IBookServices _bookServices;

        public SearchBooksCommand(IInputReader reader, IOutputWriter writer, IBookServices bookServices)
        {
            _reader = reader;
            _writer = writer;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _writer.WriteLine("Enter search criteria\r\n(author,title,year,language)");
            var searchcriteria = _reader.ReadLine().ToLower();
            string result = string.Empty;
            switch (searchcriteria)
            {
                case "author":
                    _writer.WriteLine("Enter author:");
                    var authorName = _reader.ReadLine();
                    result = _bookServices.AllBooksToString(_bookServices.SearchByAuthor(authorName));
                    break;
                case "title":
                    _writer.WriteLine("Enter title:");
                    var title = _reader.ReadLine();
                    result = _bookServices.AllBooksToString(_bookServices.SearchByTitle(title));
                    break;
                case "language":
                    _writer.WriteLine("Enter language:");
                    var language = _reader.ReadLine();
                    result = _bookServices.AllBooksToString(_bookServices.SearchByLanguage(language));
                    break;
                //case "subject":
                //    _writer.WriteLine("Enter subject:");
                //    var subject = _reader.ReadLine();
                //    result = _bookServices.AllBooksToString(_bookServices.SearchByTitle(subject));
                //    break;
                case "year":
                    _writer.WriteLine("Enter year:");
                    var year = int.Parse(_reader.ReadLine());
                    result = _bookServices.AllBooksToString(_bookServices.SearchByYear(year));
                    break;
                default: throw new ArgumentException("Suck criteria does not exist!");
            }

            if (result == string.Empty)
            {
                return "No results were found!";
            }
            return $"{result}" +
                   "\nresult from search!";
        }
    }
}

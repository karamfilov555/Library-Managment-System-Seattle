//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace LMS.Services
//{
//    public class BackgroundServices
//    {
//        public BackgroundServices()
//        {

//        }

//        public async static Task ReturnBooks()
//        {
//            const int dayInMilisec = 86400000;

//            //TO DO
//            var midnight = DateTime.Now - DateTime.Today.AddDays(1);
//            //var differnce = (midnight - DateTime.Now)

//            var startTime = 0;
//            var dateForCheck = DateTime.Now;

//            var timer = new Timer(ReturnExpiredBooksInDb, dateForCheck, startTime, dayInMilisec);
//        }

//        private static void ReturnExpiredBooksInDb(object stateInfo)
//        {
//            var expiredBooks = _context.HistoryRegistries
//                            .Where(b => b.ReturnDate >= (DateTime)stateInfo)
//                            .Select(x => x.IsReturned == true);

//            _context.SaveChanges();
//        }
//    }
//}



using LMS.Data;
using LMS.Models;
using LMS.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ReturningBooksService : IHostedService, IDisposable
{
    private Timer _timer;
    private IServiceProvider services;

    public ReturningBooksService(IServiceProvider serviceProvider)
    {
        services = serviceProvider;
    }
    public Task StartAsync(CancellationToken stoppingToken)
    {
        const int dayInMilisec = 86400000;

        //TO DO
        var midnight = DateTime.Now - DateTime.Today.AddDays(1);
        //var differnce = (midnight - DateTime.Now)

        var startTime = 0;
        var dateForCheck = DateTime.Now;

        var timer = new Timer(ReturnExpiredBooksInDb, dateForCheck, startTime, dayInMilisec);

        return Task.CompletedTask;
    }

    private void ReturnExpiredBooksInDb(object stateInfo)
    {
        using (var scope = services.CreateScope())
        {
            var context =
                scope.ServiceProvider
                    .GetRequiredService<LMSContext>();
          
            var historyRegistriesToChange = context.HistoryRegistries.Where(x => x.ReturnDate < DateTime.Now).ToList();

            foreach (var item in historyRegistriesToChange)
            {
                ChangeToReturned(item);
                context.Books.First(x => x.Id == item.BookId).IsCheckedOut = false;
            }

            context.SaveChanges();
        }    
    }

    private void ChangeToReturned(HistoryRegistry historyRegistryToChange)
    {
        historyRegistryToChange.IsReturned = true;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
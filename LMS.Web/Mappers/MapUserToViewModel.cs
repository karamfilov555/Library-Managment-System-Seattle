using LMS.Models;
using LMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Mappers
{
    public static class MapUserToViewModel
    {
        public static UserViewModel MapToViewModel(this User user)
        {
            var viewModel = new UserViewModel();
            viewModel.Id = user.Id;
            //viewModel.Role = user.Roles;
            viewModel.Username = user.UserName;
            return viewModel;
        }
        //public static TeamViewModel MapToViewModel(this Team team)
        //{
        //    var viewModel = new TeamViewModel();
        //    viewModel.Id = team.Id;
        //    viewModel.Name = team.Name;
        //    viewModel.TotalGoals = team.Players.Sum(p => p.Goals);
        //    viewModel.PlayersCount = team.Players.Count;
        //    return viewModel;
        //}
    }
}

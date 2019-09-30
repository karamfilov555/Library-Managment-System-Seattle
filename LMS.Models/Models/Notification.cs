﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class Notification
    {
        public Notification()
        {

        }
        [Required]
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Username { get; set; }
        public bool IsSeen { get; set; }

    }
}

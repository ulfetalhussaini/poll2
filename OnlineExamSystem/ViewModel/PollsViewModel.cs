using OnlineExamSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamSystem.ViewModel
{
    public class PollsViewModel
    {
        public Poll Poll { get; set; }
        public PollOption PollOption { get; set; }

        public int PollCount { get; set; }
        public int PollId { get; set; }

         public int C_id { get; set; }

        public string pollsubject { get; set; }
        public List<string> optiontext { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExamSystem.ViewModel
{
    public class OptionViewModel
    {
        public Poll polo { get; set; }
        public pollresponse prs { set; get; }
    }
    public class pollresponse

    {
        public int C_id { get; set; }
        public int pollid { get; set; }
        public int optionid { get; set; }
    }

    public class PollOption
    {
        public int polllCid { set; get; }

        public int polllid { set; get; }
        public string optiontext { set; get; }
      
    }
    public class Poll
    {
        public int C_id { set; get; }
        public string pollsubject { set; get; }

        public List<PollOption> PollOptions { set; get; }
        public Poll()
        {
        PollOptions = new List<PollOption>();
         }
}
}
namespace OnlineExamSystem.Controllers
{
     public class question
    {
        public string optiontext { get; set; }
        public string pollsubject { get; set; }
        public int PollCount { get; set; }

        public int C_id { get; set; }

        public int Questionid { get; set; }


        public int pollOptionId { get; set; }
        public int pollId { get; set; }
        public string SelectedAnswer { set; get; }


        public int RpollId { get; set; }
        public int RpollOptionId { get; set; }


    }
}
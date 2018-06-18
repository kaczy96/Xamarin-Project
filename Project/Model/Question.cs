using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project.Model
{
    public class Question
    {
        private int Id { get; set; }
        private string Image { get; set; }
        private string AnswerA { get; set; }
        private string AnswerB { get; set; }
        private string AnswerC { get; set; }
        private string AnswerD { get; set; }
        private string CorrectAnswer { get; set; }

        public Question(int Id, string Image, string AnswerA, string AnswerB, string AnswerC, string AnswerD, string CorrectAnswer)
        {
            this.Id = Id;
            this.Image = Image;
            this.AnswerA = AnswerA;
            this.AnswerB = AnswerB;
            this.AnswerC = AnswerC;
            this.AnswerD = AnswerD;
            this.CorrectAnswer = CorrectAnswer;

        }

    }
}
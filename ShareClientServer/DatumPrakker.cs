using System;
using System.Collections.Generic;
using System.Text;

namespace DP
{
    public class DatumPrakker
    {
        public String name { get; }
        public String id { get; }
        public String createdByUsername { get; }
        public List<DateTime> chooseableDates { get; }
        //private List<String> entitledUsers { get; }
        public List<DPAnswer> answers { get; }

        public DatumPrakker(string DP_Name, string DP_createdByUsername, List<DateTime> DP_chooseableDates)
        {
            name = DP_Name;
            chooseableDates = DP_chooseableDates;
            createdByUsername = DP_createdByUsername;
            id = Guid.NewGuid().ToString().ToUpper();
            answers = new List<DPAnswer>();
            //entitledUsers = DP_entitledUsers;
        }

        public void addDPAnswer(DPAnswer answer)
        {
            answers.Add(answer);
        }

        public class DPAnswer
        {
            public String dpID { get; }
            private String username { get; }
            private List<DateTime> choosenDates { get; }

            public DPAnswer(string DP_ID, string DPA_username, List<DateTime> DPA_choosenDates)
            {
                dpID = DP_ID;
                username = DPA_username;
                choosenDates = DPA_choosenDates;
            }

            public override string ToString()
            {
                return $"\t Username: {username} \n" +
                       $"\t choosenDates: \n\t {string.Join(", \n\t ", choosenDates)}\n";
            }
        }

        public override string ToString()
        {

            return $"Name: {name} \n" +
                   $"ID: {id} \n" +
                   $"CreatedBy: {createdByUsername} \n" +
                   $"chooseableDates: \n\t {string.Join(", \n\t ", chooseableDates)}\n" +
                   $"answers: \n {string.Join("\n", answers)}\n";
        }
    }
}

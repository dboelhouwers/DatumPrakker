using System;
using System.Collections.Generic;
using System.Text;

namespace DP
{
    [Serializable]
    public class DatumPrakker
    {
        public String id { get; }
        public String name { get; }
        public String createdByUsername { get; }
        public List<DateTime> chooseableDates { get; }
        //private List<String> entitledUsers { get; }
        public List<DPAnswer> answers { get; }

        public DatumPrakker(string DP_id, string DP_Name, string DP_createdByUsername, List<DateTime> DP_chooseableDates)
        {
            id = DP_id;
            name = DP_Name;
            createdByUsername = DP_createdByUsername;
            chooseableDates = DP_chooseableDates;
            answers = new List<DPAnswer>();
            //entitledUsers = DP_entitledUsers;
        }

        public DatumPrakker()
        {

        }

        [Serializable]
        public class DPAnswer
        {
            public String dpID { get; }
            public String username { get; }
            public List<DateTime> choosenDates { get; }

            public DPAnswer(string DP_ID, string DPA_username, List<DateTime> DPA_choosenDates)
            {
                dpID = DP_ID;
                username = DPA_username;
                choosenDates = DPA_choosenDates;
            }

            public DPAnswer()
            {

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

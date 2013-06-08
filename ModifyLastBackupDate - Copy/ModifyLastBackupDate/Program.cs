using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ModifyLastBackupDate
{
    class Program
    {
        static SqlDataAdapter adp;

        static void Main(string[] args)
        {
            DataTable dt = GetDataRow2373();

            DataTable updatedDt = DoAction(dt);

            UpdateDataRow2373(updatedDt);
        }

        private static void UpdateDataRow2373(DataTable updatedDt)
        {
            //adp.Update(updatedDt);
        }

        private static DataTable DoAction(DataTable dt)
        {
            string regexLastBackup = @"(?<=Last Backup: ).+?(?=</SPAN)";
            string postQuestion = dt.Rows[0]["PostQuestion"].ToString();
            string matchValue = Regex.Match(postQuestion, regexLastBackup).Value;
            postQuestion = postQuestion.Replace(matchValue, DateTime.Now.ToString());
            dt.Rows[0]["PostQuestion"] = postQuestion;

            return dt;
        }

        private static DataTable GetDataRow2373()
        {
            string sqlString = "select [PostId], [PostQuestion] from [Postings] where [PostId] = @postId;";
            string connectionString = @"server=.\sqlexpress;Trusted_Connection=true;database=MyPostings";
            DataTable dt = new DataTable();
            adp = new SqlDataAdapter(sqlString, connectionString);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adp);
            adp.SelectCommand.Parameters.AddWithValue("postId", 2373);
            adp.Fill(dt);

            return dt;
        }
    }
}


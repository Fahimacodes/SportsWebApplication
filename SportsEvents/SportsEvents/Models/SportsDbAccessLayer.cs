using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SportsEvents.Models;

namespace SportsEvents.Models
{
    public class SportsDbAccessLayer
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportsEvents;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public string AddUser(Membership membership)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_User_Add", con);
                cmd.CommandType = CommandType.StoredProcedure;             
                cmd.Parameters.AddWithValue("@Name", membership.Name);
                cmd.Parameters.AddWithValue("@DOB", membership.DOB);
                cmd.Parameters.AddWithValue("@Gender", membership.Gender);
                cmd.Parameters.AddWithValue("@Email", membership.Email);
                cmd.Parameters.AddWithValue("@TelephoneNo", membership.TelephoneNo);
                cmd.Parameters.AddWithValue("@MobileNo", membership.MobileNo);
                cmd.Parameters.AddWithValue("@HouseNo", membership.HouseNo);
                cmd.Parameters.AddWithValue("@StreetName", membership.StreetName);
                cmd.Parameters.AddWithValue("@PostCode", membership.PostCode);
                cmd.Parameters.AddWithValue("@WorkLocation", membership.WorkLocation);
                cmd.Parameters.AddWithValue("@SportID", membership.SportID);
                cmd.Parameters.AddWithValue("@Biography", membership.Biography);
                cmd.Parameters.AddWithValue("@Skills", membership.Skills);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Activity Membership Successful!");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }

        public int LogInCheck(Admin admin)
        {
            SqlCommand cmd = new SqlCommand("Sp_Admin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", admin.UserName);
            cmd.Parameters.AddWithValue("@Password", admin.Password);
            SqlParameter obAdmin = new SqlParameter();
            obAdmin.ParameterName = "@Isvalid";
            obAdmin.SqlDbType = SqlDbType.Bit;
            obAdmin.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(obAdmin);
            con.Open();
            cmd.ExecuteNonQuery();
            int res = Convert.ToInt32(obAdmin.Value);
            con.Close();
            return res;
        }

        public int UserLogInCheck(Member member)
        {
            SqlCommand cmd = new SqlCommand("Sp_Member", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", member.UserName);
            cmd.Parameters.AddWithValue("@Password", member.Password);
            SqlParameter obMember = new SqlParameter();
            obMember.ParameterName = "@Isvalid";
            obMember.SqlDbType = SqlDbType.Bit;
            obMember.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(obMember);
            con.Open();
            cmd.ExecuteNonQuery();
            int res = Convert.ToInt32(obMember.Value);
            con.Close();
            return res;
        }
    }
}

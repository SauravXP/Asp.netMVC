using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETMVC_BootstrapCustomThemeDemo.Models;
using System.Security;
using System.Web.Security;

namespace ASPNETMVC_BootstrapCustomThemeDemo.Service
{
    public class DBContext
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<userModel> GetUser()
        {
            try
            {
                List<userModel> lst = new List<userModel>();
                SqlCommand cmd = new SqlCommand("select * from Users", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new userModel
                    {
                        /*                    Id = Convert.ToInt32(dr["ID"]),
                                            Emailid = Convert.ToString(dr["EmailId"]),
                                            Password= Convert.ToString(dr["Password"]),
                                            Name = Convert.ToString(dr["Name"]), */
                        UserId = Convert.ToInt32(dr[0]),
                        UserName = Convert.ToString(dr[1]),
                        Email = Convert.ToString(dr[2]),
                        Password = Convert.ToString(dr[3]),
                        Picture = Convert.ToString(dr[4]),
                        //RoleId = Convert.ToInt32(dr[5]),



                    });
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Add(userModel um)
        {
            try
            {
                //string qryStr = "Insert into SiteUser values('" + um.UserName + "','" + um.EmailId + "','" + um.Password + "','"+um.Address+"','"+um.RoleId+"' )";
                string qryStr = "Insert into Users values('" + um.UserName + "','" + um.Email + "','" + um.Password + "','" + um.Picture + "' )";
                SqlCommand scmd = new SqlCommand(qryStr, con);
                if (con.State == ConnectionState.Closed)

                    con.Open();
                int dt = scmd.ExecuteNonQuery();
                con.Close();  //there was nothing before... this can be removed....
                if (dt > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        ///CHECK IF THE USER EXISTS in the table
        public bool IsUserExists(string Email)
        {
            bool UserExists=false;
            string qry = "select * From Users where Email=@Email";
            try
            {
                SqlCommand sqlCmd = new SqlCommand(qry, con);
                sqlCmd.Parameters.AddWithValue("@Email", Email);
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = sqlCmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    UserExists = true;
                }
                else { UserExists = false; }

                return UserExists;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsValidUser(string Email,string password)
        {
            var encryptpassword = Base64Encode(password);
            bool IsValid=false;
            string qry = "select * From Users where Email=@email and Password=@password";
            try
            {
                SqlCommand scd = new SqlCommand(qry, con);
                scd.Parameters.AddWithValue("@Email", Email);
                scd.Parameters.AddWithValue("@Password", password);
                SqlDataAdapter sda = new SqlDataAdapter(scd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = scd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0) {
                    IsValid = true; 
                }
                else
                {
                    IsValid = false; 
                }

                //con.Close();
                return IsValid;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// /////////
        /// </summary>
        // <param name="ENCODDING AND DECODING FUNCTION"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainTxt)
        {
            var PlainTextBytes=System.Text.Encoding.UTF8.GetBytes(plainTxt);
            return System.Convert.ToBase64String(PlainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes=System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }

    }

    }

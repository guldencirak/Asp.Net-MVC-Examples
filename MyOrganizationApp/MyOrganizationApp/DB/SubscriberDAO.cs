using System;
using System.Collections.Generic;
using System.Linq;
using MyOrganizationApp.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyOrganizationApp.DB
{
    public class SubscriberDAO
    {
        private MySqlConnection _con = new 
            MySqlConnection("Server=localhost;Database=Subscriptions;Uid=spstudent;Pwd=spstudent;");

        private bool ValidateSubscription(Subscriber s)
        {
            if (s == null)
                return false;

            List<Subscriber> subscriptions = new List<Subscriber>();

            bool result = false;

            try
            {
                string cmdStr = "select * from Subscriber where BirthDate=@date and EMail=@email";
                MySqlCommand cmd = new MySqlCommand(cmdStr, _con);

                cmd.Parameters.AddWithValue("@date",s.BirthDate);
                cmd.Parameters.AddWithValue("@email",s.EMail);

                _con.Open();

                MySqlDataReader dr = cmd.ExecuteReader();
                result = !dr.Read();
            }
            catch(Exception exc){
                
            }
            finally{
                if (_con.State == ConnectionState.Open)
                    _con.Close();
            }

            return result;
        }

        public bool Insert(Subscriber s)
        {
            try
            {
                if (!ValidateSubscription(s))
                    return false;

                string cmdStr = "insert into Subscriber (EMail, Name, RegisterDate, BirthDate) values (@email, @name, @rdate, @date)";

                MySqlCommand cmd = new MySqlCommand(cmdStr, _con);

                cmd.Parameters.AddWithValue("@date", s.BirthDate);
                cmd.Parameters.AddWithValue("@email", s.EMail);
                cmd.Parameters.AddWithValue("@name",s.Name);
                cmd.Parameters.AddWithValue("@rdate", s.RegisterDate);

                _con.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception exc) { 
            
            }
            finally
            {
                if (_con.State == ConnectionState.Open)
                    _con.Close();
            }

            return true;
        }

        public List<Subscriber> GetSubscribersByEmail(string email)
        {
            List<Subscriber> subscribers = new List<Subscriber>();

            try
            {
                string cmdStr = "select * from Subscriber where EMail='" + email + "'";
                MySqlCommand cmd = new MySqlCommand(cmdStr, _con);

                _con.Open();

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    subscribers.Add(new Subscriber
                    {
                        Name = (string)dr[2],
                        EMail = (string)dr[3],
                        BirthDate = (DateTime)dr[4],
                        RegisterDate = (DateTime)dr[5]
                    });
                }
            }
            catch (Exception exc) { }
            finally
            {
                if (_con.State == ConnectionState.Open)
                    _con.Close();
            }

            return subscribers;
        }
    }
}

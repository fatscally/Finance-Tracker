using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFT.Interfaces;
using PFT.Base;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    public class ItemData :IItemData
    {

        public void Save(Item item)
        {
            int itemId = item.Id;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            //if (itemId <= 0)
            //    itemId = Select(item).Id;

            //if it is still <=0 after the select then INSERT
            if (itemId <= 0)
                insert(item);
            else
                Update(item);
        }

        public Item Select(Item item) 
        {
            if (item.Name == "")
                return item;

            return Select(item.Id);
        }

        public Item Select(int itemId)
        {
            try
            {
                Item item = new Item();

                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Items WHERE Id=" + itemId;

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.DefaultPrice = float.Parse(reader["DefaultPrice"].ToString());
                }

                return item;
            }
            catch
            {
                throw;
            }
        }

        private void insert(Item item)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Items (Name, Description, DefaultPrice) Values('" + item.Name + "', '" + item.Description + "', '" + item.DefaultPrice + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        private void Update(Item item)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Items Set Name='" + item.Name + "', Description='" + item.Description + "', DefaultPrice='" + item.DefaultPrice + "'  WHERE Id = " + item.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Item item)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Items WHERE Id = " + item.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the total of all money spent for the Item for the given date range.
        /// </summary>
        /// <param name="startDate">Start date of period to calculate</param>
        /// <param name="endDate">End date of period to calculate</param>
        /// /// <param name="isIncome">True if calculating incomes</param>
        /// <returns>Sum of money spent </returns>
        public double GetItemTotalSpend(int id, DateTime startDate, DateTime endDate, bool isIncome)
        {
            TransactionCol returnCol = new TransactionCol();
            string strCmdText;


            string strIsIncome = "0";  //stupid SQLCE bug won't handle True/False
            if (isIncome)
                strIsIncome = "1";


            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();

                strCmdText = "SELECT SUM(Price) AS TotalSpend FROM Transactions ";
                strCmdText += "WHERE ItemId = " + id + " AND IsIncome = " + strIsIncome;
                strCmdText += " AND Date>= CONVERT(DATETIME, '" + startDate.ToString("yyyy/MM/dd") + " 00:00:00') ";
                strCmdText += "AND Date<= CONVERT(DATETIME, '" + endDate.ToString("yyyy/MM/dd ") + "23:59:59')";

                cmd.CommandText = strCmdText;
                object rtnVal = cmd.ExecuteScalar();

                double outNumber;
                double.TryParse(rtnVal.ToString(), out outNumber);

                return outNumber;

            }
            catch
            {
                //throw;
                return 0;
            }
        }


    }
}

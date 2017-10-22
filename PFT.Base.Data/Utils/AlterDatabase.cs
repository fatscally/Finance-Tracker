using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;


namespace PFT.Data
{
    public class SqlCeUtilities
    {


#region "Table definitions"

        public void CreateTransactionsTable()
        {
            dropTable("Transactions");
            
            createTable("create table Transactions ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "ItemId int NOT NULL, "
              + "Price money NOT NULL, "
              + "PaymentTypeId int NULL, "
              + "[IsIncome] bit NULL, "
              + "Comment nvarchar (255) NULL, "
              + "SupplierId int NULL, "
              + "InsertType nchar (1) NOT NULL, "
              + "Date datetime NOT NULL, "
              + "PRIMARY KEY(Id) )");
         }

        public void CreateTransactionAutoRepeatTable()
        {
            dropTable("TransactionAutoRepeat");

            createTable("create table TransactionAutoRepeat ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "ItemId int NOT NULL, "
              + "Price money NOT NULL, "
              + "Comment nvarchar (255) NULL, "
              + "SupplierId int NULL, "
              + "RepeatTypeId int NULL, "
              + "RepeatDay int NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateItemsTable()
        {
            dropTable("Items");

            createTable("create table Items ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "[DefaultPrice] money NULL, "
              + "[IsIncome] bit NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateTagsTable()
        {
            dropTable("Tags");

            createTable("create table Tags ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "[ParentTagId] int NOT NULL DEFAULT -1, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateSuppliersTable()
        {
            dropTable("Suppliers");

            createTable("create table Suppliers ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreatePaymentTypesTable()
        {
            dropTable("PaymentTypes");

            createTable("create table PaymentTypes ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateTransaction_TagsTable()
        {
            dropTable("Transaction_Tags");

            createTable("create table Transaction_Tags ("
              + "TransactionId int NOT NULL, "
              + "[TagId] int NOT NULL)");
        }

        public void CreateSettingsTable()
        {
            dropTable("Settings");

            createTable("create table Settings ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Key] nvarchar(50) NOT NULL, "
              + "[Value] nvarchar(100) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateItemDefaultTagsTable()
        {
            dropTable("ItemDefaultTags");

            createTable("create table ItemDefaultTags ("
              + "ItemId int NOT NULL, "
              + "TagId int NOT NULL"
              + " )");
        }

        public void CreateItemDefaultPaymentTypeTable()
        {
            dropTable("ItemDefaultPaymentType");

            createTable("create table ItemDefaultPaymentType ("
              + "ItemId int NOT NULL, "
              + "PaymentTypeId int NOT NULL"
              + " )");
        }

        public void CreateRepeatTypesTable()
        {
            dropTable("RepeatTypes");

            createTable("create table RepeatTypes ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "PRIMARY KEY(Id) )");
        }

        public void CreateDateRanges()
        {
            dropTable("DateRanges");

            createTable("create table DateRanges ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(20) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }


        /// <summary>
        /// How many times an item was bought at a supplier
        /// </summary>
        internal void CreateItemSupplierMatchCountTable()
        {
            dropTable("ItemSupplierMatchCount");

            createTable("create table ItemSupplierMatchCount ("
              + "ItemId int NOT NULL, "
              + "SupplierId int NOT NULL, "
              + "PurchaseCount int NOT NULL)");
        }


        //might want to do this.
        //internal void CreateTagGroupsTable()
        //{
        //    dropTable("TagGroups");

        //    createTable("create table Settings ("
        //      + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
        //      + "[Setting] nvarchar(50) NOT NULL, "
        //      + "[Value] nvarchar(100) NULL, "
        //      + "PRIMARY KEY(Id) )");
        //}

#endregion  

        #region"Metadata Inserts"


        
        /// <summary>
        /// Insert payment repeat types into RepeatTypes table
        /// </summary>
        public void InsertRepeatTypesRows()
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name) Values('Weekly')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name) Values('Monthly')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Insert payment types Metadata into PaymentTypes table
        /// </summary>
        public void InsertPaymentTypesRows()
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Cash','Cash money')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Credit Card','Credit Card')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Direct Debit','Direct Debit')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Cheque','Cheque')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Insert Date Ranges for searching into DateRanges table
        /// </summary>
        public void InsertDateRangesTypesRows()
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Today','Today')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Since Monday','Since Monday')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Last 7 Days','Last 7 Days')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('This Month','This Month')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Last 28 Days','Last 28 Days')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('This Year','This Year')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        #endregion





        private string createTable(string sql)
        {

            PFT.Data.Connection conn = new PFT.Data.Connection();
            SqlCeCommand cmd;
            

            try
            {
                cmd = new SqlCeCommand(sql, conn.LocalConnection());
                cmd.ExecuteNonQuery();
                return "Table Created";
            }
            catch (SqlCeException sqlexception)
            {
                return sqlexception.Message;
            } 
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.LocalConnection().Close();
            }
        }

        private string dropTable(string tableName)
        {
            PFT.Data.Connection conn = new PFT.Data.Connection();
            SqlCeCommand cmd;

            using (SqlCeConnection exConnection = conn.LocalConnection())
            {
                if (exConnection.TableExists(tableName))
                {
                    try
                    {
                        //cmd = new SqlCeCommand(sql, conn.LocalConnection());
                        string sql = "drop table " + tableName;
                        cmd = new SqlCeCommand(sql, conn.LocalConnection());
                        cmd.ExecuteNonQuery();
                        return tableName + " dropped.";
                    }
                    catch (SqlCeException sqlexception)
                    {
                        return sqlexception.Message;
                    }
                }
                return "No connection to database found.";
            }
        }



        public void UpgradeDatabase()
        {
            string filename = @"C:\Documents and Settings\RBRENNAN\My Documents\visual studio 2010\Projects\PFT\PFT\PTF.sdf";
            var engine = new System.Data.SqlServerCe.SqlCeEngine("Data Source=" + filename);
            engine.EnsureVersion40(filename);
        }

    }



    public static class SqlCeExtentions
    {
        public static bool TableExists(this SqlCeConnection connection, string tableName)
        {
            if (tableName == null) throw new ArgumentNullException("tableName");
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Invalid table name");
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("TableExists requires an open and available Connection. The connection's current state is " + connection.State);
            }

            using (SqlCeCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME = @tableName";
                command.Parameters.AddWithValue("tableName", tableName);
                object result = command.ExecuteScalar();
                return result != null;
            }
        }
    }
    

    public static class SqlCeUpgrade
    {
        public static void EnsureVersion40(this System.Data.SqlServerCe.SqlCeEngine engine, string filename)
        {
            SQLCEVersion fileversion = DetermineVersion(filename);
            if (fileversion == SQLCEVersion.SQLCE20)
                throw new ApplicationException("Unable to upgrade from 2.0 to 4.0");

            if (SQLCEVersion.SQLCE40 > fileversion)
            {
                engine.Upgrade();
            }
        }
        private enum SQLCEVersion
        {
            SQLCE20 = 0,
            SQLCE30 = 1,
            SQLCE35 = 2,
            SQLCE40 = 3
        }
        private static SQLCEVersion DetermineVersion(string filename)
        {
            var versionDictionary = new Dictionary<int, SQLCEVersion> 
        { 
            { 0x73616261, SQLCEVersion.SQLCE20 }, 
            { 0x002dd714, SQLCEVersion.SQLCE30},
            { 0x00357b9d, SQLCEVersion.SQLCE35},
            { 0x003d0900, SQLCEVersion.SQLCE40}
        };
            int versionLONGWORD = 0;
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    fs.Seek(16, SeekOrigin.Begin);
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        versionLONGWORD = reader.ReadInt32();
                    }
                }
            }
            catch
            {
                throw;
            }
            if (versionDictionary.ContainsKey(versionLONGWORD))
            {
                return versionDictionary[versionLONGWORD];
            }
            else
            {
                throw new ApplicationException("Unable to determine database file version");
            }
        }


    }



}

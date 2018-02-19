using System;
using System.Data;
using System.Data.SqlClient;

namespace TestarwKaiGoustarw.SQL
{
    public static class CreateSqlServerDatabase
    {
        public static void Create()
        {
            DropAndCreate();

            Console.ReadLine();
        }

        private static void DropAndCreate()
        {
            SqlConnection myConn =
                new SqlConnection(@"Data Source=(localdb)\ProjectsV13; Initial Catalog=master; Integrated Security=True;");

            string ifExistsDropAndCreateNew = @"WHILE EXISTS(select NULL from sys.databases where name='Test')
                    BEGIN
                        DECLARE @SQL varchar(max)
                        SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
                        FROM MASTER..SysProcesses
                        WHERE DBId = DB_ID(N'FileSystemDB') AND SPId <> @@SPId
                        EXEC(@SQL)
                        DROP DATABASE [Test]
                    END
                    
                    CREATE DATABASE Test
                    ";

            SqlCommand myCommand = new SqlCommand(ifExistsDropAndCreateNew, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong " + ex);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }


    }
}

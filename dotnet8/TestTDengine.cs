using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TDengine.Driver.Client;
using TDengine.Driver;

namespace dotnet8;

internal class TestTDengine
{
    public void test() {

        // Display the number of command line arguments.
        //TDengineTest.Main(args);

        var connectionString = "host=127.0.0.1;port=6030;username=root;password=taosdata";
        try
        {
            // Connect to TDengine server using Native
            var builder = new ConnectionStringBuilder(connectionString);
            // Open connection with using block, it will close the connection automatically
            using (var client = DbDriver.Open(builder))
            {
                Console.WriteLine("Connected to " + connectionString + " successfully.");

                //// create database
                //var affected = client.Exec("CREATE DATABASE IF NOT EXISTS yzwl");
                //Console.WriteLine($"Create database yzwl successfully, rowsAffected: {affected}");
                //// create table
                //affected = client.Exec("CREATE STABLE IF NOT EXISTS yzwl.meters (ts TIMESTAMP, v FLOAT) TAGS (meterid INT, pc INT)");
                //Console.WriteLine($"Create stable yzwl.meters successfully, rowsAffected: {affected}");
                //// insert data
                //for (int k = 0; k < 10; k++)
                //{
                //    var insertQuery = "insert into yzwl.meters(tbname,ts,v,meterid,pc) values ";
                //    for (int i = 1001; i < 1005; i++)
                //    {
                //        for (int j = 450001; j < 450010; j++)
                //        {
                //            insertQuery += string.Format("('mp_{0}_{1}','{2}',{3},{0},{1})", i, j, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new Random().NextDouble() * 100);
                //        }
                //    }

                //    var affectedRows = client.Exec(insertQuery);
                //    Console.WriteLine("Successfully inserted " + affectedRows + " rows to power.meters.");
                //    Thread.Sleep(1000);
                //}

                // select data
                //var query = string.Format("SELECT * FROM yzwl.meters where meterid={0} and pc={1}", 1001, 450001);
                //using (var rows = client.Query(query))
                //{
                //    while (rows.Read())
                //    {
                //        // Add your data processing logic here
                //        var ts = (DateTime)rows.GetValue(0);
                //        var v = (float)rows.GetValue(1);
                //        var meterid = (int)rows.GetValue(2);
                //        var pc = (int)rows.GetValue(3);
                //        Console.WriteLine($"ts: {ts:yyyy-MM-dd HH:mm:ss}, value: {v}, meterid: {meterid},pc:{pc}");
                //    }
                //}

            }
        }
        catch (TDengineError e)
        {
            // handle TDengine error
            Console.WriteLine("Failed to connect to " + connectionString + "; ErrCode:" + e.Code + "; ErrMessage: " + e.Error);
            throw;
        }
        catch (Exception e)
        {
            // handle other exceptions
            Console.WriteLine("Failed to connect to " + connectionString + "; Err:" + e.Message);
            throw;
        }

    }
}

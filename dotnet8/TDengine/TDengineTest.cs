/*
 * Copyright (c) 2019 TAOS Data, Inc. <jhtao@taosdata.com>
 *
 * This program is free software: you can use, redistribute, and/or modify
 * it under the terms of the GNU Affero General Public License, version 3
 * or later ("AGPL"), as published by the Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;

namespace dotnet8.TDengine;

class TDengineTest
{
    //connect parameters
    private string host;
    private string configDir;
    private string user;
    private string password;
    private short port = 0;

    //sql parameters
    private string dbName;
    private string stableName;
    private string tablePrefix;

    private bool isInsertData;
    private bool isQueryData;

    private long tableCount;
    private long totalRows;
    private long batchRows;
    private long beginTimestamp = 1551369600000L;

    private nint conn = nint.Zero;
    private long rowsInserted = 0;

    public static void Main1(string[] args)
    {
        TDengineTest tester = new TDengineTest();
        tester.ReadArgument(args);

        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("Starting Testing...");
        Console.WriteLine("---------------------------------------------------------------");

        tester.InitTDengine();
        tester.ConnectTDengine();
        tester.CreateDbAndTable();
        tester.ExecuteInsert();
        tester.ExecuteQuery();
        tester.CloseConnection();

        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("Stop Testing...");
        Console.WriteLine("---------------------------------------------------------------");

    }

    public long GetArgumentAsLong(string[] argv, string argName, int minVal, int maxVal, int defaultValue)
    {
        int argc = argv.Length;
        for (int i = 0; i < argc; ++i)
        {
            if (argName != argv[i])
            {
                continue;
            }
            if (i < argc - 1)
            {
                string tmp = argv[i + 1];
                if (tmp[0] == '-')
                {
                    Console.WriteLine("option {0:G} requires an argument", tmp);
                    ExitProgram();
                }

                long tmpVal = Convert.ToInt64(tmp);
                if (tmpVal < minVal || tmpVal > maxVal)
                {
                    Console.WriteLine("option {0:G} should in range [{1:G}, {2:G}]", argName, minVal, maxVal);
                    ExitProgram();
                }

                return tmpVal;
            }
        }

        return defaultValue;
    }

    public string GetArgumentAsString(string[] argv, string argName, string defaultValue)
    {
        int argc = argv.Length;
        for (int i = 0; i < argc; ++i)
        {
            if (argName != argv[i])
            {
                continue;
            }
            if (i < argc - 1)
            {
                string tmp = argv[i + 1];
                if (tmp[0] == '-')
                {
                    Console.WriteLine("option {0:G} requires an argument", tmp);
                    ExitProgram();
                }
                return tmp;
            }
        }

        return defaultValue;
    }

    public void PrintHelp(string[] argv)
    {
        for (int i = 0; i < argv.Length; ++i)
        {
            if ("--help" == argv[i])
            {
                string indent = "    ";
                Console.WriteLine("taosTest is simple example to operate TDengine use C# Language.\n");
                Console.WriteLine("{0:G}{1:G}", indent, "-h");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "TDEngine server IP address to connect");
                Console.WriteLine("{0:G}{1:G}", indent, "-u");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "The TDEngine user name to use when connecting to the server, default is root");
                Console.WriteLine("{0:G}{1:G}", indent, "-p");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "The TDEngine user name to use when connecting to the server, default is taosdata");
                Console.WriteLine("{0:G}{1:G}", indent, "-d");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Database used to create table or import data, default is db");
                Console.WriteLine("{0:G}{1:G}", indent, "-s");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Super Tables used to create table, default is mt");
                Console.WriteLine("{0:G}{1:G}", indent, "-t");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Table prefixs, default is t");
                Console.WriteLine("{0:G}{1:G}", indent, "-w");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Whether to insert data");
                Console.WriteLine("{0:G}{1:G}", indent, "-r");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Whether to query data");
                Console.WriteLine("{0:G}{1:G}", indent, "-n");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "How many Tables to create, default is 10");
                Console.WriteLine("{0:G}{1:G}", indent, "-b");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "How many rows per insert batch, default is 10");
                Console.WriteLine("{0:G}{1:G}", indent, "-i");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "How many rows to insert, default is 100");
                Console.WriteLine("{0:G}{1:G}", indent, "-c");
                Console.WriteLine("{0:G}{1:G}{2:G}", indent, indent, "Configuration directory");

                ExitProgram();
            }
        }
    }

    public void ReadArgument(string[] argv)
    {
        PrintHelp(argv);
        host = GetArgumentAsString(argv, "-h", "127.0.0.1");
        user = GetArgumentAsString(argv, "-u", "root");
        password = GetArgumentAsString(argv, "-p", "taosdata");
        dbName = GetArgumentAsString(argv, "-d", "db");
        stableName = GetArgumentAsString(argv, "-s", "st");
        tablePrefix = GetArgumentAsString(argv, "-t", "t");
        isInsertData = GetArgumentAsLong(argv, "-w", 0, 1, 1) != 0;
        isQueryData = GetArgumentAsLong(argv, "-r", 0, 1, 1) != 0;
        tableCount = GetArgumentAsLong(argv, "-n", 1, 10000, 10);
        batchRows = GetArgumentAsLong(argv, "-b", 1, 1000, 500);
        totalRows = GetArgumentAsLong(argv, "-i", 1, 10000000, 10000);
        configDir = GetArgumentAsString(argv, "-c", "C:/TDengine/cfg");
    }

    public void InitTDengine()
    {
        TDengine.Options((int)TDengineInitOption.TDDB_OPTION_CONFIGDIR, configDir);
        TDengine.Options((int)TDengineInitOption.TDDB_OPTION_SHELL_ACTIVITY_TIMER, "60");
        TDengine.Init();
        Console.WriteLine("TDengine Initialization finished");
    }

    public void ConnectTDengine()
    {
        string db = "";
        conn = TDengine.Connect(host, user, password, db, port);
        if (conn == nint.Zero)
        {
            Console.WriteLine("Connect to TDengine failed");
            ExitProgram();
        }
        else
        {
            Console.WriteLine("Connect to TDengine success");
        }
    }

    public void CreateDbAndTable()
    {
        if (!isInsertData)
        {
            return;
        }

        StringBuilder sql = new StringBuilder();
        sql.Append("create database if not exists ").Append(dbName);
        nint res = TDengine.Query(conn, sql.ToString());
        if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
        {
            Console.Write(sql.ToString() + " failure, ");
            if (res != nint.Zero)
            {
                Console.Write("reason: " + TDengine.Error(res));
            }
            Console.WriteLine("");
            ExitProgram();
        }
        else
        {
            Console.WriteLine(sql.ToString() + " success");
        }
        TDengine.FreeResult(res);

        sql.Clear();
        sql.Append("use ").Append(dbName);
        res = TDengine.Query(conn, sql.ToString());
        if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
        {
            Console.Write(sql.ToString() + " failure, ");
            if (res != nint.Zero)
            {
                Console.Write("reason: " + TDengine.Error(res));
            }
            Console.WriteLine("");
            ExitProgram();
        }
        else
        {
            Console.WriteLine(sql.ToString() + " success");
        }
        TDengine.FreeResult(res);

        sql.Clear();
        sql.Append("create table if not exists ").Append(stableName).Append("(ts timestamp, v1 bool, v2 tinyint, v3 smallint, v4 int, v5 bigint, v6 float, v7 double, v8 binary(10), v9 nchar(10)) tags(t1 int)");
        res = TDengine.Query(conn, sql.ToString());
        if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
        {
            Console.Write(sql.ToString() + " failure, ");
            if (res != nint.Zero)
            {
                Console.Write("reason: " + TDengine.Error(res));
            }
            Console.WriteLine("");
            ExitProgram();
        }
        else
        {
            Console.WriteLine(sql.ToString() + " success");
        }
        TDengine.FreeResult(res);

        for (int i = 0; i < tableCount; i++)
        {
            sql.Clear();
            sql = sql.Append("create table if not exists ").Append(tablePrefix).Append(i)
              .Append(" using ").Append(stableName).Append(" tags(").Append(i).Append(")");
            res = TDengine.Query(conn, sql.ToString());
            if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
            {
                Console.Write(sql.ToString() + " failure, ");
                if (res != nint.Zero)
                {
                    Console.Write("reason: " + TDengine.Error(res));
                }
                Console.WriteLine("");
                ExitProgram();
            }
            else
            {
                Console.WriteLine(sql.ToString() + " success");
            }
            TDengine.FreeResult(res);
        }

        Console.WriteLine("create db and table success");
    }

    public void ExecuteInsert()
    {
        if (!isInsertData)
        {
            return;
        }

        DateTime start = new DateTime();
        long loopCount = totalRows / batchRows;

        for (int table = 0; table < tableCount; ++table)
        {
            for (long loop = 0; loop < loopCount; loop++)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into ").Append(tablePrefix).Append(table).Append(" values");
                for (int batch = 0; batch < batchRows; ++batch)
                {
                    long rows = loop * batchRows + batch;
                    sql.Append("(")
                       .Append(beginTimestamp + rows)
                       .Append(", 1, 2, 3,")
                       .Append(rows)
                       .Append(", 5, 6, 7, 'abc', 'def')");
                }
                nint res = TDengine.Query(conn, sql.ToString());
                if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
                {
                    Console.Write(sql.ToString() + " failure, ");
                    if (res != nint.Zero)
                    {
                        Console.Write("reason: " + TDengine.Error(res));
                    }
                    Console.WriteLine("");
                }

                int affectRows = TDengine.AffectRows(res);
                rowsInserted += affectRows;

                TDengine.FreeResult(res);
            }
        }

        DateTime end = new DateTime();
        TimeSpan ts = end - start;

        Console.Write("Total {0:G} rows inserted, {1:G} rows failed, time spend {2:G} seconds.\n"
          , rowsInserted, totalRows * tableCount - rowsInserted, ts.TotalSeconds);
    }

    public void ExecuteQuery()
    {
        if (!isQueryData)
        {
            return;
        }

        DateTime start = new DateTime();
        long queryRows = 0;

        for (int i = 0; i < 1/*this.tableCount*/; ++i)
        {
            string sql = "select * from " + dbName + "." + tablePrefix + i;
            Console.WriteLine(sql);

            nint res = TDengine.Query(conn, sql);
            if (res == nint.Zero || TDengine.ErrorNo(res) != 0)
            {
                Console.Write(sql.ToString() + " failure, ");
                if (res != nint.Zero)
                {
                    Console.Write("reason: " + TDengine.Error(res));
                }
                Console.WriteLine("");
                ExitProgram();
            }

            int fieldCount = TDengine.FieldCount(res);
            Console.WriteLine("field count: " + fieldCount);

            List<TDengineMeta> metas = TDengine.FetchFields(res);
            for (int j = 0; j < metas.Count; j++)
            {
                TDengineMeta meta = metas[j];
                Console.WriteLine("index:" + j + ", type:" + meta.type + ", typename:" + meta.TypeName() + ", name:" + meta.name + ", size:" + meta.size);
            }

            nint rowdata;
            StringBuilder builder = new StringBuilder();
            while ((rowdata = TDengine.FetchRows(res)) != nint.Zero)
            {
                queryRows++;
                for (int fields = 0; fields < fieldCount; ++fields)
                {
                    TDengineMeta meta = metas[fields];
                    int offset = nint.Size * fields;
                    nint data = Marshal.ReadIntPtr(rowdata, offset);

                    builder.Append("---");

                    if (data == nint.Zero)
                    {
                        builder.Append("NULL");
                        continue;
                    }

                    switch ((TDengineDataType)meta.type)
                    {
                        case TDengineDataType.TSDB_DATA_TYPE_BOOL:
                            bool v1 = Marshal.ReadByte(data) == 0 ? false : true;
                            builder.Append(v1);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_TINYINT:
                            byte v2 = Marshal.ReadByte(data);
                            builder.Append(v2);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_SMALLINT:
                            short v3 = Marshal.ReadInt16(data);
                            builder.Append(v3);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_INT:
                            int v4 = Marshal.ReadInt32(data);
                            builder.Append(v4);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_BIGINT:
                            long v5 = Marshal.ReadInt64(data);
                            builder.Append(v5);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_FLOAT:
                            float v6 = (float)Marshal.PtrToStructure(data, typeof(float));
                            builder.Append(v6);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_DOUBLE:
                            double v7 = (double)Marshal.PtrToStructure(data, typeof(double));
                            builder.Append(v7);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_BINARY:
                            string v8 = Marshal.PtrToStringAnsi(data);
                            builder.Append(v8);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_TIMESTAMP:
                            long v9 = Marshal.ReadInt64(data);
                            builder.Append(v9);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_NCHAR:
                            string v10 = Marshal.PtrToStringAnsi(data);
                            builder.Append(v10);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_UTINYINT:
                            byte v11 = Marshal.ReadByte(data);
                            builder.Append(v11);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_USMALLINT:
                            ushort v12 = (ushort)Marshal.ReadInt16(data);
                            builder.Append(v12);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_UINT:
                            uint v13 = (uint)Marshal.ReadInt32(data);
                            builder.Append(v13);
                            break;
                        case TDengineDataType.TSDB_DATA_TYPE_UBIGINT:
                            ulong v14 = (ulong)Marshal.ReadInt64(data);
                            builder.Append(v14);
                            break;
                    }
                }
                builder.Append("---");

                if (queryRows <= 10)
                {
                    Console.WriteLine(builder.ToString());
                }
                builder.Clear();
            }

            if (TDengine.ErrorNo(res) != 0)
            {
                Console.Write("Query is not complete, Error {0:G}",
                    TDengine.ErrorNo(res), TDengine.Error(res));
            }
            Console.WriteLine("");

            TDengine.FreeResult(res);
        }

        DateTime end = new DateTime();
        TimeSpan ts = end - start;

        Console.Write("Total {0:G} rows inserted, {1:G} rows query, time spend {2:G} seconds.\n"
         , rowsInserted, queryRows, ts.TotalSeconds);
    }

    public void CloseConnection()
    {
        if (conn != nint.Zero)
        {
            TDengine.Close(conn);
        }
    }

    static void ExitProgram()
    {
        TDengine.Cleanup();
        Environment.Exit(0);
    }
}

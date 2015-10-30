using DQ.LXJHGL.COMMON;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thyt.TiPLM.DAL.Common;

namespace DQ.LXJHGL.SVR
{
    public class DALXJHGLS : DABase
    {
        public DALXJHGLS(DBParameter dbParam) : base(dbParam) { }

        internal List<LXJHGLInstance> ImportTasks(List<COMMON.LXJHGLInstance> tasks)
        {
            List<LXJHGLInstance> duplicated = new List<LXJHGLInstance>();
            this.dbParam.Open();

            OracleCommand queryCmd = new OracleCommand();
            queryCmd.Connection = (OracleConnection)this.dbParam.Connection;
            queryCmd.CommandText = "SELECT count(*) FROM dq_route_taskmng WHERE ID=:ID and VERSION=:VERSION";
            queryCmd.Parameters.Add(":ID", OracleDbType.NVarchar2);
            queryCmd.Parameters.Add(":VERSION", OracleDbType.Int32);

            OracleCommand insertCmd = new OracleCommand();
            insertCmd.Connection = (OracleConnection)this.dbParam.Connection;
            insertCmd.CommandText = @"INSERT INTO dq_route_taskmng(ID,NAME,VERSION,RELEASER,RELEASETIME,TYPE,CREATOR,PLANEDTIME,TASKCREATIME,STATUS,DIFFICULTY)
                                      values(:ID,:NAME,:VERSION,:RELEASER,:RELEASETIME,:TYPE,:CREATOR,:PLANEDTIME,:TASKCREATIME,:STATUS,:DIFFICULTY)    ";
            insertCmd.Parameters.Add(":ID", OracleDbType.NVarchar2);
            insertCmd.Parameters.Add(":NAME", OracleDbType.NVarchar2);
            insertCmd.Parameters.Add(":VERSION", OracleDbType.Int32);
            insertCmd.Parameters.Add(":RELEASER", OracleDbType.NVarchar2);
            insertCmd.Parameters.Add(":RELEASETIME", OracleDbType.Date);
            insertCmd.Parameters.Add(":TYPE", OracleDbType.NVarchar2);
            insertCmd.Parameters.Add(":CREATOR", OracleDbType.NVarchar2);
            insertCmd.Parameters.Add(":PLANEDTIME", OracleDbType.Date);
            insertCmd.Parameters.Add(":TASKCREATIME", OracleDbType.Date);
            insertCmd.Parameters.Add(":STATUS", OracleDbType.Int32);
            insertCmd.Parameters.Add(":DIFFICULTY", OracleDbType.Int32);


            foreach (var task in tasks)
            {
                queryCmd.Parameters[":ID"].Value = task.Id;
                queryCmd.Parameters[":VERSION"].Value = task.Version;
                object cnt = queryCmd.ExecuteScalar();
                if (cnt != null && Convert.ToInt32(cnt) > 0) duplicated.Add(task);
                else
                {
                    insertCmd.Parameters[":ID"].Value = task.Id;
                    insertCmd.Parameters[":NAME"].Value = task.Name;
                    insertCmd.Parameters[":VERSION"].Value = task.Version;
                    insertCmd.Parameters[":RELEASER"].Value = task.Releaser;
                    insertCmd.Parameters[":RELEASETIME"].Value = task.Releasetime;
                    insertCmd.Parameters[":TYPE"].Value = task.Type;
                    insertCmd.Parameters[":CREATOR"].Value = task.Creator;
                    insertCmd.Parameters[":PLANEDTIME"].Value = task.Planedtime;
                    insertCmd.Parameters[":TASKCREATIME"].Value = task.Taskcreatime;
                    insertCmd.Parameters[":STATUS"].Value = LXJHGLStatus.未分配;
                    insertCmd.Parameters[":DIFFICULTY"].Value = task.Difficulty;
                    insertCmd.ExecuteNonQuery();
                }
            }
            this.dbParam.Commit();
            this.dbParam.Close();
            return duplicated;
        }

        internal bool UpdateTasks(List<LXJHGLInstance> tasks)
        {
            this.dbParam.Open();

            OracleCommand queryCmd = new OracleCommand();
            queryCmd.Connection = (OracleConnection)this.dbParam.Connection;
            queryCmd.CommandText = "SELECT count(*) FROM dq_route_taskmng WHERE ID=:ID and VERSION=:VERSION";
            queryCmd.Parameters.Add(":ID", OracleDbType.NVarchar2);
            queryCmd.Parameters.Add(":VERSION", OracleDbType.Int32);

            OracleCommand updateCmd = new OracleCommand();
            updateCmd.Connection = (OracleConnection)this.dbParam.Connection;
            updateCmd.CommandText = @"UPDATE dq_route_taskmng SET 
                                            NAME=:NAME,
                                            RELEASER=:RELEASER,
                                            RELEASETIME = :RELEASETIME,
                                            TYPE=:TYPE,
                                            CREATOR=:CREATOR,
                                            PLANEDTIME=:PLANEDTIME,
                                            TASKCREATIME=:TASKCREATIME,
                                            STATUS=:STATUS,
                                            BZ=:BZ,
                                            PROCESS=:PROCESS,
                                            DIFFICULTY=:DIFFICULTY,
                                            COMPLETETIME=:COMPLETETIME
                                      WHERE ID=:ID AND VERSION=:VERSION
                                      ";

            updateCmd.Parameters.Add(":NAME", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":RELEASER", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":RELEASETIME", OracleDbType.Date);
            updateCmd.Parameters.Add(":TYPE", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":CREATOR", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":PLANEDTIME", OracleDbType.Date);
            updateCmd.Parameters.Add(":TASKCREATIME", OracleDbType.Date);
            updateCmd.Parameters.Add(":STATUS", OracleDbType.Int32);
            updateCmd.Parameters.Add(":BZ", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":PROCESS", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":DIFFICULTY", OracleDbType.Int32);
            updateCmd.Parameters.Add(":COMPLETETIME", OracleDbType.Date);
            updateCmd.Parameters.Add(":ID", OracleDbType.NVarchar2);
            updateCmd.Parameters.Add(":VERSION", OracleDbType.Int32);

            foreach (var task in tasks)
            {
                updateCmd.Parameters[":ID"].Value = task.Id;
                updateCmd.Parameters[":NAME"].Value = task.Name;
                updateCmd.Parameters[":VERSION"].Value = task.Version;
                updateCmd.Parameters[":RELEASER"].Value = task.Releaser;
                updateCmd.Parameters[":RELEASETIME"].Value = task.Releasetime;
                updateCmd.Parameters[":TYPE"].Value = task.Type;
                updateCmd.Parameters[":CREATOR"].Value = task.Creator;
                updateCmd.Parameters[":PLANEDTIME"].Value = task.Planedtime;
                updateCmd.Parameters[":TASKCREATIME"].Value = task.Taskcreatime;
                updateCmd.Parameters[":STATUS"].Value = task.Status;
                updateCmd.Parameters[":BZ"].Value = task.Bz;
                updateCmd.Parameters[":PROCESS"].Value = task.Process;
                updateCmd.Parameters[":DIFFICULTY"].Value = task.Difficulty;
                updateCmd.Parameters[":COMPLETETIME"].Value = task.Completetime;
                updateCmd.ExecuteNonQuery();
            }
            this.dbParam.Commit();
            this.dbParam.Close();
            return true;

        }

        internal List<LXJHGLInstance> GetTasksByStatus(LXJHGLStatus status, string userName)
        {
            List<LXJHGLInstance> result = new List<LXJHGLInstance>();
            this.dbParam.Open();
            OracleCommand queryCmd = new OracleCommand();
            queryCmd.Connection = (OracleConnection)this.dbParam.Connection;
            queryCmd.CommandText = @"SELECT ID,
                                            NAME,
                                            VERSION,
                                            RELEASER,
                                            RELEASETIME,
                                            TYPE,
                                            CREATOR,
                                            PLANEDTIME,
                                            TASKCREATIME,
                                            STATUS,
                                            BZ,
                                            PROCESS,
                                            DIFFICULTY,
                                            COMPLETETIME
                                     FROM dq_route_taskmng 
                                     WHERE STATUS=:status and CREATOR like '%' || :userName || '%' ";
            queryCmd.Parameters.Add(":status", OracleDbType.Int32);
            queryCmd.Parameters[":status"].Value = status;
            queryCmd.Parameters.Add(":userName", OracleDbType.NVarchar2);
            queryCmd.Parameters[":userName"].Value = userName;
            var reader = queryCmd.ExecuteReader();
            while (reader.Read())
            {
                LXJHGLInstance task = new LXJHGLInstance();
                task.Id = reader.GetOracleString(0).IsNull ? string.Empty : reader.GetOracleString(0).Value;
                task.Name = reader.GetOracleString(1).IsNull ? string.Empty : reader.GetOracleString(1).Value;
                task.Version = reader.GetOracleDecimal(2).IsNull ? 0 : reader.GetOracleDecimal(2).ToInt32();
                task.Releaser = reader.GetOracleString(3).IsNull ? string.Empty : reader.GetOracleString(3).Value;
                task.Releasetime = reader.GetOracleDate(4).IsNull ? DateTime.MinValue : reader.GetOracleDate(4).Value;
                task.Type = reader.GetOracleString(5).IsNull ? string.Empty : reader.GetOracleString(5).Value;
                task.Creator = reader.GetOracleString(6).IsNull ? string.Empty : reader.GetOracleString(6).Value;
                task.Planedtime = reader.GetOracleDate(7).IsNull ? DateTime.MinValue : reader.GetOracleDate(7).Value;
                task.Taskcreatime = reader.GetOracleDate(8).IsNull ? DateTime.MinValue : reader.GetOracleDate(8).Value;
                task.Status = reader.GetOracleDecimal(9).IsNull ? (LXJHGLStatus)1 : (LXJHGLStatus)reader.GetOracleDecimal(9).ToInt32();
                task.Bz = reader.GetOracleString(10).IsNull ? string.Empty : reader.GetOracleString(10).Value;
                task.Process = reader.GetOracleString(11).IsNull ? string.Empty : reader.GetOracleString(11).Value;
                task.Difficulty = reader.GetOracleDecimal(12).IsNull ? 0 : reader.GetOracleDecimal(12).ToInt32();
                task.Completetime = reader.GetOracleDate(13).IsNull ? DateTime.MinValue : reader.GetOracleDate(13).Value;
                result.Add(task);
            }
            this.dbParam.Close();
            return result;
        }

        internal List<LXJHGLInstance> GetAllTasks()
        {
            List<LXJHGLInstance> result = new List<LXJHGLInstance>();
            this.dbParam.Open();
            OracleCommand queryCmd = new OracleCommand();
            queryCmd.Connection = (OracleConnection)this.dbParam.Connection;
            queryCmd.CommandText = @"SELECT ID,
                                            NAME,
                                            VERSION,
                                            RELEASER,
                                            RELEASETIME,
                                            TYPE,
                                            CREATOR,
                                            PLANEDTIME,
                                            TASKCREATIME,
                                            STATUS,
                                            BZ,
                                            PROCESS,
                                            DIFFICULTY,
                                            COMPLETETIME
                                     FROM dq_route_taskmng";
            var reader = queryCmd.ExecuteReader();
            while (reader.Read())
            {
                LXJHGLInstance task = new LXJHGLInstance();
                task.Id = reader.GetOracleString(0).IsNull ? string.Empty : reader.GetOracleString(0).Value;
                task.Name = reader.GetOracleString(1).IsNull ? string.Empty : reader.GetOracleString(1).Value;
                task.Version = reader.GetOracleDecimal(2).IsNull ? 0 : reader.GetOracleDecimal(2).ToInt32();
                task.Releaser = reader.GetOracleString(3).IsNull ? string.Empty : reader.GetOracleString(3).Value;
                task.Releasetime = reader.GetOracleDate(4).IsNull ? DateTime.MinValue : reader.GetOracleDate(4).Value;
                task.Type = reader.GetOracleString(5).IsNull ? string.Empty : reader.GetOracleString(5).Value;
                task.Creator = reader.GetOracleString(6).IsNull ? string.Empty : reader.GetOracleString(6).Value;
                task.Planedtime = reader.GetOracleDate(7).IsNull ? DateTime.MinValue : reader.GetOracleDate(7).Value;
                task.Taskcreatime = reader.GetOracleDate(8).IsNull ? DateTime.MinValue : reader.GetOracleDate(8).Value;
                task.Status = reader.GetOracleDecimal(9).IsNull ? (LXJHGLStatus)1 : (LXJHGLStatus)reader.GetOracleDecimal(9).ToInt32();
                task.Bz = reader.GetOracleString(10).IsNull ? string.Empty : reader.GetOracleString(10).Value;
                task.Process = reader.GetOracleString(11).IsNull ? string.Empty : reader.GetOracleString(11).Value;
                task.Difficulty = reader.GetOracleDecimal(12).IsNull ? 0 : reader.GetOracleDecimal(12).ToInt32();
                task.Completetime = reader.GetOracleDate(13).IsNull ? DateTime.MinValue : reader.GetOracleDate(13).Value;
                result.Add(task);
            }
            this.dbParam.Close();
            return result;
        }
    }
}

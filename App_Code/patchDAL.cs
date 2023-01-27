using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for patchDAL
/// </summary>
public class patchDAL
{
    public DataSet FillAllGroups()
    {

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = @"select  distinct ReverseGroupName from vPatchStatusByAgent ";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    ///cmd.Parameters.AddWithValue("@ChartValue", ChartValue);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }

    }

    public DataSet FillPatchReport(string SearchMachName)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = @"select agentGuid,ComputerName, ReverseGroupName, installed, pending, rebootPending from vPatchStatusByAgent where MachineId LIKE ''+@SearchMachName+'%' order by ReverseGroupName asc ";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    cmd.Parameters.AddWithValue("@SearchMachName", SearchMachName);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet FillPatchReportbyGroup(string SearchGroupName)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = @"select agentGuid,ComputerName, ReverseGroupName, installed, pending, rebootPending from vPatchStatusByAgent where ReverseGroupName LIKE ''+@SearchGroupName+'%' order by ReverseGroupName asc ";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    cmd.Parameters.AddWithValue("@SearchGroupName", SearchGroupName);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet PendingPatchReport(string MachineAgentGuid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            // AND   s.schedTogether = 1
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = "pcv_PatchDashboardV2";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@agentGuid", MachineAgentGuid);
                    cmd.Parameters.AddWithValue("@Option", "FilterPatchh");
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet InstalledPatchReport(string MachineAgentGuid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = @" select  VM.machName,Pending.bulletinId as 'KB Article',Pending.updateTitle as 'Article Title',Pending.installDate 'Install Date' from 
                (
                 SELECT agentGuid,bulletinId,updateTitle,installDate
                 FROM patchStatus s
            JOIN patchData pd ON s.patchDataId = pd.patchDataId
                             AND pd.updateClassification != 999
       --- WHERE s.agentGuid = 102613436190283
        AND   s.patchState =1
       
		) as Pending

		left join vMachine VM  ON Pending.agentGuid = VM.agentGuid 
		WHERE Pending.agentGuid=@MachineAgentGuid ";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    cmd.Parameters.AddWithValue("@MachineAgentGuid", MachineAgentGuid);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet PendingReport(string MachineAgentGuid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            using (DataSet ds = new DataSet())
            {
                con.Open();
                string cmdstr = @"  select VM.machName,Pending.bulletinId as 'KB Article',Pending.updateTitle as 'Article Title',Pending.installDate 'Install Date' from 
                (
                 SELECT agentGuid,bulletinId,updateTitle,installDate
                 FROM patchStatus s
            JOIN patchData pd ON s.patchDataId = pd.patchDataId
            AND patchState = 0 
            AND ((s.schedTogether = 1) OR (s.manualUpdateFlag = 1))
		) as Pending

		left join vMachine VM  ON Pending.agentGuid = VM.agentGuid 
		WHERE Pending.agentGuid=@MachineAgentGuid ";
                using (SqlCommand cmd = new SqlCommand(cmdstr, con))
                {
                    cmd.Parameters.AddWithValue("@MachineAgentGuid", MachineAgentGuid);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.SelectCommand.CommandTimeout = 180;

                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }
    public DataTable GetEngWiseDayTickets()
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"DECLARE @columns NVARCHAR(MAX), @sql NVARCHAR(MAX);
                                                        SET @columns = N'';

                                                        SELECT @columns += N', p.' + QUOTENAME(Status)
                                                          FROM ( select   distinct Status from kasadmin.vSDTicket where Status is not null   ) AS x;
                                                        SET @sql = N'

                                                        SELECT CustomFieldValue,' + STUFF(@columns, 1, 2, '') + '
                                                        FROM
                                                        (
                                                           select CustomFieldValue,Count(distinct vSDTicket.TicketNumber) as [Ticket Count],kasadmin.vSDTicket.Status from kasadmin.vSDCustomFields 
                                                            join kasadmin.vSDTicket  on kasadmin.vSDTicket.TicketNumber=kasadmin.vSDCustomFields.TicketNumber 
	                                                        where   kasadmin.vSDCustomFields.CustomFieldCaption=''Engineer Name'' 
                                                            and cast(CreationDate as Date) = cast(getdate() as Date)
	                                                        group by  CustomFieldValue,vSDTicket.Status  
                                                        ) AS j
                                                        PIVOT
                                                        (
                                                         Max([Ticket Count]) FOR Status IN ('
                                                          + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '')
                                                          + ')
                                                        ) AS p
                                                        ';
                                                        PRINT @sql;
                                                        EXEC sp_executesql @sql;
                                                        ", con))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    using (DataTable table = new DataTable())
                    {
                        adp.Fill(table);
                        return table;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    //public DataTable GetEngWiseDayTickets()
    //{
    //    DataTable table = new DataTable();
    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
    //    {
    //        using (SqlDataAdapter ad = new SqlDataAdapter(@"DECLARE @columns NVARCHAR(MAX), @sql NVARCHAR(MAX);
    //                                                    SET @columns = N'';

    //                                                        SELECT @columns += N', p.' + QUOTENAME(Status)
    //                                                          FROM ( select   distinct Status from kasadmin.vSDTicket where Status is not null   ) AS x;
    //                                                        SET @sql = N'

    //                                                        SELECT CustomFieldValue,' + STUFF(@columns, 1, 2, '') + '
    //                                                        FROM
    //                                                        (
    //                                                           select CustomFieldValue,Count(distinct vSDTicket.TicketNumber) as [Ticket Count],kasadmin.vSDTicket.Status from kasadmin.vSDCustomFields 
    //                                                            join kasadmin.vSDTicket  on kasadmin.vSDTicket.TicketNumber=kasadmin.vSDCustomFields.TicketNumber 
    //                                                         where   kasadmin.vSDCustomFields.CustomFieldCaption=''Engineer Name'' 
    //                                                            and cast(CreationDate as Date) = cast(getdate() as Date)
    //                                                         group by  CustomFieldValue,vSDTicket.Status  
    //                                                        ) AS j
    //                                                        PIVOT
    //                                                        (
    //                                                         Max([Ticket Count]) FOR Status IN ('
    //                                                          + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '')
    //                                                          + ')
    //                                                        ) AS p
    //                                                        ';
    //                                                        PRINT @sql;
    //                                                        EXEC sp_executesql @sql;", con))
    //        {
    //            //ad.SelectCommand.CommandType = CommandType.StoredProcedure;
    //            using (DataSet ds = new DataSet())
    //            {
    //                ad.Fill(table);
    //            }

    //        }
    //    }
    //    return table;
    //}
    public DataSet GetAssigneeWiseDayTickets()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                                                    DECLARE @columns NVARCHAR(MAX), @sql NVARCHAR(MAX);
                                                    SET @columns = N'';

                                                    SELECT @columns += N', p.' + QUOTENAME(Status)
                                                      FROM (select   distinct Status from kasadmin.vSDTicket ) AS x;
                                                    SET @sql = N'
                                                    select SDDD.*,A.TicketCounts from (
                                                     select  Count(TicketNumber)TicketCounts,Assignee from kasadmin.vSDTicket group by Assignee
                                                     ) as A
                                                     left join
                                                     (
                                                    SELECT Assignee,' + STUFF(@columns, 1, 2, '') + '
                                                    FROM
                                                    (
                                                      select  Count(TicketNumber)TicketCounts,Assignee,Status from kasadmin.vSDTicket group by Assignee,Status
                                                    ) AS j
                                                    PIVOT
                                                    (
                                                     Max(TicketCounts) FOR Status IN ('
                                                      + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '')
                                                      + ')
                                                    ) AS p
                                                    ) as SDDD

                                                    on A.Assignee=SDDD.Assignee;';
                                                    PRINT @sql;
                                                    EXEC sp_executesql @sql;
                                                    ", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }



}

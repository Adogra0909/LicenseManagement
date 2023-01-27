using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for rptDashboard
/// </summary>
public class rptDashboard
{



    //public DataSet getLogging(DateTime logFrom, DateTime logTo)
    //{
    //    con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //    DataSet ds = new DataSet();
    //    const string sql = "SELECT Columns FROM dbo.Table WHERE LoggedAt BETWEEN @LoggedFrom AND @LoggedTo ... ODRER BY ...";
    //    using (var mycon = new SqlConnection(sql))
    //    using (var da = new SqlDataAdapter(sql, mycon))
    //    {
    //        da.SelectCommand.Parameters.AddWithValue("@LoggedFrom", logFrom);
    //        da.SelectCommand.Parameters.AddWithValue("@LoggedTo", logFrom);
    //        try
    //        {
    //            da.Fill(ds);
    //        }
    //        catch (Exception f)
    //        {
    //            // log here
    //            throw;
    //        }
    //    }
    //    return ds;
    //}

    public DataSet FillGroupDetails(string ChartValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            string cmdstr = @"SELECT   a.machName as 'Machine Name',a.groupName as 'Group Name'
                                    FROM machNameTab a
                                    LEFT OUTER JOIN agentState b
                                    ON a.agentGuid = b.agentGuid
                                    WHERE a.partitionID = 1 AND(b.online IS NOT NULL) and groupName = '" + ChartValue + "' order by a.machName asc";
            using (SqlCommand cmd = new SqlCommand(cmdstr, con))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }

    }
    public DataSet FillGroupLowDiskMachines(string ChartValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            string cmdstr = @"        select TB1.machName 'Machine Name',TB1.groupName 'Group Name',TB1.driveletter 'Drive Letter',TB1.drivetype 'Drive Type',TB1.TotalSpace 'Total Space',TB1.[UsedSpace] 'Used Space',TB1.[FreeSpace] 'Free Space',vw_AgentDiskSpaceGB.OSInformation 'OS Information' from (
		select agentGuid,machName,groupName,driveletter,drivetype,TotalSpace *.00098 as TotalSpace,[UsedSpace],[FreeSpace]
		from vCurrdiskInfo where DriveType = 'Fixed' 
		and TotalSpace >0 
		and VolumeName not like '%recovery%' 
		and VolumeName not like 'System Reserved' 
		and VolumeName not like 'HP_TOOLS'
		) as TB1
		join vw_AgentDiskSpaceGB on vw_AgentDiskSpaceGB.agentGuid=TB1.agentGuid
		and vw_AgentDiskSpaceGB.Drive=TB1.DriveLetter 
		and TB1.FreeSpace <= 1024*5   and TB1.groupName = '" + ChartValue + "' order by TB1.machName asc";
            using (SqlCommand cmd = new SqlCommand(cmdstr, con))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }

    }
    public DataSet FillAntivirusMachines(string ChartValue)
    {

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            string cmdstr = @" select ComputerName 'Computer Name',GroupName 'Group Name',productName 'Antivirus',OperatingSystem 'Operating System', 
                             version 'Version',active 'Active',uptodate 'Uptodate'
                             from vAuditSecurityProductsRpt 
                             where (productType=0  and productType is not null) 
                             and productName!='Windows Defender' and productName='" + ChartValue + "' order by [Group Name] asc";
            using (SqlCommand cmd = new SqlCommand(cmdstr, con))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet FillPatchStatus(string ChartValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_PatchStatusRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    cmd.Parameters.AddWithValue("@Category", ChartValue);
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        //count = ds.Tables["tableName"].Rows.Count;
                        return ds;
                    }
                }
            }
        }


    }

    public DataSet FillHDDReport(string ChartValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_harddisk", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    cmd.Parameters.AddWithValue("@Range", ChartValue);
                    cmd.Parameters.AddWithValue("@Option", "HDDReport");
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        //count = ds.Tables["tableName"].Rows.Count;
                        return ds;
                    }
                }
            }
        }
    }

    public DataSet FillManufacture(string ChartValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_HardInvDash", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    cmd.Parameters.AddWithValue("@Manufacturer", ChartValue);
                    cmd.Parameters.AddWithValue("@Option", "ManufacturerReport");
                    using (DataSet ds = new DataSet())
                    {
                        adp.Fill(ds);
                        //count = ds.Tables["tableName"].Rows.Count;
                        return ds;
                    }
                }
            }
        }
    }

}
using System;
using System.Data;


public class AMCBLL
{
    AMCDAL objAMCDAL = new AMCDAL();
    public Int32 InsertAMCDetails(BEL objBEL)
    {
        AMCDAL objAMCDAL = new AMCDAL();
        try
        {
            return objAMCDAL.AMCInsert(objBEL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objAMCDAL = null;
        }
    }

    public DataSet GetBookRecords()
    {
        AMCDAL objAMCDAL = new AMCDAL();
        try
        {
            return objAMCDAL.GetAMCRecords();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objAMCDAL = null;
        }
    }

    public Int32 UpdateAMCDetails(BEL objBEL)
    {
        AMCDAL objAMCDAL = new AMCDAL();
        try
        {
            return objAMCDAL.UpdateAMC(objBEL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objAMCDAL = null;
        }
    }

    public DataSet SearchAMCDetails(BEL objBEL)
    {

        AMCDAL objAMCDAL = new AMCDAL();
        try
        {
            return objAMCDAL.SearchAMCDetails(objBEL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objAMCDAL = null;
        }
    }


    public Int32 DeleteAMC(BEL objBEL)
    {
        AMCDAL objAMCDAL = new AMCDAL();
        try
        {
            return objAMCDAL.DeleteAMCDetails(objBEL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objAMCDAL = null;
        }
    }
}

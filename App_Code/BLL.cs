using System;
using System.Data;


public class BLL
{
    public DataTable GetEngWiseDayTicketsBLL()
    {
        patchDAL objDal = new patchDAL();
        try
        {
            return objDal.GetEngWiseDayTickets();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }
    }

    public DataSet GetAssigneeWiseDayTickets()
    {
        patchDAL objDal = new patchDAL();
        try
        {
            return objDal.GetAssigneeWiseDayTickets();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }
    }

    public DataTable Location()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillLocation();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
    public DataTable ActiveLocation()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillActiveLocation();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable LocationStoreEmail(string LocationCode)
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillLocationStoreEmail(LocationCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillLocationDepartments(string LocationCode)
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillLocationDepartments(LocationCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable AllProducts()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillProducts();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public int NewAssetRequest(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {

            return ooAssetDal.NewAssetRequest(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public string AddLocation(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {

            return ooAssetDal.AddLocation(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public int AddDepartment(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {

            return ooAssetDal.AddDepartments(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public DataTable FillDepartments()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillDepartments();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillActiveDepartments()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillActiveDepartments();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillAllRequests()

    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllRequests();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }
    }

    public DataTable FillAllRequestsCustom()

    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllRequestsCustom();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }
    }

    public DataTable FillRequestDetails(string RequestNo)
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillRequestDetails(RequestNo);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable DepartmentsApprovers(string DepartmentCode)
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillDepartmentsApprovers(DepartmentCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillAllProducts()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllProducts();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillAllProductsOut()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllProductsOut();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
    public DataTable FillAllProductsIn()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllProductsIn();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
    public DataTable FillProductDetails(string SerialNo)
    {

        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillProductDetails(SerialNo);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }


    }
    public DataTable FillProductDetailsLocationWise(string SerialNo, string LocationCode)
    {

        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillProductDetailsLocationWise(SerialNo, LocationCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }


    }

    public int AssetOut(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AssetOut(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public int AssetIn(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AssetIn(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }
    public int AddCompany(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AddCompany(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public int AddManufacturer(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AddManufacturer(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }
    public int UpdateManufacturer(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.UpdateManufacturer(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public int AddSupplier(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AddSuppliers(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public int UpdateSupplier(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.UpdateSupplier(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }

    public DataTable FillAllSuppliersDetails(string SupplierID)
    {

        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllSuppliersDetails(SupplierID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }


    }

    public DataTable FillAllSuppliers()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllSuppliers();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
    public DataTable FillAllActiveSuppliers()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllActiveSuppliers();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public Int32 UpdateDepartment(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.UpdateDepartment(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }
    }

    public Int32 UpdateLocation(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.UpdateLocation(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }
    }

    public DataTable FillAllStatus()
    {

        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllStatus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }


    }

    public int AddStatus(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.AddStatus(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }
    public int UpdateStatus(BEL oobel)
    {
        AssetDal ooAssetDal = new AssetDal();
        try
        {
            return ooAssetDal.UpdateStatus(oobel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ooAssetDal = null;
        }

    }


    public DataTable FillUserDetails(int UserID)
    {
        commanDal objDal = new commanDal();
        try
        {
            return objDal.FillUserDetails(UserID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillAllManufacturer()

    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllManufacturer();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
    public DataTable FillAllActiveManufacturer()

    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllActiveManufacturer();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable FillAllManufacturerDetails(string Manufacturer)

    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillAllManufacturerDetails(Manufacturer);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataTable GetAllMachines()
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.GetAllMachines();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }
    }

    public DataTable GetSingleMachineDetails(string machineGuid)

    {
        AssetDal objDal = new AssetDal();
        return objDal.GetSingleMachineDetails(machineGuid);

    }
    public DataTable LocationDetails(string LocationCode)
    {
        AssetDal objDal = new AssetDal();
        try
        {
            return objDal.FillLocationDetails(LocationCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }
}
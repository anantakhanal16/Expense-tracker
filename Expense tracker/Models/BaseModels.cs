using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Expense_tracker.Models
{
    public class BaseModel : IDisposable
    {
        public DataSet WorkDataSet = null;
        public DataSet TempDataSet = null;

        public void Dispose()
        {
            ((IDisposable)WorkDataSet).Dispose();
        }

        //public BaseModel()
        //{
        //    

        //}
        //private bool disposed();
        //public void Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //    this.Dispose(true);

        //}
        //~BaseModel()
        //{
        //    this.Dispose(false);
        //}
        //protected virtual void Dispose(bool disposing = true)
        //{
        //    if (this.disposed)
        //    {
        //        return;
        //    }
        //    this.disposed = true;

        //    if (disposing)
        //    {
        //        if (WorkDataSet != null)
        //        {
        //            WorkDataSet.Dispose();
        //        }

        //        if (TempDataSet != null)
        //        {
        //            TempDataSet.Dispose();
        //        }
        //    }
        //    if (StreamReader != null)
        //    {
        //        StreamReader.Dispose();
        //    }
        //}
        //protected void ThrowExceptionIfDisposed()
        //{
        //    throw new ObjectDisposedException(this.GetType().FullName);
        //}
        //private void IntializedDisposeFinalizedPattern()
        //{
        //    this.disposed = false;
        //}
    }
}
using System;

namespace ArkApplication.Framework.Common.Operations
{

    public class OperationException : Exception
    {

         #region Constr

         public OperationException(string message)
         {
             OperationError = new OperationError(message);
         }

         public OperationException(OperationError error)
         {
             if (error == null)
             {
                throw new ArgumentNullException("error");
             }

             OperationError = error;
         }

         public OperationException(OperationError error, params object[] args)
         {
             if (error == null)
             {
                throw new ArgumentNullException("error");
             }
             if(args !=null)
             {
                 error.MessageFormatParameters = args;
             }
             OperationError = error;
         }
         
         public OperationException(OperationError error, Exception inner)
             : base("", inner)
         {
              if (error == null)
              {
                throw new ArgumentNullException("error");
              }
              OperationError = error;
         }

         #endregion

         #region Filed
         
         public OperationError OperationError { get; private set; }

         public override string Message 
         {
             get
             {
                 if(OperationError == null)
                 {
                      return "Operation error has occurred.";
                 }
                 return string.Format("{0}: {1}", OperationError.Code, OperationError.Message);
             }
         }

         #endregion

     
    }

}
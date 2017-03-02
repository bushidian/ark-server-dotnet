namespace ArkApplication.Framework.Common.Operations
{
    public class OperationResult<T>
    {
 
        #region Constr

        public OperationResult()
            : this(true){
        }
        
        
        public OperationResult(bool s)
        {
            status = s;           
        }

        public OperationResult(bool s, T d)
        {
            status = s;
            data = d;
        }

        public OperationResult(bool s, T d, string err)
        {
            status = s;
            data = d;
            error = err;
        }

        #endregion

        #region Filed

        public bool status { get; set; }
        
        public string error { get; set; }
        
        public T data { get; set; }

        #endregion
    }

    public class OperationResult
    {

        #region

        public OperationResult()
            : this(true)
        {
        }

        public OperationResult(bool s)
        {
            status = s;
        }

        public OperationResult(string err)
            : this(false, err)
        {
        }

        public OperationResult(bool s, string err)
        {
            status = s;
            error = err;
        }

        #endregion

        #region Filed

        public bool status { get; set; }
        
        public string error { get; set; }
        
        #endregion
    }

}
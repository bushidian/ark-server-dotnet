using System.Collections.Generic;
using System.Linq;

namespace ArkApplication.Framework.Common.Operations
{

     public sealed class OperationError
     {

          #region Filed

          public string Code { get; set; }

          public string Message { get; set; }

          public IEnumerable<object> MessageFormatParameters
          {
              set 
              {
                  if (value == null || !value.Any<object>())
                  {
                      return ;
                  }
                  Message = string.Format(this.Message, value.ToArray<object>());
              }
          }

          #endregion
     
          #region Constr

          public OperationError(string message)
          {
              Message = message;
          }

          public OperationError(string code, string message)
          {
              Code = code;
              Message = message;
          }

          #endregion

          #region Impl

          public object Clone()
          {
              return new OperationError(Code, Message);
          }

          #endregion

     }
      
}
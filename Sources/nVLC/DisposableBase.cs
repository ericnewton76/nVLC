//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;

namespace Implementation
{
   /// <summary>
   /// Base class for managing native resources.
   /// </summary>
   public abstract class DisposableBase : IDisposable
   {
      private bool m_isDisposed;

      /// <summary>
      /// 
      /// </summary>
      public void Dispose()
      {
         if (!m_isDisposed)
         {
            Dispose(true);
            GC.SuppressFinalize(this);

            m_isDisposed = true;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="disposing"></param>
      protected abstract void Dispose(bool disposing);
      //      if (disposing)
      //      {
      //         // get rid of managed resources 
      //      }
      //      // get rid of unmanaged resources 

      /// <summary>
      /// 
      /// </summary>
      ~DisposableBase()
      {
         if (!m_isDisposed)
         {
            Dispose(false);
            m_isDisposed = true;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      protected void VerifyObjectNotDisposed()
      {
         if (m_isDisposed)
         {
            throw new ObjectDisposedException(this.GetType().Name);
         }
      }
   } 
}

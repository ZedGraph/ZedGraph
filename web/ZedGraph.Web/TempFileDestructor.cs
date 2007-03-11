using System;
using System.IO;
using System.Web.Caching;
using System.Text;

namespace ZedGraph.Web
{
    /// <summary>
    /// The ZedGraphWeb class which remove temporary file 
    /// associated with this object. File is deleted when 
    /// this object is removed from cache.
    /// </summary>
    public class TempFileDestructor
    {
        string fileName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"></param>
        public TempFileDestructor(string fileName) {
            this.fileName = fileName;
        }

        /// <summary>
        /// Called when object removed from cache
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <param name="r"></param>
        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r) {
            try {
                File.Delete(fileName);
            }
            catch { /* nothing todo... */ }
        }
    }
}

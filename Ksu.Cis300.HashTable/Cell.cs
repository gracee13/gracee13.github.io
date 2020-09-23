/* Cell.cs
 * Author: Grace Earnhardt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.HashTable
{
    public struct Cell<TKey, TValue>
    {
        /// <summary>
        /// gets and sets key value pair
        /// </summary>
        public KeyValuePair<TKey, TValue> Data { get; set; }

        /// <summary>
        /// gets and sets the hash code
        /// </summary>
        public int HashCode { get; set; }

        /// <summary>
        /// gets and sets if the object is removed
        /// </summary>
        public bool IsRemoved { get; set; }

        /// <summary>
        /// gets and set the next property
        /// </summary>
        public int Next { get; set; }

        /// <summary>
        /// initializes the cell properties
        /// </summary>
        /// <param name="data">given data</param>
        /// <param name="hash">given hash code</param>
        /// <param name="removed">gived removed boolean</param>
        /// <param name="next">given next</param>
        public Cell(KeyValuePair<TKey, TValue> data, int hash, bool removed, int next)
        {
            Data = data;
            HashCode = hash;
            IsRemoved = removed;
            Next = next;
        }
    }
}

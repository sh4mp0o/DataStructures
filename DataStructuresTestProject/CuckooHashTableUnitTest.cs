using HashTableLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTestProject
{
    public class CuckooHashTableUnitTest
    {
        [Fact]
        public void ItemExistsAfterAdding()
        {
            int size = 100;
            var cTable = new CuckooHashTable<int, int>();
            for (int i = 0; i < size; i++)
            {
                cTable.Add(i, i);
            }
            for (int i = 0; i < size; i++)
            {
                Assert.True(cTable.ContainsKey(i));
            }
            Assert.Equal(size, cTable.Count);
        }
    }
}

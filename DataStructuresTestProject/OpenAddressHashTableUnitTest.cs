﻿using HashTableLib;

namespace DataStructuresTestProject
{
    public class OpenAddressHashTableUnitTest
    {
        [Fact]
        public void ItemExistsAfterAdding()
        {
            int size = 100;
            var oaTable = new OpenAddressHashTable<int, int>();
            for (int i = 0; i < size; i++)
            {
                oaTable.Add(i, i);
            }
            for (int i = 0; i < size; i++)
            {
                Assert.True(oaTable.ContainsKey(i));
            }
            Assert.Equal(size, oaTable.Count);
        }
        [Fact]
        public void CountTest()
        {
            var oaTable = new OpenAddressHashTable<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                oaTable.Add(oaTable.Count, i);
            }
            Assert.Equal(n, oaTable.Count);

            oaTable.Remove(0);
            Assert.Equal(n - 1, oaTable.Count);
        }
        [Fact]
        public void ClearTest()
        {
            var oaTable = new OpenAddressHashTable<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                oaTable.Add(oaTable.Count, i);
            }

            oaTable.Clear();
            Assert.Empty(oaTable);
        }
        [Fact]
        public void ContainsTest()
        {
            var oaTable = new OpenAddressHashTable<int, int>
            {
                { 0, 1 }
            };
            Assert.Contains(new KeyValuePair<int, int>(0, 1), oaTable);
        }
        [Fact]
        public void RemoveTest()
        {
            var oaTable = new OpenAddressHashTable<int, int>
            {
                { 0, 1 }
            };
            oaTable.Remove(0);
            Assert.Empty(oaTable);
        }
        [Fact]
        public void TryGetValueTest()
        {
            var oaTable = new OpenAddressHashTable<int, int>
            {
                { 0, 1 }
            };
            Assert.True(oaTable.TryGetValue(0, out _));
            oaTable.Remove(0);
            Assert.False(oaTable.TryGetValue(0, out _));
        }
        [Fact]
        public void CollisionTest()
        {
            int n = 5;
            var oaTable = new OpenAddressHashTable<int, int>();
            for (int i = 0; i < n; i++)
            {
                oaTable.Add(new KeyValuePair<int, int>(i * 11, 0));
            }

            oaTable.Add(new KeyValuePair<int, int>(23, 0));

            oaTable.Remove(11);

            oaTable.Remove(22);

            oaTable.Remove(33);

            Assert.True(oaTable.ContainsKey(44));

            Assert.True(oaTable.ContainsKey(23));
        }
    }
}

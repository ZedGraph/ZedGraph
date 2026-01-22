// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RollingPointPairListTests.cs" company="ZedGraph Project">
//   This library is free software; you can redistribute it and/or
//   modify it under the terms of the GNU Lesser General Public
//   License as published by the Free Software Foundation; either
//   version 2.1 of the License, or (at your option) any later version.
//   
//   This library is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//   Lesser General Public License for more details.
//   
//   You should have received a copy of the GNU Lesser General Public
//   License along with this library; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// </copyright>
// <summary>
//   Tests for the RollingPointPairList Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZedGraph
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class RollingPointPairListTests
    {
        [Test]
        public void WrappedListYieldCorrectData()
        {
            // Arrange
            var list = new RollingPointPairList(10);

            // Act
            for (var i = 0; i < 15; i++)
                list.Add(i, i, i);

            // Assert
            Assert.AreEqual(10, list.Count);
            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(i + 5, list[i].X);
                Assert.AreEqual(i + 5, list[i].Y);
                Assert.AreEqual(i + 5, list[i].Z);
            }
        }

        [Test]
        public void RemoveRange_OnAWrappedList_YieldCorrectData()
        {
            // Arrange
            var list = new RollingPointPairList(10);
            for (var i = 0; i < 15; i++)
                list.Add(i, i, i);

            // Act
            list.RemoveRange(list.Count - 2, 2);

            // Assert
            Assert.AreEqual(8, list.Count);
            for (var i = 0; i < 8; i++)
            {
                Assert.AreEqual(i + 5, list[i].X);
                Assert.AreEqual(i + 5, list[i].Y);
                Assert.AreEqual(i + 5, list[i].Z);
            }
        }

        [TestCase(-1, 0)]
        [TestCase(10, 0)]
        [TestCase(0, -1)]
        [TestCase(8, 8)]
        public void RemoveRange_WithInvalidParameters_ThrowsAndListRemainsUntouched(int index, int count)
        {
            // Arrange
            var list = new RollingPointPairList(10);
            for (var i = 0; i < 10; i++)
                list.Add(i, i, i);

            // Act
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveRange(index, count));

            // Assert
            Assert.AreEqual(10, list.Count);
            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, list[i].X);
                Assert.AreEqual(i, list[i].Y);
                Assert.AreEqual(i, list[i].Z);
            }
        }

        [TestCase(10, 0, 0, ExpectedResult = 10)]
        [TestCase(10, 0, 1, ExpectedResult = 9)]
        [TestCase(10, 0, 5, ExpectedResult = 5)]
        [TestCase(10, 0, 10, ExpectedResult = 0)]
        [TestCase(10, 5, 0, ExpectedResult = 10)]
        [TestCase(10, 5, 1, ExpectedResult = 9)]
        [TestCase(10, 5, 3, ExpectedResult = 7)]
        [TestCase(10, 5, 5, ExpectedResult = 5)]
        [TestCase(10, 9, 0, ExpectedResult = 10)]
        [TestCase(10, 9, 1, ExpectedResult = 9)]
        public int RemoveRange_WithValidParameters_Act(int initialCount, int index, int count)
        {
            // Arrange
            var list = new RollingPointPairList(initialCount);
            for (var i = 0; i < initialCount; i++)
                list.Add(i, i, i);

            // Act
            list.RemoveRange(index, count);

            // Assert
            return list.Count;
        }

        [Test]
        public void RemoveRangeThenAddPoints_MaintainsCorrectCountAndData()
        {
            // Arrange
            var list = new RollingPointPairList(10);
            list.Add(1, 1, 1);
            list.Add(2, 2, 2);
            list.Add(3, 3, 3);

            // Act
            list.RemoveRange(list.Count - 2, 2);
            list.Add(4, 4, 4);
            list.Add(5, 5, 5);

            // Assert
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(1.0, list[0].X);
            Assert.AreEqual(1.0, list[0].Y);
            Assert.AreEqual(1.0, list[0].Z);

            Assert.AreEqual(4.0, list[1].X);
            Assert.AreEqual(4.0, list[1].Y);
            Assert.AreEqual(4.0, list[1].Z);

            Assert.AreEqual(5.0, list[2].X);
            Assert.AreEqual(5.0, list[2].Y);
            Assert.AreEqual(5.0, list[2].Z);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Tea2D.Core.Memory.Pools;
using Tea2D.Tests.Helpers;
using Xunit;

namespace Tea2D.Tests.Core.Memory.Pools
{
    [TestCaseOrderer("Tea2D.Tests.Helpers.PriorityOrderer", "Tea2D.Tests")]
    public class ObjectPoolTests
    {
        [Fact]
        [TestPriority(1)]
        public void When_Before_Test_Should_Be_Empty_Pool()
        {
            var type = ObjectPool<object>.Instance.GetType();
            var field = type.GetField("_rentedArray", BindingFlags.NonPublic | BindingFlags.Instance);

            field.Should().NotBeNull();
            var value = field!.GetValue(ObjectPool<object>.Instance) as bool[];

            var expected = value.AsSpan().Contains(true);
            expected.Should().BeFalse();
        }
        
        [Theory]
        [MemberData(nameof(RentData))]
        public void Rent_With_Using_When_All_Ok_Should_Rreturn_Array(int length)
        {
            using var rentedSpan = ObjectPool<object>.Instance.Rent(length);

            rentedSpan.Span.Length.Should().Be(length);
        }
        
        [Theory]
        [MemberData(nameof(RentData))]
        public void Rent_With_Return_When_All_Ok_Should_Rreturn_Array(int length)
        {
            var rentedSpan = ObjectPool<object>.Instance.Rent(length);

            rentedSpan.Span.Length.Should().Be(length);
            
            ObjectPool<object>.Instance.Return(in rentedSpan);
        }
        
        [Fact]
        [TestPriority(-1)]
        public void When_After_Test_Should_Be_Empty_Pool()
        {
            var type = ObjectPool<object>.Instance.GetType();
            var field = type.GetField("_rentedArray", BindingFlags.NonPublic | BindingFlags.Instance);

            field.Should().NotBeNull();
            var value = field!.GetValue(ObjectPool<object>.Instance) as bool[];

            var expected = value.AsSpan().Contains(true);
            expected.Should().BeFalse();
        }

        public static IEnumerable<object[]> RentData()
        {
            var rnd = new Random();

            for (var i = 0; i < 100; i++)
            {
                yield return new object[] { rnd.Next(0, 1000) };
            }
        }
    }
}
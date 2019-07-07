﻿using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace MiLibNetCore.Core.Test
{
    public class AbpCollectionExtensionsTests
    {
        [Fact]
        public void AddIfNotContains_With_Predicate()
        {
            var collection = new List<int> { 4, 5, 6 };

            collection.AddIfNotContains(x => x == 5, () => 5);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(x => x == 42, () => 42);
            collection.Count.ShouldBe(4);

            collection.AddIfNotContains(x => x < 8, () => 8);
            collection.Count.ShouldBe(4);

            collection.AddIfNotContains(x => x > 999, () => 8);
            collection.Count.ShouldBe(5);
        }
    }
}
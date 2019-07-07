using System;
using Xunit;
using Shouldly;

namespace MiLibNetCore.Core.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void EnsureEndsWith_Test()
        {
            //Expected use-cases
            "Test".EnsureEndsWith('!').ShouldBe("Test!");
            "Test!".EnsureEndsWith('!').ShouldBe("Test!");
            @"C:\test\folderName".EnsureEndsWith('\\').ShouldBe(@"C:\test\folderName\");
            @"C:\test\folderName\".EnsureEndsWith('\\').ShouldBe(@"C:\test\folderName\");

            //Case differences
            "TurkeY".EnsureEndsWith('y').ShouldBe("TurkeYy");
        }
    }
}

using Xunit;
using static Lab2.Program;

namespace Lab2Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(1, 1)] // 1-0
    [InlineData(2, 2)] // 2-1-0 2-0
    [InlineData(3, 4)] // 3-2-1-0 3-1-0 3-2-0 3-0
    [InlineData(4, 7)] // 4-3-2-1-0 4-3-2-1-0 4-3-1-0 4-3-2-0 4-3-0 4-2-1-0 4-2-0 4-1-0
    [InlineData(5, 13)] // 5-4-3-2-1-0 5-4-3-2-1-0 5-4-3-1-0 5-4-3-2-0 5-4-3-0 5-4-2-1-0 5-4-2-0 5-4-1-0 5-3-2-1-0 5-3-1-0 5-3-2-0 5-3-0 5-2-1-0 5-2-0
    public void BallJumpingDownMethodTest(int step, int expectedResult)
    {
        Assert.Equal(BallJumpingDown(step), expectedResult);
    }
}
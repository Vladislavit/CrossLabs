using System;
using Lab3;
using Xunit;

namespace Lab3Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(new[]
    {
        "3 2",
        "1 2 10",
        "1 3 5",
        "5",
        "10 10 10 10 5"
    }, "3")]
    [InlineData(new[]
    {
        "3 2",
        "1 2 10",
        "1 3 5",
        "5",
        "5 10 10 10 10"
    }, "INCORRECT")]
    [InlineData(new[]
    {
        "3 2",
        "1 2 10",
        "1 3 5",
        "4",
        "10 10 10 10"
    }, "1")]
    [InlineData(new[]
    {
        "3 2",
        "1 2 10",
        "1 3 5",
        "0"
    }, "1")]
    [InlineData(new[]
    {
        "3 2",
        "1 2 10",
        "1 3 5",
        "4",
        "10 10 10 5"
    }, "INCORRECT")]
    public void GeneralExamplesTest(string[] input, string expectedRoom)
    {
        Labyrinth labyrinth = new Labyrinth(input);
        Assert.Equal(labyrinth.GetResult(), expectedRoom);
    }

    [Fact]
    public void OneRoomTwoCorridorsOneColorException()
    {
        Labyrinth labyrinth = new Labyrinth(new[]
        {
            "4 2",
            "1 2 10",
            "1 3 5"
        });
        Assert.Throws<Exception>(() => labyrinth.AddCorridor(1, 4, 5));
    }

    [Theory]
    [InlineData("3 2",
                "1 2 10",
                "1 4 5",
                "5",
                "10 10 10 10 5")] // One of the rooms > N
    [InlineData("i 2",
                "1 2 10",
                "1 4 5",
                "5",
                "10 10 10 10 5")] // N as a symbol 'i'
    [InlineData("3 2",
                "1 2 101",
                "1 3 5",
                "5",
                "10 10 10 10 5")] // Color > 100
    [InlineData("3 2",
                "1 2 10",
                "1 4 red",
                "5",
                "10 10 10 10 5")] // Color as a word
    public void InvalidLabyrinthData(params string[] input)
    {
        Assert.Throws<Exception>(() => new Labyrinth(input));
    }
}
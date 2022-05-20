using FluentAssertions;
using Tea2D.Core.Memory;
using Xunit;

namespace Tea2D.Tests.Core.Memory;

public class ValueStringTests
{
    [Fact]
    public void Append_When_All_Ok_Should_Append_New_String()
    {
        var underTest = new ValueString(stackalloc char[255]);

        underTest.Append("Hello");
        underTest.Append(' ');
        underTest.Append("World");
        underTest.Append('!');

        underTest.ToString().Should().Be("Hello World!");
    }

    [Fact]
    public void TrimStart_When_All_Ok_Should_Return_Trimmed_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart('H', 'e', 'l', 'o', ' ');

        underTest.ToString().Should().Be("World!");
    }

    [Fact]
    public void TrimStart_When_Symbols_Not_Present_In_String_Should_Return_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart('e', 'l', 'o', 'W', 'r', 'd', '!');

        underTest.ToString().Should().Be("Hello World!");
    }

    [Fact]
    public void TrimStart_When_Pass_Symbols_Presents_In_String_Should_Return_Empty_ValueString()
    {
        var underTest = new ValueString("asasasassssssaaaaassssaasssssaaaaaasss", stackalloc char[255]);
        
        underTest.TrimStart('a', 's');

        underTest.Length.Should().Be(0);
        underTest.IsEmpty.Should().BeTrue();
        underTest.ToString().Should().BeEmpty();
    }

    [Fact]
    public void TrimStart_When_Pass_Char_Array_Should_Return_Trimmed_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart(' ', 'H', 'e', 'l', 'o');

        underTest.ToString().Should().Be("World!");
    }

    [Fact]
    public void TrimStart_When_Pass_Char_Array_And_Symbols_Not_Present_In_String_Should_Return_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart('e', 'l', 'o');

        underTest.ToString().Should().Be("Hello World!");
    }

    [Fact]
    public void TrimStart_When_Pass_Char_Should_Return_Trimmed_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart('H');
        underTest.TrimStart('e');
        underTest.TrimStart('l');
        underTest.TrimStart('o');
        underTest.TrimStart(' ');

        underTest.ToString().Should().Be("World!");
    }

    [Fact]
    public void TrimStart_When_Pass_Char_And_Symbol_Not_Present_In_String_Should_Return_String()
    {
        var underTest = new ValueString(stackalloc char[255]);
        underTest.Append("Hello World!");

        underTest.TrimStart('e');
        underTest.TrimStart('l');
        underTest.TrimStart('o');
        underTest.TrimStart(' ');

        underTest.ToString().Should().Be("Hello World!");
    }

    [Fact]
    public void Replace_When_All_Ok_Should_Replace_String()
    {
        var underTest = new ValueString("Hello World", stackalloc char[255]);

        underTest.Replace("l", "He");

        underTest.ToString().Should().Be("HeHeHeo WorHed");
    }

    [Fact]
    public void Clear_When_All_Ok_Should_Clear_Span()
    {
        var underTest = new ValueString("Hello World", stackalloc char[255]);

        underTest.Clear();

        underTest.Length.Should().Be(0);
        underTest.IsEmpty.Should().BeTrue();
        underTest.ToString().Should().BeEmpty();
    }
    
    [Fact]
    public void Clear_When_Init_With_Empty_String_Should_Not_Clear()
    {
        var underTest = new ValueString(stackalloc char[255]);

        underTest.Length.Should().Be(0);
        underTest.IsEmpty.Should().BeTrue();
        underTest.ToString().Should().BeEmpty();
        
        underTest.Clear();

        underTest.Length.Should().Be(0);
        underTest.IsEmpty.Should().BeTrue();
        underTest.ToString().Should().BeEmpty();
    }

    [Theory]
    [InlineData(" Hello World ", "Hello World")]
    [InlineData(" Hello World", "Hello World")]
    [InlineData("Hello World ", "Hello World")]
    [InlineData("    Hello World ", "Hello World")]
    [InlineData("    Hello World    ", "Hello World")]
    public void Trim_When_All_Ok_Should_Trim(string initString, string expectedResult)
    {
        var underTest = new ValueString(initString, stackalloc char[255]);
        
        underTest.Trim();

        underTest.ToString().Should().Be(expectedResult);
    }
    
    [Theory]
    [InlineData("     ")]
    [InlineData("")]
    public void Trim_When_Contains_WhiteSpace_Or_Empty_String_Should_Clear(string initString)
    {
        var underTest = new ValueString(initString, stackalloc char[255]);
        
        underTest.Trim();

        underTest.Length.Should().Be(0);
        underTest.IsEmpty.Should().BeTrue();
        underTest.ToString().Should().BeEmpty();
    }
}
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Galgje.Tests;

[TestClass]
public class InputTests
{
    [DataTestMethod]
    [DataRow("aa")]
    [DataRow("")]
    public void InputChecker_ReturnsException_InputLenghtLargerThenOne_OrZero(string input)
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            Input.StringToChar(input);
        });
    }

    [DataTestMethod]
    [DataRow('.')]
    [DataRow('-')]
    public void InputChecker_ReturnsException_InputNotLetterOrDigit(char input)
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            Input.IsNotSpecial(input);
        });
    }

    [DataTestMethod]
    [DataRow('1')]
    [DataRow('4')]
    public void InputChecker_ReturnsException_InputNumeric(char input)
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {

            Input.IsNotNumeric(input);
        });
    }

    [DataTestMethod]
    [DataRow('A', 'a')]
    [DataRow('a', 'a')]
    public void InputChecker_ReturnsLowerCase_InputUpper(char input, char expected)
    {

        char result = Input.ToLower(input);

        Assert.AreEqual(expected, result);
    }
}


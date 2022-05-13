using Domain;
using Galgje;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Galgje.Tests;

[TestClass]
public class ControllerTests
{
    Controller controller;

    [TestInitialize]
    public void TestInitialize()
    {
        controller = new Controller();

        controller.WordToGuess = "unittest";
        controller.WrongTries = 2;
        controller.TotalTries = 2;
    }

    [TestMethod]
    public void GameReset_ResetsTheGame() {
        controller.GameReset();
        Assert.AreEqual(0, controller.WrongTries);
        Assert.AreEqual(0, controller.TotalTries);
        Assert.AreNotEqual("unittest", controller.WordToGuess);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
    "Deze heb je al eens gegokt")]
    public void InputAdmin_ThrowsException_IfLetterAlreadyGuessed()
    {
        controller.Tried.Add('u');

        controller.InputAdmin('u');
    }

    [TestMethod]
    public void InputAdmin_AddsChartoTried()
    {
        controller.InputAdmin('u');
        Assert.IsTrue(controller.Tried.Contains('u'));
        Assert.IsFalse(controller.Tried.Contains('x'));
    }

    [TestMethod]
    public void InputAdmin_UpsTotalTriesAfterValidGuess()
    {
        controller.InputAdmin('u');
        Assert.AreEqual(3, controller.TotalTries);
    }

    [TestMethod]
    public void DisplayGoodGuesses_DisplaysAllGoodGuesses()
    {
        controller.InputAdmin('u');
        Assert.AreEqual("u.......", controller.DisplayGoodGuesses());
    }

    [TestMethod]
    public void GoodGuessHandler_ReturnsFalse_IfWordIsNotYetGuessed()
    {
        Assert.IsFalse(controller.GoodGuessHandler());
    }

    [TestMethod]
    public void BadGuessHandler_ReturnsFalse_IfWrongTriesIsNotReached()
    {
        Assert.IsFalse(controller.BadGuessHandler('x'));
    }

    [TestMethod]

    public void BadGuessHandler_AddWrongGuessToWrongLetters()
    {
        controller.BadGuessHandler('x');
        Assert.IsTrue(controller.WrongLetters.Contains('x'));
        Assert.IsFalse(controller.WrongLetters.Contains('c'));
    }
}




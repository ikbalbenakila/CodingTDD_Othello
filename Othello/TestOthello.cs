using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Otello
{
    [TestFixture]
    public class TestOthello
    {
        [Test]
        [TestCase(CaseState.Black, CaseState.White)]
        [TestCase(CaseState.White, CaseState.Black)]
        public void Othello_Play_CurrentPlayerIsBlackOrWhite_NextPlayerColorIsTheOther(CaseState color1, CaseState color2)
        {
            Othello othello = new Othello(color1);

            CaseState caseState = othello.CurrentPlayerColor;

            othello.Play();

            Assert.AreEqual(caseState, color1);
            Assert.AreEqual(othello.CurrentPlayerColor, color2);
        }

        [Test]
        public void Othello_InitGame_GetGameTable()
        {
            Othello othello = new Othello();

            CaseState[,] gameState = othello.GetGameState();

            Assert.AreEqual(gameState.GetLength(0), 8);
            Assert.AreEqual(gameState.GetLength(1), 8);
        }

        [Test]
        public void Othello_InitGame_VerifyPositionOfFirstPieces()
        {
            Othello othello = new Othello();

            CaseState[,] gameState = othello.GetGameState();

            Assert.AreEqual(gameState[4, 4], CaseState.Black);
            Assert.AreEqual(gameState[4, 5], CaseState.White);
            Assert.AreEqual(gameState[5, 4], CaseState.White);
            Assert.AreEqual(gameState[5, 5], CaseState.Black);
        }

        [Test]
        public void Othello_GetAvailablePositions()
        {
            Othello othello = new Othello();

            CaseState[,] gameState = othello.GetAvailablePositions();

            Assert.AreEqual(gameState[3, 4], CaseState.Available);
            Assert.AreEqual(gameState[4, 3], CaseState.Available);
            Assert.AreEqual(gameState[5, 6], CaseState.Available);
            Assert.AreEqual(gameState[6, 5], CaseState.Available);

        }
    }
}

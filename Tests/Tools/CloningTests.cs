using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Tests
{
    [TestClass()]
    public class CloningTests
    {
        [TestMethod()]
        public void CloneTest()
        {

            var piece = new King();
            var p2 = piece.DeepCopy();
            Assert.AreEqual(piece.GetType(), p2.GetType(),$"Type are not equal {p2.GetType()}");
            Assert.AreEqual(piece.Color, p2.Color, $"Type are not equal {p2.Color}");


        }
    }
}
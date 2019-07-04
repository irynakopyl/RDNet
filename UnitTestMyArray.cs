using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arr;

namespace UnitTestMyArray
{
    [TestClass]
    public class ArrayUnitTest
    {
        [TestMethod]
        public void Indexator_5elem_125Returned()
        {
            //arrange
            int firstElem = 3;
            int amount = 6;
            int expected = 125;
            MyArray<int> testing = new MyArray<int>(firstElem, amount);
            testing[5] = expected;
            //act
            int actual = testing[5];
            //assert
            Assert.AreEqual(125, actual);

        }
        [TestMethod]
        public void Indexator_IndexBiggerThanAmount_IndexOutOfRangeExceptionExpected()
        {
            //arrange
            int firstElem = 3;
            int amount = 6;
            MyArray<int> testing = new MyArray<int>(firstElem, amount);

            //act
            try
            {
                int result = testing[10];
            }
            catch(IndexOutOfRangeException e)
            { StringAssert.Contains(e.Message, testing.Message);
                return;
            }
            //assert
            Assert.Fail("No exceptions");

        }
       
    }
}

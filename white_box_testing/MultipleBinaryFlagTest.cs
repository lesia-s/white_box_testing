using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.BinaryFlag;
using System;

namespace white_box_testing
{
    [TestClass]
    public class MultipleBinaryFlag_Test
    {
        MultipleBinaryFlag flag_1;
        MultipleBinaryFlag flag_2;
        MultipleBinaryFlag flag_3;

        [TestInitialize]
        public void InitializeFlags()
        {
            flag_1 = new MultipleBinaryFlag(7);
            flag_2 = new MultipleBinaryFlag(7, true);
            flag_3 = new MultipleBinaryFlag(7, false);
        }

        [TestMethod]
        public void GetTypeTest()
        {
            Assert.AreEqual("MultipleBinaryFlag", flag_1.GetType().Name);
            Assert.AreEqual("MultipleBinaryFlag", flag_2.GetType().Name);
            Assert.AreEqual("MultipleBinaryFlag", flag_3.GetType().Name);
        }

        [TestMethod]
        public void LengthLessThanPermissible()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(0));
        }

        [TestMethod]
        public void LengthGreaterThanPermissible()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868705));
        }

        [TestMethod]
        public void VariousPermissibleLengths()
        {
            Assert.IsNotNull(flag_1);
            Assert.IsNotNull(flag_2);
            Assert.IsNotNull(flag_3);

            Assert.IsNotNull(new MultipleBinaryFlag(2));
            Assert.IsNotNull(new MultipleBinaryFlag(2021));
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704));
        }

        [TestMethod]
        public void SecondDefaultValue()
        {
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsTrue(flag_2.GetFlag());
            Assert.AreEqual(flag_1.GetFlag(), flag_2.GetFlag());
        }

        [TestMethod]
        public void GetFlagTest()
        {
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsTrue(flag_2.GetFlag());
            Assert.IsFalse(flag_3.GetFlag());
        }

        [TestMethod]
        public void EqualityTest()
        {
            MultipleBinaryFlag flag_1_copy = new MultipleBinaryFlag(7);

            Assert.AreNotEqual(flag_1, flag_2);
            Assert.AreNotEqual(flag_2, flag_3);
            Assert.AreNotEqual(flag_1, flag_3);
            Assert.AreNotEqual(flag_1, flag_1_copy);

            Assert.AreEqual(flag_1, flag_1);
            Assert.AreEqual(flag_1.GetFlag(), flag_1_copy.GetFlag());
        }

        [TestMethod]
        public void SetFlagOutOfRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_1.SetFlag(7));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_2.SetFlag(20));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_3.SetFlag(7));
        }

        [TestMethod]
        public void ResetFlagOutOfRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_1.ResetFlag(7));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_2.ResetFlag(20));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => flag_3.ResetFlag(7));
        }

        [TestMethod]
        public void SetFlagPosition()
        {
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsTrue(flag_2.GetFlag());
            Assert.IsFalse(flag_3.GetFlag());

            flag_1.SetFlag(2);
            Assert.IsTrue(flag_1.GetFlag());
            flag_2.SetFlag(0);
            Assert.IsTrue(flag_2.GetFlag());
            flag_3.SetFlag(5);
            Assert.IsFalse(flag_3.GetFlag());
        }

        [TestMethod]
        public void ResetFlagPosition()
        {
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsTrue(flag_2.GetFlag());
            Assert.IsFalse(flag_3.GetFlag());

            flag_1.ResetFlag(2);
            Assert.IsFalse(flag_1.GetFlag());
            flag_2.ResetFlag(0);
            Assert.IsFalse(flag_2.GetFlag());
            flag_3.ResetFlag(5);
            Assert.IsFalse(flag_3.GetFlag());
        }

        [TestMethod]
        public void SetFlagAll()
        {
            Assert.IsFalse(flag_3.GetFlag());
            for (uint i = 0; i < 7; i++)
            {
                flag_3.SetFlag(i);
            }
            Assert.IsTrue(flag_3.GetFlag());

            Assert.IsTrue(flag_1.GetFlag());
            for (uint i = 0; i < 7; i++)
            {
                flag_1.SetFlag(i);
            }
            Assert.IsTrue(flag_1.GetFlag());
        }

        [TestMethod]
        public void ResetFlagAll()
        {
            Assert.IsFalse(flag_3.GetFlag());
            for (uint i = 0; i < 7; i++)
            {
                flag_3.ResetFlag(i);
            }
            Assert.IsFalse(flag_3.GetFlag());

            Assert.IsTrue(flag_1.GetFlag());
            for (uint i = 0; i < 7; i++)
            {
                flag_1.ResetFlag(i);
            }
            Assert.IsFalse(flag_1.GetFlag());
        }

        [TestMethod]
        public void SetFlagResetFlagTest()
        {
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsTrue(flag_2.GetFlag());
            Assert.IsFalse(flag_3.GetFlag());

            flag_1.SetFlag(5);
            flag_3.SetFlag(4);
            Assert.IsTrue(flag_1.GetFlag());
            Assert.IsFalse(flag_3.GetFlag());

            flag_1.ResetFlag(5);
            flag_2.ResetFlag(4);
            Assert.IsFalse(flag_1.GetFlag());
            Assert.IsFalse(flag_2.GetFlag());
        }

        [TestMethod]
        public void DisposeTest()
        {
            flag_1.Dispose();
            flag_2.Dispose();
            flag_3.Dispose();

            Assert.IsNotNull(flag_1);
            Assert.IsNotNull(flag_2);
            Assert.IsNotNull(flag_3);
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("TTTTTTT", flag_1.ToString());
            Assert.AreEqual("TTTTTTT", flag_2.ToString());
            Assert.AreEqual("FFFFFFF", flag_3.ToString());

            flag_1.ResetFlag(0);
            flag_2.ResetFlag(3);
            flag_3.SetFlag(0);

            Assert.AreEqual("FTTTTTT", flag_1.ToString());
            Assert.AreEqual("TTTFTTT", flag_2.ToString());
            Assert.AreEqual("TFFFFFF", flag_3.ToString());
        }
    }
}

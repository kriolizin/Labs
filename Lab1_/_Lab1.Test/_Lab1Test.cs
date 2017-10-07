using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _Lab1;

namespace Lab1.Test
{
    [TestClass]
    public class _Lab1Test
    {
        public _Lab1Test() {}

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFunctionCoulomb()
        {
            Form1 obj = new Form1();
            Assert.AreEqual((float) 2 , obj.functionCoulomb(1, 2, 2, 4));
            Assert.AreEqual((float)4, obj.functionCoulomb(2, 2, 2, 4));
        }

        [TestMethod]
        public void TestIsDifferentCharges()
        {
            Form1 obj = new Form1();

            Charge obj1 = new Charge();
            Charge obj2 = new Charge();

            obj1.setPolarity(1);
            obj2.setPolarity(-1);

            Assert.AreEqual(true, obj.isDifferentCharges(obj1, obj2));


            obj1.setPolarity(-1);
            obj2.setPolarity(-1);

            Assert.AreEqual(false, obj.isDifferentCharges(obj1, obj2));

            obj1.setPolarity(1);
            obj2.setPolarity(1);
            Assert.AreEqual(false, obj.isDifferentCharges(obj1, obj2));
        }

        [TestMethod]
        public void TestIsClash()
        {
            Form1 obj = new Form1();

            Charge obj1 = new Charge();
            Charge obj2 = new Charge();

            obj1.position = new Vector2(0f, 0f);
            obj2.position = new Vector2(3f, 4f);
            obj1.radius = 2;
            obj2.radius = 3;

            Assert.AreEqual(true, obj.isClash(obj1, obj2));

            obj1.radius = 1;

            Assert.AreEqual(false, obj.isClash(obj1, obj2));
        }

        [TestMethod]
        public void TestGetSingleVector()
        {
            Vector2 v1 = new Vector2(0, 1);
            Form1 obj = new Form1();

            Vector2 v2 = obj.getSingleVector(v1, new Vector2(0, 0));

            Assert.AreEqual(v1.X,  v2.X);
            Assert.AreEqual(v1.Y, v2.Y);
        }
    }
}

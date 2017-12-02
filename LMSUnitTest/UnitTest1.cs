using System;
using LMS_Starter.Model;
using Xunit;

namespace LMSUnitTest
{
    public class UnitTest1
    {
        //[Theory]
        //[InlineData(2)]
        //[InlineData(5)]
        //[InlineData(7)]
        //[InlineData(9)]
        //[InlineData(11)]
        //public void MyFirstTheory(int value)
        //{
        //    Assert.True(IsOdd(value));
        //}

        //bool IsOdd(int value)
        //{
        //    return value % 2 == 1;
        //}

        //[Fact]
        //public void PassingTest()
        //{
        //    Assert.Equal(4, Add(2, 2));
        //}
        //int Add(int x, int y)
        //{
        //    return x + y;
        //}

        [Fact]
        public void TestCreateNewStudentFromBody()
        {
            var student = new Student { ID = 10, FirstName = "Abby", LastName = "Chan", Email = "Abby.ch@qut.com" };
            var studentToDB = Student.StudentMapping(student);
            Assert.Equal<int>(studentToDB.ID, 0);
        }
    }
}

namespace BULSTests
{
    using System.Collections.Generic;
    using BangaloreUniversityLearningSystem.Data;
    using BangaloreUniversityLearningSystem.Enums;
    using BangaloreUniversityLearningSystem.Interfaces;
    using BangaloreUniversityLearningSystem.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetMethodTestst
    {
        private IRepository<Course> courses;
        private UsersRepository users;

        // Arrange
        [TestInitialize]
        public void SetUp()
        {
            this.courses = new Repository<Course>();

            this.users = new UsersRepository();
        }

        [TestMethod]
        public void TestGet_NormalCase_ShouldReturnCorrectItem()
        {
            var courseList = new List<Course>()
            {
                new Course("Object Oriented Programming"),
                new Course("High Quality Code"),
                new Course("AdvancedC#")
            };

            foreach (var course in courseList)
            {
                courses.Add(course);
            }

            // Act
            var actualCourse = courses.Get(3);

            // Assert
            Assert.AreEqual(courseList[2], actualCourse);
        }

        [TestMethod]
        public void TestGet_OutOfRangeCase_ShouldReturnDefaultType()
        {
            var actualCourse = courses.Get(5);

            Assert.AreEqual(null, actualCourse);
        }

        [TestMethod]
        public void TestGetUserByUsername_NormalCase_ShouldReturnUser()
        {
            var userList = new List<User>()
            {
                new User("newUser", "123456", Role.Student),
                new User("anotherNewUser", "1234567", Role.Lecturer),
                new User("andAnotherOne", "1234568", Role.Student)
            };

            foreach (var user in userList)
            {
                users.Add(user);
            }

            var actualUser = users.GetByUsername("newUser");

            Assert.AreEqual(userList[0], actualUser);
        }

        [TestMethod]
        public void TestGetUserByUsername_NoSuchUsername_ShouldReturnNull()
        {
            var actualUser = users.GetByUsername("Pencho");

            Assert.AreEqual(null, actualUser);
        }
    }
}

namespace BULSTests
{
    using System;
    using System.Linq;
    using BangaloreUniversityLearningSystem.Controllers;
    using BangaloreUniversityLearningSystem.Enums;
    using BangaloreUniversityLearningSystem.Interfaces;
    using BangaloreUniversityLearningSystem.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AddLectureTests
    {
        private IBangaloreUniversityData mockedData;
        private Course course;

        [TestInitialize]
        public void InitializeMoths()
        {
            var dataMock = new Mock<IBangaloreUniversityData>();
            var courseRepoMock = new Mock<IRepository<Course>>();
            this.course = new Course("C# for babies.");

            courseRepoMock.Setup(r => r.Get(It.IsAny<int>())).Returns(this.course);

            dataMock.Setup(d => d.Courses).Returns(courseRepoMock.Object);

            this.mockedData = dataMock.Object;
        }

        [TestMethod]
        public void AddLecture_ValidCourseId_ShouldAddToCourse()
        {
            var controller = new CoursesController(this.mockedData, new User("nasko", "123456", Role.Lecturer));

            string lectureName = DateTime.Now.ToString();

            var view = controller.AddLecture(5, lectureName);

            Assert.AreEqual(this.course.Lectures.First().Name, lectureName);
            Assert.IsNotNull(view);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void AddLecture_InValidCourseId_ShouldAdd_ToCourse()
        //{
        //    var controller = new CoursesController(this.mockedData, new User("nasko", "123456", Role.Lecturer));

        //    string lectureName = DateTime.Now.ToString();

        //    var view = controller.AddLecture(int.MaxValue, lectureName);
        //}
    }
}

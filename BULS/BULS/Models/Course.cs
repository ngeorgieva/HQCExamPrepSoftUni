namespace BangaloreUniversityLearningSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        private string name;

        public Course(string name)
        {
            this.Name = name;
            this.Lectures = new HashSet<Lecture>();
            this.Students = new HashSet<User>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = string.Format("The course name must be at least 5 symbols long.");
                    throw new ArgumentException(message);
                }

                this.name = value;
            }
        }

        public HashSet<Lecture> Lectures { get; set; }

        public HashSet<User> Students { get; set; }

        public void AddLecture(Lecture lecture)
        {
            this.Lectures.Add(lecture);
        }

        public void AddStudent(User student)
        {
            this.Students.Add(student);
            student.Courses.Add(this);
        }
    }
}

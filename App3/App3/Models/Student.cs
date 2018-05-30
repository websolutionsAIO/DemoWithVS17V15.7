using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App3.Models
{
    public class Student
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string ClassName { get; set; }
        public string ImageUrl { get; set; }//= "http://media.gettyimages.com/photos/colorful-cosmos-flowers-picture-id157190551";

        public static ObservableCollection<Student> GetStudent()
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();

            students.Add(new Student() { Age = 10, FirstName = "aarav1", ClassName = "ukg",ImageUrl= "http://media.gettyimages.com/photos/colorful-cosmos-flowers-picture-id157190551"});
            students.Add(new Student() { Age = 11, FirstName = "aarav2", ClassName = "lkg", ImageUrl = "http://media.gettyimages.com/photos/colorful-cosmos-flowers-picture-id157190551" });
            students.Add(new Student() { Age = 5, FirstName = "aarav3", ClassName = "ukg1", ImageUrl = "http://media.gettyimages.com/photos/colorful-cosmos-flowers-picture-id157190551" });
            students.Add(new Student() { Age = 12, FirstName = "aarav4", ClassName = "ukg2", ImageUrl = "http://media.gettyimages.com/photos/colorful-cosmos-flowers-picture-id157190551" });
            return students;
        }
    }
}

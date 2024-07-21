using Application_.Services.Abstract;
using Core.Entities;
using Core.Messages;
using Data.UnitsOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly Unitofwork _unitofwork;
        public StudentService()
        {
            _unitofwork = new Unitofwork();
        }

        public void AddStudent()
        {
            var CountGroups = _unitofwork.Groups.GetAllGroupsCount();
            if (CountGroups <= 0)
            {
                ErrorMessages.CountIsZeroMessage("group");
                return;
            }
        GroupInput: _unitofwork.Groups.GetAllGroups();
            BasicMessages.InputMessage("group id");
            string groupIdInput = Console.ReadLine();
            int groupId;
            bool isSucceded = int.TryParse(groupIdInput, out groupId);
            if (!isSucceded || string.IsNullOrWhiteSpace(groupIdInput))
            {
                ErrorMessages.InvalidInputMessage(groupIdInput);
                goto GroupInput;
            }
            var existGroup = _unitofwork.Groups.GetStudentOfGroupById(groupId);
            if (existGroup == null)
            {
                ErrorMessages.NotFoundMessage(groupIdInput);
                goto GroupInput;
            }
            var CountStudentsinGroup = existGroup.Students.Count();
            if (CountStudentsinGroup == existGroup.Limit)
            {
                ErrorMessages.StudentLimitMessage();
                if (CountGroups == 1)
                    return;
                else
                    goto GroupInput;
            }
        NameInput: BasicMessages.InputMessage("student name");
            string studentName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentName))
            {
                ErrorMessages.InvalidInputMessage(studentName);
                goto NameInput;
            }
        SurnameInput: BasicMessages.InputMessage("student surname");
            string studentSurname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentSurname))
            {
                ErrorMessages.InvalidInputMessage(studentSurname);
                goto SurnameInput;
            }
            Student student = new Student();
            student.GroupId = groupId;
            student.Surname = studentSurname;
            student.Name = studentName;
            _unitofwork.Students.Add(student);
            _unitofwork.Commit();
            BasicMessages.SuccessMessage(studentName, "added");
        }

        public void DeleteStudent()
        {
        StudentInput: GetAllStudents();
            BasicMessages.InputMessage("student id");
            string InputId = Console.ReadLine();
            int studentId;
            bool isSucceded = int.TryParse(InputId, out studentId);
            if (!isSucceded || string.IsNullOrWhiteSpace(InputId))
            {
                ErrorMessages.InvalidInputMessage(InputId);
                goto StudentInput;
            }
            var existStudent = _unitofwork.Students.GetByIdWithGroup(studentId);
            if (existStudent == null)
            {
                ErrorMessages.NotFoundMessage(InputId);
                goto StudentInput;
            }
            _unitofwork.Students.Delete(existStudent);
            _unitofwork.Commit();
            BasicMessages.SuccessMessage(existStudent.Name, "deleted");
        }

        public void GetAllStudents()
        {

            _unitofwork.Students.GetAllStudents();
        }

        public void GetStudentDetail()
        {
        StudentInput: 
            GetAllStudents();
            BasicMessages.InputMessage("student id");
            string InputId = Console.ReadLine();
            int studentId;
            bool isSucceded = int.TryParse(InputId, out studentId);
            if (!isSucceded || string.IsNullOrWhiteSpace(InputId))
            {
                ErrorMessages.InvalidInputMessage(InputId);
                goto StudentInput;
            }
            var existStudent = _unitofwork.Students.GetByIdWithGroup(studentId);
            if (existStudent == null)
            {
                ErrorMessages.NotFoundMessage(InputId);
                goto StudentInput;
            }
            Console.WriteLine($"Id: {existStudent.Id} | Surname: {existStudent.Surname} Name: {existStudent.Name} | Group: {existStudent.Group.Name}");
        }
        public void GetStudentsOfGroup()
        {
        GroupInput: _unitofwork.Groups.GetAllGroups();
            BasicMessages.InputMessage("group id");
            string inputId = Console.ReadLine();
            int groupId;
            bool isSucceded = int.TryParse(inputId, out groupId);
            if (!isSucceded || string.IsNullOrWhiteSpace(inputId))
            {
                ErrorMessages.InvalidInputMessage(inputId);
                goto GroupInput;
            }
            var existGroup = _unitofwork.Groups.GetStudentOfGroupById(groupId);
            if (existGroup == null)
            {
                ErrorMessages.NotFoundMessage(inputId);
                goto GroupInput;
            }
            foreach (var student in existGroup.Students)
            {
                Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname}");
            }
        }

        public void UpdateStudent()
        {
            var studentCount = _unitofwork.Students.GetAllStudentsCount();
            if (studentCount <= 0)
            {
                ErrorMessages.CountIsZeroMessage("student");
                return;
            }
        UpdateInput: GetAllStudents();
            BasicMessages.InputMessage("student id");
            string inputId = Console.ReadLine();
            int studentId;
            bool isSucceded = int.TryParse(inputId, out studentId);
            if (!isSucceded || string.IsNullOrWhiteSpace(inputId))
            {
                ErrorMessages.InvalidInputMessage(inputId);
                goto UpdateInput;
            }
            var existStudent = _unitofwork.Students.GetByIdWithGroup(studentId);
            if (existStudent == null)
            {
                ErrorMessages.NotFoundMessage(inputId);
                goto UpdateInput;
            }
            string studentName = existStudent.Name;
            string studentSurname = existStudent.Surname;
            int studentGroupId = existStudent.GroupId;
        NameInput: BasicMessages.WantToChangeMessage("student name");
            string input = Console.ReadLine();
            char result;
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto NameInput;
            }
            if (result == 'y')
            {
            StudentNameInput: BasicMessages.InputMessage("new student name");
                studentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(studentName))
                {
                    ErrorMessages.InvalidInputMessage(studentName);
                    goto StudentNameInput;
                }
            }
        SurnameInput: BasicMessages.WantToChangeMessage("student surname");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto SurnameInput;
            }
            if (result == 'y')
            {
            StudentSurnameInput: BasicMessages.InputMessage("new student surname");
                studentSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(studentSurname))
                {
                    ErrorMessages.InvalidInputMessage(studentSurname);
                    goto StudentSurnameInput;
                }
            }
        GroupInput: BasicMessages.WantToChangeMessage("group");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto GroupInput;
            }
            if (result == 'y')
            {
                var CountGroup = _unitofwork.Groups.GetAllGroupsCount();
                if (CountGroup <= 1)
                {
                    ErrorMessages.CountIsZeroMessage("group");
                    return;
                }
                _unitofwork.Groups.GetAllGroups();
                BasicMessages.InputMessage("group id");
                input = Console.ReadLine();
                isSucceded = int.TryParse(input, out studentGroupId);
                if (!isSucceded || string.IsNullOrWhiteSpace(input))
                {
                    ErrorMessages.InvalidInputMessage(input);
                    goto GroupInput;
                }
                var existGroup = _unitofwork.Groups.GetStudentOfGroupById(studentGroupId);
                if (existGroup == null)
                {
                    ErrorMessages.NotFoundMessage(input);
                    goto GroupInput;
                }
                existStudent.Name = studentName;
                existStudent.Surname = studentSurname;
                existStudent.GroupId = studentGroupId;
                _unitofwork.Students.Update(existStudent);
                _unitofwork.Commit();
                BasicMessages.SuccessMessage(studentName, "updated");
            }
        }
    }
}

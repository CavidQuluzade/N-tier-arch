using Application_.Services;
using Application_.Services.Concrete;
using Core.Constats;
using Core.Entities;
using Core.Messages;

namespace Presentation
{
    public static class Program
    {
        private static readonly GroupService _groupService = new GroupService();
        private static readonly StudentService _studentService = new StudentService();
        

        static void Main(string[] args)
        {
            bool isSucceded = true;
            bool Exit = true;
            while (Exit)
            {
                ShowMenu();
                BasicMessages.InputMessage("choice");
                string choiceInput = Console.ReadLine();
                isSucceded = int.TryParse(choiceInput, out int choice);
                if (isSucceded)
                {
                    switch ((Operations)choice)
                    {
                        case Operations.ViewGroups:
                            _groupService.GetAllGroups();
                            break;
                        case Operations.GetDetailsofGroup:
                            _groupService.GetDetailsOfGroup();
                            break;
                        case Operations.AddGroup:
                            _groupService.AddGroup();
                            break;
                        case Operations.UpdateGroup:
                            _groupService.UpdateGroup();
                            break;
                        case Operations.DeleteGroup:
                            _groupService.RemoveGroup();
                            break;
                        case Operations.ViewStudentsInGroup:
                            _studentService.GetStudentsOfGroup();
                            break;
                        case Operations.ViewStudents:
                            _studentService.GetAllStudents();
                            break;
                        case Operations.GetDetailsofStudent:
                            _studentService.GetStudentDetail();
                            break;
                        case Operations.AddStudent:
                            _studentService.AddStudent();
                            break;
                        case Operations.UpdateStudent:
                            _studentService.UpdateStudent();
                            break;
                        case Operations.DeleteStudent:
                            _studentService.DeleteStudent();
                            break;
                        case Operations.Exit:
                            Exit = false;
                            break;
                        default:
                            ErrorMessages.InvalidInputMessage(choiceInput);
                            break;
                    }
                }
                else
                {
                    ErrorMessages.InvalidInputMessage(choiceInput);
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("0   Exit");
            Console.WriteLine("1   View Groups");
            Console.WriteLine("2   Get Details of Group");
            Console.WriteLine("3   Add Group");
            Console.WriteLine("4   Update Group");
            Console.WriteLine("5   Delete Group");
            Console.WriteLine("6   View Students in group");
            Console.WriteLine("7   View Students");
            Console.WriteLine("8   Get Details of Student");
            Console.WriteLine("9   Add Student");
            Console.WriteLine("10  Update Student");
            Console.WriteLine("11  Delete Student");
        }
    }
}

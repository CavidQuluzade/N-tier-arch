using Application_.Services.Abstract;
using Core.Entities;
using Core.Messages;
using Data.Context;
using Data.UnitsOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_.Services
{
    public class GroupService : IGroupService
    {
        private readonly Unitofwork _unitofwork;
        public GroupService()
        {
            _unitofwork = new Unitofwork();
        }
        public void AddGroup()
        {
        GroupNameInput: BasicMessages.InputMessage("group name");
            string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ErrorMessages.InvalidInputMessage(groupName);
                goto GroupNameInput;
            }
            if (_unitofwork.Groups.GetGroupByName(groupName) != null)
            {
                ErrorMessages.ExistMessage(groupName);
                goto GroupNameInput;
            }
        GroupLimitInput: BasicMessages.InputMessage("group limit");
            string groupLimitInput = Console.ReadLine();
            int groupLimit;
            bool isSucceded = int.TryParse(groupLimitInput, out groupLimit);
            if (!isSucceded || string.IsNullOrWhiteSpace(groupLimitInput))
            {
                ErrorMessages.InvalidInputMessage(groupLimitInput);
                goto GroupLimitInput;
            }
            if (groupLimit <= 0 || groupLimit > 20)
            {
                ErrorMessages.LimitInputMessage(groupLimitInput);
                goto GroupLimitInput;
            }
        BeginDateInput: BasicMessages.InputMessage("Begin date");
            string BegindateInput = Console.ReadLine();
            DateTime BeginDate;
            isSucceded = DateTime.TryParseExact(BegindateInput, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out BeginDate);
            if (!isSucceded)
            {
                ErrorMessages.InvalidInputMessage(BegindateInput);
                goto BeginDateInput;
            }
        EndDateInput: BasicMessages.InputMessage("End date");
            string EnddateInput = Console.ReadLine();
            DateTime EndDate;
            isSucceded = DateTime.TryParseExact(EnddateInput, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate);
            if (!isSucceded || BeginDate.AddMonths(6) > EndDate)
            {
                ErrorMessages.InvalidInputMessage(BegindateInput);
                goto EndDateInput;
            }
            Group group = new Group();
            group.Name = groupName;
            group.BeginDate = BeginDate;
            group.EndDate = EndDate;
            group.Limit = groupLimit;
            _unitofwork.Groups.Add(group);
            _unitofwork.Commit();
            BasicMessages.SuccessMessage(groupName, "added");
        }

        public void GetAllGroups()
        {
            _unitofwork.Groups.GetAllGroups();
        }

        public void GetDetailsOfGroup()
        {
        GroupInput: GetAllGroups();
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
            Console.WriteLine($"Id: {groupId} | Name: {existGroup.Name} | Limit: {existGroup.Limit} | BeginDate: {existGroup.BeginDate} | EndDate: {existGroup.EndDate}");
            Console.WriteLine("Students:");
            foreach (var student in existGroup.Students)
            {
                Console.WriteLine($"{student.Surname} {student.Name}");
            }
        }

        public void RemoveGroup()
        {
            var groupCount = _unitofwork.Groups.GetAllGroupsCount();
            if (groupCount <= 0)
            {
                ErrorMessages.CountIsZeroMessage("group");
                return;
            }
        GroupIdInput: GetAllGroups();
            Console.WriteLine("All students will be deleted");
            BasicMessages.InputMessage("group id");
            string inputId = Console.ReadLine();
            int groupId;
            bool isSucceded = int.TryParse(inputId, out groupId);
            if (!isSucceded || string.IsNullOrWhiteSpace(inputId))
            {
                ErrorMessages.InvalidInputMessage(inputId);
                goto GroupIdInput;
            }
            var existGroup = _unitofwork.Groups.GetStudentOfGroupById(groupId);
            if (existGroup == null)
            {
                ErrorMessages.NotFoundMessage(inputId);
                goto GroupIdInput;
            }
            foreach (var student in existGroup.Students)
            {
                _unitofwork.Students.Delete(student);
            }
            _unitofwork.Groups.Delete(existGroup);
            _unitofwork.Commit();
            BasicMessages.SuccessMessage(existGroup.Name, "deleted");
        }

        public void UpdateGroup()

        {
            var groupCount = _unitofwork.Groups.GetAllGroupsCount();
            if (groupCount <= 0)
            {
                ErrorMessages.CountIsZeroMessage("group");
                return;
            }
        UpdateInput: GetAllGroups();
            BasicMessages.InputMessage("group id");
            string inputId = Console.ReadLine();
            int groupId;
            bool isSucceded = int.TryParse(inputId, out groupId);
            if (!isSucceded || string.IsNullOrWhiteSpace(inputId))
            {
                ErrorMessages.InvalidInputMessage(inputId);
                goto UpdateInput;
            }
            var existGroup = _unitofwork.Groups.GetStudentOfGroupById(groupId);
            if (existGroup == null)
            {
                ErrorMessages.NotFoundMessage(inputId);
                goto UpdateInput;
            }
            string groupName = existGroup.Name;
            int groupLimit = existGroup.Limit;
            DateTime groupBegindate = existGroup.BeginDate;
            DateTime groupEnddate = existGroup.EndDate;
        NameInput: BasicMessages.WantToChangeMessage("group name");
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
            GroupNameInput: BasicMessages.InputMessage("new group name");
                groupName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(groupName))
                {
                    ErrorMessages.InvalidInputMessage(groupName);
                    goto GroupNameInput;
                }
                var existName = _unitofwork.Groups.GetGroupByName(groupName);
                if (existName != null)
                {
                    ErrorMessages.ExistMessage("group name");
                    goto NameInput;
                }
                existGroup.Name = groupName;
            }
        LimitInput: BasicMessages.WantToChangeMessage("group limit");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto LimitInput;
            }
            if (result == 'y')
            {
            GroupLimitInput: BasicMessages.InputMessage("new limit");
                input = Console.ReadLine();
                isSucceded = int.TryParse(input, out groupLimit);
                if (!isSucceded || string.IsNullOrWhiteSpace(groupName))
                {
                    ErrorMessages.InvalidInputMessage(groupName);
                    goto GroupLimitInput;
                }
                var studentCount = existGroup.Students.Count();
                if (groupLimit < studentCount)
                {
                    ErrorMessages.StudentLimitMessage();
                    goto GroupLimitInput;
                }
                existGroup.Limit = groupLimit;
            }
        BegindateInput: BasicMessages.WantToChangeMessage("group begin date");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto BegindateInput;
            }
            if (result == 'y')
            {
                BasicMessages.InoutBeginDateMessage("begin date");
                input = Console.ReadLine();
                isSucceded = DateTime.TryParseExact(input, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out groupBegindate);
                if (!isSucceded || string.IsNullOrWhiteSpace(input))
                {
                    ErrorMessages.InvalidInputMessage(input);
                    goto BegindateInput;
                }
                existGroup.BeginDate = groupBegindate;

            }
        EnddateInput: BasicMessages.WantToChangeMessage("group end date");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out result);
            if (!isSucceded || string.IsNullOrWhiteSpace(input) || result != 'y' && result != 'n')
            {
                ErrorMessages.InvalidInputMessage(input);
                goto EnddateInput;
            }
            if (result == 'y')
            {
                BasicMessages.InoutEndDateMessage("end date", 6);
                input = Console.ReadLine();
                isSucceded = DateTime.TryParseExact(input, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out groupEnddate);
                if (!isSucceded || string.IsNullOrWhiteSpace(input))
                {
                    ErrorMessages.InvalidInputMessage(input);
                    goto EnddateInput;
                }
            }
            if (existGroup.BeginDate.AddMonths(6).Date >= groupEnddate.Date)
            {
                ErrorMessages.InvalidInputMessage("group date");
                goto BegindateInput;
            }
            existGroup.EndDate = groupEnddate;
            _unitofwork.Groups.Update(existGroup);
            _unitofwork.Commit();
            BasicMessages.SuccessMessage(groupName, "updated");
        }
    }
}

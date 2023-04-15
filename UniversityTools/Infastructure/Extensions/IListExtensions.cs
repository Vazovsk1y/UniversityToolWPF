using System.Collections.Generic;
using System.Linq;
using UniversityTool.Domain.Models;

namespace UniversityTool.Infastructure.Extensions
{
    internal static class IListExtensions
    {
        public static void AddDepapartament(this IList<Departament> departamentTree, Departament departament)
            => departamentTree.Add(departament);

        public static void AddGroup(this IList<Departament> departamentsTree, Group group)
        {
            var departament = departamentsTree.FirstOrDefault(d => d.Id == group.DepartamentId);
            departament?.Groups.Add(group);
        }

        public static void AddStudent(this IList<Departament> departaments, Student student)
        {
            var group = departaments.SelectMany(d => d.Groups).FirstOrDefault(g => g.Id == student.GroupId);
            group?.Students.Add(student);
        }

        public static void UpdateDepartament(this IList<Departament> departaments, Departament departamentToUpdate, Departament updatingDepartament)
        {
            var index = departaments.IndexOf(departamentToUpdate);
            if (index != -1)
            {
                departaments[index] = updatingDepartament;
            }
        }

        public static void UpdateGroup(this IList<Departament> departaments, Group groupToUpdate, Group updatingGroup)
        {
            var departament = departaments.FirstOrDefault(d => d.Id == groupToUpdate.DepartamentId);

            if (departament is not null)
            {
                int index = departament.Groups.IndexOf(groupToUpdate);
                if (index != -1)
                {
                    departament.Groups[index] = updatingGroup;
                }
            }
        }

        public static void UpdateStudent(this IList<Departament> departaments, Student studentToUpdate, Student updatingStudent)
        {
            var group = departaments.SelectMany(d => d.Groups).FirstOrDefault(g => g.Id == studentToUpdate.GroupId);

            if (group is not null)
            {
                int index = group.Students.IndexOf(studentToUpdate);
                if (index is not -1)
                {
                    group.Students[index] = updatingStudent;
                }
            }
        }

        public static void DeleteDepartament(this IList<Departament> departaments, Departament departament)
        {
            var index = departaments.IndexOf(departament);
            if (index is not -1)
            {
                departaments.RemoveAt(index);
            }
        }

        public static void DeleteGroup(this IList<Departament> departaments, Group group)
        {
            var departament = departaments.FirstOrDefault(d => d.Id == group.DepartamentId);

            if (departament is not null)
            {
                int index = departament.Groups.IndexOf(group);
                if (index is not -1)
                {
                    departament.Groups.RemoveAt(index);
                }
            }
        }

        public static void DeleteStudent(this IList<Departament> departaments, Student student)
        {
            var group = departaments.SelectMany(d => d.Groups).FirstOrDefault(g => g.Id == student.GroupId);

            if (group is not null)
            {
                int index = group.Students.IndexOf(student);
                if (index is not -1)
                {
                    group.Students.RemoveAt(index);
                }
            }
        }
    }
}

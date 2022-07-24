
using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.Models;

namespace Demo.Pl.Mappers
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee , EmployeeViewModel>().ReverseMap();
        }
    }
}

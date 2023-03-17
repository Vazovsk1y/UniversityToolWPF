using UniversityTool.DataBase.Response;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Response;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using System.Diagnostics;

namespace UniversityTool.DataBase.Services
{
    internal class DepartamentService : IDepartamentService
    {
        private readonly IBaseRepository<Departament> _departamentRepository;
        internal Departament Departament { get; private set; }

        public DepartamentService(IBaseRepository<Departament> departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<IBaseResponse<Departament>> AddDepartament(string title)
        {
            if (title is null)
                return new BaseResponce<Departament>
                {
                    Description = "The Title was null",
                    StatusCode = StatusCode.Fail,
                };

            try
            {
                Departament = await _departamentRepository.Add(new Departament { Title = title }).ConfigureAwait(false);

                if (Departament is null)
                {
                    return new BaseResponce<Departament>
                    {
                        Description = "The adding was failed",
                        StatusCode = StatusCode.Fail,
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new BaseResponce<Departament>
                {
                    Description = "The adding was failed",
                    StatusCode = StatusCode.Fail,
                };
            }

            return new BaseResponce<Departament>
            {
                Description = "The adding was sucsesfull",
                StatusCode = StatusCode.Success,
                Data = Departament
            };
        }
    }
}

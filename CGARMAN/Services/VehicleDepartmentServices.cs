using RepositoryPatternWithUOW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class VehicleDepartmentServices
    {
        IUnitOfWork unitOfWork;
        public VehicleDepartmentServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
    }
}

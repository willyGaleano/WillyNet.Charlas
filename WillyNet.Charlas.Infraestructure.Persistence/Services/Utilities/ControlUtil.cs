using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Interfaces.Utilities;
using WillyNet.Charlas.Core.Application.Specifications.Controls;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Infraestructure.Persistence.Services.Utilities
{
    public class ControlUtil : IControlUtil
    {
        private readonly IRepositoryAsync<Control> _repositoryControl;
        public ControlUtil(IRepositoryAsync<Control> repositoryControl)
        {
            _repositoryControl = repositoryControl;
        }

        public async Task<bool> CreateOrUpdateCantControl(string userId, DateTime fechaRegistro)
        {
            try
            {
                var countLimitCharlas = await _repositoryControl.CountAsync(
                    new CountControlByUserDateSpecification(userId, fechaRegistro)
                );

                if (countLimitCharlas == 0)
                {
                    await _repositoryControl.AddAsync(new Control
                    {
                        ControlId = Guid.NewGuid(),
                        FecSesion = fechaRegistro,
                        Tope = 1,
                        UserAppId = userId
                    });
                }
                else
                {
                    var control = await _repositoryControl.GetBySpecAsync(
                            new GetControllByUserDateSpecification(userId, fechaRegistro)
                        );

                    if (control.Tope >= 2)
                        return false;
                    control.Tope += 1;
                    await _repositoryControl.UpdateAsync(control);
                }


                return true;
            }
            catch(Exception ex)
            {
               throw new ApiException(ex.Message);
            }
        }


    }
}


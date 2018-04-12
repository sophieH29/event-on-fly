using System;
using System.Threading.Tasks;
using EventOnFly.Data.RequestProcessors;
using EventOnFly.Dtos;
using EventOnFly.Enums;

namespace EventOnFly.BusinessLogic
{
    public interface IRegistrationService
    {
        Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly IProcedureExecutor procedureExecutor;

        public RegistrationService(IProcedureExecutor procedureExecutor)
        {
            this.procedureExecutor = procedureExecutor;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            var serviceExists = await procedureExecutor.ExecProcedureNonQuery<bool>(ProcedureName.CheckServiceUserExists, form.Username, form.Email);
            if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
            await procedureExecutor.ExecuteProcedureNoResult(ProcedureName.CheckServiceUserExists, form.Username, form.Email);
            return StartRegistrationResult.Success;
        }
    }
}

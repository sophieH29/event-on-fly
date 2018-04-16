using System.Threading.Tasks;
using EventOnFly.Data.DbAccess;
using EventOnFly.Enums;
using EventOnFly.VendorSide.Dtos;

namespace EventOnFly.VendorSide.BusinessLogic
{
    public interface IRegistrationService
    {
        Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly IProcedureExecutor procedureExecutor;
        private readonly ITransacrionManager transactionManager;

        public RegistrationService(IProcedureExecutor procedureExecutor, ITransacrionManager transactionManager)
        {
            this.procedureExecutor = procedureExecutor;
            this.transactionManager = transactionManager;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await transactionManager.ExecuteWithinTransaction(async () =>
            {
                var serviceExists =
                    await procedureExecutor.ExecProcedureNonQuery<bool>(ProcedureName.CheckServiceUserExists, form.Username, form.Email);
                if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
                await procedureExecutor.ExecuteProcedureNoResult(ProcedureName.CheckServiceUserExists, form.Username, form.Email);
                return StartRegistrationResult.Success;
            });
        }
    }
}

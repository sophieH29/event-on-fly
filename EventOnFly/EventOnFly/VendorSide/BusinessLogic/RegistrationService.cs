using System.Threading.Tasks;
using EventOnFly.Data.DbAccess;
using EventOnFly.Data.DbAccess.Parameters;
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

        public RegistrationService(
            IProcedureExecutor procedureExecutor, 
            ITransacrionManager transactionManager)
        {
            this.procedureExecutor = procedureExecutor;
            this.transactionManager = transactionManager;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await transactionManager.ExecuteWithinTransaction(async () =>
            {
                var serviceExists =
                    await procedureExecutor.ExecProcedureNonQuery<bool>(
                        ProcedureName.UspCheckServiceUserExists, 
                        new ProcedureParameter("username", form.Username), 
                        new ProcedureParameter("email", form.Email));
                if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
                await procedureExecutor.ExecuteProcedureNoResult(
                    ProcedureName.UspCreateNewService,
                    new ProcedureParameter("username", form.Username),
                    new ProcedureParameter("email", form.Email),
                    new ProcedureParameter("password", form.Password),
                    new ProcedureParameter("attachedServiceTypes", form.AttachedServiceTypes));
                return StartRegistrationResult.Success;
            });
        }
    }
}

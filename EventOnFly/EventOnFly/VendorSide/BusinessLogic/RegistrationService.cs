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
        private readonly IDbMediator dbMediator;

        public RegistrationService(
            IProcedureExecutor procedureExecutor, 
            ITransacrionManager transactionManager,
            IDbMediator dbMediator)
        {
            this.procedureExecutor = procedureExecutor;
            this.transactionManager = transactionManager;
            this.dbMediator = dbMediator;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await transactionManager.ExecuteWithinTransaction(async () =>
            {
                var serviceExists =
                    await procedureExecutor.ExecProcedureNonQuery<bool>(
                        ProcedureName.CheckServiceUserExists, 
                        new ProcedureParameter("username", form.Username), 
                        new ProcedureParameter("email", form.Email));
                if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
                await procedureExecutor.ExecuteProcedureNoResult(
                    ProcedureName.CreateNewService,
                    new ProcedureParameter("username", form.Username),
                    new ProcedureParameter("email", form.Email),
                    new ProcedureParameter("password", form.Password),
                    new ProcedureParameter("attachedServiceTypes", form.AttachedServiceTypes));
                return StartRegistrationResult.Success;
            });
        }
    }
}

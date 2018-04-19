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
        private readonly IDbRequestExecutor dbRequestExecutor;

        public RegistrationService(
            IProcedureExecutor procedureExecutor,
            IDbRequestExecutor dbRequestExecutor)
        {
            this.procedureExecutor = procedureExecutor;
            this.dbRequestExecutor = dbRequestExecutor;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await dbRequestExecutor.ExecuteDbRequestWithResult(async token =>
            {
                var serviceExists =
                    await procedureExecutor.ExecProcedureNonQuery<bool>(
                        ProcedureName.UspCheckServiceUserExists,
                        token,
                        new ProcedureParameter("username", form.Username), 
                        new ProcedureParameter("email", form.Email));
                if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
                await procedureExecutor.ExecuteProcedureNoResult(
                    ProcedureName.UspCreateNewService,
                    token,
                    new ProcedureParameter("username", form.Username),
                    new ProcedureParameter("email", form.Email),
                    new ProcedureParameter("password", form.Password),
                    new ProcedureParameter("attachedServiceTypes", form.AttachedServiceTypes));
                return StartRegistrationResult.Success;
            });
        }
    }
}

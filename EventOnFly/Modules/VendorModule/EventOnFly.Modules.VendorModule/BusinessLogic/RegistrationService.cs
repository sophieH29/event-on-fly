using EventOnFly.Common.Interfaces.Enums;
using EventOnFly.DataAccess.Data.DbAccess;
using EventOnFly.DataAccess.Data.DbAccess.Parameters;
using EventOnFly.Modules.VendorModule.Dtos;
using System.Threading.Tasks;

namespace EventOnFly.Modules.VendorModule.BusinessLogic
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IProcedureExecutor procedureExecutor;
        private readonly IDbRequestWrapper dbRequestWrapper;

        public RegistrationService(
            IProcedureExecutor procedureExecutor,
            IDbRequestWrapper dbRequestWrapper)
        {
            this.procedureExecutor = procedureExecutor;
            this.dbRequestWrapper = dbRequestWrapper;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await dbRequestWrapper.ExecuteDbRequestWithResult(async token =>
            {
                var serviceExists =
                    await procedureExecutor.ExecProcedureNonQuery<bool>(
                        ProcedureName.spCheckServiceUserExists,
                        token,
                        new ProcedureParameter("username", form.Username),
                        new ProcedureParameter("email", form.Email));
                if (serviceExists) return StartRegistrationResult.UserAlreadyExists;
                await procedureExecutor.ExecuteProcedureNoResult(
                    ProcedureName.spCreateNewService,
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

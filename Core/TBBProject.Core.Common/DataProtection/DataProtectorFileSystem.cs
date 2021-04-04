using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;

namespace TBBProject.Core.Common
{
    public class DataProtectorFileSystem: IDataProtection
    {
        private readonly IDataProtector _dataProtector;
        private readonly ILogger _logger;

        public DataProtectorFileSystem(IDataProtectionProvider provider, ILogger<DataProtectorFileSystem> logger)
        {
            this._dataProtector = provider.CreateProtector("TBBProject.Key.v1");
            this._logger = logger;
        }

        public string Protect(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            try
            {
                var protectedInput = this._dataProtector.Protect(input);
                return protectedInput;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, input);
                throw;
            }
        }

        public string Unprotect(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            try
            {
                var unprotectedInput = this._dataProtector.Unprotect(input);
                return unprotectedInput;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, input);
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace EventMonitoring.Clients.WMI
{
    abstract class WMI : IDisposable
    {
        private string _wqlString;
        private string _address;
        private string _domain;
        private string _user;
        private string _password;
        private CimSession _session;
        private WSManSessionOptions _sessionOptions;

        public WMI(string wql)
        {
            _wqlString = wql;
            _session = CimSession.Create("localhost");
        }

        public WMI(string wql, string address,string domain, string user, string password)
        {
            _wqlString = wql;
            _address = address;
            _user = user;
            _password = password;
            _sessionOptions = new WSManSessionOptions();
            _sessionOptions.AddDestinationCredentials(
                new CimCredential(PasswordAuthenticationMechanism.Default, 
                                    _domain, 
                                    _user, 
                                    GetSecureString(_password)));
            _session = CimSession.Create(_address, _sessionOptions);
        }

        private SecureString GetSecureString(string input)
        {
            if (input == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in input)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _session.Close();
                    _sessionOptions.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _wqlString = null;
                _address = null;
                _domain = null;
                _user = null;
                _password = null;
                _session = null;
                _sessionOptions = null;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~WMI()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

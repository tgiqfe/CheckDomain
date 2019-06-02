using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace CheckDomain
{
    class ADDomain
    {
        private readonly static string[] SYSTEM_ACCOUNT =
            new string[] { "System", "Local Service", "Network Service", Environment.MachineName + "$" };

        public string DomainName { get; set; }

        public ADDomain()
        {
            GetDomainName();
        }

        public string GetDomainName()
        {
            ManagementObject mo = new ManagementClass("Win32_ComputerSystem").
                GetInstances().
                OfType<ManagementObject>().
                FirstOrDefault(x => (bool)x["PartOfDomain"]);
            this.DomainName = mo != null ? mo["Domain"] as string : null;
            return DomainName;
        }

        //  実行中のPCがドメイン参加済みかどうか
        public bool IsDomainPC()
        {
            return !string.IsNullOrEmpty(DomainName);
        }

        //  実行中のPCがワークグループかどうか
        //  !IsDomainPCでも良いです。
        public bool IsWorkgroupPC()
        {
            return string.IsNullOrEmpty(DomainName);
        }

        //  実行中のユーザーがシステムアカウント (SYSTEM, Local Service, Network Service) かどうか
        public bool IsSytsemAccount()
        {
            return SYSTEM_ACCOUNT.Any(x => x.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));
        }

        //  実行中のユーザーがドメインユーザーかどうか
        public bool IsDomainUser()
        {
            return !IsSytsemAccount() && IsDomainPC() && (Environment.UserDomainName != Environment.MachineName);
        }

        //  実行中のユーザーがローカルユーザーかどうか
        //  !IsDomainUserでも良いです。
        public bool IsLocalUser()
        {
            return !IsSytsemAccount() && IsWorkgroupPC() && (Environment.UserDomainName == Environment.MachineName);
        }
    }
}

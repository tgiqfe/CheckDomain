using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            ADDomain domain = new ADDomain();

            Console.WriteLine("ドメイン参加：{0}", domain.IsDomainPC().ToString());
            Console.WriteLine("ワークグループ：{0}", domain.IsWorkgroupPC().ToString());
            Console.WriteLine("システムアカウントで実行：{0}", domain.IsSytsemAccount().ToString());
            Console.WriteLine("ドメインユーザーで実行：{0}", domain.IsDomainUser().ToString());
            Console.WriteLine("ローカルユーザーで実行：{0}", domain.IsLocalUser().ToString());


            Console.ReadLine();
        }
    }
}

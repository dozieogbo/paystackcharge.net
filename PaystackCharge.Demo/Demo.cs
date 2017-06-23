using Paystack.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paystack.NET.Demo
{
    class Demo
    {
        static void Main(string[] args)
        {
            // Use default
            PaystackCharge pc = new PaystackCharge();
            Print(pc.AddForKobo(975000));

            // Always 1.5% flat
            pc = new PaystackCharge(additionalCharge: 0);
            Print(pc.AddForKobo(985000));

            // 1.5% with an additional 50ngn if above 2500ngn
            pc = new PaystackCharge(additionalCharge: 5000);
            Print(pc.AddForKobo(980000));

            // 3.9% with a additional 100ngn if above 2500ngn - 10Mngn charge cap (essentially infinite).
            pc = new PaystackCharge(0.039, 10000, 250000, 1000000000);
            Print(pc.AddForKobo(951000));
            
            Console.ReadKey();
        }

        private static void Print(long result)
        {
            Console.WriteLine(result);
        }
    }
}

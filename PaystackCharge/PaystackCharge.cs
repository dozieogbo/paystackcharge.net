using System;

namespace Paystack.NET
{
    public class PaystackCharge
    {
        private double Percentage;
        private long AdditionalCharge;
        private long CrossoverTotal;
        private long Cap;

        private double ChargeDivider
        {
            get
            {
                return 1 - Percentage;
            }
        }

        private long Crossover
        {
            get
            {
                return (long)Math.Ceiling(CrossoverTotal * ChargeDivider - AdditionalCharge);
            }
        }

        private long FlatlinePlusCharge
        {
            get
            {
                return (long)Math.Floor((Cap - AdditionalCharge) / Percentage);
            }
        }

        private long Flatline
        {
            get
            {
                return FlatlinePlusCharge - Cap;
            }
        }

        public PaystackCharge(double percentage = 0.015, long additionalCharge = 10000, long crossoverTotal = 250000, long cap = 20000)
        {
            Percentage = percentage;
            AdditionalCharge = additionalCharge;
            CrossoverTotal = crossoverTotal;
            Cap = cap;
        }

        public long AddForKobo(long amountInKobo)
        {
            if (amountInKobo > Flatline)
                return amountInKobo + Cap;
            else if (amountInKobo > Crossover)
                return (long)Math.Ceiling((amountInKobo + AdditionalCharge) / ChargeDivider);
            else
                return (long)Math.Ceiling(amountInKobo / ChargeDivider);
        }

        public long AddForNgn(double amountInNgn)
        {
            return AddForKobo((long)Math.Ceiling(amountInNgn * 100)) / 100;
        }
    } 
}
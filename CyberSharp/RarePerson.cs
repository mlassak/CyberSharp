using CyberSharp.Helpers;

namespace CyberSharp
{
    public class RarePerson : AbstractPerson
    {
        private const decimal balanceMin = 0.5M;
        private const decimal balanceMax = 1.5M;
        private const int defenceConst = 15;

        public RarePerson() : base()
        {
            base.BtcVallet = new BitcoinVallet(
                RNGesus.GenerateInitBalance(balanceMin, balanceMax),
                Generator.GetRandomBtcAddress(),
                Generator.GetRandomPassword()
                );
        }

        public override int CalculateDefence() => defenceConst;
    }
}

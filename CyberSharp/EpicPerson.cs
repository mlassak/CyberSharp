using CyberSharp.Helpers;

namespace CyberSharp
{
    public class EpicPerson : AbstractPerson
    {
        private const decimal balanceMin = 1M;
        private const decimal balanceMax = 2.5M;
        private static readonly int[] defenceSequence = { 10, 15, 20 };

        public EpicPerson() : base()
        {
            base.BtcVallet = new BitcoinVallet(
                RNGesus.GenerateInitBalance(balanceMin, balanceMax),
                Generator.GetRandomBtcAddress(),
                Generator.GetRandomPassword()
                );
        }

        public override int CalculateDefence() => defenceSequence[base.HackCounter % defenceSequence.Length];
    }
}

using CyberSharp.Helpers;

namespace CyberSharp
{
    public class Player
    {
        private const decimal initBalance = 0.05M;
        private const int initCriminalityLevel = 0;
        private const int initHackingSkill = 26;
        private const decimal learningPrice = 0.005M;
        private const decimal findTargetPrice = 0.01M;
        private const decimal bribePrice = 0.05M;

        public string Name { get; set; }
        public int HackingSkill { get; set; } = initHackingSkill;              
        public int CriminalityLevel { get; set; } = initCriminalityLevel;           
        public BitcoinVallet BtcVallet { get; } = new BitcoinVallet(initBalance, Generator.GetRandomBtcAddress(), Generator.GetRandomPassword());

        public Player(string name) => Name = name;


        public bool IncreaseHack()
        {
            if (BtcVallet.Balance >= learningPrice)
            {
                BtcVallet.Withdraw(learningPrice);
                HackingSkill += 1;
                return true;
            }
            return false;
        }


        /**
         * 
         *return:
         *  0 - criminality level was already at 0, no change
         *  1 - criminality level decreased successfully
         * -1 - unsufficient funds for a criminility level decrease, decrease failed
         */
        public int DecreaseCriminalityLevel()
        {
            if (CriminalityLevel == 0)             
            {
                return 0;
            }
            if (BtcVallet.Balance >= bribePrice)
            {
                BtcVallet.Withdraw(bribePrice);
                CriminalityLevel -= 1;
                return 1;
            }
            return -1;
        }


        public bool FindTarget()
        {
            if (BtcVallet.Balance >= findTargetPrice)
            {
                BtcVallet.Withdraw(findTargetPrice);
                return true;
            }
            return false;
        }


        public string StatsToString() => $"Hacking skill: {HackingSkill}, Criminality level: {CriminalityLevel}, Balance: {BtcVallet.Balance} BTC";
       
    }
}

using System;

namespace CyberSharp
{

	public class BitcoinVallet
	{
		public decimal Balance { get; set; }
        public string Adress { get; init; }
        public string Password { get; set; }


        public BitcoinVallet(decimal initialAmount, string adress, string password)
        {
            if (initialAmount < 0)
            {
                throw new ArgumentException("Cannot deposit a negative amount of BTC");
            }

            Balance = initialAmount;
            Adress = adress;
            Password = password;
        }


        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot deposit a negative amount of BTC");
            }

            Balance = Balance + amount;
        }


		public void Withdraw(decimal amount)
        {
            if (amount < 0)            
            {
                throw new ArgumentException("Cannot withdraw a negative amount of BTC");
            }
			if (Balance - amount < 0)
            {
                throw new ArgumentException("Cannot withdraw more than the vallet contains");
            }

            Balance -= amount;
        }
	}

}

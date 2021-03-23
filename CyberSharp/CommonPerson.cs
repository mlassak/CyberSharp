using CyberSharp;
using CyberSharp.Helpers;

public class CommonPerson : AbstractPerson
{
	private const decimal balanceMin = 0M;
	private const decimal balanceMax = 0.5M;
	private static readonly int[] defenceSequence = { 0, 10 };

	public CommonPerson() : base()
	{
		if (RNGesus.RNG.NextDouble() > 0.25)       
		{
			base.BtcVallet = new BitcoinVallet(
				RNGesus.GenerateInitBalance(balanceMin, balanceMax),
				Generator.GetRandomBtcAddress(),
				Generator.GetRandomPassword()
				);
        } 
	}

	public override int CalculateDefence() => defenceSequence[base.HackCounter % defenceSequence.Length]; 
}

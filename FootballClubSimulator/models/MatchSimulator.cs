namespace FootballClubSimulator.models;

public class MatchSimulator
{
    private readonly int _opportunitiesForGoal = 6;
    private readonly int _diceSize = 10;
    private readonly int _defendingDiceHandicap = 2;

    public MatchOutcome SimulateOutcome(Club firstClub, Club secondClub)
    {
        int firstClubScore = 0;
        int secondClubScore = 0;
        int opportunityCounter = 0;
        while (opportunityCounter < _opportunitiesForGoal)
        {
            firstClubScore += SimulateGoalOpportunity(firstClub, secondClub);
            secondClubScore += SimulateGoalOpportunity(secondClub, firstClub);
            opportunityCounter++;
        }
        
        
        return new MatchOutcome(firstClub, firstClubScore, secondClub, secondClubScore);
    }

    private int SimulateGoalOpportunity(Club attackingClub, Club defendingClub)
    {
        List<int> attackingClubDiceRolls = SimulateAttackRoll(attackingClub);
        List<int> defendingClubDiceRolls = SimulateDefendingRoll(defendingClub);

        int highestAttackRoll = FindHighestRoll(attackingClubDiceRolls);
        int highestDefendingRoll = FindHighestRoll(defendingClubDiceRolls);

        if (highestAttackRoll > highestDefendingRoll)
        {
            return 1;
        }

        return 0;
    }

    private int FindHighestRoll(List<int> diceRolls)
    {
        int highestRoll = 1;
        foreach (var diceRoll in diceRolls)
        {
            if (diceRoll > highestRoll) { highestRoll = diceRoll; }
        }

        return highestRoll;
    }

    private List<int> SimulateAttackRoll(Club attackingClub)
    {
        List<int> attackDiceRolls = new List<int>();
        int amountOfOffenseDice = attackingClub.Offense;
        for (int i = 0; i < amountOfOffenseDice; i++)
        {
            int diceRollValue = RollDice();
            attackDiceRolls.Add(diceRollValue);
        }

        return attackDiceRolls;
    }

    private List<int> SimulateDefendingRoll(Club defendingClub)
    {
        List<int> defendingDiceRolls = new List<int>();
        int amountOfDefenseDice = defendingClub.Defense + _defendingDiceHandicap; 
        for (int i = 0; i < amountOfDefenseDice; i++)
        {
            int diceRollValue = RollDice();
            defendingDiceRolls.Add(diceRollValue);
        }

        return defendingDiceRolls;
    }

    private int RollDice()
    {
        Random random = new Random();
        return random.Next(_diceSize) + 1;
    }
}

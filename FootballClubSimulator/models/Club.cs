namespace FootballClubSimulator.models;

public class Club
{
    public int Defense { set; get; }
    public int Offense { set; get; }
    public string ClubName { private set; get; }


    public Club(string clubName)
    {
        ClubName = clubName;
        Defense = RollRandomStat();
        Offense = RollRandomStat();

    }
    public Club(string clubName, int defense, int offense)
    {
        ClubName = clubName;
        Defense = LimitStat(defense);
        Offense = LimitStat(offense);
    }


    private int LimitStat(int stat)
    {
        if (stat > 10)
        {
            return 10;
        }
        else if (stat < 1)
        {
            return 1;
        }
        else
        {
            return stat;
        }
    }

    private int RollRandomStat()
    {
        int maxStat = 10;
        Random random = new Random();
        int randomStat = random.Next(maxStat) + 1;
        return randomStat;
    }
}
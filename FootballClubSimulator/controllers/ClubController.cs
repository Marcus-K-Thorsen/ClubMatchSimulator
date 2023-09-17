using FootballClubSimulator.models;

namespace FootballClubSimulator.controllers;

public class ClubController
{
    /*
    public Club GetClub(string name)
    {
        return new Club(name, new League("error league"));
    }

    public List<Club> GetClubs()
    {
        List<Club> allClubs = new List<Club>() { GetClub("Some Club") };
        return allClubs;
    }

    public Club AddClub(Club newClub)
    {
        List<Club> allClubs = GetClubs();
        bool newClubDoesNotExist = !allClubs.Exists(club => club.ClubName == newClub.ClubName );
        if (newClubDoesNotExist)
        {
            allClubs.Add(newClub);
        }
        return newClub;
    }

    public Club UpdateClub(Club changedClub)
    {
        List<Club> allClubs = GetClubs();
        bool changedClubExists = allClubs.Exists(club => club.ClubName == changedClub.ClubName );
        if (changedClubExists)
        {
            int indexOfClub = allClubs.FindIndex(club => club.ClubName == changedClub.ClubName);
            allClubs[indexOfClub] = changedClub;
        }
        return changedClub;
    }
    */
}
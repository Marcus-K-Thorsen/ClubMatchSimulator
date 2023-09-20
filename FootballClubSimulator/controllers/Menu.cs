using FootballClubSimulator.models;
using FootballClubSimulator.repositories;
using FootballClubSimulator.util;

namespace FootballClubSimulator.controllers;

public class Menu
{
    private readonly StandardRepository<League> _leagueRepo = new LeagueRepo();
    
    
    public void StartMenu()
    {
        Console.WriteLine("Hello welcome to the Football Standings App.");
        Console.WriteLine("Choose a League to see more details, by typing its name or number, or just type '0' or 'exit.");
        Console.WriteLine();
        
        List<League> allLeagues = _leagueRepo.ReadAll();
        int amountOfLeagues = allLeagues.Count;
        if (amountOfLeagues == 0)
        {
            List<League> initLeagues = GetInitData();
            _leagueRepo.WriteAll(initLeagues);
            allLeagues = initLeagues;
            amountOfLeagues = initLeagues.Count;
        }
        
        
        
        bool userWantsToContinue = true;
        
        while (userWantsToContinue)
        {
            int counter = 0;
            allLeagues.ForEach(league => Console.WriteLine($"{++counter}. League: {league.LeagueName}"));
            bool incorrectInput = false;
            Console.WriteLine("Please type the number or name for the league you want or '0' / 'exit' to end the program."); 
            string? input = Console.ReadLine()?.ToLower();
            if (input is not ("0" or "exit"))
            {
                for (int i = 0; i < amountOfLeagues; i++) 
                { 
                    League league = allLeagues[i]; 
                    if (input == league.LeagueName.ToLower() || input == $"{i+1}")
                    {
                        Console.WriteLine($"Loading in League Menu for: '{league.LeagueName}'...");
                        LeagueMenu(league);
                        incorrectInput = false;
                        break;
                    }
                    incorrectInput = true;
                }
            }
            else
            {
                userWantsToContinue = false;
                incorrectInput = false;
            }
            if (incorrectInput)
            {
                Console.WriteLine();
                Console.WriteLine($"Input: '{input}' is not a valid input, try again...");
                Console.WriteLine();
            }
        }
    }

    private void LeagueMenu(League thePickedLeague)
    {
        // Show all the teams within the picked league
        Console.WriteLine($"Here are the Teams currently participating in this League: '{thePickedLeague.LeagueName}'.");
        thePickedLeague.Teams.ForEach(team => Console.WriteLine($"Name: {team.ClubName} ({team.ClubNameAbbreviated}) - Defense: '{team.Defense}' - Offense: '{team.Offense}'"));
        bool continuePickingLeagueChoice = true;
        
        // Pick a choice between:
        do
        {
            List<Club> leagueClubsSortedOutcome;
            Console.WriteLine();
            Console.WriteLine("Type '0' to go back and pick a new league.");
            Console.WriteLine("Type '1' to see the outcomes of the first rounds of all of the league's Teams.");
            Console.WriteLine("Type '2' to see the outcomes of the second rounds for the league's Upper tier.");
            Console.WriteLine("Type '3' to see the outcomes of the second rounds for the league's Lower tier.");
            Console.WriteLine();
            
            Console.WriteLine("Enter you choice:");
            string? choiceInput = Console.ReadLine();
            switch (choiceInput)
            {
                case "0":
                    // Type: "0": Break out of choice loop, as in go back and pick a new league or exit the program
                    Console.WriteLine($"Exiting the League Menu for: '{thePickedLeague.LeagueName}'.");
                    continuePickingLeagueChoice = false;
                    break;
                case "1":
                    // Type: "1": See outcome of the first rounds (22), prints out all the teams after first rounds
                    Console.WriteLine("Calculating the outcomes for the First rounds of all the teams...");
                    leagueClubsSortedOutcome = thePickedLeague.CalculateFirstRoundsAllTeams();
                    PrintClubsInConsole(leagueClubsSortedOutcome);
                    break;
                case "2":
                    // Type: "2": See outcome of the seconds rounds (5) upperbrackets, prints out the upper teams after alle rounds
                    Console.WriteLine("Calculating the outcomes for the Second rounds of the Upper Teams...");
                    leagueClubsSortedOutcome = thePickedLeague.CalculateSecondRoundsUpperTeams();
                    PrintClubsInConsole(leagueClubsSortedOutcome);
                    break;
                case "3":
                    // Type "3": See outcome of the seconds rounds (5) lowerbrackets, prints out the lower teams after alle rounds
                    Console.WriteLine("Calculating the outcomes for the Second rounds of the Lower Teams...");
                    leagueClubsSortedOutcome = thePickedLeague.CalculateSecondRoundsLowerTeams();
                    PrintClubsInConsole(leagueClubsSortedOutcome);
                    break;
                default:
                    Console.WriteLine($"The input: '{choiceInput}' is not valid. Please enter either '0', '1', '2' or '3'.");
                    // Type "?": Tells user that is not a valid input and ask for a new input
                    break;
            }
        } while (continuePickingLeagueChoice);
        
        
        
        
        
        


    }

    private List<League> GetInitData()
    {
        League league1 = new League("Super Ligaen");
        League league2 = new League("Nordic Bet Ligaen");

        Club club1 = new Club("Kjøbenhavns Boldklub", "KB", league1);
        Club club2 = new Club("Boldklubben Frem", "BF", league1);
        Club club3 = new Club("Boldklubben Dana", "BD", league1);
        Club club4 = new Club("Akademisk Boldklub", "AB", league1);
        Club club5 = new Club("Østerbro Boldklub", "ØB", league1);
        Club club6 = new Club("Boldklubben af 1893", "B 93", league1);
        Club club7 = new Club("Boldklubben Urania", "BU", league1);
        Club club8 = new Club("Boldklubben Lydia", "BL", league1);
        Club club9 = new Club("Boldklubben af 1899", "B 99", league1);
        Club club10 = new Club("Kristelig Boldklub", "KRB", league1);
        Club club11 = new Club("Boldklubben Sylvia", "BS", league1);
        Club club12 = new Club("Boldklubben Apollo", "BA", league1);
        
        Club club13 = new Club("Odense Boldklub", "OB", league2);
        Club club14 = new Club("Aalborg Boldfans", "AF", league2);
        Club club15 = new Club("Randers Lover", "RL", league2);
        Club club16 = new Club("Esbjerg Mestre", "EM", league2);
        Club club17 = new Club("Jyllinge Boldklub", "JB", league2);
        Club club18 = new Club("Neastved Boldklub", "NB", league2);
        Club club19 = new Club("Hellerup Rigmen", "HR", league2);
        Club club20 = new Club("Hells Angels", "HA", league2);
        Club club21 = new Club("Barcelona Real", "BR", league2);
        Club club22 = new Club("Islam Boldklub", "IB", league2);
        Club club23 = new Club("Manchester United", "MU", league2);
        Club club24 = new Club("Satanistisk Boldklub", "SB", league2);
        
        league1.CalculateNewTeamStandings();
        league2.CalculateNewTeamStandings();
        
        List<League> leagues = new List<League>()
        {
            league1,
            league2
        };
        return leagues;
    }
    
    private void PrintClubsInConsole(List<Club> clubs) 
    {
    // Helper function to format a column with a specific width
    string FormatColumn(string value, int width)
    {
        return value.PadRight(width);
    }

    // Define the width for each column
    int posWidth = 4;
    int teamWidth = 30;
    int otherColumnWidth = 18;

    // Draw the top border
    Console.WriteLine(new string('-', posWidth) + '+' +
                      new string('-', teamWidth) + '+' +
                      new string('-', otherColumnWidth * 9) + '+');

    // Print headers
    Console.WriteLine($"{FormatColumn("Pos", posWidth)} {FormatColumn("Team", teamWidth)} {FormatColumn("Matches", otherColumnWidth)} {FormatColumn("Wins", otherColumnWidth)} {FormatColumn("Draws", otherColumnWidth)} {FormatColumn("Losses", otherColumnWidth)} {FormatColumn("Goals For", otherColumnWidth)} {FormatColumn("Goals Against", otherColumnWidth)} {FormatColumn("Goals Difference", otherColumnWidth)} {FormatColumn("Points", otherColumnWidth)} {FormatColumn("Streak", otherColumnWidth)}");
    
    for (int i = 0; i < clubs.Count; i++)
    {
        Club club = clubs[i];
        string line = $"{FormatColumn(club.PositionInTable.ToString(), posWidth)} {FormatColumn($"{club.ClubName} ({club.ClubNameAbbreviated})", teamWidth)} {FormatColumn(club.MatchesPlayed.ToString(), otherColumnWidth)} {FormatColumn(club.GamesWon.ToString(), otherColumnWidth)} {FormatColumn(club.GamesDrawn.ToString(), otherColumnWidth)} {FormatColumn(club.GamesLost.ToString(), otherColumnWidth)} {FormatColumn(club.GoalsFor.ToString(), otherColumnWidth)} {FormatColumn(club.GoalsAgainst.ToString(), otherColumnWidth)} {FormatColumn(club.GoalDifference.ToString(), otherColumnWidth)} {FormatColumn(club.Points.ToString(), otherColumnWidth)} {FormatColumn(club.GetStreak(), otherColumnWidth)}";

        // Set the color based on club index
        if (i < 2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (i >= clubs.Count - 2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        
        Console.WriteLine(line);
        
        // Reset the color
        Console.ResetColor();
    }

    // Draw the bottom border
    Console.WriteLine(new string('-', posWidth) + '+' +
                      new string('-', teamWidth) + '+' +
                      new string('-', otherColumnWidth * 9) + '+');
}
}
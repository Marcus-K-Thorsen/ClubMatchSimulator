// See https://aka.ms/new-console-template for more information

using FootballClubSimulator.models;

MatchSimulator match = new MatchSimulator();

Club clubA = new Club("AF Gutterne");
Club clubB = new Club("BFF Trunserne");

Console.WriteLine($"{clubA.ClubName} Offense: '{clubA.Offense}' Defence: '{clubA.Defense}'");
Console.WriteLine("----------------------------------VS----------------------------------");
Console.WriteLine($"{clubB.ClubName} Offense: '{clubB.Offense}' Defence: '{clubB.Defense}'");
Console.WriteLine();
MatchOutcome outcome1 = match.SimulateOutcome(clubA, clubB);
Console.WriteLine($"Away From Home: {outcome1.FirstClub.ClubName} Score: '{outcome1.FirstClubScore}'");
Console.WriteLine($"At Home Turf: {outcome1.SecondClub.ClubName} Score: '{outcome1.SecondClubScore}'");

MatchOutcome outcome2 = match.SimulateOutcome(clubB, clubA);
Console.WriteLine($"Away From Home: {outcome2.FirstClub.ClubName} Score: '{outcome2.FirstClubScore}'");
Console.WriteLine($"At Home Turf: {outcome2.SecondClub.ClubName} Score: '{outcome2.SecondClubScore}'");
# How we worked:
We exclusively used pair-programming, working from one computer to simplify Git usage. We alternated between coding, and observing.
Halfway into the project, we lost two team-members (Kasper & Mathias).
We would like to preface that we have NO idea how football or leagues work in real life, which is why we might have made some funny choices regarding match-ups haha.

# How to clone application:
Clone the repository from the MASTER branch. Somehow we created both a main and master branch.. We didnt know how to fix that.

# NiceToKnow
We use Club and Teams synonymously in this application. In hindsight, we should have picked one and stuck with it.
Also in our Round.csv files, every line should be called a "Match", but we called them Rounds also, which created some pretty confusing code. 
Again, the lines should have been called matches to enhance code readability. 

We have made a class called FileHandler, that takes care of reading and writing the csv files the same way, while a generic abstract class called StandardRepository.
This repository class is inherited by the repository classes that takes care of the instances of a specific class, such as ClubRepo that makes certain that Clubs are read and written
in the right way.

# How the program works:
Start the application.
The application creates a setup file, that includes the names of two Leagues (Super Ligaen & Nordic Bet Ligaen) that is played and tracked in the application.
The application also creates a teams file, that contains 24 football clubs (teams). 12 plays in Super Ligaen and the other 12 in Nordic Bet Ligaen.
Lastly, the application creates 32 rounds of play. Each team in each League plays against every other team, in one home match, and one away match, in every round.
If these files already exist, the program just reads the files instead of writing them.

When the application launches, it shows the two leagues in the terminal. From here you have the choice to either pick one of the leagues by entering 1 or 2 to
showcase the standings of that league, or exit the program by typing 0.
When you pick a league, the names of the teams participating in the league will be showcased in the terminal.
From here you have 4 choices: 
- To go back (0).
- Entering 1 to see the first 22 rounds results. 
- Entering 2 to see the second round upper tier bracket results (top 6 teams plays 5 rounds to decide outcome of upper bracket).
- Entering 3 to see the second round lower tier bracket results (bottom 6 teams plays 5 rounds to decide outcome of lower bracket).


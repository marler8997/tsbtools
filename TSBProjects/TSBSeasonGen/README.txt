TSBSeasonGen README
======================================================================================
Version 2.0

Features Added:
1. Improved GUI.
2. Ability to specify teams by filename (useful for specifying other leagues)
3. Generate content for the SNES version of TSBTool (add a couple extra DBs).
4. Auto Correct defensive sim data (sim PI and sim PR values for a team should add up to 255).

Not many features have been added to version 2.0 but the GUI should make it more intuitive to use.
The simplest way to generate a season is to just specify a year in the schedule text box,
and click "Generate Season". This will generate a season in NFL history.

For the more complex case, you can enter text into the fields next to a team helmet.
You can specify a team 3 different ways.
1. By Year            (Example: '1992').
2. By Year/Team pair  (Example: '1992 Bears')
3. By filename:       (Example: 'C:\NCAA_DATA\2004\BostonCollege.txt')

The 3rd method was added in order to support different football leagues (namely NCAA).

Note: I had intended to release a few years of NCAA data with this release, but I ran into
a few snags and am not including it.

In the previous versions of TSBSeasonGen the defensive sim attributes were messed up. I
didn't realize that for both sim pass defense and sim pass rush needed to add up to 255 
for each team.

Common Errors:
While using TSBTool you may get error messages. If you put '1999' next to the Oilers, you should 
get an error. This is because the Oilers didn't exist in 1999. You should instead insert '1999 Titans'.
Keep in mind that the following franchises have had name changes:
1. Jets   (Titans)
2. Browns (Ravens)
3. Oilers (Titans)
4. Chiefs (Texans)

Spelling errors and specifying a year for which there is no NFL data could also cause
errors to be reported.

======================================================================================
Version 1.2

Defects Fixed:
1. 1999 Marshall Faulk not found (data defect). 
2. Punter attributes in TSBSeasonGen are all the same (all punters are ranked 100 [data defect])
3. NY Titans resolved to the Oilers (in the schedule).
4. Fixed Schedule defects 1960-1979.
5. Fixed OL defect where it chose the 'worst' Linemen instead of the 'better' linemen.
   (It chooses a player (OL) based on seasons player and jersey number)


Features Added:
1. Ability for the user to define the attributes used by TSBSeasonGen.
2. Substitute teams if they exist as a 1991 team, but in the given year they were named 
    something else (i.e. NY titans --> jets, Dallas Texans --> Chiefs).
3. Ability for the user to define certain attributes of players that meet certain criteria 
   (int ability for a guy with a certain amount of ints, max speed for guys that gain over 2000 
    yards ... )
4. Schedule output for the new scheduler in TSBTool (variable games in a week).
5. Modified TSBSeasonGen to specify the offensive preference 
   (Heavy sim rushing/passing, little more rushing/passing).


NEW!!!
-----------How do I change the way TSBSeasonGen gives doles out attributes? -----------------
The major drawback of TSBSeasonGen 1.0* was that the user could not affect the way the 
players came out (attribute-wise).

Now you can.

You have 2 ways of doing this.
1. Change the attributes in the TSB_Data\**Ranking.txt.
2. Modify the SeasonGenOptions.txt file.

About the Ranking files:
The rankings are sorted according ( what I believe) are the 2 most important attributes to the given position.
The data was taken from the original TSB and massaged very slightly.
The players in the nfl data files are all ranked based on how they did in that season (notice the
 'ranking=x' in the data files). If the player ranked number 5 in a season, they will get the attributes
 on the 5th line in the respective TSB_Data\**Ranking.txt file.

That being said, there are exceptions. If a QB was in the super bowl, he is guaranteed to be in the top 5.
The defensive players are also ranked based on how the defense ranked.
	
If you think that the offenses come out 'Juiced', simply go to the Ranking files and either change the attributes
or 'Comment out' some of the top lines (If a line starts with the '#' character it is considered a 'comment',
and it is ignored).

About the SeasonGenOptions file:
In this file you can tell TSBSeasonGen how to set some important attributes.
You have the ability to affect the following:
1. QB PC.
2. Top return man's MS.
3. Defensive line and linebacker INT ability.
4. Defensive back INT ability.
5. WR REC ability.
6. WR MS ability.
7. RB REC ability.
8. RB MS ability.
9. TE REC ability.
10. TE MS ability.
11. Skill player's BC ability.

To specify attributes and requirements for these, you specify lists like:

WR_MS = { 1500,75,  1000,50  }

These lists can contain as many pairs as you like (an unpaired number will be considered an error). The
second number in the pair must be a valid TSB attribute number (if it's not, it's an error).
The first number is a 'requirement' the second number is what the attribute will be if the 'requirement' is met.
In order to meet a 'requirement' the player will need to exceed the requirement. So in the example above, 
a player that gains 1501 receiving yards will get a MS of 75, a player that gains 1500 receiving yards 
will not (he'll get a 50).

If you wish to not use one of the requirement/attribute maps, you can simply 'comment it out' 
with the '#' character (like this ==> #WR_MS = { 1500,75,  1000,50  } ).

My personal preference is to stick to using the 'Ranking' files, and only use the TSBSeasonGenOptions file
for a few things (like return man speed and interception ability). In fact, by default I have most of them 
turned off (commented out).


====================================================================================
Version 1.0.1
Fixed defect where TSBSeasonGen did not work on Linux under Mono because of the Path separator issue


====================================================================================
Version 1.0

Requirements:
Microsoft.NET framework 1.1

You can obtain the .NET framework from Microsoft for free by going to 
http://www.dotnetgerman.com/links/dl_distri.asp

If you are a programmer, and do not have the .NET framework installed, you may want to
consider downloading the .NET SDK. It's also a free download from Microsoft.
http://www.dotnetgerman.com/links/dl_dotnetsdk.asp
You may also want to take a look at Lutz Roeder's .NET Reflector (This program can 
disassemble the .NET byte code and display a close representation of the source code 
of a .NET program [View->Disassembler]).

=============================== About ================================================
This program reads text files in a certain format and generates output to be read by the
TSBTool program (to be applied to a Tecmo Super Bowl ROM [nes only]).
This tool was created because I thought it would be neat to just specify a year and have
the NFL season for that year auto generated so that a user could play through the specified season
in a football video game. Tecmo Super Bowl was the chosen target because it was fairly well known
how to modify the ROM. Originally, I had planned to target NFL2K5 (Xbox) but that would have
taken a long time for me to figure out.

The files used by this program to generate NFL seasons for TSBTool are in the 
"NFL_DATA" directory. These files are human readable plain text files. You can make a player
'Good' in a given season by pumping up their ranking.

All of the Player attributes assigned to players by this program are taken from the
original Tecmo Super Bowl ROM.

The data in the text files was taken from http://jt-sw.com/football/pro/rosters.nsf.

============================== Usage ================================================
NEW! --> TSBSeasonGen can be used from within TSBTool (v1.2) if they are in the same directory.
         TSBTool will have a menu item called 'TSBSeasonGen'. You can specify a year or
         A configuration file from TSBTool.

# Generate the file for the 1985 season.
TSBSeasonGen 1985 > 1985_TSBTool_data.txt
 or 
TSBSeasonGen 1985 | TSBTool -stdin -out:1985.nes input_rom.nes

# Generate a season with the configuration specified in 'configFile.txt'
TSBSeasonGen -config:configFile.txt > special_TSBToolData.txt
 or
TSBSeasonGen -config:configFile.txt | TSBTool -stdin -out:special.nes input_rom.nes

# Print help message
TSBSeasonGen /?

============================== Config File ===========================================
There is a sample config file (ConfigFile.txt) included with this program.

"SCHEDULE=1995"
Specifies that you want to use the 1995 schedule.

In the config file you can specify a certain year for a team.
"2002 49ers"
Specifies that you want the 49ers to have their roster from the 2002 season

"cardinals = 2000 ravens"
Specifies that you want to replace the cardinals with the '2000 ravens'

=========================== Special files =========================================
Important files:
1. TSB_Data\NumberFaceData.txt
2. TSB_Data\AttributeMap.txt
3. SeasonGenOptions.txt
4. TSB_Data\PositionRanking\DBRanking.txt
5. TSB_Data\PositionRanking\DLRanking.txt
6. TSB_Data\PositionRanking\FBRanking.txt
7. TSB_Data\PositionRanking\KickerRanking.txt
8. TSB_Data\PositionRanking\LBRanking.txt
9. TSB_Data\PositionRanking\OLRanking.txt
10. TSB_Data\PositionRanking\PunterRanking.txt
11. TSB_Data\PositionRanking\QBRanking.txt
12. TSB_Data\PositionRanking\RBRanking.txt
13. TSB_Data\PositionRanking\TERanking.txt
14. TSB_Data\PositionRanking\WRRanking.txt

NumberFaceData.txt is a file that you can place 'Face' and jersey number mappings into.
These jersey numbers take precedence over anything in the 'NFL_DATA\<year>\<team name>.txt' files.
The face will be applied to the specified player.
If the player is in the file twice, only the first occurrence will be taken into account.

In the AttributeMap.txt file you can specify attributes for players. The data in this file
takes precedence over anything generated by the program. You can also specify only certain
attributes for a player.
Lets say that John Elway had an off year, and his Attributes were rated quite low.
You know he has a rocket arm, but since the program only assigns attributes off of
stats, Elway ends up with throwing power of 25.
You can fix this a couple different ways.
1.  Specify All of John Elway's attributes.
     QB1, john ELWAY, 25, 69, 38, 13, 75, 56, 69, 50 ,[11, 9, 0 ]

2. Or you can specify only certain attributes and let the program assign the rest.
    QB1, warren MOON, ?, ?, 38, ?, 75, ?, ?, 50 ,[?, ?, ? ]
    
The '?' characters tell the program to use what it calculates for that attribute, the
numbers (in this case) indicate to the program that we want Elway to have 38 Max Speed, 
75 passing speed, and 50 avoid pass block points.

SeasonGenOptions.txt is a file where you can specify how some of the attributes for the players come out
  (Read through this file for information on how to use it).

TSB_Data\PositionRanking\**Ranking.txt
These are the base rankings used to assign the players their attributes.

============================ Defects =============================================
At the time of this writing I don't know of any defects in this program.
This program has defects in it, I just haven't found them yet. If you find them
post your findings in a thread in the forums at www.knobbe.org .


=========================== Data Generated =======================================
The data generated by this program (applied to a ROM via TSBTool.exe) is not 'Top-Quality'.
Many of the players will not have the correct jersey numbers or correct skin color/face.

If you want to make a 'Quality' ROM you need to make sure that most of the players
have the correct skin color and jersey number.
This data is not easy to come by. I looked many times for this data and could not
find this data. The skin color and jersey number data that this program does know
about was mostly taken from various 'Top-Quality' modified ROMS (and the Original 
TSB).



## BattleshipGame
C# | .NET Core | React | Javascript | Entity Framework.
This Battleship game is played by artificial intelligence

## Table of contents
* [General info](#general-info)
* [Opening the project](#Opening-the-project)
* [Gameplay](#gameplay)
* [Technologies](#technologies)
* [Status](#status)

## General info
This game is played by artificial intelligence on boards 10x10.
Ships are randomly placed on each board.

Each player has:
- size 5 ship named "Carrier"
- size 4 ship named "Battleship"
- size 3 ship named "Cruiser"
- size 2 ship named "Submarine"
- size 2 ship named "Patrol boat"
- size 1 ship named "Tactical boat"
- size 1 ship named "Destroyer"

After pressing start button first player chooses a field from the opponent's board to hit.
If a player hits an opponent's ship, the field changes to red and he can try to hit a different field again.
Player will hit next to the field where the last successful hit was, to try to destroy the ship.
If he destroys the ship, all nearby fields will be marked as miss.
If he misses, the field will be marked yellow and it's another player's turn.
The game continues until one of the players destroys all enemy ships.

## Opening the project

User can run GameAPI from your IDE.
To start react application you need to install node.js, then in terminal/command line search for folder battleship-interface and write "npm start".

## Gameplay

This is home screen of my game

![HomeScreen](https://ibb.co/SXXyJj7)

User can create here new game or check the game instructions.

![Game instrunctions](https://ibb.co/K2Fg1b9)

After clicking "Create new game!" button, new modal will apear.
User can name your players here.
![New game modal](https://ibb.co/rfGmVmS)

Form validates if user typed new names. He can't proceed not doing this.
![Validation](https://ibb.co/wcjQhT5)

This is main gameplay view.
Just after pressing the "Start game!" button, the whole game will start and players will be trying to destroy opponents ships.
![Main view](https://ibb.co/JpF87cL)

During game, user can change the speed of the gameplay
![Game speed](https://ibb.co/JF1XpPh)

During the gameplay, last moves are displayed in the middle of the screen.
![Gameplay](https://ibb.co/T866cBC)

When user press "Pause button!", gameplay will stop untill he press the "Start game!" button again.
![Pause game](https://ibb.co/nPBMh1M)

When one player destroys all enemy ships, final modal with winner name will appear.
If user press "Close" button, he will be redirected to home screen, where he will be able to start new game.
![Final modal](https://ibb.co/QY6cwX6)

## Technologies
*C#
*.NET Core
*React
*Javascript
*Entity Framework

## Status
Project is almost finished. I plan to add some static files: player photos, ship images and some sound effects.

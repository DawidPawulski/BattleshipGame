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

This is a home screen of my game

![HomeScreen](https://user-images.githubusercontent.com/28674766/116146514-dddb0100-a6de-11eb-809e-293780c7e016.png)


User can create here a new game or check the game instructions.

![Game instructions](https://user-images.githubusercontent.com/28674766/116146528-e16e8800-a6de-11eb-94b1-8b8fb3cd5055.png)


After clicking "Create new game!" button, new modal will apear.
User can name his players here.

![New game modal](https://user-images.githubusercontent.com/28674766/116146547-e59aa580-a6de-11eb-9542-dceb73a94826.png)


Form validates if user typed new names. He can't proceed without doing this.

![Validation](https://user-images.githubusercontent.com/28674766/116146453-ce5bb800-a6de-11eb-8d06-04e1f1c1f37d.png)

This is the main gameplay view.
Just after pressing the "Start game!" button, the whole game will start and players will be trying to destroy opponents ships.

![Main view](https://user-images.githubusercontent.com/28674766/116146399-c0a63280-a6de-11eb-8681-5cd5dd667ac4.png)

During the game, user can change the speed of a gameplay.

![Game speed](https://user-images.githubusercontent.com/28674766/116146375-b84df780-a6de-11eb-9fcb-5cbb0bd89387.png)


During the gameplay, the lastest moves are displayed in the middle of the screen.

![Gameplay](https://user-images.githubusercontent.com/28674766/116146342-ae2bf900-a6de-11eb-9f81-9cf4f11c40cc.png)

When user presses "Pause button!", gameplay will stop untill he presses the "Start game!" button again.

![Pause game](https://user-images.githubusercontent.com/28674766/116146302-a3716400-a6de-11eb-8925-e3c6d3c5cc42.png)

When one player destroys all enemy ships, the final modal with winner name will appear.
If a user presses "Close" button, he will be redirected to the home screen, where he will be able to start a new game.

![Final modal](https://user-images.githubusercontent.com/28674766/116146239-8f2d6700-a6de-11eb-8912-c34f0ec7d581.png)


## Technologies
*C#
*.NET Core
*React
*Javascript
*Entity Framework

## Status
Project is almost finished. I plan to add some static files: player photos, ship images and some sound effects.

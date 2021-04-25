import React,{Component} from 'react';

export class Instruction extends Component{

    render(){
        return(
            <div>
                <div className="mt-5 d-flex justify-content-center">
                  <h3>Battleship game instruction</h3><br />
                </div>
                <div className="mt-5 d-flex justify-content-center">
                    This game is played by artificial intelligence on boards 10x10.<br />
                    Ships are randomly placed on each board.<br /><br />
                    Each player has:<br />
                    - size 5 ship named "Carrier"<br />
                    - size 4 ship named "Battleship"<br />
                    - size 3 ship named "Cruiser"<br />
                    - size 2 ship named "Submarine"<br />
                    - size 2 ship named "Patrol boat"<br />
                    - size 1 ship named "Tactical boat"<br />
                    - size 1 ship named "Destroyer"<br /><br />

                    After pressing start button first player chooses a field from the opponent's board to hit.<br />
                    If a player hits an opponent's ship, the field changes to red and he can try to hit a different field again.<br />
                    Player will hit next to the field where the last successful hit was, to try to destroy the ship.<br />
                    If he destroys the ship, all nearby fields will be marked as miss.<br />
                    If he misses, the field will be marked yellow and it's another player's turn.<br />
                    The game continues until one of the players destroys all enemy ships.<br />
                </div>
            </div>
        )
    }
}
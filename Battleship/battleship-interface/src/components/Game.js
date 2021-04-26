import React,{Component} from 'react';

export class Game extends Component{

    constructor(props){
        super(props);
        this.state={
            firstPlayer: {},
            secondPlayer: {},
        };
    }

    async handleGetPlayers() {
        const pathToGetFirstPlayer = 'player/'+localStorage.getItem('firstPlayerId');
        const pathToGetSecondPlayer = 'player/'+localStorage.getItem('secondPlayerId');

        let firstPlayerResponse = await get(pathToGetFirstPlayer);
        let secondPlayerResponse = await get(pathToGetSecondPlayer);
        this.setState({firstPlayer: firstPlayerResponse});
        this.setState({secondPlayer: secondPlayerResponse});
    }

    render(){
    }
}
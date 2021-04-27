import React,{Component} from 'react';

export class Game extends Component{

    constructor(props){
        super(props);
        this.state={
            firstPlayerBoard: [],
            secondPlayerBoard: [],
            firstPlayer: {},
            secondPlayer: {},
            table: [],
            boardRows: [],
            boardColumns: [],
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

    async createBoards(){
        const defaultBoardSize = 100;
        const requestBodyToCreateFirstBoard = {size: defaultBoardSize, PlayerId: this.state.firstPlayer.Id};
        const requestBodyToCreateSecondBoard = {size: defaultBoardSize, PlayerId: this.state.secondPlayer.Id};

        let firstPlayerBoardResponse = await create('board', JSON.stringify(requestBodyToCreateFirstBoard));
        let secondPlayerBoardResponse = await create('board', JSON.stringify(requestBodyToCreateSecondBoard));
        this.setState({firstPlayerBoard: firstPlayerBoardResponse});
        this.setState({secondPlayerBoard: secondPlayerBoardResponse});
    }

    populateBoardHeaders(){
        this.setState({
            boardRows: [
                {class: RowNames[RowNames.ARow], name: "A"},
                {class: RowNames[RowNames.BRow], name: "B"},
                {class: RowNames[RowNames.CRow], name: "C"},
                {class: RowNames[RowNames.DRow], name: "D"},
                {class: RowNames[RowNames.ERow], name: "E"},
                {class: RowNames[RowNames.FRow], name: "F"},
                {class: RowNames[RowNames.GRow], name: "G"},
                {class: RowNames[RowNames.HRow], name: "H"},
                {class: RowNames[RowNames.IRow], name: "I"},
                {class: RowNames[RowNames.JRow], name: "J"}
            ],
            boardColumns: Array(10).fill().map((element, index) => index + 1)
        });
    }

    render(){
    }
}
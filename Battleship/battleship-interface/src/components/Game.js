import React,{Component} from 'react';
import {Row,Col,Container} from 'react-bootstrap';
import {RowNames} from '../enums/RowNamesEnum';
import {MoveMessages} from '../enums/MoveMessages';
import {Button,ButtonToolbar} from 'react-bootstrap';
import {EndGameModal} from './modals/EndGameModal';
import {get,create,update} from '../helpers/apiHelpers';
import {messageToDisplay} from '../helpers/warLogMessageHelper';
import {Board} from './Board';

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
            opponentPlayerId: Number,
            currentBoard: [],
            gameSpeed: 1000,
            intervalId: Number,
            playGame: false,
            endGameModalShow: false,
            warLog: []
        };
    }

    async componentDidMount(){
        await this.prepareGame();
    }

    async prepareGame(){
        this.populateBoardHeaders();
        await this.handleGetPlayers();
        await this.createBoards();
        await this.placeShips();
        this.setDefaultValuesForNewGame();
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

    async placeShips(){
        const pathToPlaceFirstPlayerShips = 'ship/' + this.state.firstPlayerBoard.Id + '/place-ships';
        const pathToPlaceSecondPlayerShips = 'ship/' + this.state.secondPlayerBoard.Id + '/place-ships';

        let firstPlayerShips = await update(pathToPlaceFirstPlayerShips, JSON.stringify({Player:this.state.firstPlayer}));
        let secondPlayerShips = await update(pathToPlaceSecondPlayerShips, JSON.stringify({Player:this.state.secondPlayer}));
        this.setState({firstPlayerBoard: firstPlayerShips});
        this.setState({secondPlayerBoard: secondPlayerShips});
    }

    setDefaultValuesForNewGame(){
        const defaultGameSpeed = 1000;

        this.setState({opponentPlayerId: this.state.secondPlayer.Id});
        localStorage.setItem('speed', defaultGameSpeed);
    }

    async getPlayerMove(){
        let response = await update('move/'+this.state.opponentPlayerId)
        this.setState({currentBoard: response});
    }

    async loop(){
        let firstPlayerId = this.state.firstPlayer.Id;
        let secondPlayerId = this.state.secondPlayer.Id;

        let id = setInterval(async () => {
                    await this.getPlayerMove();
                    await this.displayMessageInWarLog();
                    await this.assignCurrentBoardToCorrectPlayer();
                    if(this.state.currentBoard.Message === MoveMessages.Miss){
                        this.state.opponentPlayerId === secondPlayerId ? this.setState({opponentPlayerId: firstPlayerId}) 
                                    : this.setState({opponentPlayerId: secondPlayerId});
                    }
                    this.setState({gameSpeed: localStorage.getItem('speed')});
                }, this.state.gameSpeed);
        this.setState({intervalId: id})
    }

    

    displayMessageInWarLog(){
        const maximumLengthOfWarLog = 10;
        let playerName = this.state.opponentPlayerId === this.state.firstPlayer.Id ? this.state.secondPlayer.Name : this.state.firstPlayer.Name;
        let opponentName = this.state.opponentPlayerId === this.state.firstPlayer.Id ? this.state.firstPlayer.Name : this.state.secondPlayer.Name;
        let boardMessage = MoveMessages[this.state.currentBoard.Message];
        let warLogMessage = messageToDisplay(boardMessage, playerName, opponentName, this);
        
        if (this.state.warLog.length > maximumLengthOfWarLog){
            this.state.warLog.splice(0, this.state.warLog.length-maximumLengthOfWarLog);
        }
        this.setState({warLog: [...this.state.warLog, warLogMessage]});
    }

    assignCurrentBoardToCorrectPlayer(){
        if (this.state.opponentPlayerId === this.state.firstPlayer.Id)
        {
            this.setState({firstPlayerBoard: this.state.currentBoard})
        }
        else if (this.state.opponentPlayerId === this.state.secondPlayer.Id)
        {
            this.setState({secondPlayerBoard: this.state.currentBoard})
        }
    }

    async componentDidUpdate(prevProps, prevState){
        if(this.state.endGameModalShow)
        {
            clearInterval(this.state.intervalId);
        }

        if(this.state.playGame && this.state.gameSpeed !== prevState.gameSpeed)
        {
            clearInterval(this.state.intervalId);
            this.loop();
        }
    }

    render(){
        let nameOfTheWinner = this.state.opponentPlayerId === this.state.firstPlayer.Id 
                            ? this.state.secondPlayer.Name : this.state.firstPlayer.Name;

        return (
            <Container className="mt-5">
                <Row>
                    <Col className="text-center"><h2>Battleship Game</h2></Col>
                </Row>
                <Row className="mt-5 text-center">
                    <Col className="mt-2">
                        <Board label="First player name" spanId="firstPlayerName" playerName={this.state.firstPlayer.Name}
                            boardDivId="firstPlayerBoard" boardColumns={this.state.boardColumns} boardRows={this.state.boardRows}
                            playerFields={this.state.firstPlayerBoard.Fields}/>
                    </Col>
                    <Col>
                        <div>
                        <ButtonToolbar className="mt-5 d-flex justify-content-center">
                            {!this.state.playGame ? 
                                (<Button variant='success' onClick={()=>{
                                    this.setState({playGame:true});
                                    this.loop();
                                }}>
                                    Start game!
                                </Button>)
                                :
                                    (<Button variant='danger' onClick={()=>{
                                        this.setState({playGame: false}); 
                                        clearInterval(this.state.intervalId);
                                        }}>
                                    Pause game!
                                </Button>)
                            }
                        </ButtonToolbar>
                        </div>
                        <div className="war-log mt-3">
                            {this.state.warLog.map((log, index) => {
                                return (<div className="mt-2"><b>{log}</b><br/></div>)
                            })}
                        </div>
                    </Col>
                    <Col className="mt-2">
                        <Board label="Second player name" spanId="secondPlayerName" playerName={this.state.secondPlayer.Name}
                                boardDivId="secondPlayerBoard" boardColumns={this.state.boardColumns} boardRows={this.state.boardRows}
                                playerFields={this.state.secondPlayerBoard.Fields}/>
                    </Col>
                </Row>
                <EndGameModal show={this.state.endGameModalShow}
                    playername={nameOfTheWinner}></EndGameModal>
            </Container>
      )
    }
}
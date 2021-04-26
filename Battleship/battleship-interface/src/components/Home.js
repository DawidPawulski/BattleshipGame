import React,{Component} from 'react';
import {Button,ButtonToolbar} from 'react-bootstrap';
import {StartGameModal} from './modals/StartGameModal';

export class Home extends Component{
    constructor(props){
        super(props);
        this.state={startGameModalShow:false};
    }


    render(){
        let startGameModalClose=()=>this.setState({startGameModalShow:false});

        return(
            <div>
                <div className="mt-5 d-flex justify-content-center">
                  <h2>Battleship Game</h2><br />
                </div>
                <div className="mt-5 d-flex justify-content-center">
                    This game is played by artificial intelligence
                </div>
                <ButtonToolbar className="mt-5 d-flex justify-content-center">
                    <Button variant='success'
                    onClick={()=>this.setState({startGameModalShow:true})}>
                        Create new game!
                    </Button>

                    <StartGameModal show={this.state.startGameModalShow}
                        onHide={startGameModalClose} />
                </ButtonToolbar>
            </div>
        )
    }
}
import React,{Component} from 'react';
import {Modal,Button,Row,Col} from 'react-bootstrap';
import {Redirect} from 'react-router'

export class EndGameModal extends Component{
    constructor(props){
        super(props);
        this.state={
            redirect: null
        }
    }

    redirectToHomePage = () => { 
        this.setState({ redirect: "/" });
      }

    render(){
        const {playername} = this.props;
        
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />
        }

        return(
            <div className="container">
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Body>
                        <Row>
                            <Col>
                                <h1 className="m-2 text-center">{playername} wins!</h1>
                            </Col>
                        </Row>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button id="endGameButton" variant="danger" onClick={this.redirectToHomePage}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        )
    }
}
import React,{Component} from 'react';
import {Modal,Button,Row,Col,Form} from 'react-bootstrap';
import {Redirect} from 'react-router';
import {create} from '../../helpers/apiHelpers';

export class StartGameModal extends Component{
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
        this.state={
            redirect: null,
            firstPlayerName: '',
            secondPlayerName: '',
        }
    }

    // Create players after submitting the form
    async handleSubmit(event){
        event.preventDefault();
        await this.handleCreatePlayers();
        this.setState({ redirect: "/game" });
    }

    async handleCreatePlayers(){
        let firstPlayer = await create('player', JSON.stringify({name: this.state.firstPlayerName}));
        let secondPlayer = await create('player', JSON.stringify({name: this.state.secondPlayerName}));
        localStorage.setItem('firstPlayerId', firstPlayer.Id);
        localStorage.setItem('secondPlayerId', secondPlayer.Id);
    }

    render(){
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />
        }

        return(
            <div className="container">
                <Modal {...this.props} className="start-modal" size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Header>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Start new game
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="FirstPlayerName">
                                        <Form.Label>First player name: </Form.Label>
                                        <Form.Control type="text" name="FirstPlayerName" required
                                        onChange = {(event) => this.setState({firstPlayerName: event.target.value })}
                                        placeholder="First player name" />
                                    </Form.Group>

                                    <Form.Group controlId="SecondPlayerName">
                                        <Form.Label>Second player name: </Form.Label>
                                        <Form.Control type="text" name="SecondPlayerName" required
                                        onChange = {(event) => this.setState({secondPlayerName: event.target.value })}
                                        placeholder="Second player name"/>
                                    </Form.Group>

                                    <Form.Group>
                                        <Button varian="primary" type="submit">
                                            Create players
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        )
    }
}
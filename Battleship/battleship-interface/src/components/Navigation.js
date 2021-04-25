import React,{Component} from 'react';
import {NavLink} from 'react-router-dom';
import {Navbar,Nav,NavDropdown} from 'react-bootstrap';

export class Navigation extends Component{

    render(){
        return(
            <Navbar bg="dark" expand="lg">
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-nabar-nav">
                    <Nav>
                        <NavLink className="d-inline p-2 bg-dark text-white" to="/">
                            Home
                        </NavLink>
                        <NavLink className="d-inline p-2 bg-dark text-white" to="/instruction">
                            Instruction
                        </NavLink>
                        <NavDropdown title={
                            <span className="d-inline p-2 bg-dark text-white">Game speed</span>} 
                                onSelect={(evt) => localStorage.setItem('speed', evt)} id="nav-dropdown">
                            <NavDropdown.Item eventKey="1000">Normal speed</NavDropdown.Item>
                            <NavDropdown.Item eventKey="500">Double speed</NavDropdown.Item>
                            <NavDropdown.Item eventKey="300">Tripple speed</NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        )
    }
}
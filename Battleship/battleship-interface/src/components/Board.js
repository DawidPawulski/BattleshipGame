import React,{Component} from 'react';
import {RowNames} from '../enums/RowNamesEnum';
import {Table} from 'react-bootstrap';

export class Board extends Component{
    constructor(props){
        super(props);
    }

    render(){

        return(
            <div>
                <div>
                    {this.props.label}<br />
                    <b><span id={this.props.spanId}>{this.props.playerName}</span></b>
                </div>
                <div id={this.props.boardDivId}>
                    <Table className="mt-4" striped bordered hover size="sm">
                        <thead>
                            <tr>
                                <th> </th>
                                {this.props.boardColumns && this.props.boardColumns.map((element, index) => {
                                    return (
                                        <th key={index}>{element}</th>
                                    )
                                })}
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.boardRows && this.props.boardRows.map((item, index) => {
                                let boardFields = this.props.playerFields;
                                if(boardFields){
                                    boardFields = boardFields.sort((a, b) => a.OrderNumber - b.OrderNumber);
                                }
                                                    
                                return (
                                    <tr key={index} className={item.class}>
                                        <td key={index}>
                                            <b>{item.name}</b>
                                        </td>
                                        {boardFields && boardFields.map((field, index) => {
                                            const shipIdNotExists = 0;
                                            let fieldRowName = RowNames[field.RowName];

                                            var fieldClasses = `${field.IsHit ? 'hit': 'no-hit'} 
                                                                ${field.ShipId > shipIdNotExists ? 'ship' : 'field-empty'}`;

                                            if(fieldRowName === item.class){
                                                return (<td key={index} className={fieldClasses}></td>);
                                            }
                                        })}
                                    </tr>
                                )
                            })}
                        </tbody>
                    </Table>
                </div>
            </div>
        )
    }
}
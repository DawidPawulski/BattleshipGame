export const messageToDisplay = (boardMessage, playerName, opponentName, that) => {
    let warLogMessage = '';

    switch(boardMessage){
        case 'Miss':
            warLogMessage = `${playerName} misses the shot`;
            break;
        case 'Hit':
            warLogMessage = `${playerName} hits ${opponentName}'s ship`;
            break;
        case 'BattleshipDestroyed':
            warLogMessage = `${playerName} destroys battleship!`;
            break;
        case 'CarrierDestroyed':
            warLogMessage = `${playerName} destroys carrier!`;;
            break;
        case 'CruiserDestroyed':
            warLogMessage = `${playerName} destroys cruiser!`;;
            break;
        case 'DestroyerDestroyed':
            warLogMessage = `${playerName} destroys destroyer!`;;
            break;
        case 'SubmarineDestroyed':
            warLogMessage = `${playerName} destroys submarine!`;;
            break;
        case 'PatrolBoatDestroyed':
            warLogMessage = `${playerName} destroys patrol boat!`;;
            break;
        case 'TacticalBoatDestroyed':
            warLogMessage = `${playerName} destroys tactical boat!`;;
            break;
        case 'Win':
            warLogMessage = `${playerName} wins the game!!!`;
            that.setState({endGameModalShow: true});
            break;
        default:
            warLogMessage = 'Waiting for some action!';
    }

    return warLogMessage;
}
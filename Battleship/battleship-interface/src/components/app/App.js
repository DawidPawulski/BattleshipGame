import './App.css';
import {Home} from '../Home';
import {Game} from '../Game';
import {Navigation} from '../Navigation';

import {BrowserRouter, Route, Switch} from 'react-router-dom';
import { Instruction } from '../Instruction';

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <Navigation/>

        <Switch>
          <Route path='/' component={Home} exact />
          <Route path='/instruction' component={Instruction} />
          <Route path='/game' component={Game} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;

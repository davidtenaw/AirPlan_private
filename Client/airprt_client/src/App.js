import logo from './logo.svg';
import './App.css';
import AirportImage from './Images/Airport.jpeg';

import Station from './Compononts/Station';

function App() {
  var divstyle={backgroundImage: `url(${AirportImage})`,backgroundSize:"cover", fontSize: 30, color:'white', padding: 10, margin: 10,}

  return (
    
    <div  style={divstyle}  className="App">
      <table >
        <tbody>
        <tr>
          <td><Station name="1"></Station></td>
          <td></td>
          <td><Station name="2"></Station></td>
          <td></td>
          <td><Station name="3"></Station></td>
          <td></td>
          <td><Station name="4"></Station></td>
          <td></td>
          <td><Station name="9"></Station></td>
        </tr>
        <tr>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
        </tr>
        <tr>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td><Station name="8"></Station></td>
          <td></td>
          <td><Station name="5"></Station></td>
          <td></td>
        </tr>
        <tr>
          <td></td>
          <td></td>
          <td></td>
          <td colSpan={3}></td>
          <td><Station name="6"></Station></td>
          <td><Station name="7"></Station></td>
          <td></td>
          <td></td>
        </tr>
        </tbody>
      </table>
      
      
      

    </div>
  );
}

export default App;

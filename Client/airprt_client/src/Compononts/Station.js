import React, { useEffect } from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useState} from 'react';
import image from '../Images/image.jpg';
import Plane from './Plane';

function Station({name}) {
var divstyle={width:200,height:200,backgroundImage: `url(${image})`,backgroundRepeat:"repeat",backgroundSize:"cover"}
var divstyleWithouPicther={width:200,height:200}

    const connection = new HubConnectionBuilder().withUrl("https://localhost:7072/airport").build();
   // connection.start().then(()=> console.log("ariport is operiting")).catch((err)=>console.error(err));
  const [planName,SetPlanName]= useState("");
  connection.on(name,msg =>{console.log(name +"  "+ msg);
  SetPlanName(msg);})
  
  useEffect(() => {
    connection.start().catch(err => console.error("Error starting SignalR:", err));
    return () => {
      connection.stop().catch(err => console.error("Error stopping SignalR:", err));
    };
  }, []);
  const isPlane= (name)=>{
  if(name!= ""){
    if(name.includes('landing')){
      return  <Plane dirction='135' color="blue"/>
    }
    return  <Plane dirction='45' color="Red"/>
  }  
  }
  return (
  <div style={divstyleWithouPicther}>Station {name}
    {isPlane(planName)}
     {planName}

    </div>
  )
}

export default Station
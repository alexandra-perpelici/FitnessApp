/* eslint-disable no-unused-vars */
import { BrowserRouter, Routes, Route, useNavigate } from "react-router-dom";
import React, { createContext, useState, useEffect } from 'react';
import UserComponent from './Components/Login';
//import TopBar from './Components/TopBar';
import GymComponent from './Components/GymComponent';
import PoolsComponent from './Components/PoolsComponent';
import ClimbingComponent from './Components/ClimbingComponent';
import GymManagementComponent from './Components/GymManagementComponent';
import PoolManagementComponent from './Components/PoolManagementComponent';
import ClimbingManagementComponent from './Components/ClimbingManagementComponent';
import Subs from './Components/Subs';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';
import './index.css';
import HomePage from './Components/HomePage';
import SeeSubscriptions from './Components/SeeSubscriptions';
import AdminMenuComponent from "./Components/Admin_Menu";

import UserSeeBookings from "./Components/UserSeeBookings";
import axios from 'axios';


const LogoutComponent = () => {
    const navigate = useNavigate(); 

   
    useEffect(() => {
       
        sessionStorage.removeItem('userId');
        
        navigate('/'); 
    }, [navigate]); 

    return null;
};

function App() {
    const [user, setUser] = useState(false);

    let interval = 86400000;
    
        useEffect(() => {
            const deleteData = async () => {
                try {
                    await axios.delete('https://localhost:7194/Fitness_App/DeleteOldBookings');
                    console.log('DELETE request sent to:', 'https://localhost:7194/Fitness_App/DeleteOldBookings');
                } catch (error) {
                    console.error('Error sending DELETE request:', error);
                }
                console.log("OLD DATA WAS DELETED");
            };
            

            const checkMidnightAndDelete = async () => {
                const now = new Date();
                console.log('Current time:', now.getHours(), now.getMinutes());
               
                let h = now.getHours();
              
                if (h === 0 && now.getMinutes() === 0) {

               
                deleteData();

               
                    const intervalId = setInterval(deleteData, interval);

            
                return () => clearInterval(intervalId);
                }
            }
            

         
            const intervalId = setInterval(checkMidnightAndDelete, 5000);

           
            return () => clearInterval(intervalId);
            
        }, ['https://localhost:7194/Fitness_App/DeleteOldBookings', interval]);
    

    return (
        <div id="root">
            <ToastContainer />
            <>
                <BrowserRouter>
                    <div className="app-container">
                    {user} { }
                    <Routes>
                        <Route exact path="/" element={<UserComponent setUser={setUser} />} />
                        <Route exact path="/home" element={<HomePage />} />
                        <Route path="/subs" element={<Subs />} />
                        <Route path="/logout" element={<LogoutComponent />} /> 
                        <Route path="/gym" element={<GymComponent />} />
                        <Route path="/pools" element={<PoolsComponent />} />
                        <Route path="/climbing" element={<ClimbingComponent />} />
                        <Route path="/gymman" element={<GymManagementComponent />} />
                        <Route path="/poolsman" element={<PoolManagementComponent />} />
                        <Route path="/climbingman" element={<ClimbingManagementComponent />} />
                        <Route path="/seesubs" element={<SeeSubscriptions />} />
                        <Route path="/admin_menu" element={<AdminMenuComponent />} />
                        <Route path="/userseebookings" element={<UserSeeBookings />} />
                    </Routes>
                    </div>
                </BrowserRouter>
            </>
        </div>
    );
}

export default App;
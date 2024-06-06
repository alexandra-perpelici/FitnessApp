/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

const UserSeeBookings = () => {
    const [bookings, setBookings] = useState([]);
    const [showBookings, setShowBookings] = useState(false);

    useEffect(() => {
        const userId = parseInt(sessionStorage.getItem('userId'));
        if (userId) {
            axios.get(`https://localhost:7194/Fitness_App/GetBookingByUser/${userId}`)
                .then(response => {
                    setBookings(response.data);
                })
                .catch(error => {
                    console.error('Error fetching bookings:', error);
                    
                });
        }
    }, []);

    const dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

    const formatBooking = (booking) => {
        const hour = `${booking.hour}:00-${booking.hour + 2}:00`;
        return `Hour: ${hour}, Trainer: ${booking.trainerName}`;
    };

    const handleViewBookings = () => setShowBookings(true);
    const handleHideBookings = () => setShowBookings(false);

    const groupBookingsByDay = (bookings) => {
        return bookings.reduce((acc, booking) => {
            const dayName = dayNames[booking.day];
            if (!acc[dayName]) {
                acc[dayName] = [];
            }
            acc[dayName].push(booking);
            return acc;
        }, {});
    };

    const groupedBookings = groupBookingsByDay(bookings);

    return (
        <div className="management">
            <div className="navbar">
                <nav>
                    <ul className="nav-items">
                        <li><Link to="/home">Home</Link></li>
                        <li><Link to="/pools">Pools</Link></li>
                        <li><Link to="/gym">Gym</Link></li>
                        <li><Link to="/climbing">Climbing</Link></li>
                        <li><Link to="/subs">Make a subscription</Link></li>
                        <li><Link to="/userseebookings">See bookings</Link></li>
                        <li><Link to="/logout">Logout</Link></li>
                    </ul>
                </nav>
            </div>
            <div className="centered-content">
                <h3>See Bookings</h3>
                <div>
                    {!showBookings && <button onClick={handleViewBookings}>View</button>}
                    {showBookings && <button onClick={handleHideBookings}>Hide</button>}
                </div>

                {showBookings && (
                    <div>
                        {dayNames.map(day => (
                            <div key={day}>
                                <h4>{day}</h4>
                                <ul>
                                    {groupedBookings[day] ? (
                                        groupedBookings[day].map(booking => (
                                            <li key={booking.booking_id}>
                                                {formatBooking(booking)}
                                            </li>
                                        ))
                                    ) : (
                                        <li>No bookings</li>
                                    )}
                                </ul>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default UserSeeBookings;
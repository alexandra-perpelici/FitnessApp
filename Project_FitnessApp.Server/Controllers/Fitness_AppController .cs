
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_FitnessApp.Data;
using Project_FitnessApp.Server.Models;


namespace Project_FitnessApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Fitness_AppController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public Fitness_AppController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }


        // FOR USERS:

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _myDbContext.Users.ToListAsync();
            return Ok(users);
        }


        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _myDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new { user.User_id, user.User_name, user.Password, user.Role, user.Email });
        }

        [HttpGet("GetUserByEmailAuth/{email}")]
        public async Task<IActionResult> GetUserByEmailAuth(string email)
        {
            var user = await _myDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return Ok(null);
            }

            return Ok(new { user.User_id, user.User_name, user.Password, user.Role, user.Email });
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _myDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            await _myDbContext.AddAsync(userRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(userRequest);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _myDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _myDbContext.Users.Remove(user);
            await _myDbContext.SaveChangesAsync();

            return Ok("User deleted successfully");
        }


        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User userRequest)
        {

            var existingUser = await _myDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.User_name = userRequest.User_name;
            existingUser.Password = userRequest.Password;
            existingUser.Role = userRequest.Role;

            try
            {
                _myDbContext.Users.Update(existingUser);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("User updated successfully");
        }


        private bool UserExists(int id)
        {
            return _myDbContext.Users.Any(e => e.User_id == id);
        }


        // FOR POOLS: 

        [HttpGet("GetAllPools")]

        public async Task<IActionResult> GetAllPools()
        {
            var pools = await _myDbContext.Pools.ToListAsync();
            return Ok(pools);
        }

        [HttpGet("GetPool/{id}")]
        public async Task<IActionResult> GetPool(int id)
        {
            var pool = await _myDbContext.Pools.FindAsync(id);
            if (pool == null)
            {
                return NotFound("Pool not found");
            }
            return Ok(pool);
        }

        [HttpPost("AddPool")]

        public async Task<IActionResult> AddPool([FromBody] Pool poolRequest)
        {
            await _myDbContext.AddAsync(poolRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(poolRequest);
        }

        [HttpDelete("DeletePool/{id}")]

        public async Task<IActionResult> DeletePool(int id)
        {
            var pool = await _myDbContext.Pools.FindAsync(id);
            if (pool == null)
            {
                return NotFound("Pool not found");
            }

            _myDbContext.Pools.Remove(pool);
            await _myDbContext.SaveChangesAsync();
            return Ok("Pool deleted successfully");
        }

        [HttpPut("UpdatePool/{id}")]

        public async Task<IActionResult> UpdatePool(int id, Pool poolRequest)
        {

            var existingPool = await _myDbContext.Pools.FindAsync(id);
            if (existingPool == null)
            {
                return NotFound();
            }

            existingPool.Pool_name = poolRequest.Pool_name;
            existingPool.Pool_depth = poolRequest.Pool_depth;
            existingPool.Temp = poolRequest.Temp;

            try
            {
                _myDbContext.Pools.Update(existingPool);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Pool updated successfully");
        }

        private bool PoolExists(int id)
        {
            return _myDbContext.Pools.Any(e => e.Pool_id == id);
        }


        // FOR CLIMBING: 

        [HttpGet("GetAllWalls")]

        public async Task<IActionResult> GetAllWalls()
        {
            var walls = await _myDbContext.Climbing.ToListAsync();
            return Ok(walls);
        }

        [HttpGet("GetWall/{id}")]
        public async Task<IActionResult> GetWall(int id)
        {
            var wall = await _myDbContext.Climbing.FindAsync(id);
            if (wall == null)
            {
                return NotFound("Wall not found");
            }
            return Ok(wall);
        }

        [HttpPost("AddWall")]

        public async Task<IActionResult> AddWall([FromBody] Climbing wallRequest)
        {
            await _myDbContext.AddAsync(wallRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(wallRequest);
        }

        [HttpDelete("DeleteWall/{id}")]

        public async Task<IActionResult> DeleteWall(int id)
        {
            var wall = await _myDbContext.Climbing.FindAsync(id);
            if (wall == null)
            {
                return NotFound("Wall not found");
            }

            _myDbContext.Climbing.Remove(wall);
            await _myDbContext.SaveChangesAsync();
            return Ok("Wall deleted successfully");
        }

        [HttpPut("UpdateWall/{id}")]

        public async Task<IActionResult> UpdateWall(int id, Climbing wallRequest)
        {

            var existingWall = await _myDbContext.Climbing.FindAsync(id);
            if (existingWall == null)
            {
                return NotFound();
            }

            existingWall.Wall = wallRequest.Wall;
            existingWall.Level = wallRequest.Level;

            try
            {
                _myDbContext.Climbing.Update(existingWall);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WallExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Wall updated successfully");
        }

        private bool WallExists(int id)
        {
            return _myDbContext.Climbing.Any(e => e.Climbing_id == id);
        }


        // FOR TRAINERS: 

        [HttpGet("GetAllTrainers")]

        public async Task<IActionResult> GetAllTrainers()
        {
            var trainers = await _myDbContext.Trainers.ToListAsync();
            return Ok(trainers);
        }

        [HttpGet("GetTrainer/{id}")]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var trainer = await _myDbContext.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound("Trainer not found");
            }
            return Ok(trainer);
        }



        [HttpGet("GetSwimmingTrainer")]
        public async Task<IActionResult> GetSwimmingTrainer()
        {
            var swimmingTrainer = await _myDbContext.Trainers.FirstOrDefaultAsync(t => t.Type == "Pool");
            if (swimmingTrainer == null)
            {
                return NotFound("Swimming trainer not found");
            }
            return Ok(swimmingTrainer);
        }

        [HttpGet("GetGymTrainer")]
        public async Task<IActionResult> GetGymTrainer()
        {
            var gymTrainer = await _myDbContext.Trainers.FirstOrDefaultAsync(t => t.Type == "Gym");
            if (gymTrainer == null)
            {
                return NotFound("Gym trainer not found");
            }
            return Ok(gymTrainer);
        }

        [HttpGet("GetClimbingTrainer")]
        public async Task<IActionResult> GetClimbingTrainer()
        {
            var climbingTrainer = await _myDbContext.Trainers.FirstOrDefaultAsync(t => t.Type == "Climbing");
            if (climbingTrainer == null)
            {
                return NotFound("Climbing trainer not found");
            }
            return Ok(climbingTrainer);
        }


        // FOR EQUIPMENT: 

        [HttpGet("GetAllEquipments")]

        public async Task<IActionResult> GetAllEquipments()
        {
            var equipments = await _myDbContext.Equipment.ToListAsync();
            return Ok(equipments);


        }


        [HttpGet("GetEquipment/{id}")]
        public async Task<IActionResult> GetEquipment(int id)
        {
            var equipment = await _myDbContext.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound("Equipment not found");
            }
            return Ok(equipment);
        }

        [HttpPost("AddEquipment")]

        public async Task<IActionResult> AddEquipment([FromBody] Equipment equipmentRequest)
        {
            await _myDbContext.AddAsync(equipmentRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(equipmentRequest);
        }

        [HttpDelete("DeleteEquipment/{id}")]

        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _myDbContext.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound("Equipment not found");
            }

            _myDbContext.Equipment.Remove(equipment);
            await _myDbContext.SaveChangesAsync();
            return Ok("Equipment deleted successfully");
        }

        [HttpPut("UpdateEquipment/{id}")]

        public async Task<IActionResult> UpdateEquipment(int id, Equipment equipmentRequest)
        {

            var existingEquipment = await _myDbContext.Equipment.FindAsync(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }

            existingEquipment.Equipment_name = equipmentRequest.Equipment_name;
            existingEquipment.Body = equipmentRequest.Body;

            try
            {
                _myDbContext.Equipment.Update(existingEquipment);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Equipment updated successfully");
        }

        private bool EquipmentExists(int id)
        {
            return _myDbContext.Equipment.Any(e => e.Equipment_id == id);
        }




        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _myDbContext.Booking.ToListAsync();
            return Ok(bookings);
        }
        [HttpDelete("DeleteBookingsByUser/{userId}")]
        public async Task<IActionResult> DeleteBookingsByUser(int userId)
        {
            try
            {
                var bookings = _myDbContext.Booking.Where(b => b.User_id == userId);
                if (!bookings.Any())
                {
                    return NotFound("No bookings found for this user.");
                }

                _myDbContext.Booking.RemoveRange(bookings);
                await _myDbContext.SaveChangesAsync();

                return Ok("All bookings deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] Booking newBooking)
        {
            try
            {
                var conflictingBooking = await _myDbContext.Booking
                    .Where(b => b.User_id == newBooking.User_id && b.Day == newBooking.Day && b.Hour == newBooking.Hour)
                    .FirstOrDefaultAsync();

                if (conflictingBooking != null)
                {
                    return BadRequest("You already have a booking at this time.");
                }

                _myDbContext.Booking.Add(newBooking);
                await _myDbContext.SaveChangesAsync();
                return Ok(newBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpDelete("DeleteBooking/{id}")]

        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _myDbContext.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            _myDbContext.Booking.Remove(booking);
            await _myDbContext.SaveChangesAsync();
            return Ok("Booking deleted successfully");
        }
        [HttpGet("GetBookingByUser/{id}")]
        public async Task<IActionResult> GetBookingByUser(int id)
        {
            try
            {
                var bookings = await _myDbContext.Booking
                    .Where(b => b.User_id == id)
                    .Join(_myDbContext.Trainers,
                        booking => booking.Trainer_id,
                        trainer => trainer.Trainer_id,
                        (booking, trainer) => new
                        {
                            booking.Booking_id,
                            booking.Day,
                            booking.Hour,
                            booking.User_id,
                            booking.Trainer_id,
                            TrainerName = trainer.Trainer_name
                        })
                    .ToListAsync();

                if (bookings == null || !bookings.Any())
                {
                    return NotFound("Bookings not found");
                }

                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetBookingByTrainer/{id}")]
        public async Task<IActionResult> GetBookingByTrainer(int id)
        {
            try
            {
                var booking = await _myDbContext.Booking
                    .Where(b => b.Trainer_id == id)
                    .Join(_myDbContext.Users,
                        booking => booking.User_id,
                        user => user.User_id,
                        (booking, user) => new
                        {
                            booking.Booking_id,
                            booking.Day,
                            booking.Hour,
                            booking.User_id,
                            booking.Trainer_id,
                            user.User_name
                        })
                    .ToListAsync();

                if (booking == null || !booking.Any())
                {
                    return NotFound("Bookings not found");
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

  
        [HttpDelete("DeleteOldBookings")]
        public async Task<IActionResult> DeleteOldBookings()
        {
            try
            {
                var today = DateTime.Today;
                var todayDayOfWeek = (int)today.DayOfWeek;
                var currentHour = today.Hour;

                var oldBookings = await _myDbContext.Booking
                    .Where(a => a.Day < todayDayOfWeek || (a.Day == todayDayOfWeek && a.Hour < currentHour))
                    .ToListAsync();

                if (!oldBookings.Any())
                {
                    return Ok("No old bookings to delete");
                }

                _myDbContext.Booking.RemoveRange(oldBookings);
                await _myDbContext.SaveChangesAsync();

                return Ok($"{oldBookings.Count} old bookings deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] Subscription subscription)
        {
            try
            {
                var user = await _myDbContext.Users.FindAsync(subscription.User_id);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var newSubscription = new Subscription
                {
                    Type = subscription.Type,
                    Time_sub = subscription.Time_sub,
                    User_id = subscription.User_id
                };

                await _myDbContext.Subscription.AddAsync(newSubscription);
                await _myDbContext.SaveChangesAsync();

                return Ok("Subscription added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("GetAllSubscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = await _myDbContext.Subscription.ToListAsync();
            return Ok(subscriptions);
        }

        [HttpGet("GetSubscriptionByTrainer/{type}")]
        public async Task<IActionResult> GetSubscriptionByTrainer(string type)
        {
            try
            {
                var subscriptions = await _myDbContext.Subscription
                    .Where(u => u.Type == type)
                    .Join(_myDbContext.Users,
                        subscription => subscription.User_id,
                        user => user.User_id,
                        (subscription, user) => new
                        {
                            subscription.Subscription_id,
                            subscription.Type,
                            subscription.User_id,
                            subscription.Time_sub,
                            user.User_name
                        })
                    .ToListAsync();

                if (subscriptions == null || !subscriptions.Any())
                {
                    return NotFound($"Subscriptions not found for type: {type}");
                }

                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteSubscription/{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var subscription = await _myDbContext.Subscription.FindAsync(id);
            if (subscription == null)
            {
                return NotFound("Booking not found");
            }

            _myDbContext.Subscription.Remove(subscription);
            await _myDbContext.SaveChangesAsync();
            return Ok("Subscription deleted successfully");
        }


        [HttpGet("CheckSubscription/{userId}/{utilityType}")]
        public async Task<ActionResult<bool>> CheckSubscription(int userId, string utilityType)
        {
            var user = await _myDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var subscription = await _myDbContext.Subscription
                .FirstOrDefaultAsync(s => s.User_id == userId && s.Type == utilityType && s.Time_sub <= DateTime.Now);

            return Ok(subscription != null);
        }


        /*[HttpGet("GetBookingsByUserAndTrainer/{userId}/{trainerId}")]
        public async Task<IActionResult> GetBookingsByUserAndTrainer(int userId, int trainerId)
        {
            try
            {
                var bookings = await _myDbContext.Booking
                    .Where(b => b.User_id == userId && b.Trainer_id == trainerId)
                    .ToListAsync();

                if (bookings == null || !bookings.Any())
                {
                    return NotFound("Bookings not found");
                }

                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/


    }
}
